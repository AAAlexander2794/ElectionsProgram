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
    public class LoadCommand : ICommand
    {
        private ViewModels.ViewModel _base;

        public LoadCommand(ViewModels.ViewModel @base) 
        {
            _base = @base;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            try
            {
                var newBase = ProcessorIO.LoadDBFromXml("data", "electionBase.xml");
                _base.Load(newBase);
                MessageBox.Show("Данные загружены.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
