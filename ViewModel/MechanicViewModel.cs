
using System.Collections.Generic;
using System.Linq;

namespace MMSaveEditor.ViewModel
{
    public class MechanicViewModel : PersonViewModel<Mechanic>
    {
        public override List<Person> GetPeopleFromTeam(Team t)
        {
            return t.Mechanics.OfType<Person>().ToList();
        }

    }
}
