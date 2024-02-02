using ElectionsProgram.Processors;
using ElectionsProgram.Reports.Protocols;
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
                ////
                //string protocolFolder = $@"{_viewModel.SettingsFilePathes.Каталог_документов}{_viewModel.SettingsFilePathes.Протоколы_Выходной_каталог}";
                //string templatePath = $@"{_viewModel.SettingsFilePathes.Каталог_настроек}{_viewModel.SettingsFilePathes.Протоколы_Шаблон_Кандидаты}";
                //ProtocolCandidatesBuilder.BuildProtocolsCandidates(
                //    _viewModel.Regions.ToList(),
                //    _viewModel.ProtocolSettings,
                //    protocolFolder,
                //    templatePath);
                ////
                //Logger.Add("Протоколы кандидатов созданы.");

                //
                //
                string protocolFolder = $@"{_viewModel.SettingsFilePathes.Каталог_документов}{_viewModel.SettingsFilePathes.Протоколы_Выходной_каталог}";
                string templatePath = $@"{_viewModel.SettingsFilePathes.Каталог_настроек}{_viewModel.SettingsFilePathes.Протоколы_Шаблон_общий}";
                ProtocolPresident2024.BuildProtocols(
                    _viewModel.Candidates.ToList(),
                    _viewModel.Parties.ToList(),
                    _viewModel.ProtocolSettings,
                    protocolFolder,
                    templatePath);
                //
                Logger.Add("Протоколы созданы.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
