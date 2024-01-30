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

        private string _название_Падеж_им = "Без округа";
        public string Название_Падеж_им { get => _название_Падеж_им; 
            set
            { 
                if (value != "") _название_Падеж_им = value;
            }
        }

        public string Название_Падеж_дат { get; set; } = "";

        public string Дополнительно { get; set; } = "";

        public List<Candidate> Candidates { get; set; } = new List<Candidate>();

        public Region() { }
    }
}
