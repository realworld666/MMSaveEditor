// Decompiled with JetBrains decompiler
// Type: NavigationButtonPreferences
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;

public class NavigationButtonPreferences : NavigationButton
{
  public PreferencesScreen.Mode mode = PreferencesScreen.Mode.audioSettings;

  public override bool HighlightIfOnScreen()
  {
    string currentScreenName = UIManager.instance.currentScreen_name;
    string screenScreenName = UIManager.instance.currentScreen_screenName;
    if ((this.goToScreen == screenScreenName || this.goToScreen == currentScreenName) && (!(this.goToScreen == "PreferencesScreen") && !(screenScreenName == "PreferencesScreen") || ((PreferencesScreen) UIManager.instance.currentScreen).mode == this.mode))
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
      if (this.goToScreen == "PreferencesScreen")
        UIManager.instance.GetScreen<PreferencesScreen>().SwitchMode(this.mode);
      UIManager.instance.ChangeScreen(this.goToScreen, UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    }
    else
    {
      if (string.IsNullOrEmpty(this.openDialogBox))
        return;
      UIManager.instance.dialogBoxManager.Show(this.openDialogBox);
    }
  }
}
