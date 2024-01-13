using ElectionsProgram.Entities;
using ElectionsProgram.Processors;
using ElectionsProgram.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ElectionsProgram.Commands
{
    /// <summary>
    /// Сопоставляет талоны партиям согласно номерам в таблице партий.
    /// </summary>
    public class MatchPartiesAndTalonsCommand(ViewModel viewModel) : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly ViewModel _vm = viewModel;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            try
            {
                //
                foreach (var party in _vm.Parties)
                {
                    // Добавляем к партии имеющиеся талоны (должны быть уже загружены)
                    party.Талон_Россия_1 = _vm.PartiesTalons_Россия_1.FirstOrDefault(t => t.Number == party.View.Талон_Россия_1);
                    party.Талон_Россия_24 = _vm.PartiesTalons_Россия_24.FirstOrDefault(t => t.Number == party.View.Талон_Россия_24);
                    party.Талон_Маяк = _vm.PartiesTalons_Маяк.FirstOrDefault(t => t.Number == party.View.Талон_Маяк);
                    party.Талон_Вести_ФМ = _vm.PartiesTalons_Вести_ФМ.FirstOrDefault(t => t.Number == party.View.Талон_Вести_ФМ);
                    party.Талон_Радио_России = _vm.PartiesTalons_Радио_России.FirstOrDefault(t => t.Number == party.View.Талон_Радио_России);
                }
                //
                Logger.Add($"Талоны сопоставлены партиям. Партий: {_vm.Parties.Count}.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
