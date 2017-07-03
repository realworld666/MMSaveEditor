// Decompiled with JetBrains decompiler
// Type: UIChemistryWithMech
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIChemistryWithMech : MonoBehaviour
{
  public TextMeshProUGUI mechanicName;
  public TextMeshProUGUI weeksTogether;
  public Flag flag;
  public UIAbilityStars mechanicStars;
  public UICharacterPortrait mechanicPortrait;
  public Slider chemistry;
  public UIStaffMechanicBonusIcon bonusOne;
  public UIStaffMechanicBonusIcon bonusTwo;
  public GameObject chemistryWithMechanicObject;
  public GameObject noMechanicObject;
  public TextMeshProUGUI noMechanicLabel;
  public GameObject positiveIconModifier;
  public GameObject negativeIconModifier;
  private Mechanic mMechanic;

  public void SetupForMechanic(Driver inDriver, Mechanic inMechanic)
  {
    if (inDriver.IsFreeAgent())
    {
      GameUtility.SetActive(this.chemistryWithMechanicObject, false);
      GameUtility.SetActive(this.noMechanicObject, true);
      StringVariableParser.subject = (Person) inDriver;
      this.noMechanicLabel.text = Localisation.LocaliseID("PSG_10008588", (GameObject) null);
      StringVariableParser.subject = (Person) null;
    }
    else if (inDriver.IsReserveDriver())
    {
      GameUtility.SetActive(this.chemistryWithMechanicObject, false);
      GameUtility.SetActive(this.noMechanicObject, true);
      StringVariableParser.subject = (Person) inDriver;
      this.noMechanicLabel.text = Localisation.LocaliseID("PSG_10001585", (GameObject) null);
      StringVariableParser.subject = (Person) null;
    }
    else
    {
      GameUtility.SetActive(this.chemistryWithMechanicObject, true);
      GameUtility.SetActive(this.noMechanicObject, false);
      this.mMechanic = inMechanic;
      this.SetupMechanicDetails();
      this.SetupChemistryDetails();
      float singleModifierForStat = inDriver.personalityTraitController.GetSingleModifierForStat(PersonalityTrait.StatModified.MechanicRelationship);
      GameUtility.SetActive(this.positiveIconModifier, (double) singleModifierForStat > 0.0);
      GameUtility.SetActive(this.negativeIconModifier, (double) singleModifierForStat < 0.0);
    }
  }

  private void SetupMechanicDetails()
  {
    this.mechanicName.text = this.mMechanic.shortName;
    this.flag.SetNationality(this.mMechanic.nationality);
    this.mechanicStars.SetAbilityStarsData((Person) this.mMechanic);
    this.mechanicPortrait.SetPortrait((Person) this.mMechanic);
  }

  private void SetupChemistryDetails()
  {
    bool flag = this.mMechanic.IsFreeAgent();
    GameUtility.SetActive(this.chemistry.gameObject, !flag);
    GameUtility.SetActive(this.bonusOne.gameObject, !flag);
    GameUtility.SetActive(this.bonusTwo.gameObject, !flag);
    if (!this.mMechanic.IsFreeAgent())
    {
      StringVariableParser.intValue1 = this.mMechanic.CalculateCurrentRelationshipWithDriverForUI().numberOfWeeks;
      this.weeksTogether.text = Localisation.LocaliseID("PSG_10010664", (GameObject) null);
      this.chemistry.value = this.mMechanic.CalculateCurrentRelationshipWithDriverForUI().relationshipAmount / 100f;
      this.bonusOne.Setup(this.mMechanic.IsBonusLevelUnlocked(1), this.mMechanic.bonusOne);
      this.bonusTwo.Setup(this.mMechanic.IsBonusLevelUnlocked(2), this.mMechanic.bonusTwo);
    }
    else
      this.weeksTogether.text = "-";
  }

  public void OnMouseEnter()
  {
    Driver driver = this.mMechanic.contract.GetTeam().GetDriver(this.mMechanic.driver);
    bool flag = driver.personalityTraitController.IsModifingStat(PersonalityTrait.StatModified.MechanicRelationship) || this.mMechanic.currentDriverRelationshipModificationHistory.historyEntryCount > 0;
    if (!driver.CanShowStats() || !flag)
      return;
    UIManager.instance.dialogBoxManager.GetDialog<DriverStatsModifiersRollover>().ShowRollover(this.chemistry.normalizedValue, driver, PersonalityTrait.StatModified.MechanicRelationship, string.Empty, false);
  }

  public void OnMouseExit()
  {
    UIManager.instance.dialogBoxManager.GetDialog<DriverStatsModifiersRollover>().HideRollover();
  }
}
