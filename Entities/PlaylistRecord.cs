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
        public string MediaresourceName { get; set; } = "";

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
        public TimeSpan? DurationNominal { get; set; }

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
        public TimeSpan? DurationActual { get; set; }

        /// <summary>
        /// Форма выступления
        /// </summary>
        public string BroadcastForm { get; set; } = "";

        /// <summary>
        /// Название ролика/тема дебатов
        /// </summary>
        public string BroadcastCaption { get; set; } = "";

        public PlaylistRecord(PlaylistRecordView view)
        {
            View = view;
            //
            MediaresourceName = View.MediaresourceName;
            Date = DateOnly.FromDateTime(DateTime.Parse(View.Date));
            Time = TimeOnly.FromDateTime(DateTime.Parse(View.Time));
            // Хронометраж номинальный
            var dateTime = ParseStringToDateTime(View.DurationNominal);
            DurationNominal = dateTime != null ? ((DateTime)dateTime).TimeOfDay : null;
            //
            RegionNumber = View.RegionNumber;
            ClientType = View.ClientType;
            ClientName = View.ClientName;
            // Хронометраж фактический
            dateTime = ParseStringToDateTime(View.DurationActual);
            DurationActual = dateTime != null ? ((DateTime)dateTime).TimeOfDay : null;
            //
            BroadcastForm = View.BroadcastForm;
            BroadcastCaption = View.BroadcastCaption;
        }

        public static DateTime? ParseStringToDateTime(string str)
        {
            // Если строка пустая, возвращаем null
            if (str.Trim() == "") return null;
            //
            DateTime dateTime;
            // Пробуем парсить как текст
            bool success = DateTime.TryParse(str, out dateTime);
            // 
            if (success)
            {
                return dateTime;
            }
            else
            {
                // Пробуем парсить как значение времени из Excel
                double d;
                //
                success = double.TryParse(str, out d);
                //
                if (success)
                {
                    dateTime = DateTime.FromOADate(d);
                    return dateTime;
                }
            }
            // Если никакой парсинг не помог, возвращаем null
            return null;
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
                BroadcastCaption = View.BroadcastCaption,
                ClientName = View.ClientName,
                ClientType = View.ClientType,
                Date = View.Date,
                DurationActual = View.DurationActual,
                DurationNominal = View.DurationNominal,
                MediaresourceName = View.MediaresourceName,
                RegionNumber = View.RegionNumber,
                Time = View.Time
            };
            return view;
        }
    }
}
