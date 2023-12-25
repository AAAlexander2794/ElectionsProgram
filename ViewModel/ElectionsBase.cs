using ElectionsProgram.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    internal class ElectionsBase : INotifyPropertyChanged
    {
        private ObservableCollection<Mediaresource> _mediaresources = 
            new ObservableCollection<Mediaresource>();
        /// <summary>
        /// Список медиаресурсов
        /// </summary>
        public ObservableCollection<Mediaresource> Mediaresources { get =>  _mediaresources;
            set
            {
                _mediaresources = value;
                OnPropertyChanged();
            }
        }
            

        /// <summary>
        /// Список партий
        /// </summary>
        public ObservableCollection<Party> Parties = 
            new ObservableCollection<Party>();

        /// <summary>
        /// Список кандидатов
        /// </summary>
        public ObservableCollection<Candidate> Candidates = 
            new ObservableCollection<Candidate>();

        private string _description = "This is text";
        public string Description { get => _description; 
            set 
            {
                _description = value;
                OnPropertyChanged();
            }
        } 

        public ElectionsBase()
        {
            // Создаем базовые медиаресурсы
            Mediaresource media1 = new Mediaresource("Россия-1");
            Mediaresource media2 = new Mediaresource("Россия-24");
            Mediaresource media3 = new Mediaresource("Маяк");
            Mediaresource media4 = new Mediaresource("Вести ФМ");
            Mediaresource media5 = new Mediaresource("Радио России");
            Mediaresources.Add(media1);
            Mediaresources.Add(media2);
            Mediaresources.Add(media3);
            Mediaresources.Add(media4);
            Mediaresources.Add(media5);
            // Создаем дополнительную "партию", обозначающую отсутствие партии
            Parties.Add(new Party(new PartyView("Самовыдвижение")));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
