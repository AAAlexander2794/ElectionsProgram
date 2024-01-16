using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    public class TalonRecordView
    {
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
        public string Date { get; set; } = "";

        /// <summary>
        /// Время (отрезок)
        /// </summary>
        public string Time { get; set; } = "";

        /// <summary>
        /// Хронометраж номинальный (по плану)
        /// </summary>
        public string Duration { get; set; } = "";

        /// <summary>
        /// Дополнительное описание
        /// </summary>
        public string Description { get; set; } = "";

        ///// <summary>
        ///// Конструктор со всеми свойствами
        ///// </summary>
        ///// <param name="mediaresourceName"></param>
        ///// <param name="talonNumber"></param>
        ///// <param name="date"></param>
        ///// <param name="time"></param>
        ///// <param name="durationNominal"></param>
        ///// <param name="description"></param>
        //public TalonRecordView(
        //    string mediaresourceName, 
        //    string talonNumber, 
        //    string date, 
        //    string time, 
        //    string durationNominal, 
        //    string description = "")
        //{
        //    MediaresourceName = mediaresourceName;
        //    TalonNumber = talonNumber;
        //    Date = date;
        //    Time = time;
        //    DurationNominal = durationNominal;
        //    Description = description;
        //}

        ///// <summary>
        ///// Базовый конструктор.
        ///// </summary>
        ///// <remarks>
        ///// Нужен, чтобы автоматически создавать экземпляры.
        ///// </remarks>
        //public TalonRecordView() { }

        //public new string ToString()
        //{
        //    return $"{MediaresourceName} № {TalonNumber} {Date} {Time} {DurationNominal} {Description}";
        //}
    }
}
