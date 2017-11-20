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

        public ObservableCollection<PoliticalVote> ActiveRules
        {
            get
            {
                return championshipData?.Rules.ActiveRules;
            }
        }

        public ObservableCollection<PoliticalVote> NextYearRules
        {
            get
            {
                return championshipData?.NextYearsRules.ActiveRules;
            }
        }

        public List<PoliticalVote> AllRules
        {
            get
            {
                List<PoliticalVote> mVotes = new List<PoliticalVote>();
                mVotes.Clear();
                List<int> intList = new List<int>((IEnumerable<int>)VotesManager.Instance.voteIDs);
                int count = intList.Count;
                for (int index = 0; index < count; ++index)
                {
                    PoliticalVote inVote = VotesManager.Instance.votes[intList[index]];//.Clone();
                    inVote.Initialize(ChampionshipData);
                    if (inVote.HasImpactOfType<PoliticalImpactChangeTrack>())
                    {
                        /*PoliticalImpactChangeTrack impactOfType = inVote.GetImpactOfType<PoliticalImpactChangeTrack>();
                        if (impactOfType.CanTrackImpactBeApplied(this.widget.championship) && this.CanAddTrackImpact(impactOfType, this.widget.championship))
                            this.mVotes.Add(inVote);*/
                        continue;
                    }
                    else if (!championshipData.politicalSystem.HasVote(inVote) && championshipData.politicalSystem.CanVoteBeUsed(inVote, championshipData.politicalSystem.votesForSeason) && inVote.CanBeUsed())
                        mVotes.Add(inVote);
                }

                return mVotes;
            }
        }

        public ObservableCollection<RaceEventCalendarData> RaceEvents
        {
            get
            {
                if (championshipData != null)
                {
                    return championshipData.CalendarData;
                }
                return null;
            }
        }

        public List<Circuit> AllCircuits
        {
            get
            {
                return CircuitManager.Instance.circuits.Where(c => RaceEvents.All(e => e.circuit.circuitID != c.circuitID)).ToList();
            }
        }

    }
}
