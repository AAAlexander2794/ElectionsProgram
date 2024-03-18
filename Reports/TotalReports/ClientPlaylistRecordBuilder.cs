using ElectionsProgram.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Reports.TotalReports
{
    /// <summary>
    /// Берет фактические строки вещания клиента и групперует их по временным отрезкам,
    /// которые были указаны в протоколах (костыль для президентских выборов 2024). 
    /// </summary>
    public static class ClientPlaylistRecordBuilder
    {
        public static void GroupClientRecords(Playlist playlist)
        {
            // Только ТВ переделываем
            if (playlist.MediaresourceName != "Россия 1" && playlist.MediaresourceName != "Россия 24")
            {
                return;
            }
            // По каждому клиенту
            foreach (var client in playlist.Clients)
            {
                List<PlaylistRecord> newRecords = new List<PlaylistRecord>();
                //
                foreach (var record in client.PlaylistRecords)
                {
                    // Утро
                    CheckRecord(record, newRecords, "05:00:00", "11:00:00");
                    // День
                    CheckRecord(record, newRecords, "11:00:00", "17:00:00");
                    // Вечер
                    CheckRecord(record, newRecords, "17:00:00", "22:00:00");
                }
                /// <remarks>
                /// Здесь мы имеем заполненный список <see cref="newRecords"/>, 
                /// в котором строки вещания из плейлиста собраны по
                /// отрезкам времени "утро, день, вечер".
                /// </remarks>
                // Клиенту подключаем новый список
                client.PlaylistRecords = newRecords;
            }
        }

        private static void CheckRecord(PlaylistRecord record, List<PlaylistRecord> newRecords, string timeStart, string timeEnd)
        {
            if (record.Time >= TimeOnly.Parse($"{timeStart}") && record.Time < TimeOnly.Parse($"{timeEnd}"))
            {
                // Ищем новую запись с такой же датой и временем
                var sameRecord = newRecords.FirstOrDefault(r =>
                r.Date == record.Date &&
                r.View.Time == $"{timeStart}-{timeEnd}");
                // Если записи с такими датой и временем еще не было
                if (sameRecord == null)
                {
                    newRecords.Add(record);
                    //
                    record.View.Time = $"{timeStart}-{timeEnd}";
                }
                //
                else
                {
                    // Добавляем время вещания в этом отрезке
                    sameRecord.DurationActual += record.DurationActual;
                    // Прибавляем сумму
                    sameRecord.Price += record.Price;
                }
            }
        }
    }
}
