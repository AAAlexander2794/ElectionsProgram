using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    /// <summary>
    /// Запись о вещании по плану.
    /// </summary>
    /// <remarks>
    /// Из таких записей состоит <see cref="Talon"/>.
    /// </remarks>
    public class TalonRecord
    {
        public TalonRecordView View { get; set; } = new TalonRecordView();

        /// <summary>
        /// Название медиаресурса
        /// </summary>
        public string MediaresourceName { get; set; } = "";

        /// <summary>
        /// Номер талона
        /// </summary>
        public string TalonNumber { get; set; } = "";

        /// <summary>
        /// Дата
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Время (отрезок)
        /// </summary>
        public TimeOnly Time { get; set; }

        /// <summary>
        /// Хронометраж номинальный (по плану)
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Дополнительное описание
        /// </summary>
        public string Description { get; set; } = "";

        #region Конструкторы

        public TalonRecord(TalonRecordView view)
        {
            View = view;
            MediaresourceName = view.MediaresourceName;
            // Номер талона
            TalonNumber = view.TalonNumber;
            // Если хронометраж в формате "0:00:00"
            if (View.Duration.Length == 7)
            {
                Duration = TimeOnly.FromDateTime(DateTime.Parse("0" + View.Duration.Replace('.', ','))).ToTimeSpan();
            }
            // Если хронометраж в формате "00:00"
            else if (View.Duration.Length == 5)
            {
                Duration = TimeOnly.FromDateTime(DateTime.Parse("00:" + View.Duration.Replace('.', ','))).ToTimeSpan();
            }
            // Если хронометраж в формате "00:00:00" (ну или другой, тогда ошибка)
            else
            {
                Duration = TimeOnly.FromDateTime(DateTime.Parse(View.Duration.Replace('.', ','))).ToTimeSpan();
            }
            // Дата
            Date = DateOnly.FromDateTime(DateTime.Parse(View.Date));
            // Время (отрезок). Происходит замена точки на запятую (вот такая культура)
            Time = TimeOnly.FromDateTime(DateTime.Parse(View.Time.Replace('.', ',')));
            // Примечание
            Description = View.Description;
        }

        #endregion Конструкторы

        /// <summary>
        /// Вывод информации в текстовую строку для протокола.
        /// </summary>
        /// <returns></returns>
        public string ToProtocolString()
        {
            string time = Time.ToString("hh:mm:ss");
            return $"{Date} {time} {Duration} {Description}";
        }
    }
}
