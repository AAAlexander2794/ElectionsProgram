using DocumentFormat.OpenXml.Math;
using ElectionsProgram.Builders;
using ElectionsProgram.Entities;
using ElectionsProgram.Processors;
using ElectionsProgram.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ElectionsProgram.Commands
{
    public class LoadPartiesCommand : ICommand
    {
        private ViewModels.ViewModel _vm;

        public LoadPartiesCommand(ViewModels.ViewModel viewModel)
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
            // Читаем Excel
            DataTable dt = ProcessorExcel.LoadFromExcel(@"Настройки/_database/Партии.xlsx");
            // Формируем список партий из DataTable
            var list = BuilderParties.GetParties(dt);
            // Добавляем список партий в VM
            _vm.Parties = new System.Collections.ObjectModel.ObservableCollection<Entities.Party>(list);
            // Добавляем партию "Самовыдвижение", если ее нет
            if (_vm.Parties.FirstOrDefault(p => p.View.Название_полное == "Самовыдвижение") == null)
            {
                _vm.Parties.Add(new Party(new PartyView()
                {
                    Название_полное = "Самовыдвижение",
                    Название_краткое = "Самовыдвижение",
                    Название_условное = "Самовыдвижение"
                }));
            }
            // 
            MessageBox.Show(_vm.Parties.Count.ToString());
        }
    }
}
