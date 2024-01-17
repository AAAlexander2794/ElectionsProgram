using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.Entities
{
    /// <summary>
    /// Кандидат на выборы
    /// </summary>
    public class Candidate : IClient, INotifyPropertyChanged
    {
        private Talon? талон_Россия_1;
        private Talon? талон_Россия_24;
        private Talon? талон_Маяк;
        private Talon? талон_Радио_России;
        private Talon? талон_Вести_ФМ;

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

        string IClient.Type => "Кандидат";

        #region Талоны

        public Talon? Талон_Россия_1 { get => талон_Россия_1; set { талон_Россия_1 = value; OnPropertyChanged(); } }

        public Talon? Талон_Россия_24 { get => талон_Россия_24; set { талон_Россия_24 = value; OnPropertyChanged(); } }

        public Talon? Талон_Маяк { get => талон_Маяк; set { талон_Маяк = value; OnPropertyChanged(); } }

        public Talon? Талон_Радио_России { get => талон_Радио_России; set { талон_Радио_России = value; OnPropertyChanged(); } }

        public Talon? Талон_Вести_ФМ { get => талон_Вести_ФМ; set { талон_Вести_ФМ = value; OnPropertyChanged(); } }

        #endregion Талоны

        #region Конструкторы

        public Candidate(CandidateView candidateView) 
        {
            View = candidateView;
            Фамилия_И_О = $"{View.Фамилия} {View.Имя[0]} {View.Отчество[0]}";
        }

        #endregion Конструкторы

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
