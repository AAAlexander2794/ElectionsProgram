using ElectionsProgram.Builders;
using ElectionsProgram.Entities;
using ElectionsProgram.Processors;
using ElectionsProgram.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ElectionsProgram.Commands
{
    /// <summary>
    /// Загружает талоны и общее вещание для партий из Excel 
    /// </summary>
    public class LoadPartiesTalonsCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private ViewModel _viewModel;

        public LoadPartiesTalonsCommand(ViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            try
            {
                DataTable dt, dtCommon;
                // Россия-1
                dt = ExcelProcessor.LoadFromExcel(_viewModel.SettingsFilePathes.Партии_Талоны_Россия_1);
                dtCommon = ExcelProcessor.LoadFromExcel(_viewModel.SettingsFilePathes.Партии_Общее_Россия_1);
                _viewModel.PartiesTalons_Россия_1 = new ObservableCollection<Talon>(TalonsBuilder.ParseTalonsVariantBase(dt, "Россия-1", dtCommon));
                // Росия-24
                dt = ExcelProcessor.LoadFromExcel(_viewModel.SettingsFilePathes.Партии_Талоны_Россия_24);
                dtCommon = ExcelProcessor.LoadFromExcel(_viewModel.SettingsFilePathes.Партии_Общее_Россия_24);
                _viewModel.PartiesTalons_Россия_24 = new ObservableCollection<Talon>(TalonsBuilder.ParseTalonsVariantBase(dt, "Россия-24", dtCommon));
                // Маяк
                dt = ExcelProcessor.LoadFromExcel(_viewModel.SettingsFilePathes.Партии_Талоны_Маяк);
                dtCommon = ExcelProcessor.LoadFromExcel(_viewModel.SettingsFilePathes.Партии_Общее_Маяк);
                _viewModel.PartiesTalons_Маяк = new ObservableCollection<Talon>(TalonsBuilder.ParseTalonsVariantBase(dt, "Маяк", dtCommon));
                // Вести ФМ
                dt = ExcelProcessor.LoadFromExcel(_viewModel.SettingsFilePathes.Партии_Талоны_Вести_ФМ);
                dtCommon = ExcelProcessor.LoadFromExcel(_viewModel.SettingsFilePathes.Партии_Общее_Вести_ФМ);
                _viewModel.PartiesTalons_Вести_ФМ = new ObservableCollection<Talon>(TalonsBuilder.ParseTalonsVariantBase(dt, "Вести ФМ", dtCommon));
                // Радио России
                dt = ExcelProcessor.LoadFromExcel(_viewModel.SettingsFilePathes.Партии_Талоны_Радио_России);
                dtCommon = ExcelProcessor.LoadFromExcel(_viewModel.SettingsFilePathes.Партии_Общее_Радио_России);
                _viewModel.PartiesTalons_Радио_России = new ObservableCollection<Talon>(TalonsBuilder.ParseTalonsVariantBase(dt, "Радио России", dtCommon));
                //
                string message = $"Талоны партий загружены.\n" +
                    $"Россия-1: {_viewModel.PartiesTalons_Россия_1.Count}\n" +
                    $"Россия-24: {_viewModel.PartiesTalons_Россия_24.Count}\n" +
                    $"Маяк: {_viewModel.PartiesTalons_Маяк.Count}\n" +
                    $"Вести ФМ: {_viewModel.PartiesTalons_Вести_ФМ.Count}\n" +
                    $"Радио России: {_viewModel.PartiesTalons_Радио_России.Count}";
                Logger.Add(message);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
