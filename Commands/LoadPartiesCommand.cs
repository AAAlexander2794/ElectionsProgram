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
                // Создаем пустой список партий
                List<Party> parties = new List<Party>();
                //
                foreach (var item in partyViews)
                {
                    // Создаем партию на основе представления
                    var party = new Party(item);
                    // Добавляем к партии имеющиеся талоны (должны быть уже загружены)
                    party.Талон_Россия_1 = _vm.PartiesTalons_Россия_1.FirstOrDefault(t => t.Number == party.View.Талон_Россия_1);
                    party.Талон_Россия_24 = _vm.PartiesTalons_Россия_24.FirstOrDefault(t => t.Number == party.View.Талон_Россия_24);
                    party.Талон_Маяк = _vm.PartiesTalons_Маяк.FirstOrDefault(t => t.Number == party.View.Талон_Маяк);
                    party.Талон_Вести_ФМ = _vm.PartiesTalons_Вести_ФМ.FirstOrDefault(t => t.Number == party.View.Талон_Вести_ФМ);
                    party.Талон_Радио_России = _vm.PartiesTalons_Радио_России.FirstOrDefault(t => t.Number == party.View.Талон_Радио_России);
                    // Добавляем партию в список
                    parties.Add(party);
                }
                // Добавляем список партий в VM
                _vm.Parties = new ObservableCollection<Party>(parties);
                // 
                string message = $"Партии загружены.\n" +
                    $"Количество: {_vm.Parties.Count}.";
                Logger.Add(message);
                MessageBox.Show(message);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки партий.\n{ex.Message}");
            }
        }
    }
}
