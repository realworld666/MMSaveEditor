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

        public List<CarPartComponent> AllComponentsLevel1
        {
            get
            {
                return CarPartComponentsManager.Instance.GetComponentsOfLevel(1, CarPartComponent.ComponentType.Engineer);
            }
        }

        public CarPartComponent ComponentLevel1
        {
            get
            {
                if (PersonData != null)
                {
                    CarPartComponent myComponent = PersonData.availableComponents.FirstOrDefault(c => c.level == 1);
                    if (myComponent != null)
                    {
                        return AllComponentsLevel1.FirstOrDefault(c => c.id == myComponent.id);
                    }
                }
                return null;
            }
            set
            {
                PersonData.availableComponents.RemoveAll(c => c.level == 1);
                PersonData.availableComponents.Add(value);
            }
        }

        public List<CarPartComponent> AllComponentsLevel2
        {
            get
            {
                return CarPartComponentsManager.Instance.GetComponentsOfLevel(2, CarPartComponent.ComponentType.Engineer);
            }
        }

        public CarPartComponent ComponentLevel2
        {
            get
            {
                if (PersonData != null)
                {
                    CarPartComponent myComponent = PersonData.availableComponents.FirstOrDefault(c => c.level == 2);
                    if (myComponent != null)
                    {
                        return AllComponentsLevel2.FirstOrDefault(c => c.id == myComponent.id);
                    }
                }
                return null;
            }
            set
            {
                PersonData.availableComponents.RemoveAll(c => c.level == 2);
                PersonData.availableComponents.Add(value);
            }
        }

        public List<CarPartComponent> AllComponentsLevel3
        {
            get
            {
                return CarPartComponentsManager.Instance.GetComponentsOfLevel(3, CarPartComponent.ComponentType.Engineer);
            }
        }

        public CarPartComponent ComponentLevel3
        {
            get
            {
                if (PersonData != null)
                {
                    CarPartComponent myComponent = PersonData.availableComponents.FirstOrDefault(c => c.level == 3);
                    if (myComponent != null)
                    {
                        return AllComponentsLevel3.FirstOrDefault(c => c.id == myComponent.id);
                    }
                }
                return null;
            }
            set
            {
                PersonData.availableComponents.RemoveAll(c => c.level == 3);
                PersonData.availableComponents.Add(value);
            }
        }

        public List<CarPartComponent> AllComponentsLevel4
        {
            get
            {
                return CarPartComponentsManager.Instance.GetComponentsOfLevel(4, CarPartComponent.ComponentType.Engineer);
            }
        }

        public CarPartComponent ComponentLevel4
        {
            get
            {
                if (PersonData != null)
                {
                    CarPartComponent myComponent = PersonData.availableComponents.FirstOrDefault(c => c.level == 4);
                    if (myComponent != null)
                    {
                        return AllComponentsLevel4.FirstOrDefault(c => c.id == myComponent.id);
                    }
                }
                return null;
            }
            set
            {
                PersonData.availableComponents.RemoveAll(c => c.level == 4);
                PersonData.availableComponents.Add(value);
            }
        }

        public List<CarPartComponent> AllComponentsLevel5
        {
            get
            {
                return CarPartComponentsManager.Instance.GetComponentsOfLevel(5, CarPartComponent.ComponentType.Engineer);
            }
        }

        public CarPartComponent ComponentLevel5
        {
            get
            {
                if (PersonData != null)
                {
                    CarPartComponent myComponent = PersonData.availableComponents.FirstOrDefault(c => c.level == 5);
                    if (myComponent != null)
                    {
                        return AllComponentsLevel5.FirstOrDefault(c => c.id == myComponent.id);
                    }
                }
                return null;
            }
            set
            {
                PersonData.availableComponents.RemoveAll(c => c.level == 5);
                PersonData.availableComponents.Add(value);
            }
        }
    }
}
