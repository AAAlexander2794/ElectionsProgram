﻿using ElectionsProgram.Commands;
using ElectionsProgram.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectionsProgram.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
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

        private Mediaresource _currentMediaresource;
        /// <summary>
        /// 
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

        #region Комманды

        public SaveCommand SaveCommand { get; }
        public LoadCommand LoadCommand { get; }
        public LoadDefaultDataCommand LoadDefaultDataCommand { get; }

        #endregion Комманды

        public ViewModel()
        {
            // Инициализируем команды
            SaveCommand = new SaveCommand(this);
            LoadCommand = new LoadCommand(this);
            LoadDefaultDataCommand = new LoadDefaultDataCommand(this);
        }

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
