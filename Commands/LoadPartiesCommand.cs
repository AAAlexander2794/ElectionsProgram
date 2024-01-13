using DocumentFormat.OpenXml.Math;
using ElectionsProgram.Builders;
using ElectionsProgram.Entities;
using ElectionsProgram.Processors;
using ElectionsProgram.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ElectionsProgram.Commands
{
    /// <summary>
    /// Загружает партии из Excel и добавляет к ним талоны, если те загружены.
    /// </summary>
    public class LoadPartiesCommand : ICommand
    {
        private ViewModel _vm;

        public LoadPartiesCommand(ViewModel viewModel)
        {
            _vm = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            try
            {
                // Читаем Excel
                DataTable dt = ExcelProcessor.LoadFromExcel(@"Настройки/Партии/Партии.xlsx");
                // Формируем список представления партии (PartyView) из DataTable
                var partyViews = dt.ToList<PartyView>();
                // Создаем новую коллекцию партий в VM
                _vm.Parties = new ObservableCollection<Party>();
                // 
                foreach (var partyView in partyViews)
                {
                    // Создаем партию на основе представления и добавляем в коллекцию партий
                    _vm.Parties.Add(new Party(partyView));
                }
                // 
                string message = $"Партии загружены.\n" +
                    $"Количество: {_vm.Parties.Count}.";
                Logger.Add(message);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки партий.\n{ex.Message}");
            }
        }
    }
}
