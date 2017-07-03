// Decompiled with JetBrains decompiler
// Type: UIPreferencesBar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPreferencesBar : MonoBehaviour
{
  public Button resetDefaultButton;
  public Button applyVideoChangesButton;
  public PreferencesScreen screen;

  public void OnStart()
  {
    this.resetDefaultButton.onClick.AddListener(new UnityAction(this.OnResetDefault));
    this.applyVideoChangesButton.onClick.AddListener(new UnityAction(this.OnApplyVideoChanges));
    this.UpdateButtonsState();
  }

  private void OnResetDefault()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.screen.ResetDefault();
  }

  private void OnApplyVideoChanges()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.screen.ApplyChanges(PreferencesScreen.Mode.videoSettings);
  }

  public void UpdateButtonsState()
  {
    if (this.screen.mode == PreferencesScreen.Mode.videoSettings)
      this.applyVideoChangesButton.gameObject.SetActive(true);
    else
      this.applyVideoChangesButton.gameObject.SetActive(false);
    this.resetDefaultButton.interactable = true;
  }
}
