using ElectionsProgram.Entities;
using ElectionsProgram.Processors;
using ElectionsProgram.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace ElectionsProgram.Commands
{
    public class LoadProtocolSettingsCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private ViewModel _vm;

        public LoadProtocolSettingsCommand(ViewModel vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            try
            {
                // Загружаем таблицу настроек (интересует только одна строка)
                DataTable dt = ExcelProcessor.LoadFromExcel($"Настройки/Протоколы/Протоколы.xlsx");
                // Берем первую строку
                SettingsForProtocolsView protocolSettingsView = dt.ToList<SettingsForProtocolsView>()[0];
                // Передаем настройки в ViewModel
                _vm.ProtocolSettings = new SettingsForProtocols(protocolSettingsView);
                //
                Logger.Add("Настройки протоколов загружены.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
