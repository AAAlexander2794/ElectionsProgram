using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    public class Client
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
            foreach (PlaylistRecord record in PlaylistRecords)
            {
                if (record.DurationActual != null)
                {
                    totalDuration += record.DurationActual.Value;
                }
            }
            return totalDuration;
        }

        /// <summary>
        /// Строки плейлиста данного клиента.
        /// </summary>
        public List<PlaylistRecord> PlaylistRecords { get; set; } = new List<PlaylistRecord>();
    }
}
