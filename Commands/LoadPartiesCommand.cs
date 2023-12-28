using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElectionsProgram.Commands
{
    internal class LoadPartiesCommand : ICommand
    {
        private ViewModels.ViewModel _vm;

        public LoadPartiesCommand(ViewModels.ViewModel viewModel)
        {
            _vm = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}
