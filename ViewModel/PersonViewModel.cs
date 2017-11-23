using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using MMSaveEditor.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MMSaveEditor.ViewModel
{
    public abstract class PersonViewModel<T> : ViewModelBase where T : Person
    {
        private T _personData;
        public T PersonData
        {
            get => _personData;

            set => _personData = value;
        }

        private ObservableCollection<T> _people;
        public ObservableCollection<T> People
        {
            get => _people;
            set
            {
                _people = value;
                RaisePropertyChanged(string.Empty);
            }
        }

        public void SetList(List<T> list)
        {
            People = new ObservableCollection<T>(list);
        }

        public virtual void SetModel(T pPersonData)
        {
            PersonData = pPersonData;
            if (PersonData?.nationality != null)
                Continent = PersonData.nationality.continent;

            RaisePropertyChanged(string.Empty);
            RaisePropertyChanged("Nationality");
        }

        public string TeamName
        {
            get
            {
                if (_personData?.Contract?.employeer is Team)
                {
                    var team = _personData.Contract.employeer as Team;
                    return team.Name;
                }
                return "";
            }
        }

        public string FirstName
        {
            get => PersonData?.firstName;
            set
            {
                PersonData.SetName(value, PersonData.lastName);
                RaisePropertyChanged(string.Empty);
            }
        }
        public string LastName
        {
            get => PersonData?.lastName;
            set
            {
                PersonData.SetName(PersonData.firstName, value);
                RaisePropertyChanged(string.Empty);
            }
        }

        public string ShortName => PersonData?.shortName;

        public string ThreeLetterName => PersonData?.threeLetterLastName;

        public DateTime DOB
        {
            get
            {
                return PersonData?.dateOfBirth ?? DateTime.Now;
            }
            set
            {
                if (PersonData != null)
                {
                    PersonData.dateOfBirth = value;
                }
            }
        }

        public Person.Gender Gender
        {
            get => PersonData == null ? Person.Gender.Male : PersonData.gender;
            set => PersonData.gender = value;
        }
        public IEnumerable<Person.Gender> GenderTypes => Enum.GetValues(typeof(Person.Gender)).Cast<Person.Gender>();

        public int Weight
        {
            get => PersonData == null ? 0 : PersonData.weight;

            set => PersonData.weight = value;
        }

        public int RetirementAge
        {
            get => PersonData == null ? 0 : PersonData.retirementAge;

            set => PersonData.retirementAge = value;
        }

        public float Obedience
        {
            get => PersonData == null ? 0 : PersonData.obedience;

            set => PersonData.obedience = value;
        }

        public DateTime PeakAge
        {
            get
            {
                if (PersonData != null)
                    return PersonData.peakAge;
                return DateTime.Now;
            }
            set
            {
                if (PersonData != null)
                    PersonData.peakAge = value;
            }
        }

        public float Morale
        {
            get => PersonData == null ? 0 : PersonData.Morale;

            set => PersonData.Morale = value;
        }

        public Team CurrentTeam
        {
            get
            {
                return _personData?.Contract?.employeer as Team;
            }
        }

        public void _viewTeam()
        {
            var team = _personData?.Contract?.employeer as Team;
            if (team == null)
            {
                return;
            }

            var teamVM = SimpleIoc.Default.GetInstance<TeamViewModel>();
            teamVM.SetModel(team);
            MainWindow.Instance.SwitchToTab(MainWindow.TabPage.Team);
        }

        public virtual List<Person> GetPeopleFromTeam(Team t)
        {
            return null;
        }

        private Nationality.Continent _continent;
        private ObservableCollection<Nationality> _availableNationalities = new ObservableCollection<Nationality>();

        public Nationality.Continent Continent
        {
            get
            {
                return _continent;
            }
            set
            {
                _continent = value;
                _availableNationalities.Clear();
                List<Nationality> validNationalities = NationalityManager.Instance.GetNationalitiesForContinent(_continent);
                foreach (Nationality n in validNationalities)
                {
                    _availableNationalities.Add(n);
                }

                RaisePropertyChanged("Nationality");
            }
        }

        public IEnumerable<Nationality.Continent> ContinentTypes => Enum.GetValues(typeof(Nationality.Continent)).Cast<Nationality.Continent>();

        public Nationality Nationality
        {
            get
            {
                return PersonData != null ? _availableNationalities?.FirstOrDefault(n => n.countryKey.Equals(PersonData.nationality.countryKey)) : null;
            }
            set
            {
                if (value != null)
                {
                    PersonData.nationality = value;
                    //RaisePropertyChanged(string.Empty);
                    _continent = value.continent;
                }
            }
        }

        public ObservableCollection<Nationality> PossibleNationalities
        {
            get
            {
                return _availableNationalities;
            }
        }
    }
}
