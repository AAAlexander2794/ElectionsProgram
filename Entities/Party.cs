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
    /// Политическая партия
    /// </summary>
    public class Party : INotifyPropertyChanged
    {
        private Talon? талон_Россия_1;
        private Talon? талон_Россия_24;
        private Talon? талон_Маяк;
        private Talon? талон_Радио_России;
        private Talon? талон_Вести_ФМ;

        /// <summary>
        /// Текстовые поля с данными
        /// </summary>
        public PartyView View { get; set; }

        

        #region Талоны

        public Talon? Талон_Россия_1 { get => талон_Россия_1; set { талон_Россия_1 = value; OnPropertyChanged(); }  }

        public Talon? Талон_Россия_24 { get => талон_Россия_24; set { талон_Россия_24 = value; OnPropertyChanged(); } }

        public Talon? Талон_Маяк { get => талон_Маяк; set { талон_Маяк = value; OnPropertyChanged(); } }

        public Talon? Талон_Радио_России { get => талон_Радио_России; set { талон_Радио_России = value; OnPropertyChanged(); } }

        public Talon? Талон_Вести_ФМ { get => талон_Вести_ФМ; set { талон_Вести_ФМ = value; OnPropertyChanged(); } }

        #endregion Талоны

        public string Представитель_Фамилия_ИО {  get; set; } = "";

        public string Представитель_ИО_Фамилия { get; set; } = "";

        #region Конструкторы

        public Party(PartyView partyView) 
        {
            View = partyView;
            // Формируем "Фамилия И.О."
            Представитель_Фамилия_ИО = $"{View.Представитель_Фамилия_Падеж_им}";
            // Если есть имя
            if (View.Представитель_Имя_Падеж_им.Length > 0)
            {
                Представитель_Фамилия_ИО += $" {View.Представитель_Имя_Падеж_им[0]}.";
                //
                Представитель_ИО_Фамилия += $" {View.Представитель_Имя_Падеж_им[0]}.";
                // Если есть имя и отчество
                if (View.Представитель_Отчество_Падеж_им.Length > 0)
                {
                    Представитель_Фамилия_ИО += $"{View.Представитель_Отчество_Падеж_им[0]}.";
                    //
                    Представитель_ИО_Фамилия += $"{View.Представитель_Отчество_Падеж_им[0]}.";
                }
            }
            //
            Представитель_ИО_Фамилия += $"{View.Представитель_Фамилия_Падеж_им}";
        }

        //public Party() { }

        #endregion Конструкторы

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
