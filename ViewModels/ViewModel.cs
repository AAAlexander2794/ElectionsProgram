using ElectionsProgram.Commands;
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

        #region Коллекции

        #region Коллекции медиаресурсов

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

        private Mediaresource? _currentMediaresource;
        /// <summary>
        /// Текущий медиаресурс
        /// </summary>
        public Mediaresource? CurrentMediaresource { get => _currentMediaresource; 
            set
            {
                _currentMediaresource = value;
                OnPropertyChanged();
            }
        }

        #endregion Коллекции медиаресурсов

        #region Коллекции партий

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

        private Party? _currentParty;
        /// <summary>
        /// Текущая партия (выбранная)
        /// </summary>
        public Party? CurrentParty { get => _currentParty;
            set
            {
                _currentParty = value;
                OnPropertyChanged();
            }
        }

        #region Талоны для партий

        private ObservableCollection<Talon> _partiesTalons_Россия_1 = new ObservableCollection<Talon>();
        /// <summary>
        /// Талоны 
        /// </summary>
        public ObservableCollection<Talon> PartiesTalons_Россия_1 { get => _partiesTalons_Россия_1;
            set
            {
                _partiesTalons_Россия_1 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Talon> _partiesTalons_Россия_24 = new ObservableCollection<Talon>();
        /// <summary>
        /// Талоны 
        /// </summary>
        public ObservableCollection<Talon> PartiesTalons_Россия_24
        {
            get => _partiesTalons_Россия_24;
            set
            {
                _partiesTalons_Россия_24 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Talon> _partiesTalons_Маяк = new ObservableCollection<Talon>();
        /// <summary>
        /// Талоны 
        /// </summary>
        public ObservableCollection<Talon> PartiesTalons_Маяк
        {
            get => _partiesTalons_Маяк;
            set
            {
                _partiesTalons_Маяк = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Talon> _partiesTalons_Вести_ФМ = new ObservableCollection<Talon>();
        /// <summary>
        /// Талоны 
        /// </summary>
        public ObservableCollection<Talon> PartiesTalons_Вести_ФМ
        {
            get => _partiesTalons_Вести_ФМ;
            set
            {
                _partiesTalons_Вести_ФМ = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Talon> _partiesTalons_Радио_России = new ObservableCollection<Talon>();
        /// <summary>
        /// Талоны 
        /// </summary>
        public ObservableCollection<Talon> PartiesTalons_Радио_России
        {
            get => _partiesTalons_Радио_России;
            set
            {
                _partiesTalons_Радио_России = value;
                OnPropertyChanged();
            }
        }

        #endregion Талоны для партий

        #endregion Коллекции партий

        #region Коллекции кандидатов

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

        private Candidate? _currentCandidate;
        /// <summary>
        /// Текущий кандидат (выбранный)
        /// </summary>
        public Candidate? CurrentCandidate { get => _currentCandidate;
            set
            {
                _currentCandidate = value;
                OnPropertyChanged();
            }
        }

        #region Талоны для кандидатов

        private ObservableCollection<Talon> _candidatesTalons_Россия_1 = new ObservableCollection<Talon>();
        /// <summary>
        /// Талоны 
        /// </summary>
        public ObservableCollection<Talon> CandidatesTalons_Россия_1
        {
            get => _candidatesTalons_Россия_1;
            set
            {
                _candidatesTalons_Россия_1 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Talon> _candidatesTalons_Россия_24 = new ObservableCollection<Talon>();
        /// <summary>
        /// Талоны 
        /// </summary>
        public ObservableCollection<Talon> CandidatesTalons_Россия_24
        {
            get => _candidatesTalons_Россия_24;
            set
            {
                _candidatesTalons_Россия_24 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Talon> _candidatesTalons_Маяк = new ObservableCollection<Talon>();
        /// <summary>
        /// Талоны 
        /// </summary>
        public ObservableCollection<Talon> CandidatesTalons_Маяк
        {
            get => _candidatesTalons_Маяк;
            set
            {
                _candidatesTalons_Маяк = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Talon> _candidatesTalons_Вести_ФМ = new ObservableCollection<Talon>();
        /// <summary>
        /// Талоны 
        /// </summary>
        public ObservableCollection<Talon> CandidatesTalons_Вести_ФМ
        {
            get => _candidatesTalons_Вести_ФМ;
            set
            {
                _candidatesTalons_Вести_ФМ = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Talon> _candidatesTalons_Радио_России = new ObservableCollection<Talon>();
        /// <summary>
        /// Талоны 
        /// </summary>
        public ObservableCollection<Talon> CandidatesTalons_Радио_России
        {
            get => _candidatesTalons_Радио_России;
            set
            {
                _candidatesTalons_Радио_России = value;
                OnPropertyChanged();
            }
        }

        #endregion Талоны для кандидатов

        #endregion Коллекции кандидатов

        private SettingsForProtocols _protocolSettings = 
            new SettingsForProtocols(new SettingsForProtocolsView());
        /// <summary>
        /// Настройки для создания протоколов жеребьевки.
        /// </summary>
        public SettingsForProtocols ProtocolSettings { get => _protocolSettings;
            set
            {
                _protocolSettings = value;
                OnPropertyChanged();
            }
        }

        #endregion Коллекции

        #region Комманды

        // 
        public LoadMediaresourcesCommand LoadDefaultDataCommand { get; }
        // Команды для партий
        public LoadPartiesCommand LoadPartiesCommand { get; }
        public LoadPartiesTalonsCommand LoadPartiesTalonsCommand { get; }
        public SavePartiesToExcelCommand SavePartiesToExcelCommand { get; }
        // Команды для кандидатов
        public LoadCandidatesCommand LoadCandidatesCommand { get; }
        public LoadCandidatesTalonsCommand LoadCandidatesTalonsCommand { get; }
        //
        public LoadProtocolSettingsCommand LoadProtocolSettingsCommand { get; }
        public CreateProtocolsPartiesCommand CreateProtocolsPartiesCommand { get; }

        #endregion Комманды

        #region Конструкторы

        public ViewModel()
        {
            // Инициализируем команды
            
            LoadDefaultDataCommand = new LoadMediaresourcesCommand(this);
            // Команды для партий
            LoadPartiesCommand = new LoadPartiesCommand(this);
            LoadPartiesTalonsCommand = new LoadPartiesTalonsCommand(this);
            SavePartiesToExcelCommand = new SavePartiesToExcelCommand(this);
            // Команды для кандидатов
            LoadCandidatesCommand = new LoadCandidatesCommand(this);
            LoadCandidatesTalonsCommand = new LoadCandidatesTalonsCommand(this);
            //
            LoadProtocolSettingsCommand = new LoadProtocolSettingsCommand(this);
            CreateProtocolsPartiesCommand = new CreateProtocolsPartiesCommand(this);
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
