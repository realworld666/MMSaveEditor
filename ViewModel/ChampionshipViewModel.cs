using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Linq;
using System.Collections.Generic;

namespace MMSaveEditor.ViewModel
{
    public class ChampionshipViewModel : ViewModelBase
    {
        private Championship championshipData;

        public ObservableCollection<Championship> Championships
        {
            get
            {
                return Game.instance == null ? null : new ObservableCollection<Championship>(Game.instance?.championshipManager?.GetEntityList());
            }
        }

        public Championship ChampionshipData
        {
            get
            {
                return championshipData;
            }

            set
            {
                championshipData = value;
            }
        }

        public ChampionshipRules Rules
        {
            get
            {
                return championshipData?.Rules;
            }
        }

        public ChampionshipRules NextYearsRules
        {
            get
            {
                return championshipData?.NextYearsRules;
            }
        }

        public void SetModel(Championship targetChampionship)
        {
            ChampionshipData = targetChampionship;
            if (targetChampionship != null)
            {
                targetChampionship.Rules.ValidateChampionshipRules();
            }
            RaisePropertyChanged(String.Empty);
        }

        public List<PoliticalVote> ActiveRules
        {
            get
            {
                return championshipData?.Rules.ActiveRules;
            }
        }

        public List<PoliticalVote> NextYearRules
        {
            get
            {
                return championshipData?.NextYearsRules.ActiveRules;
            }
        }
    }
}
