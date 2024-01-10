using ElectionsProgram.Builders;
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
    public class LoadCandidatesTalonsCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private ViewModel _viewModel;

        public LoadCandidatesTalonsCommand(ViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            DataTable dt;
            // Россия-1
            dt = ProcessorExcel.LoadFromExcel(@"Настройки\Кандидаты\Талоны бесплатные\Талоны Россия-1.xlsx");
            _viewModel.CandidatesTalons_Россия_1 = new ObservableCollection<Talon>(TalonsBuilder.ParseTalonsVariantBase(dt, "Россия-1"));
            // Росия-24
            dt = ProcessorExcel.LoadFromExcel(@"Настройки\Кандидаты\Талоны бесплатные\Талоны Россия-24.xlsx");
            _viewModel.CandidatesTalons_Россия_24 = new ObservableCollection<Talon>(TalonsBuilder.ParseTalonsVariantBase(dt, "Россия-24"));
            // Маяк
            dt = ProcessorExcel.LoadFromExcel(@"Настройки\Кандидаты\Талоны бесплатные\Талоны Маяк.xlsx");
            _viewModel.CandidatesTalons_Маяк = new ObservableCollection<Talon>(TalonsBuilder.ParseTalonsVariantBase(dt, "Маяк"));
            // Вести ФМ
            dt = ProcessorExcel.LoadFromExcel(@"Настройки\Кандидаты\Талоны бесплатные\Талоны Вести ФМ.xlsx");
            _viewModel.CandidatesTalons_Вести_ФМ = new ObservableCollection<Talon>(TalonsBuilder.ParseTalonsVariantBase(dt, "Вести ФМ"));
            // Радио России
            dt = ProcessorExcel.LoadFromExcel(@"Настройки\Кандидаты\Талоны бесплатные\Талоны Радио России.xlsx");
            _viewModel.CandidatesTalons_Радио_России = new ObservableCollection<Talon>(TalonsBuilder.ParseTalonsVariantBase(dt, "Радио России"));
            //
            MessageBox.Show($"Талоны кандидатов загружены.\n" +
                $"Россия-1: {_viewModel.CandidatesTalons_Россия_1.Count}\n" +
                $"Россия-24: {_viewModel.CandidatesTalons_Россия_24.Count}\n" +
                $"Маяк: {_viewModel.CandidatesTalons_Маяк.Count}\n" +
                $"Вести ФМ: {_viewModel.CandidatesTalons_Вести_ФМ.Count}\n" +
                $"Радио России: {_viewModel.CandidatesTalons_Радио_России.Count}");
        }
    }
}
