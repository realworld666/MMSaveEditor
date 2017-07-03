// Decompiled with JetBrains decompiler
// Type: UIStaffScreenDriverWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIStaffScreenDriverWidget : MonoBehaviour
{
  public UIAbilityStars abilityStars;
  public UICharacterPortrait driverPortrait;
  public Flag driverFlag;
  public TextMeshProUGUI driverName;
  public TextMeshProUGUI driverNumber;
  public TextMeshProUGUI chemistryWithLabel;
  public TextMeshProUGUI chemistryLengthLabel;
  public Slider chemistryAmount;
  public UIStaffMechanicBonusIcon bonusOne;
  public UIStaffMechanicBonusIcon bonusTwo;
  public UIMechanicBonusIcon bonusOneBigIcon;
  public UIMechanicBonusIcon bonusTwoBigIcon;
  public GameObject modifiersContainer;
  public GameObject positiveIconModifier;
  public GameObject negativeIconModifier;
  public GameObject relationshipStatus;
  public Button swapDriverButton;
  public TextMeshProUGUI swapButtonLabel;
  public UIStaffScreenDriverWidget otherDriverDetails;
  public UIPastDriverChemistryWidget mechanicsHistoryDriverChemistry;
  [SerializeField]
  private GameObject mDriverDetails;
  [SerializeField]
  private GameObject mNoData;
  private Mechanic mMechanic;
  private Driver mDriver;

  public void SetupForDriver(Mechanic inMechanic)
  {
    this.mMechanic = inMechanic;
    bool flag = !(App.instance.gameStateManager.currentState is FrontendState);
    GameUtility.SetActive(this.swapDriverButton.gameObject, this.mMechanic.contract.GetTeam().IsPlayersTeam());
    if (this.swapDriverButton.gameObject.activeSelf)
      this.swapDriverButton.interactable = !flag;
    this.swapDriverButton.onClick.RemoveAllListeners();
    this.swapDriverButton.onClick.AddListener(new UnityAction(this.OnSwapDriver));
    this.UpdateDetails();
  }

  public void UpdateDetails()
  {
    this.mDriver = !this.mMechanic.IsFreeAgent() ? this.mMechanic.contract.GetTeam().GetDriver(this.mMechanic.driver) : (Driver) null;
    this.SetupDriverDetails();
    this.SetupPastDriverRelationships();
    this.SetupChemistryDetails();
    this.SetupDriverSwap();
  }

  private void SetupDriverDetails()
  {
    if ((UnityEngine.Object) this.mDriverDetails != (UnityEngine.Object) null)
      GameUtility.SetActive(this.mDriverDetails, this.mDriver != null);
    if ((UnityEngine.Object) this.mNoData != (UnityEngine.Object) null)
      GameUtility.SetActive(this.mNoData, this.mDriver == null);
    if (this.mDriver != null)
    {
      this.driverName.text = this.mDriver.name;
      this.driverNumber.text = Localisation.LocaliseEnum((Enum) this.mDriver.contract.proposedStatus);
      this.abilityStars.SetAbilityStarsData(this.mDriver);
      this.driverPortrait.SetPortrait((Person) this.mDriver);
      this.driverFlag.SetNationality(this.mDriver.nationality);
    }
    GameUtility.SetActive(this.modifiersContainer, this.mDriver != null && this.mDriver.CanShowStats() && (this.mDriver.personalityTraitController.IsModifingStat(PersonalityTrait.StatModified.MechanicRelationship) || this.mMechanic.currentDriverRelationshipModificationHistory.historyEntryCount > 0));
  }

  private void SetupPastDriverRelationships()
  {
    if (!((UnityEngine.Object) this.mechanicsHistoryDriverChemistry != (UnityEngine.Object) null))
      return;
    this.mechanicsHistoryDriverChemistry.Setup(this.mMechanic);
  }

  private void SetupChemistryDetails()
  {
    this.bonusOne.Setup(this.mMechanic.IsBonusLevelUnlocked(1), this.mMechanic.bonusOne);
    this.bonusTwo.Setup(this.mMechanic.IsBonusLevelUnlocked(2), this.mMechanic.bonusTwo);
    this.bonusOneBigIcon.Setup(this.mMechanic.IsBonusLevelUnlocked(1), this.mMechanic.bonusOne);
    this.bonusTwoBigIcon.Setup(this.mMechanic.IsBonusLevelUnlocked(2), this.mMechanic.bonusTwo);
    if (!this.mMechanic.IsFreeAgent())
    {
      GameUtility.SetActive(this.relationshipStatus, true);
      StringVariableParser.mechanicRelationshipDriver = this.mDriver;
      this.chemistryWithLabel.text = Localisation.LocaliseID("PSG_10010185", (GameObject) null);
      StringVariableParser.mechanicRelationshipWeeks = this.mMechanic.CalculateCurrentRelationshipWithDriverForUI().numberOfWeeks;
      this.chemistryLengthLabel.text = Localisation.LocaliseID("PSG_10010186", (GameObject) null);
      this.chemistryAmount.value = this.mMechanic.CalculateCurrentRelationshipWithDriverForUI().relationshipAmount / 100f;
      float singleModifierForStat = this.mDriver.personalityTraitController.GetSingleModifierForStat(PersonalityTrait.StatModified.MechanicRelationship);
      GameUtility.SetActive(this.positiveIconModifier, (double) singleModifierForStat > 0.0);
      GameUtility.SetActive(this.negativeIconModifier, (double) singleModifierForStat < 0.0);
    }
    else
    {
      GameUtility.SetActive(this.relationshipStatus, false);
      this.chemistryWithLabel.text = string.Empty;
      this.chemistryLengthLabel.text = string.Empty;
      this.chemistryAmount.value = 0.0f;
      GameUtility.SetActive(this.positiveIconModifier, false);
      GameUtility.SetActive(this.negativeIconModifier, false);
    }
  }

  private void SetupDriverSwap()
  {
    if (!((UnityEngine.Object) this.swapButtonLabel != (UnityEngine.Object) null))
      return;
    StringVariableParser.otherDriver = this.mMechanic.contract.GetTeam().GetDriver(this.mMechanic.driver != 0 ? 0 : 1);
    StringVariableParser.subject = (Person) StringVariableParser.otherDriver;
    this.swapButtonLabel.text = Localisation.LocaliseID("PSG_10010187", (GameObject) null);
    StringVariableParser.subject = (Person) null;
  }

  private void OnSwapDriver()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mMechanic.contract.GetTeam().contractManager.SwapMechanicForDriver(this.mMechanic, this.mMechanic.driver != 0 ? 0 : 1);
    this.UpdateDetails();
    if (!((UnityEngine.Object) this.otherDriverDetails != (UnityEngine.Object) null))
      return;
    this.otherDriverDetails.UpdateDetails();
  }

  public void OnMouseEnter()
  {
    Driver driver = this.mMechanic.contract.GetTeam().GetDriver(this.mMechanic.driver);
    bool flag = driver.personalityTraitController.IsModifingStat(PersonalityTrait.StatModified.MechanicRelationship) || this.mMechanic.currentDriverRelationshipModificationHistory.historyEntryCount > 0;
    if (!driver.CanShowStats() || !flag)
      return;
    UIManager.instance.dialogBoxManager.GetDialog<DriverStatsModifiersRollover>().ShowRollover(this.chemistryAmount.value, driver, PersonalityTrait.StatModified.MechanicRelationship, string.Empty, false);
  }

  public void OnMouseExit()
  {
    UIManager.instance.dialogBoxManager.GetDialog<DriverStatsModifiersRollover>().HideRollover();
  }
}
