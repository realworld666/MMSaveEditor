// Decompiled with JetBrains decompiler
// Type: UIPreferencesButtonWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UIPreferencesButtonWidget : MonoBehaviour
{
  public Toggle gameSettingsButton;
  public Toggle videoSettingsButton;
  public Toggle audioSettingsButton;
  public Toggle controlsSettingsButton;
  public ToggleGroup settingsButtonsGroup;

  public void Awake()
  {
    this.gameObject.SetActive(false);
    this.gameSettingsButton.group = this.settingsButtonsGroup;
    this.videoSettingsButton.group = this.settingsButtonsGroup;
    this.audioSettingsButton.group = this.settingsButtonsGroup;
    this.controlsSettingsButton.group = this.settingsButtonsGroup;
  }
}
