
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MMSaveEditor.ViewModel
{
    public class MechanicViewModel : PersonViewModel<Mechanic>
    {
        public override List<Person> GetPeopleFromTeam(Team t)
        {
            return t.Mechanics.OfType<Person>().ToList();
        }

        public Dictionary<string, Mechanic.DriverRelationship> Relationships => PersonData?.allDriverRelationships;


        public MechanicBonus Bonus1
        {
            get => PersonData?.bonusOne;
            set => PersonData.bonusOne = value;
        }

        public MechanicBonus Bonus2
        {
            get => PersonData?.bonusTwo;
            set => PersonData.bonusTwo = value;
        }

        public List<MechanicBonus> AllBonuses => MechanicBonusManager.Instance.mechanicBonuses.Values.ToList();
    }
}
