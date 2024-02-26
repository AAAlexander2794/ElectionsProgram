using ElectionsProgram.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectionsProgram.Processors;

namespace ElectionsProgram.Reports.TotalReports
{
    public static class PlaylistBuilder
    {
        public static Playlist CreatePlaylist(DataTable playlistDataTable, string mediarecourceName)
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
                    AddRecord(playlist.Clients, newRecord);
                }
            }
            //
            return playlist;
        }

        public static Client AddRecord(List<Client> clients, PlaylistRecord record)
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
