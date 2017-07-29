
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FullSerializer;

[fsObject("v2", new System.Type[] { typeof(PersonalityTraitController) }, MemberSerialization = fsMemberSerialization.OptOut)]
public class PersonalityTraitController_v2
{
    private List<PersonalityTrait> permanentPersonalityTraits = new List<PersonalityTrait>();
    private List<PersonalityTrait> temporaryPersonalityTraits = new List<PersonalityTrait>();
    public List<PersonalityTrait> allTraits = new List<PersonalityTrait>();
    private List<int> mTraitHistory = new List<int>();
    private readonly int mMaxCooldownDaysRange = 180;
    private DateTime cooldownPeriodEnd = new DateTime();
    private DriverStats mDriverStats = new DriverStats();
    private int mLastRandomCooldownDayValue;
    private Driver mDriver;

    public ObservableCollection<PersonalityTrait> PermanentPersonalityTraits
    {
        get
        {
            var traits = new ObservableCollection<PersonalityTrait>(permanentPersonalityTraits);
            traits.CollectionChanged += (sender, args) =>
            {
                permanentPersonalityTraits = traits.ToList();
            };
            return traits;
        }
    }

    public List<PersonalityTrait> TemporaryPersonalityTraits
    {
        get
        {
            return temporaryPersonalityTraits;
        }

        set
        {
            temporaryPersonalityTraits = value;
        }
    }

    public PersonalityTraitController_v2(PersonalityTraitController inOldController)
    {
        this.permanentPersonalityTraits = inOldController.permanentPersonalityTraits;
        this.temporaryPersonalityTraits = inOldController.temporaryPersonalityTraits;
        this.mTraitHistory = inOldController.mTraitHistory;
        this.mMaxCooldownDaysRange = inOldController.mMaxCooldownDaysRange;
        this.cooldownPeriodEnd = inOldController.cooldownPeriodEnd;
        this.mLastRandomCooldownDayValue = inOldController.mLastRandomCooldownDayValue;
        this.mDriver = inOldController.mDriver;
        this.mDriverStats = inOldController.mDriverStats;
        this.allTraits = new List<PersonalityTrait>();
    }

    public PersonalityTraitController_v2(Driver inDriver)
    {
        this.mDriver = inDriver;
    }

    public PersonalityTraitController_v2()
    {
        this.allTraits = new List<PersonalityTrait>();
    }

    public PersonalityTrait AddPersonalityTrait(PersonalityTraitData inPersonalityTraitData, bool inActivatePersonalityTraitTrigger)
    {
        PersonalityTrait inPersonalityTrait = new PersonalityTrait(inPersonalityTraitData, this.mDriver);
        if (inPersonalityTrait.Data.removesTraits != null)
        {
            for (int index1 = 0; index1 < inPersonalityTrait.Data.removesTraits.Length; ++index1)
            {
                int removesTrait = inPersonalityTrait.Data.removesTraits[index1];
                for (int index2 = 0; index2 < this.permanentPersonalityTraits.Count; ++index2)
                {
                    if (removesTrait == this.permanentPersonalityTraits[index2].Data.ID)
                    {
                        this.RemovePersonalityTrait(this.permanentPersonalityTraits[index2]);
                        --index2;
                    }
                }
                for (int index2 = 0; index2 < this.temporaryPersonalityTraits.Count; ++index2)
                {
                    if (removesTrait == this.temporaryPersonalityTraits[index2].Data.ID)
                    {
                        this.RemovePersonalityTrait(this.temporaryPersonalityTraits[index2]);
                        --index2;
                    }
                }
            }
        }
        if (inPersonalityTraitData.type == PersonalityTraitData.TraitType.Permanent)
            this.permanentPersonalityTraits.Add(inPersonalityTrait);
        else if (inPersonalityTraitData.type == PersonalityTraitData.TraitType.Temporary)
        {
            inPersonalityTrait.SetupTraitEndTime();
            this.temporaryPersonalityTraits.Add(inPersonalityTrait);
        }
        this.allTraits.Add(inPersonalityTrait);
        if (!inPersonalityTraitData.isRepeatable)
            this.mTraitHistory.Add(inPersonalityTraitData.ID);
        this.CheckTraitAppliesModifierPotential(inPersonalityTrait);
        inPersonalityTrait.OnTraitStart();
        if (inActivatePersonalityTraitTrigger)
            this.ActivatePersonalityTraitTrigger(inPersonalityTrait);
        return inPersonalityTrait;
    }

    public void RemovePersonalityTrait(PersonalityTrait inPersonalityTrait)
    {
        if (inPersonalityTrait.Data.type == PersonalityTraitData.TraitType.Permanent)
            this.permanentPersonalityTraits.Remove(inPersonalityTrait);
        else if (inPersonalityTrait.Data.type == PersonalityTraitData.TraitType.Temporary)
            this.temporaryPersonalityTraits.Remove(inPersonalityTrait);
        if (!this.allTraits.Contains(inPersonalityTrait))
            return;
        this.allTraits.Remove(inPersonalityTrait);
    }

    private void CheckTraitAppliesModifierPotential(PersonalityTrait inPersonalityTrait)
    {
        if (!inPersonalityTrait.DoesModifyStat(PersonalityTrait.StatModified.Potential))
            return;
        this.mDriver.UpdateModifiedPotentialValue(inPersonalityTrait.GetSingleModifierForStat(PersonalityTrait.StatModified.Potential));
    }

    public void ActivatePersonalityTraitTrigger(PersonalityTrait inPersonalityTrait)
    {
        //if (Game.Instance.player.IsUnemployed() || ((inPersonalityTrait.Data.shownType == PersonalityTraitData.TriggerShownType.AllDrivers ? 1 : 0) | (!this.mDriver.IsPlayersDriver() ? 0 : (inPersonalityTrait.Data.shownType == PersonalityTraitData.TriggerShownType.PlayerDriverOnly ? 1 : 0))) == 0)
        //    return;
        //Game.Instance.dialogSystem.OnNewPersonalityTrait(this.mDriver, inPersonalityTrait);
    }

}
