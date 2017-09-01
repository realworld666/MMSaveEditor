using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSaveEditor.ViewModel
{
    public class EngineerViewModel : PersonViewModel<Engineer>
    {

        public override List<Person> GetPeopleFromTeam(Team t)
        {
            return Game.instance?.engineerManager?.GetEntityList().OfType<Person>().Where(e => e.Contract.GetTeam() == t).ToList();
        }
    }
}
