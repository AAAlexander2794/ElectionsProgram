using ElectionsProgram.Builders;
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
    public class CreateProtocolsCandidatesCommand(ViewModel viewModel) : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly ViewModel _viewModel = viewModel;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            try
            {
                ProtocolCandidatesBuilder.BuildProtocolsCandidates(
                    _viewModel.Regions.ToList(),
                    _viewModel.ProtocolSettings,
                    _viewModel.SettingsFilePathes.Протоколы_Выходной_каталог,
                    _viewModel.SettingsFilePathes.Протоколы_Шаблон_Кандидаты);
                //
                Logger.Add("Протоколы кандидатов созданы.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
