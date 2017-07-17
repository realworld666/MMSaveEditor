using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace MMSaveEditor.ViewModel
{
    public class TeamViewModel : ViewModelBase
    {
        protected Team _teamData;

        public ObservableCollection<Team> Teams => Game.Instance == null ? null : new ObservableCollection<Team>(Game.Instance?.teamManager?.GetEntityList());
        public ObservableCollection<CarPart> BrakesGT => _teamData == null ? null : new ObservableCollection<CarPart>(_teamData?.carManager?.partInventory?.brakesGTInventory);
        public ObservableCollection<CarPart> Brakes => _teamData == null ? null : new ObservableCollection<CarPart>(_teamData?.carManager?.partInventory?.brakesInventory);
        public ObservableCollection<CarPart> EngineGT => _teamData == null ? null : new ObservableCollection<CarPart>(_teamData?.carManager?.partInventory?.engineGTInventory);
        public ObservableCollection<CarPart> Engine => _teamData == null ? null : new ObservableCollection<CarPart>(_teamData?.carManager?.partInventory?.engineInventory);
        public ObservableCollection<CarPart> FrontWing => _teamData == null ? null : new ObservableCollection<CarPart>(_teamData?.carManager?.partInventory?.frontWingInventory);
        public ObservableCollection<CarPart> GearboxGT => _teamData == null ? null : new ObservableCollection<CarPart>(_teamData?.carManager?.partInventory?.gearboxGTInventory);
        public ObservableCollection<CarPart> Gearbox => _teamData == null ? null : new ObservableCollection<CarPart>(_teamData?.carManager?.partInventory?.gearboxInventory);
        public ObservableCollection<CarPart> RearWingGT => _teamData == null ? null : new ObservableCollection<CarPart>(_teamData?.carManager?.partInventory?.rearWingGTInventory);

        public int Reputation
        {
            get { return _teamData?.reputation ?? 0; }
            set
            {
                if (_teamData != null)
                {
                    _teamData.reputation = value;
                    RaisePropertyChanged(String.Empty);
                }
            }
        }

        public float Marketability
        {
            get { return _teamData?.marketability ?? 0; }
            set
            {
                if (_teamData != null)
                {
                    _teamData.marketability = value;
                    RaisePropertyChanged(String.Empty);
                }
            }
        }
        public int Pressure
        {
            get { return _teamData?.pressure ?? 0; }
            set
            {
                if (_teamData != null)
                {
                    _teamData.pressure = value;
                    RaisePropertyChanged(String.Empty);
                }
            }
        }
        public float FanBase
        {
            get { return _teamData?.fanBase ?? 0; }
            set
            {
                if (_teamData != null)
                {
                    _teamData.fanBase = value;
                    RaisePropertyChanged(String.Empty);
                }
            }
        }
        public float Aggression
        {
            get { return _teamData?.aggression ?? 0; }
            set
            {
                if (_teamData != null)
                {
                    _teamData.aggression = value;
                    RaisePropertyChanged(String.Empty);
                }
            }
        }
        public float InitialTotalFanBase
        {
            get { return _teamData?.initialTotalFanBase ?? 0; }
            set
            {
                if (_teamData != null)
                {
                    _teamData.initialTotalFanBase = value;
                    RaisePropertyChanged(String.Empty);
                }
            }
        }

        public void SetModel(Team targetTeam)
        {
            _teamData = targetTeam;
            RaisePropertyChanged(String.Empty);
        }
    }
}
