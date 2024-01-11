using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    /// <summary>
    /// Политическая партия
    /// </summary>
    public class Party : IClient
    {
        /// <summary>
        /// Текстовые поля с данными
        /// </summary>
        public PartyView View { get; set; }

        string IClient.Name => View.Название_условное;

        #region Талоны

        public Talon? Талон_Россия_1 { get; set; }

        public Talon? Талон_Россия_24 { get; set; }

        public Talon? Талон_Маяк { get; set; }

        public Talon? Талон_Радио_России { get; set; }

        public Talon? Талон_Вести_ФМ { get; set; }

        #endregion Талоны

        public string Представитель_Фамилия_ИО {  get; set; } = string.Empty;

        #region Конструкторы

        public Party(PartyView partyView) 
        {
            View = partyView;
            // Формируем "Фамилия И.О."
            Представитель_Фамилия_ИО = $"{View.Представитель_Фамилия_Падеж_им}";
            // Если есть имя
            if (View.Представитель_Имя_Падеж_им.Length > 0)
            {
                Представитель_Фамилия_ИО += $" {View.Представитель_Имя_Падеж_им[0]}.";
                // Если есть имя и отчество
                if (View.Представитель_Отчество_Падеж_им.Length > 0)
                {
                    Представитель_Фамилия_ИО += $"{View.Представитель_Отчество_Падеж_им[0]}.";
                }
            }
        }

        public Party() { }

        #endregion Конструкторы
    }
}
