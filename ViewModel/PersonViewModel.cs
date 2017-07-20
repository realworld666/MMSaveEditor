using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace MMSaveEditor.ViewModel
{
    public abstract class PersonViewModel<T> : ViewModelBase where T : Person
    {
        private T personData;

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
            PersonData = personData;
            RaisePropertyChanged( String.Empty );
        }

        public string FirstName
        {
            get
            {
                return PersonData?.firstName;
            }
            set
            {
                PersonData.SetName( value, PersonData.lastName );
                RaisePropertyChanged( String.Empty );
            }
        }
        public string LastName
        {
            get
            {
                return PersonData?.lastName;

            }
            set
            {
                PersonData.SetName( PersonData.firstName, value );
                RaisePropertyChanged( String.Empty );
            }
        }

        public string ShortName
        {
            get
            {
                return PersonData?.shortName;

            }
        }
        public string ThreeLetterName
        {
            get
            {
                return PersonData?.threeLetterLastName;

            }
        }

        public DateTime DOB
        {
            get
            {
                if( PersonData != null )
                    return PersonData.dateOfBirth;
                return DateTime.Now;
            }
            set
            {
                PersonData.dateOfBirth = value;
            }
        }

        public Person.Gender Gender
        {
            get
            {

                return PersonData == null ? Person.Gender.Male : PersonData.gender;
            }
            set
            {
                PersonData.gender = value;
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
                return PersonData == null ? 0 : PersonData.weight;
            }

            set
            {
                PersonData.weight = value;
            }
        }

        public int RetirementAge
        {
            get
            {
                return PersonData == null ? 0 : PersonData.retirementAge;
            }

            set
            {
                PersonData.retirementAge = value;
            }
        }

        public float Obedience
        {
            get
            {
                return PersonData == null ? 0 : PersonData.obedience;
            }

            set
            {
                PersonData.obedience = value;
            }
        }

        public DateTime PeakAge
        {
            get
            {
                if( PersonData != null )
                    return PersonData.peakAge;
                return DateTime.Now;
            }
            set
            {
                PersonData.peakAge = value;
            }
        }

        public float Morale
        {
            get
            {
                return PersonData == null ? 0 : PersonData.mMorale;
            }

            set
            {
                PersonData.mMorale = value;
            }
        }

        public T PersonData
        {
            get
            {
                return personData;
            }

            set
            {
                personData = value;
            }
        }
    }
}
