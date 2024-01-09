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
    public class LoadPartiesTalonsCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private ViewModel _viewModel;

        public LoadPartiesTalonsCommand(ViewModel viewModel)
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
            dt = ProcessorExcel.LoadFromExcel(@"Настройки\Партии\Талоны бесплатные\Талоны Россия-1.xlsx");
            _viewModel.PartiesTalons_Россия_1 = new ObservableCollection<Talon>(TalonsBuilder.ParseBroadcastNominalViews(dt, "Россия-1"));
            // Росия-24
            dt = ProcessorExcel.LoadFromExcel(@"Настройки\Партии\Талоны бесплатные\Талоны Россия-24.xlsx");
            _viewModel.PartiesTalons_Россия_24 = new ObservableCollection<Talon>(TalonsBuilder.ParseBroadcastNominalViews(dt, "Россия-24"));
            // Маяк
            dt = ProcessorExcel.LoadFromExcel(@"Настройки\Партии\Талоны бесплатные\Талоны Маяк.xlsx");
            _viewModel.PartiesTalons_Маяк = new ObservableCollection<Talon>(TalonsBuilder.ParseBroadcastNominalViews(dt, "Маяк"));
            // Вести ФМ
            dt = ProcessorExcel.LoadFromExcel(@"Настройки\Партии\Талоны бесплатные\Талоны Вести ФМ.xlsx");
            _viewModel.PartiesTalons_Вести_ФМ = new ObservableCollection<Talon>(TalonsBuilder.ParseBroadcastNominalViews(dt, "Вести ФМ"));
            // Радио России
            dt = ProcessorExcel.LoadFromExcel(@"Настройки\Партии\Талоны бесплатные\Талоны Радио России.xlsx");
            _viewModel.PartiesTalons_Радио_России = new ObservableCollection<Talon>(TalonsBuilder.ParseBroadcastNominalViews(dt, "Радио России"));
            //
            MessageBox.Show($"Талоны партий загружены.\n" +
                $"Россия-1: {_viewModel.PartiesTalons_Россия_1.Count}\n" +
                $"Россия-24: {_viewModel.PartiesTalons_Россия_24.Count}\n" +
                $"Маяк: {_viewModel.PartiesTalons_Маяк.Count}\n" +
                $"Вести ФМ: {_viewModel.PartiesTalons_Вести_ФМ.Count}\n" +
                $"Радио России: {_viewModel.PartiesTalons_Радио_России.Count}");
        }
    }
}
