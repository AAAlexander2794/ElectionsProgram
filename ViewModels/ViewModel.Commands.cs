using ElectionsProgram.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionsProgram.ViewModels
{
    public partial class ViewModel
    {
        // 
        public LoadMediaresourcesCommand LoadDefaultDataCommand { get; }

        //
        public LoadProtocolSettingsCommand LoadProtocolSettingsCommand { get; }

        public DrawPresidentCommand DrawPresidentCommand { get; }



        #region Команды для партий

        /// <summary>
        /// Загрузка партий
        /// </summary>
        public LoadPartiesCommand LoadPartiesCommand { get; }
        /// <summary>
        /// Загрузка талонов партий (и общего вещания)
        /// </summary>
        public LoadPartiesTalonsCommand LoadPartiesTalonsCommand { get; }
        /// <summary>
        /// Жеребьевка партий
        /// </summary>
        public MatchPartiesAndTalonsCommand MatchPartiesAndTalonsCommand { get; }
        /// <summary>
        /// Создание протоколов партий
        /// </summary>
        public CreateProtocolsPartiesCommand CreateProtocolsPartiesCommand { get; }
        /// <summary>
        /// Жеребьевка партий. Выполняет поочередно все нужные команды для создания протоколов.
        /// </summary>
        public DrawPartiesCommand DrawPartiesCommand { get; }
        /// <summary>
        /// Договоры партий
        /// </summary>
        public ContractPartiesCreateCommand ContractPartiesCreateCommand { get; }

        public SavePartiesToExcelCommand SavePartiesToExcelCommand { get; }

        #endregion Команды для партий

        #region Команды для кандидатов

        public LoadCandidatesCommand LoadCandidatesCommand { get; }
        public LoadCandidatesTalonsCommand LoadCandidatesTalonsCommand { get; }
        public MatchCandidatesAndTalonsCommand MatchCandidatesAndTalonsCommand { get; }
        public CreateProtocolsCandidatesCommand CreateProtocolsCandidatesCommand { get; }
        public DrawCandidatesCommand DrawCandidatesCommand { get; }
        public ContractCandidatesCreateCommand ContractCandidatesCreateCommand { get; }

        #endregion Команды для кандидатов

        // Команды настроек
        public SettingsPathesLoadCommand SettingsPathesLoadCommand { get; }
        public SettngsPathesSaveCommand SettngsPathesSaveCommand { get; }

        //
        public PlaylistCreateCommand PlaylistCreateCommand { get; }

        //
        public TotalReportCreateCommand TotalReportCreateCommand { get; }
    }
}
