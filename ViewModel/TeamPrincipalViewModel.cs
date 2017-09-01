using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSaveEditor.ViewModel
{
    public class TeamPrincipalViewModel : TeamPrincipalViewModelBase<TeamPrincipal>
    {

    }
    public abstract class TeamPrincipalViewModelBase<T> : PersonViewModel<T> where T : TeamPrincipal
    {
        public float RaceManagement
        {
            get => PersonData?.stats.raceManagement ?? 0f;
            set => PersonData.stats.raceManagement = value;
        }

        public float Financial
        {
            get => PersonData?.stats.financial ?? 0f;
            set => PersonData.stats.financial = value;
        }

        public float Loyalty
        {
            get => PersonData?.stats.loyalty ?? 0f;
            set => PersonData.stats.loyalty = value;
        }

        public float JobSecurity
        {
            get => PersonData?.stats.jobSecurity ?? 0f;
            set => PersonData.stats.jobSecurity = value;
        }

        public TeamPrincipal.Backstory BackStory
        {
            get => PersonData?.backStory ?? TeamPrincipal.Backstory.None;
            set => PersonData.backStory = value;
        }
        public IEnumerable<TeamPrincipal.Backstory> BackStoryTypes => Enum.GetValues(typeof(TeamPrincipal.Backstory)).Cast<TeamPrincipal.Backstory>();

        public override List<Person> GetPeopleFromTeam(Team t)
        {
            return Game.instance?.teamPrincipalManager?.GetEntityList().OfType<Person>().Where(e => e.Contract.GetTeam() == t).ToList();
        }
    }
}
