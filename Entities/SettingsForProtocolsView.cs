using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    public class SettingsForProtocolsView
    {
        #region По партиям

        [DisplayName("Партии: Фамилия И.О. члена изб. комиссии")]
        public string Партии_Член_изб_ком_Фамилия_ИО {  get; set; } = string.Empty;

        [DisplayName("Партии: И.О. Фамилия члена изб. комиссии")]
        public string Партии_Член_изб_ком_ИО_Фамилия {  get; set; } = string.Empty;

        [DisplayName("Партии: И.О. Фамилия представителя организации телерадиовещания")]
        public string Партии_Предст_СМИ_ИО_Фамилия { get; set; } = string.Empty;

        [DisplayName("Партии: Дата")]
        public string Партии_Дата {  get; set; } = string.Empty;

        #endregion По партиям

        #region По кандидатам

        [DisplayName("Кандидаты: Фамилия И.О. члена изб. комиссии")]
        public string Кандидаты_Член_изб_ком_Фамилия_ИО { get; set; } = string.Empty;

        [DisplayName("Кандидаты: И.О. Фамилия члена изб. комиссии")]
        public string Кандидаты_Член_изб_ком_ИО_Фамилия { get; set; } = string.Empty;

        [DisplayName("Кандидаты: Фамилия представителя организации телерадиовещания")]
        public string Кандидаты_Предст_СМИ_ИО_Фамилия { get; set; } = string.Empty;

        [DisplayName("Кандидаты: Дата")]
        public string Кандидаты_Дата { get; set; } = string.Empty;

        #endregion По кандидатам

        [DisplayName("Наименование СМИ \"Россия 1\"")]
        public string Название_СМИ_Россия_1 { get; set; } = string.Empty;

        [DisplayName("Наименование СМИ \"Россия 24\"")]
        public string Название_СМИ_Россия_24 { get; set; } = string.Empty;

        [DisplayName("Наименование СМИ \"Маяк\"")]
        public string Название_СМИ_Маяк { get; set; } = string.Empty;

        [DisplayName("Наименование СМИ \"Вести ФМ\"")]
        public string Название_СМИ_Вести_ФМ { get; set; } = string.Empty;

        [DisplayName("Наименование СМИ \"Радио России\"")]
        public string Название_СМИ_Радио_России { get; set; } = string.Empty;
    }
}
