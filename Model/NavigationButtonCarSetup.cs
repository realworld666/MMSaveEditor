// Decompiled with JetBrains decompiler
// Type: NavigationButtonCarSetup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;

public class NavigationButtonCarSetup : NavigationButton
{
  public NavigationButtonCarSetup.VehicleNum vehicleTarget;

  public override bool HighlightIfOnScreen()
  {
    string currentScreenName = UIManager.instance.currentScreen_name;
    string screenScreenName = UIManager.instance.currentScreen_screenName;
    if ((this.goToScreen == screenScreenName || this.goToScreen == currentScreenName) && (!(this.goToScreen == "PitScreen") && !(screenScreenName == "PitScreen") || (NavigationButtonCarSetup.VehicleNum) ((PitScreen) UIManager.instance.currentScreen).vehicle.carID == this.vehicleTarget))
      return true;
    for (int index = 0; index < this.screensToToggleOn.Count; ++index)
    {
      if ((UnityEngine.Object) UIManager.instance.currentScreen != (UnityEngine.Object) null && (currentScreenName == this.screensToToggleOn[index] || screenScreenName == this.screensToToggleOn[index]))
        return true;
    }
    return false;
  }

  public override void OnButton()
  {
    if (!string.IsNullOrEmpty(this.goToScreen))
    {
      if (this.goToScreen == "PitScreen")
        UIManager.instance.GetScreen<PitScreen>().Setup(Game.instance.vehicleManager.GetPlayerVehicles()[(int) this.vehicleTarget], PitScreen.Mode.PreSession);
      UIManager.instance.ChangeScreen(this.goToScreen, UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    }
    else
    {
      if (string.IsNullOrEmpty(this.openDialogBox))
        return;
      UIManager.instance.dialogBoxManager.Show(this.openDialogBox);
    }
  }

  public enum VehicleNum
  {
    One,
    Two,
  }
}
