using DocumentFormat.OpenXml.Wordprocessing;
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
    public class Candidate : INotifyPropertyChanged, IComparable<Candidate>
    {
        private Talon? талон_Россия_1;
        private Talon? талон_Россия_24;
        private Talon? талон_Маяк;
        private Talon? талон_Радио_России;
        private Talon? талон_Вести_ФМ;

        public int Id { get; set; }

        private string _фамилия_ИО = "";
        public string Фамилия_ИО { get => _фамилия_ИО;
            private set
            {
                _фамилия_ИО = value;
                OnPropertyChanged();
            }
        }
        public string ИО_Фамилия { get; } = "";
        public string Представитель_ИО_Фамилия { get; } = "";

        //List<Talon> Talons { get; set; }

        /// <summary>
        /// Данные кандидата в текстовом виде (как в таблицах)
        /// </summary>
        public CandidateView View { get; set; }

        /// <summary>
        /// Партия, от которой выдвигается кандидат
        /// </summary>
        public Party? Party { get; set; }

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
            if (View.Имя.Length > 0 && View.Отчество.Length > 0)
            {
                Фамилия_ИО = $"{View.Фамилия} {View.Имя[0]}.{View.Отчество[0]}.";
                ИО_Фамилия = $"{View.Имя[0]}.{View.Отчество[0]}. {View.Фамилия}";
            }
            if (View.Представитель_Имя.Length > 0 && View.Представитель_Отчество.Length > 0)
            {
                Представитель_ИО_Фамилия = $"{View.Представитель_Имя[0]}.{View.Представитель_Отчество[0]}. {View.Представитель_Фамилия}";
            }
        }

        #endregion Конструкторы

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int CompareTo(Candidate? other)
        {
            if (other is Candidate candidate) return View.Фамилия.CompareTo(candidate.View.Фамилия);
            else throw new ArgumentException("Некорректное значение параметра");
        }
    }
}
