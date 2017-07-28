using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSaveEditor.ViewModel
{
    public class DriverViewModel : PersonViewModel<Driver>
    {
        public List<PersonalityTraitData> AllTraits
        {
            get
            {
                return Game.Instance.personalityTraitManager.personalityTraits.Select(d => d.Value).ToList();
            }
        }
    }
}
