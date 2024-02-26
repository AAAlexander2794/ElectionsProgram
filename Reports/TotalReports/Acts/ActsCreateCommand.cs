using ElectionsProgram.Builders.TotalReports;
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

namespace ElectionsProgram.Reports.TotalReports.Acts
{
    public class ActsCreateCommand(ViewModel viewModel) : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly ViewModel _viewModel = viewModel;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            try
            {
                ActsBuilder.CreateActs(
                    _viewModel.Parties.ToList(),
                    _viewModel.Candidates.ToList(),
                    _viewModel.Playlist_Россия_1,
                    _viewModel.Playlist_Россия_24,
                    _viewModel.Playlist_Маяк,
                    _viewModel.Playlist_Вести_ФМ,
                    _viewModel.Playlist_Радио_России,
                    $"{_viewModel.SettingsFilePathes.Каталог_настроек}\\Итоговый отчет\\Акты оказанных услуг\\Шаблон Кандидаты РВ бесплатные.dotx",
                    $"{_viewModel.SettingsFilePathes.Каталог_настроек}\\Итоговый отчет\\Акты оказанных услуг\\Шаблон Кандидаты ТВ бесплатные.dotx",
                    $"{_viewModel.SettingsFilePathes.Каталог_настроек}\\Итоговый отчет\\Акты оказанных услуг\\Шаблон Партии РВ бесплатные.dotx",
                    $"{_viewModel.SettingsFilePathes.Каталог_настроек}\\Итоговый отчет\\Акты оказанных услуг\\Шаблон Партии ТВ бесплатные.dotx",
                    $"{_viewModel.SettingsFilePathes.Каталог_документов}\\Отчеты\\Акты");
                //
                Logger.Add("Акты созданы.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
