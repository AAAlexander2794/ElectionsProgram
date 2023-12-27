using ClosedXML.Excel;
using ElectionsProgram.Processors;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElectionsProgram.Commands
{
    public class SavePartiesToExcelCommand : ICommand
    {
        private ViewModels.ViewModel _vm;

        public SavePartiesToExcelCommand(ViewModels.ViewModel viewModel)
        {
            _vm = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            ProcessorIO.SaveToExcel(_vm.Test);
            
        }
    }
}
