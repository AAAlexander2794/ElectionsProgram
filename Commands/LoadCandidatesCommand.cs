using ElectionsProgram.Entities;
using ElectionsProgram.Processors;
using ElectionsProgram.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ElectionsProgram.Commands
{
    public class LoadCandidatesCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private ViewModel _vm;

        public LoadCandidatesCommand(ViewModel viewModel)
        {
            _vm = viewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            try
            {
                // Читаем Excel
                DataTable dt = ProcessorExcel.LoadFromExcel(@"Настройки/Кандидаты/Кандидаты.xlsx");
                // Формируем список представления кандидатов (CandidatesView) из DataTable
                var candidatesViews = dt.ToList<CandidateView>();
                // Создаем пустой список кандидатов
                List<Candidate> candidates = new List<Candidate>();
                //
                foreach (var item in candidatesViews)
                {
                    // Создаем кандидата на основе представления
                    var candidate = new Candidate(item);
                    // Добавляем к кандидатам имеющиеся талоны (должны быть уже загружены)
                    candidate.Талон_Россия_1 = _vm.CandidatesTalons_Россия_1.FirstOrDefault(t => t.Number == candidate.View.Талон_Россия_1);
                    candidate.Талон_Россия_24 = _vm.CandidatesTalons_Россия_24.FirstOrDefault(t => t.Number == candidate.View.Талон_Россия_24);
                    candidate.Талон_Маяк = _vm.CandidatesTalons_Маяк.FirstOrDefault(t => t.Number == candidate.View.Талон_Маяк);
                    candidate.Талон_Вести_ФМ = _vm.CandidatesTalons_Вести_ФМ.FirstOrDefault(t => t.Number == candidate.View.Талон_Вести_ФМ);
                    candidate.Талон_Радио_России = _vm.CandidatesTalons_Радио_России.FirstOrDefault(t => t.Number == candidate.View.Талон_Радио_России);
                    // Добавляем кандидата в список
                    candidates.Add(candidate);
                }
                // Добавляем список кандидатов в VM
                _vm.Candidates = new ObservableCollection<Candidate>(candidates);
                // 
                MessageBox.Show($"Кандидаты загружены.\n" +
                    $"Количество: {_vm.Candidates.Count}.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки кандидатов.\n{ex.Message}");
            }
        }
    }
}
