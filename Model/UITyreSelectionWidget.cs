// Decompiled with JetBrains decompiler
// Type: UITyreSelectionWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UITyreSelectionWidget : UIBaseTimerWidget
{
  public GameObject fixedTyreOption;
  public GameObject tyreSelectOption;
  public UITyreWearIcon currentTyre;
  public UITyreWearIcon targetTyre;
  public UITyreWearIcon lockedTyre;
  public UITyreWearIcon[] slickTyreIcons;
  public UITyreWearIcon[] intermediateTyreIcons;
  public UITyreWearIcon[] wetTyreIcons;
  private RacingVehicle mVehicle;

  public void SetVehicle(RacingVehicle inVehicle, PitScreen.Mode inMode)
  {
    this.mVehicle = inVehicle;
    ChampionshipRules rules = Game.instance.sessionManager.championship.rules;
    RaceEventDetails eventDetails = Game.instance.sessionManager.eventDetails;
    SessionStrategy strategy = this.mVehicle.strategy;
    bool inIsActive = eventDetails.currentSession.sessionType == SessionDetails.SessionType.Race && inMode == PitScreen.Mode.PreSession && inVehicle.strategy.lockedTyres != null;
    GameUtility.SetActive(this.fixedTyreOption, inIsActive);
    GameUtility.SetActive(this.tyreSelectOption, !inIsActive);
    if (inIsActive)
    {
      this.lockedTyre.SetTyreSet(this.mVehicle.setup.tyreSet, this.mVehicle.bonuses.bonusDisplayInfo);
      this.lockedTyre.UpdateTyreLocking(this.mVehicle, false);
      this.lockedTyre.toggle.interactable = false;
    }
    else
    {
      int tyreCount1 = strategy.GetTyreCount(SessionStrategy.TyreOption.First);
      int tyreCount2 = strategy.GetTyreCount(SessionStrategy.TyreOption.Second);
      int num = 0;
      if (rules.compoundsAvailable > 2)
        num = strategy.GetTyreCount(SessionStrategy.TyreOption.Third);
      bool inAllowLocking = eventDetails.currentSession.sessionType == SessionDetails.SessionType.Qualifying;
      for (int inIndex = 0; inIndex < this.slickTyreIcons.Length; ++inIndex)
      {
        if (inIndex < tyreCount1)
        {
          TyreSet tyre = strategy.GetTyre(SessionStrategy.TyreOption.First, inIndex);
          this.slickTyreIcons[inIndex].SetTyreSet(tyre, this.mVehicle.bonuses.bonusDisplayInfo);
          this.slickTyreIcons[inIndex].UpdateTyreLocking(this.mVehicle, inAllowLocking);
        }
        else if (inIndex - tyreCount1 < tyreCount2)
        {
          TyreSet tyre = strategy.GetTyre(SessionStrategy.TyreOption.Second, inIndex - tyreCount1);
          this.slickTyreIcons[inIndex].SetTyreSet(tyre, this.mVehicle.bonuses.bonusDisplayInfo);
          this.slickTyreIcons[inIndex].UpdateTyreLocking(this.mVehicle, inAllowLocking);
        }
        else if (inIndex - (tyreCount1 + tyreCount2) < num)
        {
          TyreSet tyre = strategy.GetTyre(SessionStrategy.TyreOption.Third, inIndex - (tyreCount1 + tyreCount2));
          this.slickTyreIcons[inIndex].SetTyreSet(tyre, this.mVehicle.bonuses.bonusDisplayInfo);
          this.slickTyreIcons[inIndex].UpdateTyreLocking(this.mVehicle, inAllowLocking);
        }
        else
          this.slickTyreIcons[inIndex].transform.parent.gameObject.SetActive(false);
      }
      int tyreCount3 = strategy.GetTyreCount(SessionStrategy.TyreOption.Intermediates);
      int tyreCount4 = strategy.GetTyreCount(SessionStrategy.TyreOption.Wets);
      for (int inIndex = 0; inIndex < this.intermediateTyreIcons.Length; ++inIndex)
      {
        if (inIndex < tyreCount3)
        {
          TyreSet tyre = strategy.GetTyre(SessionStrategy.TyreOption.Intermediates, inIndex);
          this.intermediateTyreIcons[inIndex].SetTyreSet(tyre, this.mVehicle.bonuses.bonusDisplayInfo);
          this.intermediateTyreIcons[inIndex].UpdateTyreLocking(this.mVehicle, inAllowLocking);
        }
        else
          this.intermediateTyreIcons[inIndex].transform.parent.gameObject.SetActive(false);
      }
      for (int inIndex = 0; inIndex < this.wetTyreIcons.Length; ++inIndex)
      {
        if (inIndex < tyreCount4)
        {
          TyreSet tyre = strategy.GetTyre(SessionStrategy.TyreOption.Wets, inIndex);
          this.wetTyreIcons[inIndex].SetTyreSet(tyre, this.mVehicle.bonuses.bonusDisplayInfo);
          this.wetTyreIcons[inIndex].UpdateTyreLocking(this.mVehicle, inAllowLocking);
        }
        else
          this.wetTyreIcons[inIndex].transform.parent.gameObject.SetActive(false);
      }
      this.SelectTyre(this.mVehicle.setup.tyreSet);
      this.currentTyre.SetTyreSet(this.mVehicle.setup.tyreSet, this.mVehicle.bonuses.bonusDisplayInfo);
      GameUtility.SetActive(this.targetTyre.gameObject, false);
    }
  }

  public void SelectTyre(TyreSet inTyreSet)
  {
    this.mVehicle.setup.SetTargetTyres(inTyreSet);
    this.RefreshTime();
    this.SetTyreIconTogglesAndLabels();
    this.targetTyre.SetTyreSet(inTyreSet, this.mVehicle.bonuses.bonusDisplayInfo);
    GameUtility.SetActive(this.targetTyre.gameObject, inTyreSet != this.mVehicle.setup.tyreSet);
    UIManager.instance.GetScreen<PitScreen>().OnTyreSelectionChanged(inTyreSet);
  }

  public void SetTyreIconTogglesAndLabels()
  {
    TyreSet tyreSet1 = this.mVehicle.setup.targetSetup.tyreSet;
    TyreSet tyreSet2 = this.mVehicle.setup.tyreSet;
    for (int index = 0; index < this.slickTyreIcons.Length; ++index)
    {
      this.slickTyreIcons[index].toggle.isOn = this.slickTyreIcons[index].tyreSet == tyreSet1;
      GameUtility.SetActive(this.slickTyreIcons[index].currentTyreIcon, this.slickTyreIcons[index].tyreSet == tyreSet2);
      GameUtility.SetActive(this.slickTyreIcons[index].newSelectionTyreIcon, this.slickTyreIcons[index].tyreSet == tyreSet1 && tyreSet1 != tyreSet2);
    }
    for (int index = 0; index < this.intermediateTyreIcons.Length; ++index)
    {
      this.intermediateTyreIcons[index].toggle.isOn = this.intermediateTyreIcons[index].tyreSet == tyreSet1;
      GameUtility.SetActive(this.intermediateTyreIcons[index].currentTyreIcon, this.intermediateTyreIcons[index].tyreSet == tyreSet2);
      GameUtility.SetActive(this.intermediateTyreIcons[index].newSelectionTyreIcon, this.intermediateTyreIcons[index].tyreSet == tyreSet1 && tyreSet1 != tyreSet2);
    }
    for (int index = 0; index < this.wetTyreIcons.Length; ++index)
    {
      this.wetTyreIcons[index].toggle.isOn = this.wetTyreIcons[index].tyreSet == tyreSet1;
      GameUtility.SetActive(this.wetTyreIcons[index].currentTyreIcon, this.wetTyreIcons[index].tyreSet == tyreSet2);
      GameUtility.SetActive(this.wetTyreIcons[index].newSelectionTyreIcon, this.wetTyreIcons[index].tyreSet == tyreSet1 && tyreSet1 != tyreSet2);
    }
  }

  public override void RefreshTime()
  {
    this.SetTimeEstimate(this.mVehicle.setup.GetTyreTimeImpact(), this.mVehicle.setup.IsOnTheCriticalPath(SessionSetup.PitCrewSizeDependentSteps.Tyres));
  }
}
