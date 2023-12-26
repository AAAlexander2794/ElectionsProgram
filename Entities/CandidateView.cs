﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    /// <summary>
    /// Кандидат в представлении для таблицы
    /// </summary>
    public class CandidateView
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

        #region Конструкторы

        public CandidateView(string name, string surname) 
        {
            Name = name;
            Surname = surname;
        }

        public CandidateView() { }

        #endregion Конструкторы
    }
}
