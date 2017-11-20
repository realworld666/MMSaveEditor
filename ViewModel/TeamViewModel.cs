using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Linq;
using System.Collections.Generic;

namespace MMSaveEditor.ViewModel
{
    public class TeamViewModel : ViewModelBase
    {
        private Team teamData;

        public ObservableCollection<Team> Teams => Game.instance == null ? null : new ObservableCollection<Team>(Game.instance?.teamManager?.GetEntityList());
        public ObservableCollection<CarPart> BrakesGT => TeamData == null ? null : new ObservableCollection<CarPart>(TeamData?.carManager?.partInventory?.GetPartInventory(CarPart.PartType.BrakesGT));
        public ObservableCollection<CarPart> Brakes => TeamData == null ? null : new ObservableCollection<CarPart>(TeamData?.carManager?.partInventory?.GetPartInventory(CarPart.PartType.Brakes));
        public ObservableCollection<CarPart> EngineGT => TeamData == null ? null : new ObservableCollection<CarPart>(TeamData?.carManager?.partInventory?.GetPartInventory(CarPart.PartType.EngineGT));
        public ObservableCollection<CarPart> Engine => TeamData == null ? null : new ObservableCollection<CarPart>(TeamData?.carManager?.partInventory?.GetPartInventory(CarPart.PartType.Engine));
        public ObservableCollection<CarPart> FrontWing => TeamData == null ? null : new ObservableCollection<CarPart>(TeamData?.carManager?.partInventory?.GetPartInventory(CarPart.PartType.FrontWing));
        public ObservableCollection<CarPart> GearboxGT => TeamData == null ? null : new ObservableCollection<CarPart>(TeamData?.carManager?.partInventory?.GetPartInventory(CarPart.PartType.GearboxGT));
        public ObservableCollection<CarPart> Gearbox => TeamData == null ? null : new ObservableCollection<CarPart>(TeamData?.carManager?.partInventory?.GetPartInventory(CarPart.PartType.Gearbox));
        public ObservableCollection<CarPart> RearWingGT => TeamData == null ? null : new ObservableCollection<CarPart>(TeamData?.carManager?.partInventory?.GetPartInventory(CarPart.PartType.RearWingGT));
        public ObservableCollection<CarPart> RearWing => TeamData == null ? null : new ObservableCollection<CarPart>(TeamData?.carManager?.partInventory?.GetPartInventory(CarPart.PartType.RearWing));
        public ObservableCollection<CarPart> SuspensionGT => TeamData == null ? null : new ObservableCollection<CarPart>(TeamData?.carManager?.partInventory?.GetPartInventory(CarPart.PartType.SuspensionGT));
        public ObservableCollection<CarPart> Suspension => TeamData == null ? null : new ObservableCollection<CarPart>(TeamData?.carManager?.partInventory?.GetPartInventory(CarPart.PartType.Suspension));

        public List<Engineer> Engineers
        {
            get
            {
                return Game.instance?.engineerManager?.GetEntityList().Where(e => e.Contract.GetTeam() == teamData).ToList();
            }
        }

        public int Reputation
        {
            get { return TeamData?.reputation ?? 0; }
            set
            {
                if (TeamData != null)
                {
                    TeamData.reputation = value;
                    RaisePropertyChanged(String.Empty);
                }
            }
        }

        public float Marketability
        {
            get { return TeamData?.marketability ?? 0; }
            set
            {
                if (TeamData != null)
                {
                    TeamData.marketability = value;
                    RaisePropertyChanged(String.Empty);
                }
            }
        }
        public int Pressure
        {
            get { return TeamData?.pressure ?? 0; }
            set
            {
                if (TeamData != null)
                {
                    TeamData.pressure = value;
                    RaisePropertyChanged(String.Empty);
                }
            }
        }
        public float FanBase
        {
            get { return TeamData?.fanBase ?? 0; }
            set
            {
                if (TeamData != null)
                {
                    TeamData.fanBase = value;
                    RaisePropertyChanged(String.Empty);
                }
            }
        }
        public float Aggression
        {
            get { return TeamData?.aggression ?? 0; }
            set
            {
                if (TeamData != null)
                {
                    TeamData.aggression = value;
                    RaisePropertyChanged(String.Empty);
                }
            }
        }
        public float InitialTotalFanBase
        {
            get { return TeamData?.initialTotalFanBase ?? 0; }
            set
            {
                if (TeamData != null)
                {
                    TeamData.initialTotalFanBase = value;
                    RaisePropertyChanged(String.Empty);
                }
            }
        }

        public Team TeamData
        {
            get
            {
                return teamData;
            }

            set
            {
                teamData = value;
            }
        }

        public List<HQsBuilding_v1> HQBuildings
        {
            get
            {
                if (Game.instance != null && teamData != null)
                {
                    var hq = Game.instance.headquartersManager.headquarters.FirstOrDefault(h => h.Value.team.teamID == teamData.teamID);
                    return hq.Value.hqBuildings;
                }
                return null;
            }
        }

        public void SetModel(Team targetTeam)
        {
            TeamData = targetTeam;
            RaisePropertyChanged(String.Empty);
        }
    }
}
