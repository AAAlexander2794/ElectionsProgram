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
    public class SettingsPathesLoadCommand(ViewModel viewModel) : ICommand
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
                _viewModel.SettingsFilePathes.Load();
                //
                Logger.Add("Настройки путей загружены.");
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
