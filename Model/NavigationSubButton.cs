// Decompiled with JetBrains decompiler
// Type: NavigationSubButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NavigationSubButton : MonoBehaviour
{
  public string goToScreen = string.Empty;
  public string openDialogBox = string.Empty;
  private List<Mechanic> mMechanics = new List<Mechanic>();
  public NavigationSubButton.SpecialOptions options;
  public int driverIdentifier;
  public TextMeshProUGUI driverName;
  private Toggle mToggle;

  private void Awake()
  {
    this.mToggle = this.GetComponent<Toggle>();
    if (!((UnityEngine.Object) this.mToggle != (UnityEngine.Object) null))
      return;
    this.mToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggle));
  }

  private void Update()
  {
    switch (this.options)
    {
      case NavigationSubButton.SpecialOptions.AssignForDrivers:
        if (Game.IsActive())
        {
          Driver driver = (Driver) null;
          if (!Game.instance.player.IsUnemployed())
            driver = Game.instance.player.team.GetDriver(this.driverIdentifier);
          this.driverName.text = driver == null ? "No Driver" : driver.name;
          GameUtility.SetInteractable((Selectable) this.mToggle, driver != null);
          break;
        }
        break;
      case NavigationSubButton.SpecialOptions.AssignHeadDesign:
        if (Game.IsActive())
        {
          Engineer engineer = (Engineer) null;
          if (!Game.instance.player.IsUnemployed())
            engineer = (Engineer) Game.instance.player.team.contractManager.GetPersonOnJob(Contract.Job.EngineerLead);
          this.driverName.text = engineer == null ? "No Head Design" : engineer.name;
          GameUtility.SetInteractable((Selectable) this.mToggle, engineer != null);
          break;
        }
        break;
      case NavigationSubButton.SpecialOptions.AssignMechanic:
        if (Game.IsActive())
        {
          Mechanic mechanic = (Mechanic) null;
          if (!Game.instance.player.IsUnemployed())
          {
            this.mMechanics.Clear();
            Game.instance.player.team.contractManager.GetAllMechanics(ref this.mMechanics);
            mechanic = this.mMechanics[this.driverIdentifier];
          }
          this.driverName.text = mechanic == null ? "No Mechanic" : mechanic.name;
          GameUtility.SetInteractable((Selectable) this.mToggle, mechanic != null);
          break;
        }
        break;
    }
    if (this.mToggle.isOn == this.HighlightIfOnScreen())
      return;
    this.mToggle.onValueChanged.RemoveAllListeners();
    this.mToggle.isOn = this.HighlightIfOnScreen();
    this.mToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggle));
  }

  private bool HighlightIfOnScreen()
  {
    if (!(this.goToScreen == UIManager.instance.currentScreen_screenName) && !(this.goToScreen == UIManager.instance.currentScreen_name))
      return false;
    switch (this.options)
    {
      case NavigationSubButton.SpecialOptions.AssignForDrivers:
        DriverScreen screen1 = UIManager.instance.GetScreen<DriverScreen>();
        Team team1 = screen1.driver.contract.GetTeam();
        return team1.IsPlayersTeam() && screen1.driver == team1.GetDriver(this.driverIdentifier);
      case NavigationSubButton.SpecialOptions.AssignHeadDesign:
        StaffDetailsScreen screen2 = UIManager.instance.GetScreen<StaffDetailsScreen>();
        Team team2 = screen2.person.contract.GetTeam();
        return team2.IsPlayersTeam() && screen2.person == team2.contractManager.GetPersonOnJob(Contract.Job.EngineerLead);
      case NavigationSubButton.SpecialOptions.AssignMechanic:
        StaffDetailsScreen screen3 = UIManager.instance.GetScreen<StaffDetailsScreen>();
        Team team3 = screen3.person.contract.GetTeam();
        this.mMechanics.Clear();
        Game.instance.player.team.contractManager.GetAllMechanics(ref this.mMechanics);
        return team3.IsPlayersTeam() && screen3.person == this.mMechanics[this.driverIdentifier];
      default:
        return true;
    }
  }

  private void OnEnable()
  {
    if (!Game.IsActive())
      return;
    this.Update();
  }

  public void OnToggle(bool inValue)
  {
    if (inValue)
    {
      this.OnButton();
    }
    else
    {
      if (!(UIManager.instance.currentScreen_screenName != this.goToScreen))
        return;
      this.OnButton();
    }
  }

  public void OnButton()
  {
    if (!string.IsNullOrEmpty(this.goToScreen))
    {
      switch (this.options)
      {
        case NavigationSubButton.SpecialOptions.AssignForDrivers:
          UIManager.instance.ChangeScreen(this.goToScreen, (Entity) Game.instance.player.team.GetDriver(this.driverIdentifier), UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
          break;
        case NavigationSubButton.SpecialOptions.AssignHeadDesign:
          UIManager.instance.ChangeScreen(this.goToScreen, (Entity) Game.instance.player.team.contractManager.GetPersonOnJob(Contract.Job.EngineerLead), UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
          break;
        case NavigationSubButton.SpecialOptions.AssignMechanic:
          this.mMechanics.Clear();
          Game.instance.player.team.contractManager.GetAllMechanics(ref this.mMechanics);
          UIManager.instance.ChangeScreen(this.goToScreen, (Entity) this.mMechanics[this.driverIdentifier], UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
          break;
        default:
          UIManager.instance.ChangeScreen(this.goToScreen, UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
          break;
      }
    }
    else
    {
      if (string.IsNullOrEmpty(this.openDialogBox))
        return;
      UIManager.instance.dialogBoxManager.Show(this.openDialogBox);
    }
  }

  public enum SpecialOptions
  {
    None,
    AssignForDrivers,
    AssignHeadDesign,
    AssignMechanic,
  }
}
