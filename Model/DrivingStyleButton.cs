// Decompiled with JetBrains decompiler
// Type: DrivingStyleButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class DrivingStyleButton : MonoBehaviour
{
  private DrivingStyle.Mode mDrivingStyle = DrivingStyle.Mode.Neutral;
  public Color[] colors;
  private Button mButton;
  private RacingVehicle mVehicle;
  private bool mForceUpdate;

  public Button button
  {
    get
    {
      if ((Object) this.mButton == (Object) null)
        this.mButton = this.GetComponent<Button>();
      return this.mButton;
    }
  }

  private void Awake()
  {
    this.mButton = this.GetComponent<Button>();
  }

  public void SetVehicle(RacingVehicle inVehicle)
  {
    this.mVehicle = inVehicle;
    this.mForceUpdate = true;
    this.Update();
  }

  private void Update()
  {
    if (this.mVehicle == null || this.mDrivingStyle == this.mVehicle.performance.drivingStyleMode && !this.mForceUpdate)
      return;
    this.mDrivingStyle = this.mVehicle.performance.drivingStyleMode;
    Color color = this.colors[(int) this.mDrivingStyle];
    ColorBlock colors = this.button.colors;
    colors.normalColor = color;
    colors.highlightedColor = color * 0.9f;
    colors.pressedColor = color * 0.8f;
    this.button.colors = colors;
    this.mForceUpdate = false;
  }
}
