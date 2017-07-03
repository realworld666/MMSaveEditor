// Decompiled with JetBrains decompiler
// Type: AudioPreferences
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class AudioPreferences
{
  private PreferencesManager mManager;

  public void Start(PreferencesManager inManager)
  {
    this.mManager = inManager;
  }

  public void Load()
  {
    float sliderValue1 = this.mManager.GetSliderValue(this.mManager.GetSettingInt(Preference.pName.Audio_MainVolume, false));
    float sliderValue2 = this.mManager.GetSliderValue(this.mManager.GetSettingInt(Preference.pName.Audio_SfxVolume, false));
    float sliderValue3 = this.mManager.GetSliderValue(this.mManager.GetSettingInt(Preference.pName.Audio_UIVolume, false));
    float sliderValue4 = this.mManager.GetSliderValue(this.mManager.GetSettingInt(Preference.pName.Audio_MusicVolume, false));
    this.SetAudioMainVolume(sliderValue1);
    this.SetAudioSfxVolume(sliderValue2);
    this.SetAudioUIVolume(sliderValue3);
    this.SetAudioMusicVolume(sliderValue4);
    scSoundManager.Instance.SetUserVolumes(false);
  }

  public void SetAudioMainVolume(float inMainVolume)
  {
  }

  public void SetAudioSfxVolume(float inSfxVolume)
  {
  }

  public void SetAudioUIVolume(float inUIVolume)
  {
  }

  public void SetAudioMusicVolume(float inMusicVolume)
  {
  }
}
