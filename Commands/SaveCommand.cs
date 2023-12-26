﻿using ElectionsProgram.Processors;
using ElectionsProgram.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace ElectionsProgram.Commands
{
    public class SaveCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private ViewModel.ViewModel _base;

        public SaveCommand(ViewModel.ViewModel electionsBase) 
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
                ProcessorIO.SaveDBToXml(_base, "data", "electionBase.xml");
                MessageBox.Show("Данные сохранены.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
