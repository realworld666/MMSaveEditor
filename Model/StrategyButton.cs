// Decompiled with JetBrains decompiler
// Type: StrategyButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class StrategyButton : MonoBehaviour
{
  private DrivingStyle.Mode mDrivingStyle = DrivingStyle.Mode.Neutral;
  private Fuel.EngineMode mEngineMode = Fuel.EngineMode.Medium;
  public Toggle toggle;
  public CanvasGroup[] toggleGroups;
  public GameObject[] drivingStyleIcons;
  public GameObject[] engineModeIcons;
  private RacingVehicle mVehicle;
  private bool mForceUpdate;

  public void SetVehicle(RacingVehicle inVehicle)
  {
    this.mVehicle = inVehicle;
    this.mForceUpdate = true;
    this.toggle.interactable = Game.instance.sessionManager.eventDetails.currentSession.sessionType != SessionDetails.SessionType.Qualifying;
    for (int index = 0; index < this.toggleGroups.Length; ++index)
      this.toggleGroups[index].alpha = !this.toggle.interactable ? 0.2f : 1f;
    this.Update();
  }

  private void Update()
  {
    if (this.mVehicle == null)
      return;
    if (this.mDrivingStyle != this.mVehicle.performance.drivingStyleMode || this.mForceUpdate)
      this.UpdateDrivingStyleIcon();
    if (this.mEngineMode != this.mVehicle.performance.fuel.engineMode || this.mForceUpdate)
      this.UpdateEngineModeIcon();
    this.mForceUpdate = false;
  }

  private void UpdateDrivingStyleIcon()
  {
    this.mDrivingStyle = this.mVehicle.performance.drivingStyleMode;
    int mDrivingStyle = (int) this.mDrivingStyle;
    for (int index = 0; index < this.drivingStyleIcons.Length; ++index)
      this.drivingStyleIcons[index].SetActive(index == mDrivingStyle);
  }

  private void UpdateEngineModeIcon()
  {
    this.mEngineMode = this.mVehicle.performance.fuel.engineMode;
    int mEngineMode = (int) this.mEngineMode;
    for (int index = 0; index < this.engineModeIcons.Length; ++index)
      this.engineModeIcons[index].SetActive(index == mEngineMode);
  }
}
