// Decompiled with JetBrains decompiler
// Type: UICompareStaffNavigationWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICompareStaffNavigationWidget : MonoBehaviour
{
  public Button leftButton;
  public Button rightButton;
  public TextMeshProUGUI label;
  public UICompareStaffProfileWidget profileWidget;
  public CompareStaffScreen screen;
  private Person mPerson;
  private Team mTeam;
  private Team mOtherTeam;
  private int mNum;
  private int mMin;
  private int mMax;

  public void OnStart()
  {
    this.leftButton.onClick.AddListener(new UnityAction(this.OnLeftButton));
    this.rightButton.onClick.AddListener(new UnityAction(this.OnRightButton));
  }

  public void Setup(Person inPerson, bool inForceUpdate)
  {
    if ((inPerson == null || this.mPerson == inPerson) && !inForceUpdate)
      return;
    this.mPerson = inPerson;
    StringVariableParser.subject = this.mPerson;
    if (this.screen.mode == CompareStaffScreen.Mode.Driver)
    {
      Driver mPerson = (Driver) this.mPerson;
      this.mTeam = mPerson.contract.GetTeam();
      this.mOtherTeam = this.screen.GetTeamOtherPerson(this.profileWidget);
      GameUtility.SetActive(this.leftButton.gameObject, true);
      GameUtility.SetActive(this.rightButton.gameObject, true);
      List<Person> allPeopleOnJob = this.mTeam.contractManager.GetAllPeopleOnJob(Contract.Job.Driver);
      if (!mPerson.IsFreeAgent() && this.mTeam != this.mOtherTeam && allPeopleOnJob.Count > 0 && (this.mTeam.AllDriversScouted() || this.mTeam.IsPlayersTeam()))
      {
        this.mMin = 0;
        this.mMax = allPeopleOnJob.Count - 1;
        this.mNum = Mathf.Clamp(this.GetDriverIndex(mPerson), this.mMin, this.mMax);
        if (this.mNum != 2)
          this.label.text = Localisation.LocaliseID("PSG_10010198", (GameObject) null);
        else
          this.label.text = Localisation.LocaliseID("PSG_10010199", (GameObject) null);
      }
      else
      {
        this.mMin = 0;
        this.mMax = 0;
        this.mNum = 0;
        this.label.text = Localisation.LocaliseID(mPerson.IsFreeAgent() ? "PSG_10010200" : "PSG_10002274", (GameObject) null);
      }
    }
    else if (this.screen.mode == CompareStaffScreen.Mode.Engineer)
    {
      GameUtility.SetActive(this.leftButton.gameObject, false);
      GameUtility.SetActive(this.rightButton.gameObject, false);
      this.mMin = 1;
      this.mMax = 1;
      this.mNum = 1;
      this.label.text = Localisation.LocaliseID("PSG_10004602", (GameObject) null);
    }
    else if (this.screen.mode == CompareStaffScreen.Mode.Mechanic)
    {
      Mechanic mPerson = (Mechanic) this.mPerson;
      this.mTeam = mPerson.contract.GetTeam();
      this.mOtherTeam = this.screen.GetTeamOtherPerson(this.profileWidget);
      GameUtility.SetActive(this.leftButton.gameObject, true);
      GameUtility.SetActive(this.rightButton.gameObject, true);
      List<Person> allPeopleOnJob = this.mTeam.contractManager.GetAllPeopleOnJob(Contract.Job.Mechanic);
      if (this.mTeam != null && !(this.mTeam is NullTeam) && (this.mTeam != this.mOtherTeam && allPeopleOnJob.Count > 0))
      {
        this.mMin = 0;
        this.mMax = allPeopleOnJob.Count - 1;
        this.mNum = Mathf.Clamp(this.GetMechanicIndex(mPerson), this.mMin, this.mMax);
      }
      else
      {
        this.mMin = 0;
        this.mMax = 0;
        this.mNum = 0;
      }
      this.label.text = Localisation.LocaliseID("PSG_10004604", (GameObject) null);
    }
    StringVariableParser.subject = (Person) null;
    this.UpdateButtonsState();
  }

  public int GetDriverIndex(Driver inDriver)
  {
    if (inDriver != null)
    {
      Team team = inDriver.contract.GetTeam();
      if (team != null && !(team is NullTeam))
      {
        for (int inIndex = 0; inIndex < Team.driverCount; ++inIndex)
        {
          Driver driver = team.GetDriver(inIndex);
          if (driver != null && driver == inDriver)
            return inIndex;
        }
      }
    }
    return 0;
  }

  public int GetMechanicIndex(Mechanic inMechanic)
  {
    if (this.mTeam != null)
    {
      List<Person> allPeopleOnJob = this.mTeam.contractManager.GetAllPeopleOnJob(Contract.Job.Mechanic);
      if (allPeopleOnJob.Count > 0)
      {
        for (int index = 0; index < allPeopleOnJob.Count; ++index)
        {
          if (allPeopleOnJob[index] == inMechanic)
            return index;
        }
      }
    }
    return 0;
  }

  public Mechanic GetMechanic(int inIndex)
  {
    Mechanic mechanic = (Mechanic) null;
    if (this.mTeam != null)
    {
      List<Person> allPeopleOnJob = this.mTeam.contractManager.GetAllPeopleOnJob(Contract.Job.Mechanic);
      if (allPeopleOnJob.Count > inIndex)
        mechanic = (Mechanic) allPeopleOnJob[inIndex];
    }
    return mechanic;
  }

  public void UpdatePanel()
  {
    if (this.mPerson == null || this.mTeam == null)
      return;
    if (this.screen.mode == CompareStaffScreen.Mode.Driver)
    {
      Driver driver = this.mTeam.GetDriver(this.mNum);
      if (driver == null)
        return;
      this.profileWidget.Setup((Person) driver);
      this.screen.statsPanel.Setup();
    }
    else
    {
      if (this.screen.mode != CompareStaffScreen.Mode.Mechanic)
        return;
      Mechanic mechanic = this.GetMechanic(this.mNum);
      if (mechanic == null)
        return;
      this.profileWidget.Setup((Person) mechanic);
      this.screen.statsPanel.Setup();
    }
  }

  public void OnLeftButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mNum - 1 < this.mMin)
      return;
    this.mNum = Mathf.Clamp(this.mNum - 1, this.mMin, this.mMax);
    this.UpdatePanel();
    this.UpdateButtonsState();
    this.screen.RefreshLists();
  }

  public void OnRightButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mNum + 1 > this.mMax)
      return;
    this.mNum = Mathf.Clamp(this.mNum + 1, this.mMin, this.mMax);
    this.UpdatePanel();
    this.UpdateButtonsState();
    this.screen.RefreshLists();
  }

  public void UpdateButtonsState()
  {
    this.leftButton.interactable = this.mNum - 1 >= this.mMin;
    this.rightButton.interactable = this.mNum + 1 <= this.mMax;
  }
}
