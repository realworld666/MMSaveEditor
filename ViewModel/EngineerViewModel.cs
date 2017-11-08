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
        private static List<CarPartComponent> GetAllComponents(int level)
        {
            List<CarPartComponent> result = CarPartComponentsManager.Instance.GetComponentsOfLevel(level, CarPartComponent.ComponentType.Engineer);
            result.Insert(0, null);
            return result;
        }
        private CarPartComponent GetComponentLevel(int level)
        {
            if (PersonData != null)
            {
                CarPartComponent myComponent = PersonData.availableComponents.FirstOrDefault(c => c.level == level);
                if (myComponent != null)
                {
                    return GetAllComponents(level).FirstOrDefault(c => c != null && c.id == myComponent.id);
                }
            }
            return null;
        }
        private void SetComponentLevel(CarPartComponent value, int level)
        {
            if (value != null)
            {
                PersonData.availableComponents.RemoveAll(c => c != null && c.level == level);
                PersonData.availableComponents.Add(value);
            }
        }

        public List<CarPartComponent> AllComponentsLevel1
        {
            get
            {
                return GetAllComponents(1);
            }
        }

        public CarPartComponent ComponentLevel1
        {
            get
            {
                return GetComponentLevel(1);
            }
            set
            {
                SetComponentLevel(value, 1);
            }
        }

        public List<CarPartComponent> AllComponentsLevel2
        {
            get
            {
                return GetAllComponents(2);
            }
        }

        public CarPartComponent ComponentLevel2
        {
            get
            {
                return GetComponentLevel(2);
            }
            set
            {
                SetComponentLevel(value, 2);
            }
        }

        public List<CarPartComponent> AllComponentsLevel3
        {
            get
            {
                return GetAllComponents(3);
            }
        }

        public CarPartComponent ComponentLevel3
        {
            get
            {
                return GetComponentLevel(3);
            }
            set
            {
                SetComponentLevel(value, 3);
            }
        }

        public List<CarPartComponent> AllComponentsLevel4
        {
            get
            {
                return GetAllComponents(4);
            }
        }

        public CarPartComponent ComponentLevel4
        {
            get
            {
                return GetComponentLevel(4);
            }
            set
            {
                SetComponentLevel(value, 4);
            }
        }

        public List<CarPartComponent> AllComponentsLevel5
        {
            get
            {
                return GetAllComponents(5);
            }
        }

        public CarPartComponent ComponentLevel5
        {
            get
            {
                return GetComponentLevel(5);
            }
            set
            {
                SetComponentLevel(value, 5);
            }
        }
    }
}
