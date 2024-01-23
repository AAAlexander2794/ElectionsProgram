using ElectionsProgram.Builders;
using ElectionsProgram.Processors;
using ElectionsProgram.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ElectionsProgram.Commands
{
    public class ContractCandidatesCreateCommand(ViewModel viewModel) : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly ViewModel _vm = viewModel;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            try
            {
                string pathContracts = $"{_vm.SettingsFilePathes.Каталог_документов}{_vm.SettingsFilePathes.Договоры_Выходной_каталог}";
                string path_РВ = $"{_vm.SettingsFilePathes.Каталог_настроек}{_vm.SettingsFilePathes.Кандидаты_Шаблон_договора_РВ}";
                string path_ТВ = $"{_vm.SettingsFilePathes.Каталог_настроек}{_vm.SettingsFilePathes.Кандидаты_Шаблон_договора_ТВ}";
                int count = ContractCandidateBuilder.BuildContractsCandidates(
                    _vm.Candidates.ToList(),
                    pathContracts,
                    path_РВ,
                    path_ТВ);
                //
                Logger.Add($"Договоры кандидатов созданы: {count}.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
