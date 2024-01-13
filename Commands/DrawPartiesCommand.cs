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
    /// <summary>
    /// Жеребьевка партий. Выполняет поочередно команды <see cref="LoadPartiesCommand"/>, <see cref="LoadPartiesTalonsCommand"/>,
    /// <see cref="MatchPartiesAndTalonsCommand"/>, <see cref="CreateProtocolsPartiesCommand"/>.
    /// </summary>
    public class DrawPartiesCommand(ViewModel viewModel) : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly ViewModel _vm = viewModel;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            try
            {
                // Загрузка партий
                _vm.LoadPartiesCommand.Execute(null);
                // Загрузка талонов и общего вещания
                _vm.LoadPartiesTalonsCommand.Execute(null);
                // Сопоставление талонов партиям
                _vm.MatchPartiesAndTalonsCommand.Execute(null);
                // Загрузка настроек протокола
                _vm.LoadProtocolSettingsCommand.Execute(null);
                // Создание протоколов
                _vm.CreateProtocolsPartiesCommand.Execute(null);
                //
                string message = $"Жеребьевка партий проведена.\n" +
                    $"Обработано партий: {_vm.Parties.Count}.";
                Logger.Add(message);
                MessageBox.Show(message);
            }
            catch(Exception ex) 
            {
                MessageBox.Show($"Ошибка жеребьевки.\n{ex.Message}");
            }
        }
    }
}
