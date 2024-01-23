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

        /// <summary>
        /// Канал
        /// </summary>
        public string MediaResource { get; set; } = "";

        /// <summary>
        /// Дата
        /// </summary>
        public DateOnly Date { get; set; } = DateOnly.MinValue;

        /// <summary>
        /// Отрезок
        /// </summary>
        public TimeOnly Time { get; set; } = TimeOnly.MinValue;

        /// <summary>
        /// Хронометраж вещания номинальный
        /// </summary>
        public TimeSpan DurationNominal { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// Номер округа
        /// </summary>
        public string RegionNumber { get; set; } = "–";

        /// <summary>
        /// Партия/кандидат
        /// </summary>
        public string ClientType { get; set; } = "";

        /// <summary>
        /// Название партии/ФИО кандидата
        /// </summary>
        public string ClientName { get; set; } = "";

        /// <summary>
        /// Хронометраж вещания фактический
        /// </summary>
        public TimeSpan DurationActual { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// Форма выступления
        /// </summary>
        public string BroadcastType { get; set; } = "";

        /// <summary>
        /// Название ролика/тема дебатов
        /// </summary>
        public string BroadcastCaption { get; set; } = "";

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
