using ElectionsProgram.Processors;
using ElectionsProgram.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace ElectionsProgram.Commands.Temp
{
    public class SaveDBCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private ViewModel _base;

        public SaveDBCommand(ViewModel electionsBase)
        {
            _base = electionsBase;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            try
            {
                ProcessorXML.SaveDBToXml(_base, "data", "electionBase.xml");
                MessageBox.Show("Данные сохранены.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //MessageBox.Show(ex.Message);
            }
        }
    }
}
