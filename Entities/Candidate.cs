using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    /// <summary>
    /// Кандидат на выборы
    /// </summary>
    public class Candidate : IClient
    {
        public int Id { get; set; }

        public string Фамилия_И_О { get; }

        List<Talon> Talons { get; set; }

        /// <summary>
        /// Данные кандидата в текстовом виде (как в таблицах)
        /// </summary>
        public CandidateView View { get; set; }

        /// <summary>
        /// Партия, от которой выдвигается кандидат
        /// </summary>
        public Party Party { get; set; }

        string IClient.Name { get => Фамилия_И_О; }

        #region Талоны

        public Talon Талон_Россия_1 { get; set; }

        public Talon Талон_Россия_24 { get; set; }

        public Talon Талон_Маяк { get; set; }

        public Talon Талон_Радио_России { get; set; }

        public Talon Талон_Вести_ФМ { get; set; }

        #endregion Талоны

        #region Конструкторы

        public Candidate(CandidateView candidateView) 
        {
            View = candidateView;
            Фамилия_И_О = $"{View.Surname} {View.Name[0]} {View.Patronimyc[0]}";
        }

        public Candidate() { }

        #endregion Конструкторы
    }
}
