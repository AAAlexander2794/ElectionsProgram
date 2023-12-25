using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    /// <summary>
    /// Запись вещания по факту выхода в эфир в текстовом виде.
    /// </summary>
    internal class BroadcastActualView
    {
        public string MediaresourceName { get; set; }

        public string TalonNumber { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Время (отрезок)
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// Хронометраж номинальный (по плану)
        /// </summary>
        public string DurationNominal { get; set; }

        /// <summary>
        /// Хронометраж актуальный (по факту выхода в эфир)
        /// </summary>
        public string DurationActual { get; set; }

        /// <summary>
        /// Дополнительное описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Название вещания (название ролика)
        /// </summary>
        public string Caption { get; set; }

        public BroadcastActualView(
            string mediaresourceName, 
            string talonNumber, 
            string date, 
            string time, 
            string durationNominal, 
            string durationActual, 
            string caption,
            string description)
        {
            MediaresourceName = mediaresourceName;
            TalonNumber = talonNumber;
            Date = date;
            Time = time;
            DurationNominal = durationNominal;
            DurationActual = durationActual;
            Caption = caption;
            Description = description;
        }
    }
}
