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
    /// Загружает из Excel талоны для кандидатов.
    /// </summary>
    public class LoadCandidatesTalonsCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private ViewModel _viewModel;

        public LoadCandidatesTalonsCommand(ViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            //try
            //{
                DataTable dt, dtCommon;
                // Россия-1
                dt = ExcelProcessor.LoadFromExcel(@$"{_viewModel.SettingsFilePathes.Каталог_настроек}{_viewModel.SettingsFilePathes.Кандидаты_Талоны_Россия_1}");
                _viewModel.CandidatesTalons_Россия_1 = new ObservableCollection<Talon>(TalonsBuilder.ParseTalonsVariantBase(dt, "Россия-1"));
                dtCommon = ExcelProcessor.LoadFromExcel(@$"{_viewModel.SettingsFilePathes.Каталог_настроек}{_viewModel.SettingsFilePathes.Кандидаты_Общее_Россия_1}");
                _viewModel.candidatesCommonTalon_Россия_1 = TalonsBuilder.ParseTalonsVariantBase(dtCommon, "Россия-1", "");
                // Росия-24
                dt = ExcelProcessor.LoadFromExcel(@$"{_viewModel.SettingsFilePathes.Каталог_настроек}{_viewModel.SettingsFilePathes.Кандидаты_Талоны_Россия_24}");
                _viewModel.CandidatesTalons_Россия_24 = new ObservableCollection<Talon>(TalonsBuilder.ParseTalonsVariantBase(dt, "Россия-24"));
                dtCommon = ExcelProcessor.LoadFromExcel(@$"{_viewModel.SettingsFilePathes.Каталог_настроек}{_viewModel.SettingsFilePathes.Кандидаты_Общее_Россия_24}");
                _viewModel.candidatesCommonTalon_Россия_24 = TalonsBuilder.ParseTalonsVariantBase(dtCommon, "Россия-24", "");
                // Маяк
                dt = ExcelProcessor.LoadFromExcel(@$"{_viewModel.SettingsFilePathes.Каталог_настроек}{_viewModel.SettingsFilePathes.Кандидаты_Талоны_Маяк}");
                _viewModel.CandidatesTalons_Маяк = new ObservableCollection<Talon>(TalonsBuilder.ParseTalonsVariantBase(dt, "Маяк"));
                dtCommon = ExcelProcessor.LoadFromExcel(@$"{_viewModel.SettingsFilePathes.Каталог_настроек}{_viewModel.SettingsFilePathes.Кандидаты_Общее_Маяк}");
                _viewModel.candidatesCommonTalon_Маяк = TalonsBuilder.ParseTalonsVariantBase(dtCommon, "Маяк", "");
                // Вести ФМ
                dt = ExcelProcessor.LoadFromExcel(@$"{_viewModel.SettingsFilePathes.Каталог_настроек}{_viewModel.SettingsFilePathes.Кандидаты_Талоны_Вести_ФМ}");
                _viewModel.CandidatesTalons_Вести_ФМ = new ObservableCollection<Talon>(TalonsBuilder.ParseTalonsVariantBase(dt, "Вести ФМ"));
                dtCommon = ExcelProcessor.LoadFromExcel(@$"{_viewModel.SettingsFilePathes.Каталог_настроек}{_viewModel.SettingsFilePathes.Кандидаты_Общее_Вести_ФМ}");
                _viewModel.candidatesCommonTalon_Вести_ФМ = TalonsBuilder.ParseTalonsVariantBase(dtCommon, "Вести ФМ", "");
                // Радио России
                dt = ExcelProcessor.LoadFromExcel(@$"{_viewModel.SettingsFilePathes.Каталог_настроек}{_viewModel.SettingsFilePathes.Кандидаты_Талоны_Радио_России}");
                _viewModel.CandidatesTalons_Радио_России = new ObservableCollection<Talon>(TalonsBuilder.ParseTalonsVariantBase(dt, "Радио России"));
                dtCommon = ExcelProcessor.LoadFromExcel(@$"{_viewModel.SettingsFilePathes.Каталог_настроек}{_viewModel.SettingsFilePathes.Кандидаты_Общее_Радио_России}");
                _viewModel.candidatesCommonTalon_Радио_России = TalonsBuilder.ParseTalonsVariantBase(dtCommon, "Радио России", "");
                //
                string message = $"Талоны кандидатов загружены.\n" +
                    $"Россия-1: {_viewModel.CandidatesTalons_Россия_1.Count}\n" +
                    $"Россия-24: {_viewModel.CandidatesTalons_Россия_24.Count}\n" +
                    $"Маяк: {_viewModel.CandidatesTalons_Маяк.Count}\n" +
                    $"Вести ФМ: {_viewModel.CandidatesTalons_Вести_ФМ.Count}\n" +
                    $"Радио России: {_viewModel.CandidatesTalons_Радио_России.Count}";
                Logger.Add(message);
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
    }
}
