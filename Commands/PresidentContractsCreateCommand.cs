using ElectionsProgram.Processors;
using ElectionsProgram.Reports.Contracts;
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
    public class PresidentContractsCreateCommand(ViewModel viewModel) : ICommand
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
                // Загрузка партий
                _vm.LoadCandidatesCommand.Execute(null);
                // Загрузка талонов и общего вещания
                _vm.LoadCandidatesTalonsCommand.Execute(null);
                // Сопоставление талонов партиям
                _vm.MatchCandidatesAndTalonsCommand.Execute(null);
                //
                string pathContracts = $"{_vm.SettingsFilePathes.Каталог_документов}{_vm.SettingsFilePathes.Договоры_Выходной_каталог}";
                string path_РВ = $"{_vm.SettingsFilePathes.Каталог_настроек}{_vm.SettingsFilePathes.Кандидаты_Шаблон_договора_РВ}";
                string path_ТВ = $"{_vm.SettingsFilePathes.Каталог_настроек}{_vm.SettingsFilePathes.Кандидаты_Шаблон_договора_ТВ}";
                

                // Загрузка партий
                _vm.LoadPartiesCommand.Execute(null);
                // Загрузка талонов и общего вещания
                _vm.LoadPartiesTalonsCommand.Execute(null);
                // Сопоставление талонов партиям
                _vm.MatchPartiesAndTalonsCommand.Execute(null);
                //
                pathContracts = $"{_vm.SettingsFilePathes.Каталог_документов}{_vm.SettingsFilePathes.Договоры_Выходной_каталог}";
                path_РВ = $"{_vm.SettingsFilePathes.Каталог_настроек}{_vm.SettingsFilePathes.Партии_Шаблон_договора_РВ}";
                path_ТВ = $"{_vm.SettingsFilePathes.Каталог_настроек}{_vm.SettingsFilePathes.Партии_Шаблон_договора_ТВ}";

                //
                int count = ContractCandidateBuilder.BuildContractsCandidates(
                    _vm.Candidates.ToList(),
                    pathContracts,
                    path_РВ,
                    path_ТВ);
                //
                Logger.Add($"Договоры кандидатов созданы: {count}.");

                count = ContractPartyBuilder.BuildContractsParties(
                    _vm.Parties.ToList(),
                    pathContracts,
                    path_РВ,
                    path_ТВ);
                //
                Logger.Add($"Договоры партий созданы: {count}.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
