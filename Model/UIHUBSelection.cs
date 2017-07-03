// Decompiled with JetBrains decompiler
// Type: UIHUBSelection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHUBSelection : MonoBehaviour
{
  private List<UIHUBStep> mSteps = new List<UIHUBStep>();
  private SessionDetails.SessionType mSessionType = SessionDetails.SessionType.Race;
  public UIHUBDrivers driversWidget;
  public UIHUBStep selectDrivers;
  public UIHUBStep selectBonuses;
  public UIHUBStep driverStrategy;
  public UIHUBStep[] options;
  public TextMeshProUGUI[] driverNamesFirst;
  public TextMeshProUGUI[] driverNamesSecond;
  private RacingVehicle[] mVehicles;
  private Team mTeam;
  private SessionDetails mSessionDetails;
  private UIHUBStep mLastStep;

  public Team team
  {
    get
    {
      return this.mTeam;
    }
  }

  public RacingVehicle[] vehicles
  {
    get
    {
      return this.mVehicles;
    }
  }

  public List<UIHUBStep> activeSteps
  {
    get
    {
      return this.mSteps;
    }
  }

  public void OnStart()
  {
    this.selectDrivers.OnStart();
    this.selectBonuses.OnStart();
    this.driverStrategy.OnStart();
    for (int index = 0; index < this.options.Length; ++index)
      this.options[index].OnStart();
  }

  public void Setup()
  {
    SessionDetails currentSession = Game.instance.sessionManager.eventDetails.currentSession;
    if (this.mSessionDetails != currentSession || this.mTeam != Game.instance.player.team)
    {
      this.mLastStep = (UIHUBStep) null;
      this.mSessionDetails = currentSession;
      this.mSessionType = currentSession.sessionType;
      this.mTeam = Game.instance.player.team;
      this.mVehicles = Game.instance.vehicleManager.GetVehiclesByTeam(this.mTeam);
      this.mSteps.Clear();
      GameUtility.SetActive(this.selectDrivers.gameObject, this.mSessionType == SessionDetails.SessionType.Practice);
      GameUtility.SetActive(this.selectDrivers.option.gameObject, false);
      GameUtility.SetActive(this.selectBonuses.gameObject, this.mSessionType != SessionDetails.SessionType.Practice);
      GameUtility.SetActive(this.selectBonuses.option.gameObject, false);
      GameUtility.SetActive(this.driverStrategy.gameObject, this.mSessionType == SessionDetails.SessionType.Race);
      GameUtility.SetActive(this.driverStrategy.option.gameObject, false);
      for (int index = 0; index < this.options.Length; ++index)
        GameUtility.SetActive(this.options[index].option.gameObject, false);
      if (this.mSessionType == SessionDetails.SessionType.Practice)
      {
        this.selectDrivers.Setup();
        this.mSteps.Add(this.selectDrivers);
      }
      else
      {
        this.selectBonuses.Setup();
        this.mSteps.Add(this.selectBonuses);
      }
      for (int index = 0; index < this.options.Length; ++index)
      {
        this.options[index].Setup();
        this.mSteps.Add(this.options[index]);
      }
      if (this.mSessionType == SessionDetails.SessionType.Race)
      {
        this.driverStrategy.Setup();
        this.mSteps.Add(this.driverStrategy);
      }
      this.GoToFirstStep();
    }
    this.driversWidget.Setup();
  }

  public void SetDriverName(Driver inDriver, bool FirstDriver)
  {
    string empty = string.Empty;
    string str = inDriver != null ? inDriver.name : (!FirstDriver ? Localisation.LocaliseID("PSG_10001621", (GameObject) null) : Localisation.LocaliseID("PSG_10001620", (GameObject) null));
    TextMeshProUGUI[] textMeshProUguiArray = !FirstDriver ? this.driverNamesSecond : this.driverNamesFirst;
    int length = textMeshProUguiArray.Length;
    for (int index = 0; index < length; ++index)
      textMeshProUguiArray[index].text = str;
  }

  public void GoToNextStep()
  {
    int count = this.mSteps.Count;
    for (int index = 0; index < count; ++index)
    {
      UIHUBStep mStep = this.mSteps[index];
      if (!mStep.IsComplete() || index == count - 1)
      {
        mStep.toggle.isOn = true;
        mStep.OnToggle();
        if ((Object) this.mLastStep == (Object) mStep)
          this.HighlightSelection();
        this.mLastStep = mStep;
        break;
      }
    }
  }

  public void GoToFirstStep()
  {
    if (this.mSteps.Count <= 0)
      return;
    UIHUBStep mStep = this.mSteps[0];
    mStep.toggle.isOn = true;
    mStep.OnToggle();
  }

  public void GoToStep(UIHUBStep.Step inStep)
  {
    int count = this.mSteps.Count;
    for (int index = 0; index < count; ++index)
    {
      UIHUBStep mStep = this.mSteps[index];
      if (mStep.step == inStep)
      {
        mStep.toggle.isOn = true;
        mStep.OnToggle();
        break;
      }
    }
  }

  public UIHUBStep GetStep(UIHUBStep.Step inStep)
  {
    int count = this.mSteps.Count;
    for (int index = 0; index < count; ++index)
    {
      UIHUBStep mStep = this.mSteps[index];
      if (mStep.step == inStep)
        return mStep;
    }
    return (UIHUBStep) null;
  }

  public void HighlightSelection()
  {
    if (!((Object) this.mLastStep != (Object) null))
      return;
    this.mLastStep.AnimateHighlight();
  }

  public bool isComplete()
  {
    bool flag = true;
    int count = this.mSteps.Count;
    for (int index = 0; index < count; ++index)
    {
      if (!this.mSteps[index].IsComplete())
        return false;
    }
    return flag;
  }
}
