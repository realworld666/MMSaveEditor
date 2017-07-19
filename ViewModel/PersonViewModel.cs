using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace MMSaveEditor.ViewModel
{
    public abstract class PersonViewModel<T> : ViewModelBase where T : Person
    {
        protected T _personData;

        private ObservableCollection<T> _people;
        public ObservableCollection<T> People
        {
            get
            {
                return _people;
            }
            set
            {
                _people = value;
                RaisePropertyChanged( String.Empty );
            }
        }

        public void SetList( List<T> list )
        {
            People = new ObservableCollection<T>( list );
        }

        public void SetModel( T personData )
        {
            _personData = personData;
            RaisePropertyChanged( String.Empty );
        }

        public string FirstName
        {
            get
            {
                return _personData?.firstName;
            }
            set
            {
                _personData.SetName( value, _personData.lastName );
                RaisePropertyChanged( String.Empty );
            }
        }
        public string LastName
        {
            get
            {
                return _personData?.lastName;

            }
            set
            {
                _personData.SetName( _personData.firstName, value );
                RaisePropertyChanged( String.Empty );
            }
        }

        public string ShortName
        {
            get
            {
                return _personData?.shortName;

            }
        }
        public string ThreeLetterName
        {
            get
            {
                return _personData?.threeLetterLastName;

            }
        }

        public DateTime DOB
        {
            get
            {
                if( _personData != null )
                    return _personData.dateOfBirth;
                return DateTime.Now;
            }
            set
            {
                _personData.dateOfBirth = value;
            }
        }

        public Person.Gender Gender
        {
            get
            {

                return _personData == null ? Person.Gender.Male : _personData.gender;
            }
            set
            {
                _personData.gender = value;
            }
        }
        public IEnumerable<Person.Gender> GenderTypes
        {
            get
            {
                return Enum.GetValues( typeof( Person.Gender ) ).Cast<Person.Gender>();
            }
        }

        public int Weight
        {
            get
            {
                return _personData == null ? 0 : _personData.weight;
            }

            set
            {
                _personData.weight = value;
            }
        }

        public int RetirementAge
        {
            get
            {
                return _personData == null ? 0 : _personData.retirementAge;
            }

            set
            {
                _personData.retirementAge = value;
            }
        }

        public float Obedience
        {
            get
            {
                return _personData == null ? 0 : _personData.obedience;
            }

            set
            {
                _personData.obedience = value;
            }
        }

        public DateTime PeakAge
        {
            get
            {
                if( _personData != null )
                    return _personData.peakAge;
                return DateTime.Now;
            }
            set
            {
                _personData.peakAge = value;
            }
        }

        public float Morale
        {
            get
            {
                return _personData == null ? 0 : _personData.mMorale;
            }

            set
            {
                _personData.mMorale = value;
            }
        }
    }
}
