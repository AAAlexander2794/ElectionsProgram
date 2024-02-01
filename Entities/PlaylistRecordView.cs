using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    /// <summary>
    /// Строка плейлиста (запись вещания по факту выхода в эфир) в текстовом виде.
    /// </summary>
    public class PlaylistRecordView
    {
        [DisplayName("Канал")]
        public string MediaresourceName { get; set; } = "";

        /// <summary>
        /// Дата
        /// </summary>
        [DisplayName("Дата")]
        public string Date { get; set; } = "";

        /// <summary>
        /// Время (отрезок)
        /// </summary>
        [DisplayName("Отрезок")]
        public string Time { get; set; } = "";

        /// <summary>
        /// Хронометраж номинальный (по плану)
        /// </summary>
        [DisplayName("Хрон")]
        public string DurationNominal { get; set; } = "";

        [DisplayName("Округ")]
        public string RegionNumber { get; set; } = "";

        [DisplayName("Партия/кандидат")]
        public string ClientType { get; set; } = "";

        [DisplayName("Название партии/ФИО кандидата")]
        public string ClientName { get; set; } = "";

        /// <summary>
        /// Хронометраж актуальный (по факту выхода в эфир)
        /// </summary>
        [DisplayName("Факт время")]
        public string DurationActual { get; set; } = "";

        [DisplayName("Форма выступления")]
        public string BroadcastForm { get; set; } = "";

        /// <summary>
        /// Название вещания (название ролика)
        /// </summary>
        [DisplayName("Название ролика/тема дебатов")]
        public string BroadcastCaption { get; set; } = "";

        // Для финансовой части

        /// <summary>
        /// Тариф в рублях за минуту
        /// </summary>
        public string Tariff { get; set; } = "";

        /// <summary>
        /// Итоговая стоимость в зависимости от хронометража
        /// </summary>
        public string Price { get; set; } = "";

    }
}
