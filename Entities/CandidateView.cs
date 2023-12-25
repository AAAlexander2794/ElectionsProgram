using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    /// <summary>
    /// Кандидат в представлении для таблицы
    /// </summary>
    internal class CandidateView
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Отчество (может быть пустым)
        /// </summary>
        public string Patronimyc { get; set; } = string.Empty;

        public string? Description { get; set; }

        public CandidateView(string name, string surname) 
        {
            Name = name;
            Surname = surname;
        }
    }
}
