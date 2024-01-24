using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectionsProgram.Entities;

namespace ElectionsProgram.Builders.TotalReport
{
    public static partial class TotalReportBuilder
    {
        private class Client
        {
            /// <summary>
            /// Название партии/ФИО кандидата.
            /// </summary>
            public string Name { get; set; } = "";

            /// <summary>
            /// Партия/кандидат.
            /// </summary>
            public string Type { get; set; } = "";

            /// <summary>
            /// Итоговый объем фактически использованного времени эфира.
            /// </summary>
            /// <returns></returns>
            public TimeSpan GetTotalDurationActual()
            {
                TimeSpan totalDuration = TimeSpan.Zero;
                foreach (var item in PlaylistRecords)
                {
                    totalDuration += item.DurationActual;
                }
                return totalDuration;
            }

            /// <summary>
            /// Строки плейлиста данного клиента.
            /// </summary>
            public List<PlaylistRecord> PlaylistRecords { get; } = new List<PlaylistRecord>();
        }
    }
}
