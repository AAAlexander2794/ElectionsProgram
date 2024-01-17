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
    public class PlaylistCreateCommand(ViewModel viewModel) : ICommand
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
                PlaylistBuilder.BuildTable(
                    _viewModel.Parties.ToList(),
                    _viewModel.Candidates.ToList(),
                    _viewModel.SettingsFilePathes.Каталог_документов);
                //
                Logger.Add("Создан плейлист.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
