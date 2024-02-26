using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ElectionsProgram.Builders.TotalReports;
using ElectionsProgram.Entities;
using ElectionsProgram.Processors;
using ElectionsProgram.ViewModels;

namespace ElectionsProgram.Reports.TotalReports
{
    public class PlaylistsLoadCommand(ViewModel viewModel) : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly ViewModel _viewModel = viewModel;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            DataTable dt;

            //
            dt = ExcelProcessor.LoadFromExcel($"" +
                $"{_viewModel.SettingsFilePathes.Каталог_настроек}" +
                $"{_viewModel.SettingsFilePathes.Итоговый_отчет_Плейлист_Россия_1}");
            _viewModel.Playlist_Россия_1 = PlaylistBuilder.CreatePlaylist(dt, "Россия 1");
            dt = ExcelProcessor.LoadFromExcel($"" +
                $"{_viewModel.SettingsFilePathes.Каталог_настроек}" +
                $"{_viewModel.SettingsFilePathes.Итоговый_отчет_Плейлист_Россия_24}");
            _viewModel.Playlist_Россия_24 = PlaylistBuilder.CreatePlaylist(dt, "Россия 24");
            dt = ExcelProcessor.LoadFromExcel($"" +
                $"{_viewModel.SettingsFilePathes.Каталог_настроек}" +
                $"{_viewModel.SettingsFilePathes.Итоговый_отчет_Плейлист_Маяк}");
            _viewModel.Playlist_Маяк = PlaylistBuilder.CreatePlaylist(dt, "Маяк");
            dt = ExcelProcessor.LoadFromExcel($"" +
                $"{_viewModel.SettingsFilePathes.Каталог_настроек}" +
                $"{_viewModel.SettingsFilePathes.Итоговый_отчет_Плейлист_Вести_ФМ}");
            _viewModel.Playlist_Вести_ФМ = PlaylistBuilder.CreatePlaylist(dt, "Вести ФМ");
            dt = ExcelProcessor.LoadFromExcel($"" +
                $"{_viewModel.SettingsFilePathes.Каталог_настроек}" +
                $"{_viewModel.SettingsFilePathes.Итоговый_отчет_Плейлист_Радио_России}");
            _viewModel.Playlist_Радио_России = PlaylistBuilder.CreatePlaylist(dt, "Радио России");
            //
            Logger.Add("Плейлисты загружены.");
        }

        
    }
}
