// Decompiled with JetBrains decompiler
// Type: SessionModeWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SessionModeWidget : MonoBehaviour
{
  public GameObject flagContainer;
  public GameObject togglesContainer;
  public Toggle mode2DToggle;
  public Toggle mode3DToggle;
  public SessionHUD screen;
  private bool mCanDisplay;
  private bool mIsSessionActive;

  public void OnStart()
  {
    this.SetListeners();
  }

  public void Setup()
  {
    this.mCanDisplay = !App.instance.preferencesManager.videoPreferences.isRunning2DMode && SceneManager.instance.hasLoaded3DGeometry;
    this.mIsSessionActive = Game.instance.sessionManager.isSessionActive && !Game.instance.sessionManager.hasSessionEnded;
    GameUtility.SetActive(this.togglesContainer, this.mCanDisplay && this.mIsSessionActive);
    GameUtility.SetActive(this.flagContainer, !this.mCanDisplay || !this.mIsSessionActive);
    if (!this.mCanDisplay)
      return;
    this.SetToggles();
  }

  private void Update()
  {
    if (!this.mCanDisplay)
      return;
    this.mIsSessionActive = Game.instance.sessionManager.isSessionActive && !Game.instance.sessionManager.hasSessionEnded;
    GameUtility.SetActive(this.togglesContainer, this.mIsSessionActive);
    GameUtility.SetActive(this.flagContainer, !this.mIsSessionActive);
    if (!this.mIsSessionActive || !InputManager.instance.GetKeyUp(KeyBinding.Name.ChangeViewpoint))
      return;
    if (App.instance.preferencesManager.videoPreferences.isSimulationRunning2DMode)
    {
      this.mode2DToggle.isOn = false;
      this.mode3DToggle.isOn = true;
    }
    else
    {
      this.mode2DToggle.isOn = true;
      this.mode3DToggle.isOn = false;
    }
  }

  public void SetToggles()
  {
    bool flag = !App.instance.preferencesManager.videoPreferences.isSimulationRunning2DMode;
    this.RemoveListeners();
    this.mode2DToggle.isOn = !flag;
    this.mode3DToggle.isOn = flag;
    this.SetListeners();
  }

  public void RemoveListeners()
  {
    this.mode2DToggle.onValueChanged.RemoveAllListeners();
    this.mode3DToggle.onValueChanged.RemoveAllListeners();
  }

  public void SetListeners()
  {
    this.mode2DToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetToggleMode(true, this.mode2DToggle)));
    this.mode3DToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetToggleMode(false, this.mode3DToggle)));
  }

  public void SetToggleMode(bool inMode2D, Toggle inToggle)
  {
    if (!inToggle.isOn)
      return;
    this.SetMode(inMode2D);
  }

  public void SetMode(bool inMode2D)
  {
    if (inMode2D)
    {
      if (App.instance.preferencesManager.videoPreferences.isSimulationRunning2DMode)
        return;
      App.instance.preferencesManager.videoPreferences.SetSimulationRunningMode2D(true);
      this.screen.ExitWidgets();
      this.screen.OnEnter();
    }
    else
    {
      if (!App.instance.preferencesManager.videoPreferences.isSimulationRunning2DMode || !SceneManager.instance.hasLoaded3DGeometry)
        return;
      App.instance.preferencesManager.videoPreferences.SetSimulationRunningMode2D(false);
      this.screen.ExitWidgets();
      this.screen.OnEnter();
    }
  }
}
