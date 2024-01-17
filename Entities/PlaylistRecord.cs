using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    /// <summary>
    /// Запись вещания по факту выхода в эфир.
    /// </summary>
    public class PlaylistRecord
    {
        public PlaylistRecordView View { get; set; }

        public PlaylistRecord(PlaylistRecordView view)
        {
            View = view;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Полная копия своих данных таблицы.</returns>
        public PlaylistRecordView GetView()
        {
            PlaylistRecordView view = new PlaylistRecordView()
            {
                BroadcastForm = View.BroadcastForm,
                Caption = View.Caption,
                ClientName = View.ClientName,
                ClientType = View.ClientType,
                Date = View.Date,
                DurationActual = View.DurationActual,
                DurationNominal = View.DurationNominal,
                MediaresourceName = View.MediaresourceName,
                Region = View.Region,
                Time = View.Time
            };
            return view;
        }
    }
}
