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

namespace ElectionsProgram.Builders.TotalReport
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
                    PlaylistRecord newRecord = new PlaylistRecord(recordView);
                    // Передаем на добавление записи клиентам найденного плейлиста
                    AddRecord(playlist.Clients, newRecord);
                }
            }
            //
            string filePath = catalogPath + $"\\{playlist.MediaresourceName.Trim()}.xlsx";
            ExcelProcessor.SaveToExcel(playlist.ToDataTable(), filePath);
        }

        private static Client AddRecord(List<Client> clients, PlaylistRecord record)
        {
            //
            foreach (var client in clients)
            {
                // Если клиент уже есть в списке
                if (client.Name == record.View.ClientName)
                {
                    // Добавляем запись из плейлиста клиенту
                    client.PlaylistRecords.Add(record);
                    // Запись присвоена, прекращаем
                    return client;
                }
            }
            // Если всех клиентов проверили, но не прекратили, значит, клиента в списке нет - добавляем.
            // Создаем нового клиента по данным записи из плейлиста
            Client newClient = new Client()
            {
                Name = record.View.ClientName,
                Type = record.View.ClientType
            };
            // Добавляем новому клиенту запись
            newClient.PlaylistRecords.Add(record);
            // Добавляем нового клиента в список клиентов
            clients.Add(newClient);
            // Возвращаем
            return newClient;
        }

    }
}
