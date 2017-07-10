using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;

namespace MMSaveEditor.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class PersonViewModel : ViewModelBase
    {
        protected Player _personData;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public PersonViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        public void SetModel(Player personData)
        {
            _personData = personData;
            RaisePropertyChanged(String.Empty);
        }

        public string FirstName
        {
            get
            {
                return _personData?.firstName;
            }
            set
            {
                _personData.SetName(value, _personData.lastName);
                RaisePropertyChanged(String.Empty);
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
                _personData.SetName(_personData.firstName, value);
                RaisePropertyChanged(String.Empty);
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
                if (_personData != null)
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
                return Enum.GetValues(typeof(Person.Gender)).Cast<Person.Gender>();
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
                if (_personData != null)
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
