using ElectionsProgram.Entities;
using System;
using System.Collections.Generic;
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
                        totalDuration += item.DurationActual;
                    }
                }
                return totalDuration;
            }
        }
    }
}
