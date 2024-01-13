using ElectionsProgram.Processors;
using ElectionsProgram.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElectionsProgram.Commands
{
    public class LoadSettingsPathsCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private ViewModel _viewModel;

        public LoadSettingsPathsCommand(ViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            //_viewModel.SettingsFilePathes.Протоколы = "New text";
            //_viewModel.SettingsFilePathes.Партии_Таблица = "New text";
            //
            Logger.Add("Настройки загружены");
        }
    }
}
