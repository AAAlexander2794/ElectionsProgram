using ElectionsProgram.Processors;
using ElectionsProgram.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ElectionsProgram.Commands.Temp
{
    public class LoadDBCommand : ICommand
    {
        private ViewModel _base;

        public LoadDBCommand(ViewModel @base)
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
                var newBase = ProcessorXML.LoadDBFromXml("data", "electionBase.xml");
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
