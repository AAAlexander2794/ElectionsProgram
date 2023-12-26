using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public string Id { get; set; }

        /// <summary>
        /// Полное название партии в соответствии с учредительными документами
        /// </summary>
        public string Название_полное { get; set; }

        public string Название_краткое { get; set; }

        /// <summary>
        /// Название партии, которое используется в
        /// рабочих таблицах (недопустимо в отчеты)
        /// </summary>
        public string Название_условное {  get; set; }

        /// <summary>
        /// Номер и дата постановления, на основе которого создана партия
        /// </summary>
        public string Постановление { get; set; }

        /// <summary>
        /// Отметка о том, явилась ли партия (представитель) на жеребьевку
        /// </summary>
        public string Явка { get; set; }

        public string Договор_Номер { get; set; }

        public string Договор_Дата { get; set; }

        /// <summary>
        /// Дополнительное поле для технической информации
        /// </summary>
        public string Примечание { get; set; }

        public string ОГРН { get; set; }

        public string ИНН { get; set; }

        public string КПП { get; set; }

        public string Банк_счет { get; set; }

        public string Место_нахождения { get; set; }

        #region Данные представителя

        // Именительный падеж

        public string Представитель_Фамилия_Падеж_им { get; set; }

        public string Представитель_Имя_Падеж_им { get; set; }

        public string Представитель_Отчество_Падеж_им { get; set; }

        // Родительный падеж

        public string Представитель_Фамилия_Падеж_род { get; set; }

        public string Представитель_Имя_Падеж_род { get; set; }

        public string Представитель_Отчество_Падеж_род { get; set; }

        /// <summary>
        /// Доверенность на представителя
        /// </summary>
        public string Представитель_Доверенность { get; set; }

        #endregion Данные представителя

        #region Нотариус

        public string Нотариус_Город { get; set; }

        public string Нотариус_Реестр { get; set; }

        public string Нотариус_Фамилия { get; set; }

        public string Нотариус_Имя { get; set; }

        public string Нотариус_Отчество { get; set; }

        #endregion Нотариус

        #region Конструкторы

        public PartyView(
            string название_условное)
        {
            Название_условное = название_условное;
        }

        public PartyView() { }

        #endregion Конструкторы
    }
}
