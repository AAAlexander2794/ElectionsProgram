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
    internal class PartyView
    {
        public string Name { get; set; }

        /// <summary>
        /// Название партии, которое используется в
        /// рабочих таблицах (недопустимо в отчеты)
        /// </summary>
        public string Название_рабочее {  get; set; }

        public string Description { get; set; }

        public PartyView(string name)
        {
            Name = name;
        }
    }
}
