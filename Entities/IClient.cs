using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    public interface IClient
    {
        /// <summary>
        /// Название партии/ФИО кандидата.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Партия/кандидат.
        /// </summary>
        public string Type { get; }

        #region Талоны

        public Talon? Талон_Россия_1 { get; set; }

        public Talon? Талон_Россия_24 { get; set; }

        public Talon? Талон_Маяк { get; set; }

        public Talon? Талон_Радио_России { get; set; }

        public Talon? Талон_Вести_ФМ { get; set; }

        #endregion Талоны
    }
}
