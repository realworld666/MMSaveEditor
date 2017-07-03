// Decompiled with JetBrains decompiler
// Type: PromotePopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using MM2;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PromotePopup : UIDialogBox
{
  public UIPromotePopupStaffOption info;
  public UIPromotePopupStaffOption[] options;
  public ToggleGroup toggleGroup;
  private Person mPersonToHire;
  private Person mPersonToFire;
  private Person[] mPersonOptions;

  public void Setup(Person inPersonToHire)
  {
    this.mPersonToHire = inPersonToHire;
    this.okButton.onClick.RemoveAllListeners();
    this.okButton.onClick.AddListener(new UnityAction(this.OnOkButtonClick));
    this.info.Setup(this.mPersonToHire as Driver);
    this.SetupDriversOptions();
    this.SetupOptions();
  }

  public void ShowTooltip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<ContractNegotiationRollover>().ShowRollover(this.mPersonToHire);
  }

  public void HideTooltip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<ContractNegotiationRollover>().HideRollover();
  }

  private void SetupDriversOptions()
  {
    List<Person> allPeopleOnJob = Game.instance.player.team.contractManager.GetAllPeopleOnJob(Contract.Job.Driver);
    if (allPeopleOnJob.Count == 3)
      allPeopleOnJob.RemoveAt(allPeopleOnJob.Count - 1);
    this.mPersonOptions = allPeopleOnJob.ToArray();
  }

  private void SetupOptions()
  {
    this.toggleGroup.SetAllTogglesOff();
    for (int index = 0; index < this.options.Length; ++index)
    {
      UIPromotePopupStaffOption option = this.options[index];
      GameUtility.SetActive(option.gameObject, index < this.mPersonOptions.Length);
      if (option.gameObject.activeSelf)
        option.Setup(this.mPersonOptions[index] as Driver);
    }
    this.mPersonToFire = this.options[0].person;
    this.options[0].toggle.isOn = true;
  }

  public void SelectPersonToReplace(Person inPerson)
  {
    this.mPersonToFire = inPerson;
  }

  private void OnOkButtonClick()
  {
    this.ConfirmPromoteDriver();
    this.UpdateDriverPromotedAchievements();
    this.RefreshScreen();
    this.Hide();
  }

  private void ConfirmPromoteDriver()
  {
    Game.instance.player.team.contractManager.PromoteDriver(this.mPersonToFire, this.mPersonToHire);
    StringVariableParser.subject = this.mPersonToHire;
    FeedbackPopup.Open(Localisation.LocaliseID("PSG_10011101", (GameObject) null), Localisation.LocaliseID("PSG_10011101", (GameObject) null));
  }

  private void RefreshScreen()
  {
    if (!UIManager.instance.IsScreenOpen("AllDriversScreen"))
      UIManager.instance.ChangeScreen("AllDriversScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    else
      UIManager.instance.RefreshCurrentPage();
  }

  private void UpdateDriverPromotedAchievements()
  {
    App.instance.steamAchievementsManager.UnlockAchievement(Achievements.AchievementEnum.Promote_Reserve_Driver);
  }
}
