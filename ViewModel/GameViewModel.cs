using System;
using GalaSoft.MvvmLight;
using Newtonsoft.Json.Linq;

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
    public class GameViewModel : ViewModelBase
    {
        private JObject _timeData;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public GameViewModel()
        {
        }

        public void SetModels(JObject timeData)
        {
            _timeData = timeData;
            RaisePropertyChanged(String.Empty);
        }

        public DateTime DateNow
        {
            get
            {
                if (_timeData != null)
                    return DateTime.Parse(_timeData?.GetValue("mNow").ToString());
                return DateTime.Now;
            }
            set
            {

            }
        }

        public float RaceSpeed_Normal
        {
            get
            {
                if (_timeData != null)
                {
                    var speed = _timeData.GetValue("speedMultipliers");
                    return speed == null ? float.NaN : speed.Value<JArray>("a").Value<float>(6);
                }
                return float.NaN;
            }
            set { }
        }
        public float RaceSpeed_Fast
        {
            get
            {
                if (_timeData != null)
                {
                    var speed = _timeData.GetValue("speedMultipliers");
                    return speed == null ? float.NaN : speed.Value<JArray>("a").Value<float>(7);
                }
                return float.NaN;
            }
            set { }
        }
        public float RaceSpeed_Fastest
        {
            get
            {
                if (_timeData != null)
                {
                    var speed = _timeData.GetValue("speedMultipliers");
                    return speed == null ? float.NaN : speed.Value<JArray>("a").Value<float>(8);
                }
                return float.NaN;
            }
            set { }
        }
    }
}