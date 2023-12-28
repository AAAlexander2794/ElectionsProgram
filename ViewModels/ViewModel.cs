using ElectionsProgram.Commands;
using ElectionsProgram.Commands.Temp;
using ElectionsProgram.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectionsProgram.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        // Test
        private DataTable _test =
            new DataTable();
        /// <summary>
        /// Test
        /// </summary>
        public DataTable Test
        {
            get => _test;
            set
            {
                _test = value;
                OnPropertyChanged();
            }
        }

        #region Списки

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

        private Mediaresource _currentMediaresource;
        /// <summary>
        /// Текущий медиаресурс
        /// </summary>
        public Mediaresource CurrentMediaresource { get => _currentMediaresource; 
            set
            {
                _currentMediaresource = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Party> _parties = 
            new ObservableCollection<Party>();
        /// <summary>
        /// Список партий
        /// </summary>
        public ObservableCollection<Party> Parties { get => _parties;
            set
            {
                _parties = value; 
                OnPropertyChanged();
            }
        }
            
        private ObservableCollection<Candidate> _candidates = 
            new ObservableCollection<Candidate>();
        /// <summary>
        /// Список кандидатов
        /// </summary>
        public ObservableCollection<Candidate> Candidates { get => _candidates;
            set
            {
                _candidates = value;
                OnPropertyChanged();
            }
        }

        #endregion Списки

        #region Комманды

        public SaveDBCommand SaveCommand { get; }
        public LoadDBCommand LoadCommand { get; }
        public LoadDefaultDataCommand LoadDefaultDataCommand { get; }
        public SavePartiesToExcelCommand SavePartiesToExcelCommand { get; }

        #endregion Комманды

        #region Конструкторы

        public ViewModel()
        {
            // Инициализируем команды
            SaveCommand = new SaveDBCommand(this);
            LoadCommand = new LoadDBCommand(this);
            LoadDefaultDataCommand = new LoadDefaultDataCommand(this);
            SavePartiesToExcelCommand = new SavePartiesToExcelCommand(this);
        }

        #endregion Конструкторы

        #region Методы

        /// <summary>
        /// Загружаем данные из, например, файла
        /// </summary>
        /// <param name="viewModel"></param>
        public void Load(ViewModel viewModel)
        {
            Mediaresources = viewModel.Mediaresources;

        }

        #endregion Методы

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
