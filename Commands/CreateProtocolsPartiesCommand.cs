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
    public class CreateProtocolsPartiesCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private ViewModel _viewModel;

        public CreateProtocolsPartiesCommand(ViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            //try
            //{
                //
                string protocolFolder = $@"{_viewModel.SettingsFilePathes.Каталог_документов}{_viewModel.SettingsFilePathes.Протоколы_Выходной_каталог}";
                string templatePath = $@"{_viewModel.SettingsFilePathes.Каталог_настроек}{_viewModel.SettingsFilePathes.Протоколы_Шаблон_Партии}";
                ProtocolPartyBuilder.CreateProtocolsParties(_viewModel.Parties.ToList(),
                    _viewModel.ProtocolSettings,
                    protocolFolder,
                    templatePath);
                //
                string message = $"Протоколы партий созданы. Обработано: " +
                    $"Партий: {_viewModel.Parties.Count}. " +
                    $"Талонов: {_viewModel.PartiesTalons_Россия_1.Count}, " +
                    $"{_viewModel.PartiesTalons_Россия_24.Count}, " +
                    $"{_viewModel.PartiesTalons_Маяк.Count}, " +
                    $"{_viewModel.PartiesTalons_Вести_ФМ.Count}, " +
                    $"{_viewModel.PartiesTalons_Радио_России.Count}.";
                Logger.Add(message);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Ошибка при создании протоколов.\n{ex.Message}");
            //}
        }
    }
}
