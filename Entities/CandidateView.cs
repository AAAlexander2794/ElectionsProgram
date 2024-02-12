using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    /// <summary>
    /// Кандидат в представлении для таблицы
    /// </summary>
    public class CandidateView
    {
        [DisplayName("На печать")]
        public string На_печать { get; set; } = "";

        [DisplayName("Округ: №")]
        public string Округ_Номер { get; set; } = "";

        [DisplayName("Округ: Название (им. падеж)")]
        public string Округ_Название_падеж_им { get; set; } = "";

        [DisplayName("Округ: Название (дат. падеж)")]
        public string Округ_Название_падеж_дат { get; set; } = "";

        [DisplayName("Округ: Доп")]
        public string Округ_Дополнительно { get; set; } = "";

        /// <summary>
        /// Фамилия
        /// </summary>
        [DisplayName("Фамилия")]
        public string Фамилия { get; set; } = "";

        /// <summary>
        /// Имя
        /// </summary>
        [DisplayName("Имя")]
        public string Имя { get; set; } = "";

        /// <summary>
        /// Отчество (может быть пустым)
        /// </summary>
        [DisplayName("Отчество")]
        public string Отчество { get; set; } = "";

        #region Номера талонов

        [DisplayName("Талон Россия 1")]
        public string Талон_Россия_1 { get; set; } = "";

        [DisplayName("Талон Россия 24")]
        public string Талон_Россия_24 { get; set; } = "";

        [DisplayName("Талон Маяк")]
        public string Талон_Маяк { get; set; } = "";

        [DisplayName("Талон Радио России")]
        public string Талон_Радио_России { get; set; } = "";

        [DisplayName("Талон Вести ФМ")]
        public string Талон_Вести_ФМ { get; set; } = "";

        #endregion Номера талонов

        [DisplayName("Явка кандидата")]
        public string Явка_кандидата { get; set; } = "";

        [DisplayName("Явка представителя")]
        public string Явка_представителя { get; set; } = "";

        [DisplayName("Представитель: Фамилия")]
        public string Представитель_Фамилия { get; set; } = "";

        [DisplayName("Представитель: Имя")]
        public string Представитель_Имя { get; set; } = "";

        [DisplayName("Представитель: Отчество")]
        public string Представитель_Отчество { get; set; } = "";

        [DisplayName("Представитель: Доверенность")]
        public string Представитель_Доверенность {  get; set; } = "";

        [DisplayName("Постановление")]
        public string Постановление { get; set; } = "";

        [DisplayName("Договор: №")]
        public string Договор_Номер { get; set; } = "";

        [DisplayName("Договор: Дата")]
        public string Договор_Дата { get; set; } = "";

        [DisplayName("ИНН")]
        public string ИНН { get; set; } = "";

        [DisplayName("Счет банка")]
        public string Счет_банка { get; set; } = "";

        /// <summary>
        /// Полное название партии для договора
        /// </summary>
        [DisplayName("Партия")]
        public string Партия_Название_полное { get; set; } = "";

        public CandidateView() { }
    }
}
