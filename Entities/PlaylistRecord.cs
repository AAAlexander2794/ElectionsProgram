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
        public DateOnly? Date { get; set; }

        /// <summary>
        /// Отрезок
        /// </summary>
        public TimeOnly? Time { get; set; }

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

        /// <summary>
        /// Тариф в рублях за минуту
        /// </summary>
        public double Tariff { get; set; }

        /// <summary>
        /// Итоговая стоимость в зависимости от хронометража
        /// </summary>
        public double Price { get; set; }

        public PlaylistRecord(PlaylistRecordView view, string mode = "text")
        {
            View = view;
            //
            if (mode == "text") { }
            else
            {
                MediaresourceName = View.MediaresourceName;
                Date = DateOnly.FromDateTime(DateTime.Parse(View.Date));

                double result;
                bool success = double.TryParse(View.Time, out result);
                if (success)
                {
                    Time = TimeOnly.FromDateTime(DateTime.FromOADate(result));
                }
                //Time = TimeOnly.FromDateTime(DateTime.Parse(View.Time));
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
                //
                Tariff = ParseStringToDouble(View.Tariff);
                //
                Price = CalculatePrice(DurationActual, Tariff);
            }
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

        public static double ParseStringToDouble(string str)
        {
            if (string.IsNullOrEmpty(str.Trim())) return 0;
            // Пробуем парсить
            double d;
            //
            bool success = double.TryParse(str, out d);
            //
            if (success)
            {
                return d;
            }
            return 0;
        }

        public static double CalculatePrice(TimeSpan? duration, double tariff)
        {
            if (!duration.HasValue) return 0;
            // Берем секунды
            double seconds = duration.Value.TotalSeconds;
            // Тариф поминутный, считаем через секунды как части минуты
            double price = seconds * tariff / 60;
            //
            return price;
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
                Time = View.Time,
                Tariff = View.Tariff,
                Price = View.Price
            };
            return view;
        }
    }
}
