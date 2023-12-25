using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    internal class Talon
    {
        public int Id { get; set; }

        /// <summary>
        /// Медиаресурс, к которому относится талон
        /// </summary>
        public Mediaresource Mediaresource { get; set; }

        /// <summary>
        /// Номер талона
        /// </summary>
        public int Number {  get; set; }

        public List<BroadcastActual> Broadcasts { get; set; }


    }
}
