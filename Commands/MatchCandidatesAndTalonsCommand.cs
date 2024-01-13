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
    public class MatchCandidatesAndTalonsCommand(ViewModel viewModel) : ICommand
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
                foreach (var candidate in _vm.Candidates)
                {
                    // Добавляем к кандидатам имеющиеся талоны (должны быть уже загружены)
                    candidate.Талон_Россия_1 = _vm.CandidatesTalons_Россия_1.FirstOrDefault(t => t.Number == candidate.View.Талон_Россия_1);
                    candidate.Талон_Россия_24 = _vm.CandidatesTalons_Россия_24.FirstOrDefault(t => t.Number == candidate.View.Талон_Россия_24);
                    candidate.Талон_Маяк = _vm.CandidatesTalons_Маяк.FirstOrDefault(t => t.Number == candidate.View.Талон_Маяк);
                    candidate.Талон_Вести_ФМ = _vm.CandidatesTalons_Вести_ФМ.FirstOrDefault(t => t.Number == candidate.View.Талон_Вести_ФМ);
                    candidate.Талон_Радио_России = _vm.CandidatesTalons_Радио_России.FirstOrDefault(t => t.Number == candidate.View.Талон_Радио_России);
                }
                //
                Logger.Add($"Сопоставлены кандидаты и талоны.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
