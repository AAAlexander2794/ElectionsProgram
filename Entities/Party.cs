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

        List<Talon> IClient.Talons => Talons;

        #region Конструкторы

        public Party(PartyView partyView) 
        {
            View = partyView;
        }

        public Party() { }

        #endregion Конструкторы
    }
}
