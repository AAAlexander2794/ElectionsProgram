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
                DataTable dt = ExcelProcessor.LoadFromExcel(@"Настройки/Кандидаты/Кандидаты.xlsx");
                // Формируем список представления кандидатов (CandidatesView) из DataTable
                var candidatesViews = dt.ToList<CandidateView>();
                // Новый список кандидатов в VM
                _vm.Candidates = new ObservableCollection<Candidate>();
                //
                foreach (var view in candidatesViews)
                {
                    _vm.Candidates.Add(new Candidate(view));
                }
                // 
                string message = $"Кандидаты загружены.\n" +
                    $"Количество: {_vm.Candidates.Count}.";
                Logger.Add(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки кандидатов.\n{ex.Message}");
            }
        }
    }
}
