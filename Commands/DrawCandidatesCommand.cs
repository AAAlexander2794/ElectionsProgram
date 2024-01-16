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
    public class DrawCandidatesCommand(ViewModel viewModel) : ICommand
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
                // Загрузка партий
                _vm.LoadCandidatesCommand.Execute(null);
                // Загрузка талонов и общего вещания
                _vm.LoadCandidatesTalonsCommand.Execute(null);
                // Сопоставление талонов партиям
                _vm.MatchCandidatesAndTalonsCommand.Execute(null);
                // Загрузка настроек протокола
                _vm.LoadProtocolSettingsCommand.Execute(null);
                // Создание протоколов
                _vm.CreateProtocolsCandidatesCommand.Execute(null);
                //
                string message = $"Жеребьевка кандидатов проведена.\n" +
                    $"Обработано кандидатов: {_vm.Candidates.Count}.";
                Logger.Add(message);
                MessageBox.Show(message);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
