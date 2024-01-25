using ElectionsProgram.Builders.TotalReport;
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
    public class TotalReportCreateCommand(ViewModel viewModel) : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly ViewModel _viewModel = viewModel;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            //try
            //{
                DataTable dt;
                
                string outCatalogPath = $"{_viewModel.SettingsFilePathes.Каталог_документов}" +
                    $"{_viewModel.SettingsFilePathes.Итоговый_отчет_Выходной_каталог}";
                //
                dt = ExcelProcessor.LoadFromExcel($"" +
                    $"{_viewModel.SettingsFilePathes.Каталог_настроек}" +
                    $"{_viewModel.SettingsFilePathes.Итоговый_отчет_Плейлист_Россия_1}");
                TotalReportBuilder.BuildTotalReport(dt, "Россия 1", outCatalogPath);
                dt = ExcelProcessor.LoadFromExcel($"" +
                    $"{_viewModel.SettingsFilePathes.Каталог_настроек}" +
                    $"{_viewModel.SettingsFilePathes.Итоговый_отчет_Плейлист_Россия_24}");
                TotalReportBuilder.BuildTotalReport(dt, "Россия 24", outCatalogPath);
                dt = ExcelProcessor.LoadFromExcel($"" +
                    $"{_viewModel.SettingsFilePathes.Каталог_настроек}" +
                    $"{_viewModel.SettingsFilePathes.Итоговый_отчет_Плейлист_Маяк}");
                TotalReportBuilder.BuildTotalReport(dt, "Маяк", outCatalogPath);
                dt = ExcelProcessor.LoadFromExcel($"" +
                    $"{_viewModel.SettingsFilePathes.Каталог_настроек}" +
                    $"{_viewModel.SettingsFilePathes.Итоговый_отчет_Плейлист_Вести_ФМ}");
                TotalReportBuilder.BuildTotalReport(dt, "Вести ФМ", outCatalogPath);
                dt = ExcelProcessor.LoadFromExcel($"" +
                    $"{_viewModel.SettingsFilePathes.Каталог_настроек}" +
                    $"{_viewModel.SettingsFilePathes.Итоговый_отчет_Плейлист_Радио_России}");
                TotalReportBuilder.BuildTotalReport(dt, "Радио России", outCatalogPath);
                //
                Logger.Add("Итоговые отчеты созданы.");
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
    }
}
