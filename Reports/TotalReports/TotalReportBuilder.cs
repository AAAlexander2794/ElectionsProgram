using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using ElectionsProgram.Entities;
using ElectionsProgram.Processors;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ElectionsProgram.Reports.TotalReports;

namespace ElectionsProgram.Builders.TotalReports
{
    public static partial class TotalReportBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="playlistDataTable"></param>
        /// <param name="mediarecourceName"></param>
        /// <param name="catalogPath"></param>
        public static void BuildTotalReport(DataTable playlistDataTable, string mediarecourceName, string catalogPath)
        {

            // Из таблицы плейлиста формируем список текстового представления записей
            var playlistRecordsView = playlistDataTable.ToList<PlaylistRecordView>();
            // 
            Playlist playlist = new Playlist()
            {
                MediaresourceName = mediarecourceName,
            };
            //
            foreach (var recordView in playlistRecordsView)
            {
                // Если строка не пустая
                if (recordView.MediaresourceName.Trim() != "")
                {
                    // Создаем строку плейлиста из текстового представления
                    PlaylistRecord newRecord = new PlaylistRecord(recordView, "digit");
                    // Передаем на добавление записи клиентам найденного плейлиста
                    ElectionsProgram.Reports.TotalReports.PlaylistBuilder.AddRecord(playlist.Clients, newRecord);
                }
            }
            // Группируем строки по временным отрезкам "утро, день, вечер", чтобы как в протоколах
            ClientPlaylistRecordBuilder.GroupClientRecords(playlist);
            //
            string filePath = catalogPath + $"\\{playlist.MediaresourceName.Trim()}.xlsx";
            ExcelProcessor.SaveToExcel(playlist.ToDataTable(), filePath);
        }

        

        private static void CreateCertificates(Playlist playlist, string catalogPath)
        {
            foreach (var client in playlist.Clients)
            {
                var dt = playlist.ToDataTable(client.Name);

            }
            // Справки и акт о фактическом времени для договоров
            string dateTimeForCatalog = $"{DateTime.Now}";
            CreateCertificates(playlist, $"{catalogPath}\\{dateTimeForCatalog}\\");
        }

        

    }
}
