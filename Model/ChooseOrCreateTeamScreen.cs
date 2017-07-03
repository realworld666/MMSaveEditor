// Decompiled with JetBrains decompiler
// Type: ChooseOrCreateTeamScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using Steamworks;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChooseOrCreateTeamScreen : UIScreen
{
  private static uint createTeamDlcAppId = DLCManager.GetDlcByName("Create Team").appId;
  public Toggle chooseTeamToggle;
  public Toggle createTeamToggle;
  public GameObject createTeamLock;
  public Button createTeamGetContentButton;

  public override void OnStart()
  {
    base.OnStart();
    this.createTeamGetContentButton.onClick.AddListener(new UnityAction(this.OnCreateTeamGetContent));
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.SetTopBarMode(UITopBar.Mode.Core);
    this.SetBottomBarMode(UIBottomBar.Mode.Core);
    this.UpdateToggles();
    App.instance.dlcManager.OnOwnedDlcChanged += new Action(this.UpdateToggles);
  }

  public override void OnExit()
  {
    App.instance.dlcManager.OnOwnedDlcChanged -= new Action(this.UpdateToggles);
    base.OnExit();
  }

  private void UpdateToggles()
  {
    GameUtility.SetActive(this.createTeamLock, (!App.instance.dlcManager.IsDlcKnown(ChooseOrCreateTeamScreen.createTeamDlcAppId) ? 0 : (App.instance.dlcManager.IsDlcInstalled(ChooseOrCreateTeamScreen.createTeamDlcAppId) ? 1 : 0)) == 0);
    this.createTeamToggle.interactable = !this.createTeamLock.activeSelf;
    if (!this.createTeamLock.activeSelf && (this.chooseTeamToggle.isOn || this.createTeamToggle.isOn))
      return;
    this.chooseTeamToggle.isOn = true;
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    if (this.chooseTeamToggle.isOn)
    {
      CreateTeamManager.Reset();
      UIManager.instance.ChangeScreen("ChooseSeriesScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    }
    else
    {
      CreateTeamManager.StartCreateNewTeam();
      UIManager.instance.ChangeScreen("ChooseSeriesScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    }
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }

  private void OnCreateTeamGetContent()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    SteamFriends.ActivateGameOverlayToStore(new AppId_t(DLCManager.GetDlcByName("Create Team").appId), EOverlayToStoreFlag.k_EOverlayToStoreFlag_None);
  }
}
