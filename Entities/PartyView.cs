using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    /// <summary>
    /// Партия в представлении для таблицы
    /// </summary>
    public class PartyView
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [DisplayName("На печать")]
        public string На_печать { get; set; } = "";

        /// <summary>
        /// Полное название партии в соответствии с учредительными документами
        /// </summary>
        [DisplayName("Название: полное")]
        public string Название_полное { get; set; } = "";

        [DisplayName("Название: краткое")]
        public string Название_краткое { get; set; } = "";

        /// <summary>
        /// Название партии, которое используется в
        /// рабочих таблицах (недопустимо в отчеты)
        /// </summary>
        [DisplayName("Название: условное")]
        public string Название_условное {  get; set; } = "";

        /// <summary>
        /// Номер и дата постановления, на основе которого создана партия
        /// </summary>
        [DisplayName("Постановление")]
        public string Постановление { get; set; } = "";

        /// <summary>
        /// Отметка о том, явилась ли партия (представитель) на жеребьевку
        /// </summary>
        [DisplayName("Явка")]
        public string Явка { get; set; } = "";

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

        #endregion

        [DisplayName("Договор: номер")]
        public string Договор_Номер { get; set; } = "";

        [DisplayName("Договор: дата")]
        public string Договор_Дата { get; set; } = "";

        /// <summary>
        /// Дополнительное поле для технической информации
        /// </summary>
        [DisplayName("Примечание")]
        public string Примечание { get; set; } = "";

        [DisplayName("ОГРН")]
        public string ОГРН { get; set; } = "";

        [DisplayName("ИНН")]
        public string ИНН { get; set; } = "";

        [DisplayName("КПП")]
        public string КПП { get; set; } = "";

        [DisplayName("Банк. счет")]
        public string Банк_счет { get; set; } = "";

        [DisplayName("Место нахождения")]
        public string Место_нахождения { get; set; } = "";

        #region Данные представителя

        // Именительный падеж

        [DisplayName("Представитель: фамилия (им.)")]
        public string Представитель_Фамилия_Падеж_им { get; set; } = "";

        [DisplayName("Представитель: имя (им.)")]
        public string Представитель_Имя_Падеж_им { get; set; } = "";

        [DisplayName("Представитель: отчество (им.)")]
        public string Представитель_Отчество_Падеж_им { get; set; } = "";

        // Родительный падеж

        [DisplayName("Представитель: фамилия (род.)")]
        public string Представитель_Фамилия_Падеж_род { get; set; } = "";

        [DisplayName("Представитель: имя (род.)")]
        public string Представитель_Имя_Падеж_род { get; set; } = "";

        [DisplayName("Представитель: отчество (род.)")]
        public string Представитель_Отчество_Падеж_род { get; set; } = "";

        /// <summary>
        /// Доверенность на представителя
        /// </summary>
        [DisplayName("Представитель: доверенность")]
        public string Представитель_Доверенность { get; set; } = "";

        #endregion Данные представителя

        #region Данные кандидата партии

        // Именительный падеж

        [DisplayName("Кандидат: фамилия (им.)")]
        public string Кандидат_Фамилия_Падеж_им { get; set; } = "";

        [DisplayName("Кандидат: имя (им.)")]
        public string Кандидат_Имя_Падеж_им { get; set; } = "";

        [DisplayName("Кандидат: отчество (им.)")]
        public string Кандидат_Отчество_Падеж_им { get; set; } = "";

        // Родительный падеж

        [DisplayName("Кандидат: фамилия (род.)")]
        public string Кандидат_Фамилия_Падеж_род { get; set; } = "";

        [DisplayName("Кандидат: имя (род.)")]
        public string Кандидат_Имя_Падеж_род { get; set; } = "";

        [DisplayName("Кандидат: отчество (род.)")]
        public string Кандидат_Отчество_Падеж_род { get; set; } = "";

        #endregion Данные кандидата партии

        #region Нотариус

        [DisplayName("Нотариус: город")]
        public string Нотариус_Город { get; set; } = "";

        [DisplayName("Нотариус: реестр")]
        public string Нотариус_Реестр { get; set; } = "";

        [DisplayName("Нотариус: фамилия")]
        public string Нотариус_Фамилия { get; set; } = "";

        [DisplayName("Нотариус: имя")]
        public string Нотариус_Имя { get; set; } = "";

        [DisplayName("Нотариус: отчество")]
        public string Нотариус_Отчество { get; set; } = "";

        #endregion Нотариус

        public PartyView() { }

    }
}
