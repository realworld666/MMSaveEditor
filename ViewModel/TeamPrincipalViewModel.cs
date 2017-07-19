using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSaveEditor.ViewModel
{
    public class TeamPrincipalViewModel : PersonViewModel<TeamPrincipal>
    {
        public float RaceManagement
        {
            get => _personData?.stats.raceManagement ?? 0f;
            set => _personData.stats.raceManagement = value;
        }

        public float Financial
        {
            get => _personData?.stats.financial ?? 0f;
            set => _personData.stats.financial = value;
        }

        public float Loyalty
        {
            get => _personData?.stats.loyalty ?? 0f;
            set => _personData.stats.loyalty = value;
        }

        public float JobSecurity
        {
            get => _personData?.stats.jobSecurity ?? 0f;
            set => _personData.stats.jobSecurity = value;
        }

        public TeamPrincipal.Backstory BackStory
        {
            get => _personData?.backStory ?? TeamPrincipal.Backstory.None;
            set => _personData.backStory = value;
        }
        public IEnumerable<TeamPrincipal.Backstory> BackStoryTypes => Enum.GetValues(typeof(TeamPrincipal.Backstory)).Cast<TeamPrincipal.Backstory>();
    }
}
