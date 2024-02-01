using ElectionsProgram.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Builders.TotalReport
{
    public static partial class TotalReportBuilder
    {
        /// <summary>
        /// Плейлист по одному медиаресурсу.
        /// </summary>
        private class Playlist
        {
            public string MediaresourceName { get; set; } = "";

            public List<Client> Clients { get; set; } = new List<Client>();

            /// <summary>
            /// Итоговый объем фактически использованного времени эфира.
            /// </summary>
            /// <returns></returns>
            public TimeSpan GetTotalDurationActual()
            {
                TimeSpan totalDuration = TimeSpan.Zero;
                foreach (var client in Clients)
                {
                    foreach (var item in client.PlaylistRecords)
                    {
                        if (item.DurationActual != null)
                        {
                            totalDuration += item.DurationActual.Value;
                        }
                    }
                }
                return totalDuration;
            }

            public DataTable ToDataTable()
            {
                DataTable dt = new DataTable();
                // 
                dt.Columns.Add($"№ п/п");
                dt.Columns.Add($"ФИО");
                dt.Columns.Add($"Форма предвыборной агитации");
                dt.Columns.Add($"Дата выхода в эфир");
                dt.Columns.Add($"Время выхода в эфир");
                dt.Columns.Add($"Объем фактически использованного эфирного времени");
                dt.Columns.Add($"Тариф вещания");
                dt.Columns.Add($"Итоговая стоимость");
                //
                dt.Rows.Add();
                dt.Rows[dt.Rows.Count - 1][0] = "1";
                dt.Rows[dt.Rows.Count - 1][1] = "2";
                dt.Rows[dt.Rows.Count - 1][2] = "3";
                dt.Rows[dt.Rows.Count - 1][3] = "4";
                dt.Rows[dt.Rows.Count - 1][4] = "5";
                dt.Rows[dt.Rows.Count - 1][5] = "6";
                dt.Rows[dt.Rows.Count - 1][6] = "7";
                dt.Rows[dt.Rows.Count - 1][7] = "8";
                // Счетчик клиентов
                int count = 1;
                //
                foreach (var client in Clients)
                {
                    // Флаг первой строки
                    bool firstRow = true;
                    // По всем строкам вещания клиента
                    foreach(PlaylistRecord record in client.PlaylistRecords)
                    {
                        // Не добавляем строки, где не было фактического времени
                        if (record.DurationActual == null || record.DurationActual == TimeSpan.Zero) continue;
                        //
                        dt.Rows.Add();
                        // В первой строке пишем "№ п/п" и "Имя клиента"
                        if (firstRow)
                        {
                            dt.Rows[dt.Rows.Count - 1][0] = $"{count}";
                            dt.Rows[dt.Rows.Count - 1][1] = $"{client.Name}";
                            //
                            firstRow = false;
                        }
                        dt.Rows[dt.Rows.Count - 1][2] = $"{record.BroadcastForm}";
                        dt.Rows[dt.Rows.Count - 1][3] = $"{record.Date.ToShortDateString()}";
                        dt.Rows[dt.Rows.Count - 1][4] = $"{record.Time.ToLongTimeString()}";
                        dt.Rows[dt.Rows.Count - 1][5] = $"{record.DurationActual}";
                        // Стоимости
                        dt.Rows[dt.Rows.Count - 1][6] = $"{record.Tariff}";
                        dt.Rows[dt.Rows.Count - 1][7] = $"{record.Price}";
                    }
                    if (client.GetTotalDurationActual() != TimeSpan.Zero)
                    {
                        // Добавляем строку "Итого"
                        dt.Rows.Add();
                        dt.Rows[dt.Rows.Count - 1][0] = "";
                        dt.Rows[dt.Rows.Count - 1][1] = "";
                        dt.Rows[dt.Rows.Count - 1][2] = "";
                        dt.Rows[dt.Rows.Count - 1][3] = "";
                        dt.Rows[dt.Rows.Count - 1][4] = "Итого:";
                        dt.Rows[dt.Rows.Count - 1][5] = $"{client.GetTotalDurationActual()}";
                    }
                    //
                    count++;
                }
                // Добавляем строку "Всего"
                dt.Rows.Add();
                dt.Rows[dt.Rows.Count - 1][0] = "";
                dt.Rows[dt.Rows.Count - 1][1] = "";
                dt.Rows[dt.Rows.Count - 1][2] = "";
                dt.Rows[dt.Rows.Count - 1][3] = "";
                dt.Rows[dt.Rows.Count - 1][4] = "Всего:";
                dt.Rows[dt.Rows.Count - 1][5] = $"{GetTotalDurationActual()}";
                //
                return dt;
            }
        }
    }
}
