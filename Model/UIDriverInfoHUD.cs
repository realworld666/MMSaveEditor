// Decompiled with JetBrains decompiler
// Type: UIDriverInfoHUD
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class UIDriverInfoHUD : MonoBehaviour
{
  public UIDriverInfo[] playerDriverInfo;
  public UIDriverInfo targetDriverInfo;
  public UIDriverInfo driverAheadDriverInfo;
  public UIDriverInfo driverBehindDriverInfo;
  public UIVehicleInfo safetyCarInfo;
  public UISessionInfo[] sessionInfos;
  private RacingVehicle mCameraTargetVehicle;
  private float mDriverAheadTimer;
  private float mDriverBehindTimer;
  private bool mLastModeRunning2DMode;

  public void OnStart()
  {
    App.instance.cameraManager.OnTargetChange += new Action(this.OnCameraTargetChange);
  }

  public void OnUnload()
  {
    App.instance.cameraManager.OnTargetChange -= new Action(this.OnCameraTargetChange);
  }

  public void ActivateDebug(bool inValue)
  {
    for (int index = 0; index < this.playerDriverInfo.Length; ++index)
      this.playerDriverInfo[index].driverDebugInfo.ActivateDebug(inValue);
    this.targetDriverInfo.driverDebugInfo.ActivateDebug(inValue);
    this.driverAheadDriverInfo.driverDebugInfo.ActivateDebug(inValue);
    this.driverBehindDriverInfo.driverDebugInfo.ActivateDebug(inValue);
  }

  private void OnEnable()
  {
    if (Game.IsActive())
    {
      Team team = Game.instance.player.team;
      for (int inIndex = 0; inIndex < this.playerDriverInfo.Length; ++inIndex)
      {
        Driver selectedDriver = team.GetSelectedDriver(inIndex);
        RacingVehicle vehicle = Game.instance.vehicleManager.GetVehicle(selectedDriver);
        this.playerDriverInfo[inIndex].Show(vehicle);
      }
      this.OnCameraTargetChange();
      if ((UnityEngine.Object) Simulation2D.instance != (UnityEngine.Object) null)
        Simulation2D.instance.camera2D.OnLateUpdate += new Action(this.UpdateInfoPositions);
    }
    App.instance.cameraManager.gameCamera.freeRoamCamera.OnLateUpdate += new Action(this.UpdateInfoPositions);
    this.mDriverAheadTimer = 0.0f;
    this.mDriverBehindTimer = 0.0f;
  }

  private void OnDisable()
  {
    if (Game.IsActive())
    {
      this.playerDriverInfo[0].HideAndClearVehicle();
      this.playerDriverInfo[1].HideAndClearVehicle();
      this.targetDriverInfo.HideAndClearVehicle();
      this.driverAheadDriverInfo.HideAndClearVehicle();
      this.driverBehindDriverInfo.HideAndClearVehicle();
      if ((UnityEngine.Object) Simulation2D.instance != (UnityEngine.Object) null)
        Simulation2D.instance.camera2D.OnLateUpdate -= new Action(this.UpdateInfoPositions);
    }
    App.instance.cameraManager.gameCamera.freeRoamCamera.OnLateUpdate -= new Action(this.UpdateInfoPositions);
  }

  private void Update()
  {
    if (Game.instance.sessionManager.flag == SessionManager.Flag.SafetyCar)
      this.safetyCarInfo.Show((Vehicle) Game.instance.vehicleManager.safetyVehicle);
    else
      this.safetyCarInfo.Hide();
    if (this.targetDriverInfo.isShowing)
    {
      bool flag = Game.instance.sessionManager.sessionType == SessionDetails.SessionType.Race & !this.targetDriverInfo.vehicle.pathState.IsInPitlaneArea();
      RacingVehicle vehicleAheadOnPath = this.targetDriverInfo.vehicle.pathController.vehicleAheadOnPath as RacingVehicle;
      if (vehicleAheadOnPath != null)
      {
        if (vehicleAheadOnPath != this.driverAheadDriverInfo.vehicle)
        {
          if ((double) this.mDriverAheadTimer >= 0.0)
            this.mDriverAheadTimer -= GameTimer.deltaTime;
          if ((double) this.mDriverAheadTimer < 0.0 || this.targetDriverInfo.vehicle == this.driverAheadDriverInfo.vehicle)
          {
            if (!vehicleAheadOnPath.driver.IsPlayersDriver())
            {
              if (flag)
                this.driverAheadDriverInfo.Show(vehicleAheadOnPath);
              this.mDriverAheadTimer = 3f;
            }
            else
              this.driverAheadDriverInfo.HideAndClearVehicle();
          }
        }
      }
      else
        this.driverAheadDriverInfo.HideAndClearVehicle();
      RacingVehicle vehicleBehindOnPath = this.targetDriverInfo.vehicle.pathController.vehicleBehindOnPath as RacingVehicle;
      if (vehicleBehindOnPath != null)
      {
        if (vehicleBehindOnPath != this.driverBehindDriverInfo.vehicle)
        {
          if ((double) this.mDriverBehindTimer >= 0.0)
            this.mDriverBehindTimer -= GameTimer.deltaTime;
          if ((double) this.mDriverBehindTimer < 0.0 || this.targetDriverInfo.vehicle == this.driverBehindDriverInfo.vehicle)
          {
            if (!vehicleBehindOnPath.driver.IsPlayersDriver())
            {
              if (flag)
                this.driverBehindDriverInfo.Show(vehicleBehindOnPath);
              this.mDriverBehindTimer = 3f;
            }
            else
              this.driverBehindDriverInfo.HideAndClearVehicle();
          }
        }
      }
      else
        this.driverBehindDriverInfo.HideAndClearVehicle();
    }
    for (int index = 0; index < this.playerDriverInfo.Length; ++index)
    {
      if (this.playerDriverInfo[index].vehicle == this.mCameraTargetVehicle)
      {
        if (this.playerDriverInfo[index].isShowing)
          this.playerDriverInfo[index].Hide();
      }
      else if (!this.playerDriverInfo[index].isShowing)
        this.playerDriverInfo[index].Show();
    }
  }

  public void OnCameraTargetChange()
  {
    GameObject target = App.instance.cameraManager.target;
    this.mCameraTargetVehicle = (RacingVehicle) null;
    if ((UnityEngine.Object) target != (UnityEngine.Object) null)
    {
      UnityVehicle component = target.GetComponent<UnityVehicle>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null && component.vehicle is RacingVehicle)
        this.mCameraTargetVehicle = (RacingVehicle) component.vehicle;
    }
    this.ShowCameraTargetDriverInfo();
  }

  public void ShowCameraTargetDriverInfo()
  {
    if (this.mCameraTargetVehicle == null)
      return;
    this.targetDriverInfo.Show(this.mCameraTargetVehicle);
    for (int index = 0; index < this.playerDriverInfo.Length; ++index)
    {
      if (this.playerDriverInfo[index].vehicle == this.mCameraTargetVehicle)
        this.playerDriverInfo[index].Hide();
    }
    if (this.mCameraTargetVehicle.driver.IsPlayersDriver())
      return;
    for (int index = 0; index < this.playerDriverInfo.Length; ++index)
    {
      if (!this.playerDriverInfo[index].isShowing)
        this.playerDriverInfo[index].Show();
    }
  }

  public void UpdateInfoPositions()
  {
    for (int index = 0; index < this.playerDriverInfo.Length; ++index)
      this.playerDriverInfo[index].UpdatePosition();
    this.targetDriverInfo.UpdatePosition();
    this.driverAheadDriverInfo.UpdatePosition();
    this.driverBehindDriverInfo.UpdatePosition();
    this.safetyCarInfo.UpdatePosition();
    if (App.instance.preferencesManager.videoPreferences.isSimulationRunning2DMode)
    {
      for (int index = 0; index < this.sessionInfos.Length; ++index)
        this.sessionInfos[index].AvoidAnyOverlapping();
      this.mLastModeRunning2DMode = true;
    }
    else
    {
      if (!this.mLastModeRunning2DMode)
        return;
      for (int index = 0; index < this.sessionInfos.Length; ++index)
        this.sessionInfos[index].ResetInstant();
      this.mLastModeRunning2DMode = false;
    }
  }
}
