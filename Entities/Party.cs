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

        /// <summary>
        /// Талоны, которые выдали партии (может быть пустым)
        /// </summary>
        public List<Talon> Talons { get; set; }

        string IClient.Name => View.Название_условное;

        #region Талоны

        public Talon Талон_Россия_1 { get; set; }

        public Talon Талон_Россия_24 { get; set; }

        public Talon Талон_Маяк { get; set; }

        public Talon Талон_Радио_России { get; set; }

        public Talon Талон_Вести_ФМ { get; set; }

        #endregion Талоны

        #region Конструкторы

        public Party(PartyView partyView) 
        {
            View = partyView;
        }

        public Party() { }

        #endregion Конструкторы
    }
}
