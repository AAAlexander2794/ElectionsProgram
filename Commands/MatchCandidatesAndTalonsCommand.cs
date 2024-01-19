using ElectionsProgram.Entities;
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
    public class MatchCandidatesAndTalonsCommand(ViewModel viewModel) : ICommand
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
                //
                _vm.Regions.Clear();
                //
                foreach (var candidate in _vm.Candidates)
                {
                    // Добавляем к кандидатам имеющиеся талоны (должны быть уже загружены)
                    candidate.Талон_Россия_1 = _vm.CandidatesTalons_Россия_1.FirstOrDefault(t => t.Number == candidate.View.Талон_Россия_1);
                    candidate.Талон_Россия_24 = _vm.CandidatesTalons_Россия_24.FirstOrDefault(t => t.Number == candidate.View.Талон_Россия_24);
                    candidate.Талон_Маяк = _vm.CandidatesTalons_Маяк.FirstOrDefault(t => t.Number == candidate.View.Талон_Маяк);
                    candidate.Талон_Вести_ФМ = _vm.CandidatesTalons_Вести_ФМ.FirstOrDefault(t => t.Number == candidate.View.Талон_Вести_ФМ);
                    candidate.Талон_Радио_России = _vm.CandidatesTalons_Радио_России.FirstOrDefault(t => t.Number == candidate.View.Талон_Радио_России);
                    // Общее вещание
                    if (candidate.Талон_Россия_1 != null) candidate.Талон_Россия_1.CommonTalon = _vm.candidatesCommonTalon_Россия_1;
                    if (candidate.Талон_Россия_24 != null) candidate.Талон_Россия_24.CommonTalon = _vm.candidatesCommonTalon_Россия_24;
                    if (candidate.Талон_Маяк != null) candidate.Талон_Маяк.CommonTalon = _vm.candidatesCommonTalon_Маяк;
                    if (candidate.Талон_Вести_ФМ != null) candidate.Талон_Вести_ФМ.CommonTalon = _vm.candidatesCommonTalon_Вести_ФМ;
                    if (candidate.Талон_Радио_России != null) candidate.Талон_Радио_России.CommonTalon = _vm.candidatesCommonTalon_Радио_России;

                    #region Формирование округов

                    bool isInCollection = false;
                    // Проверяем по списку округов
                    for (int i = 0; i < _vm.Regions.Count; i++)
                    {
                        // Если округ текущего кандидата уже есть в списке
                        if (_vm.Regions[i].Номер == candidate.View.Округ_Номер)
                        {
                            isInCollection = true;
                            // Добавляем кандидата округу
                            _vm.Regions[i].Candidates.Add(candidate);
                        }
                    }
                    // Если округа текущего кандидата не было в списке
                    if (!isInCollection) 
                    {
                        // Создаем новый регион по данным текущего кандидата
                        Region newRegion = new Region()
                        {
                            Номер = candidate.View.Округ_Номер.Trim(),
                            Название_Падеж_им = candidate.View.Округ_Название_падеж_им.Trim(),
                            Название_Падеж_дат = candidate.View.Округ_Название_падеж_дат.Trim(),
                            Дополнительно = candidate.View.Округ_Дополнительно.Trim()
                        };
                        // Добавляем текущего кандидата
                        newRegion.Candidates.Add(candidate);
                        // Добавляем созданный округ в список округов
                        _vm.Regions.Add(newRegion);
                    }

                    #endregion Формирование округов
                }
                //
                Logger.Add($"Сопоставлены кандидаты и талоны.\n" +
                    $"Созданы округи: {_vm.Regions.Count}.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
