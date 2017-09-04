using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSaveEditor.ViewModel
{
    public class ChairmanViewModel : PersonViewModel<Chairman>
    {
        public override List<Person> GetPeopleFromTeam(Team t)
        {
            return new List<Chairman>(new[] { t.chairman }).OfType<Person>().ToList();
        }
    }
}
