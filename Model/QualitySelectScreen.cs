// Decompiled with JetBrains decompiler
// Type: QualitySelectScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QualitySelectScreen : UIScreen
{
  private QualitySelectScreen.Mode mSelectedMode = QualitySelectScreen.Mode.HighSpecMode;
  private QualitySelectScreen.Mode mRecommendedMode = QualitySelectScreen.Mode.HighSpecMode;
  public Toggle lowSpecMode;
  public Toggle highSpecMode;
  public GameObject recommendedLowSpec;
  public GameObject recommendedHighSpec;
  public GameObject lowSpecInactive;
  public GameObject highSpecInactive;
  private bool mAllowMenuSounds;

  public override void OnStart()
  {
    base.OnStart();
    this.lowSpecMode.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SelectMode(QualitySelectScreen.Mode.LowSpecMode, this.lowSpecMode)));
    this.highSpecMode.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SelectMode(QualitySelectScreen.Mode.HighSpecMode, this.highSpecMode)));
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.mAllowMenuSounds = false;
    this.hideMMDropdownButtons = true;
    this.showCoreNavigation = false;
    this.showNavigationBars = true;
    this.continueButtonInteractable = true;
    UIManager.instance.ClearForwardStack();
    this.SetTopBarMode(UITopBar.Mode.Core);
    this.SetBottomBarMode(UIBottomBar.Mode.Core);
    this.mRecommendedMode = App.instance.preferencesManager.videoPreferences.GetBestQualitySetting() != PrefVideoUnityQuality.Type.Low ? QualitySelectScreen.Mode.HighSpecMode : QualitySelectScreen.Mode.LowSpecMode;
    this.mSelectedMode = this.mRecommendedMode;
    this.SetRecommendedMode();
    this.mAllowMenuSounds = true;
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    App.instance.preferencesManager.SetSetting(Preference.pName.Video_3D, (object) (this.mSelectedMode == QualitySelectScreen.Mode.HighSpecMode), true);
    App.instance.preferencesManager.ApplyChanges();
    return UIScreen.NavigationButtonEvent.LetGameStateHandle;
  }

  private void SetRecommendedMode()
  {
    GameUtility.SetActive(this.recommendedLowSpec, this.mRecommendedMode == QualitySelectScreen.Mode.LowSpecMode);
    GameUtility.SetActive(this.recommendedHighSpec, this.mRecommendedMode == QualitySelectScreen.Mode.HighSpecMode);
    this.lowSpecMode.isOn = this.mSelectedMode == QualitySelectScreen.Mode.LowSpecMode;
    this.highSpecMode.isOn = this.mSelectedMode == QualitySelectScreen.Mode.HighSpecMode;
  }

  private void SelectMode(QualitySelectScreen.Mode inMode, Toggle inToggle)
  {
    if (!inToggle.isOn)
      return;
    if (this.mAllowMenuSounds)
      scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mSelectedMode = inMode;
    GameUtility.SetActive(this.lowSpecInactive, !this.lowSpecMode.isOn);
    GameUtility.SetActive(this.highSpecInactive, !this.highSpecMode.isOn);
  }

  public enum Mode
  {
    LowSpecMode,
    HighSpecMode,
  }
}
