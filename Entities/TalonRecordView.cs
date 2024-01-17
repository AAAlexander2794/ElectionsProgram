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

    }
}
