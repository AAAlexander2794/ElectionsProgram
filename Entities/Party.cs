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
    internal class Party : IClient
    {
        public int Id { get; set; }

        /// <summary>
        /// Талоны, которые выдали партии (может быть пустым)
        /// </summary>
        public List<Talon> Talons { get; set; }

        public PartyView View { get; set; }

        string IClient.Name => View.Название_рабочее;

        List<Talon> IClient.Talons => Talons;

        public Party(PartyView partyView) 
        {
            View = partyView;
        }
    }
}
