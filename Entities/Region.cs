using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    /// <summary>
    /// Округ
    /// </summary>
    public class Region
    {
        public string Номер { get; set; } = "";

        public string Название_Падеж_им { get; set; } = "";

        public string Название_Падеж_дат { get; set; } = "";

        public string Дополнительно { get; set; } = "";

        public List<Candidate> Candidates { get; set; } = new List<Candidate>();

        public Region() { }
    }
}
