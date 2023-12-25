using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    /// <summary>
    /// Медиаресурс (канал)
    /// </summary>
    internal class Mediaresource
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Mediaresource(string name) 
        {
            Name = name;
        }
    }
}
