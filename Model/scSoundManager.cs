// Decompiled with JetBrains decompiler
// Type: scSoundManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class scSoundManager : MonoBehaviour
{
  public float m_GearChangeUpFreqTimeOut = 15f;
  [Range(0.0f, 1f)]
  public float m_GearChangeUpFreqStep = 0.05f;
  [Range(0.0f, 1f)]
  public float m_GearChangeUpFreqStepState = 0.005f;
  [Separator]
  public float m_GearChangeDownFreqTimeOut = 15f;
  [Range(0.0f, 1f)]
  public float m_GearChangeDownFreqStep = 0.05f;
  [Range(0.0f, 1f)]
  public float m_GearChangeDownFreqStepState = 3f / 1000f;
  private List<scSoundBase> m_AllSounds = new List<scSoundBase>();
  private scSoundBlendValue mTutorialPausedVolume = new scSoundBlendValue(0.0f, 0.5f, -1f);
  private scSoundBlendValue mTutorialPausedMusicVolume = new scSoundBlendValue(0.0f, 0.5f, -1f);
  private scSoundBlendValue mSliderRoundVolume = new scSoundBlendValue(0.0f, 10f, -10f);
  private scSoundBlendValue mSliderSquareVolume = new scSoundBlendValue(0.0f, 10f, -10f);
  public float m_GearChangeUpFreq;
  public float m_GearChangeDownFreq;
  public GameObject m_SessionAudio;
  private static scSoundManager m_AudioManager;
  private static bool mCreated;
  private static bool mLoadedSoundBank_Frontend;
  private static bool mLoadedSoundBank_RaceSession;
  private static bool mLoadedSoundBank_GT;
  private bool m_WaitForSessionHudEnd;
  private SoundContainerManager m_SoundContainerManager;
  private List<scSoundManager.SoundTriggerLimit> mSoundTriggerLimits;
  private bool m_PausedFromTutorial;
  private scSoundContainer mTutorialPausedAmbience;
  private scSoundContainer mTutorialPausedMusic;
  private scSoundContainer mSliderRound;
  private scSoundContainer mSliderSquare;
  private float mSliderRoundCoolOffTimer;
  private float mSliderSquareCoolOffTimer;
  public AudioMixer m_EngineMixer;
  private static bool m_BlockSoundEvents;
  private float mInitialVolumeMaster;
  private float mInitialVolumePlayerCar;
  private float mInitialVolumeAmbience;
  private float mInitialVolumeInGame;
  private float mInitialVolumeMusic;
  private float mInitialVolumeMenu;
  private float mInitialVolumeMovie;
  private bool mHasReadInitialVolume;
  private float mMixerVolumeMaster;

  public static scSoundManager Instance
  {
    get
    {
      if ((Object) scSoundManager.m_AudioManager == (Object) null)
        Debug.LogWarning((object) "scSound Manager not initialised", (Object) null);
      return scSoundManager.m_AudioManager;
    }
  }

  public bool SessionActive { get; private set; }

  public SoundContainerManager SoundContainer
  {
    get
    {
      return this.m_SoundContainerManager;
    }
  }

  public static bool MuteAudio { get; set; }

  public static bool BlockSoundEvents
  {
    get
    {
      return scSoundManager.m_BlockSoundEvents;
    }
    set
    {
      scSoundManager.m_BlockSoundEvents = value;
    }
  }

  public static void Create()
  {
    if (scSoundManager.mCreated)
      return;
    scSoundManager.LoadPrefab("Prefabs/SoundManager");
    scSoundManager.LoadPrefab("Prefabs/Audio/SoundBank_Startup");
    scSoundManager.mCreated = true;
  }

  public static void LoadSoundBank_Frontend()
  {
    if (scSoundManager.mLoadedSoundBank_Frontend)
      return;
    scSoundManager.LoadPrefab("Prefabs/Audio/SoundBank_Frontend");
    scSoundManager.mLoadedSoundBank_Frontend = true;
  }

  public static void LoadSoundBank_RaceSession()
  {
    if (!scSoundManager.mLoadedSoundBank_RaceSession)
    {
      scSoundManager.LoadPrefab("Prefabs/Audio/SoundBank_RaceSession");
      scSoundManager.mLoadedSoundBank_RaceSession = true;
    }
    if (Game.instance.sessionManager.championship.series != Championship.Series.GTSeries || scSoundManager.mLoadedSoundBank_GT)
      return;
    scSoundManager.LoadPrefab("Prefabs/Audio/SoundBank_GT");
    scSoundManager.mLoadedSoundBank_GT = true;
  }

  private static void LoadPrefab(string fileName)
  {
    GameObject go = (GameObject) Object.Instantiate(Resources.Load(fileName));
    go.name = go.name.Replace("(Clone)", string.Empty);
    UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(go, FirstActiveSceneHolder.firstActiveScene);
  }

  public void SessionStart()
  {
    this.SessionActive = true;
    this.m_SessionAudio.SetActive(true);
  }

  public void SessionEnd(bool waitForSessionHudEnd)
  {
    if (waitForSessionHudEnd)
      this.m_WaitForSessionHudEnd = true;
    else
      this.SetSessionEnd();
  }

  private void UpdateSessionEnd()
  {
    if (!this.m_WaitForSessionHudEnd || UIManager.instance.currentScreen is SessionHUD)
      return;
    this.SetSessionEnd();
  }

  private void SetSessionEnd()
  {
    this.m_WaitForSessionHudEnd = false;
    this.SessionActive = false;
    this.EndTutorialPaused();
    this.m_SessionAudio.SetActive(false);
  }

  private void OnApplicationFocus(bool hasFocus)
  {
    scSoundManager.MuteAudio = !hasFocus;
  }

  private void Awake()
  {
    scSoundManager.MuteAudio = false;
    scSoundManager.BlockSoundEvents = true;
    scSoundManager.m_AudioManager = this;
    this.m_SoundContainerManager = new SoundContainerManager();
    this.mSoundTriggerLimits = new List<scSoundManager.SoundTriggerLimit>(171);
    for (int index = 0; index < 171; ++index)
      this.mSoundTriggerLimits.Add(new scSoundManager.SoundTriggerLimit(0));
    this.mSoundTriggerLimits[14] = new scSoundManager.SoundTriggerLimit(2);
    this.mSoundTriggerLimits[15] = new scSoundManager.SoundTriggerLimit(2);
    this.mSoundTriggerLimits[16] = new scSoundManager.SoundTriggerLimit(2);
    this.mSoundTriggerLimits[17] = new scSoundManager.SoundTriggerLimit(2);
    this.mSoundTriggerLimits[18] = new scSoundManager.SoundTriggerLimit(2);
    this.mSoundTriggerLimits[19] = new scSoundManager.SoundTriggerLimit(2);
    this.mSoundTriggerLimits[20] = new scSoundManager.SoundTriggerLimit(2);
    scSoundManager.SoundTriggerLimit sharedLimit = new scSoundManager.SoundTriggerLimit(2);
    this.mSoundTriggerLimits[34] = sharedLimit;
    this.mSoundTriggerLimits[35] = new scSoundManager.SoundTriggerLimit(sharedLimit);
    this.mSoundTriggerLimits[36] = new scSoundManager.SoundTriggerLimit(sharedLimit);
    this.mSoundTriggerLimits[37] = new scSoundManager.SoundTriggerLimit(sharedLimit);
    this.mSoundTriggerLimits[38] = new scSoundManager.SoundTriggerLimit(sharedLimit);
    this.mSoundTriggerLimits[39] = new scSoundManager.SoundTriggerLimit(sharedLimit);
    this.mSoundTriggerLimits[40] = new scSoundManager.SoundTriggerLimit(sharedLimit);
    this.mSoundTriggerLimits[41] = new scSoundManager.SoundTriggerLimit(sharedLimit);
    this.mSoundTriggerLimits[42] = new scSoundManager.SoundTriggerLimit(sharedLimit);
  }

  public void AddSound(scSoundBase soundBase)
  {
    this.m_AllSounds.Add(soundBase);
  }

  private void Update()
  {
    this.Update(GameTimer.deltaTime);
    for (int index = 0; index < 171; ++index)
      this.mSoundTriggerLimits[index].UpdateFrame();
    scMusicController.Update(GameTimer.deltaTime);
    if (scSoundManager.MuteAudio)
      this.m_EngineMixer.SetFloat("VolumeMaster", -80f);
    else
      this.m_EngineMixer.SetFloat("VolumeMaster", this.mMixerVolumeMaster);
  }

  private void Update(float dt)
  {
    this.m_SoundContainerManager.Update(dt);
    int count = this.m_AllSounds.Count;
    for (int index = 0; index < count; ++index)
    {
      if (this.m_AllSounds[index].IsPlaying)
        this.m_AllSounds[index].Update(dt);
    }
    scEngineController.SetTweaks(this.m_GearChangeUpFreqStep, this.m_GearChangeDownFreqStep, this.m_GearChangeUpFreqTimeOut, this.m_GearChangeDownFreqTimeOut, this.m_GearChangeUpFreqStepState, this.m_GearChangeDownFreqStepState);
    this.m_GearChangeUpFreq = scEngineController.GearChangeUpFreq;
    this.m_GearChangeDownFreq = scEngineController.GearChangeDownFreq;
    this.UpdateTutorialPaused(dt);
    this.UpdateSliderSounds(dt);
    this.UpdateSessionEnd();
  }

  public void Pause(bool pauseMusic, bool pauseUI = false, bool fromTutorial = false)
  {
    this.m_PausedFromTutorial = fromTutorial;
    scSessionAmbience.IsPaused = true;
    for (int index = 0; index < this.m_AllSounds.Count; ++index)
    {
      bool flag = true;
      if (!pauseMusic && this.m_AllSounds[index].SubmixID == scSoundMix.SubmixID.Music)
        flag = false;
      if (!pauseUI && this.m_AllSounds[index].SubmixID == scSoundMix.SubmixID.Menu)
        flag = false;
      if (flag)
        this.m_AllSounds[index].Pause(true, true);
    }
  }

  public void UnPause(bool fromTutorial = false)
  {
    if (this.m_PausedFromTutorial != fromTutorial)
      return;
    scSessionAmbience.IsPaused = false;
    for (int index = 0; index < this.m_AllSounds.Count; ++index)
      this.m_AllSounds[index].UnPause();
  }

  public void UpdateTutorialPaused(float deltaTime)
  {
    this.mTutorialPausedVolume.Update(deltaTime);
    this.mTutorialPausedMusicVolume.Update(deltaTime);
    if ((double) this.mTutorialPausedVolume.Value == 0.0 && (double) this.mTutorialPausedVolume.TargetValue == 0.0)
    {
      scSoundManager.CheckStopSound(ref this.mTutorialPausedAmbience);
      if ((Object) this.mTutorialPausedMusic != (Object) null)
      {
        this.mTutorialPausedMusic.UnBlockPause();
        this.mTutorialPausedMusic.Pause();
        this.mTutorialPausedMusic.BlockPause();
      }
    }
    else
    {
      SoundID soundID = SoundID.Tutorial_Paused;
      if ((Object) this.mTutorialPausedAmbience == (Object) null)
      {
        switch (Game.instance.sessionManager.championship.series)
        {
          case Championship.Series.GTSeries:
            soundID = SoundID.GT_Tutorial_Paused;
            break;
        }
        this.mTutorialPausedAmbience = scSoundManager.Instance.PlaySound(soundID, 0.0f);
        this.mTutorialPausedAmbience.BlockPause();
      }
      this.mTutorialPausedAmbience.Volume = this.mTutorialPausedVolume.Value;
      if ((Object) this.mTutorialPausedMusic != (Object) null)
        this.mTutorialPausedMusic.Volume = this.mTutorialPausedMusicVolume.Value;
    }
    if (this.SessionActive)
      return;
    scSoundManager.CheckStopSound(ref this.mTutorialPausedMusic);
  }

  public void StartTutorialPaused(bool doMusic)
  {
    if (Game.instance == null || Game.instance.sessionManager == null || (!Game.instance.sessionManager.isSessionActive || Game.instance.sessionManager.hasSessionEnded) || Game.instance.sessionManager.standings.Count <= 0)
      return;
    this.mTutorialPausedVolume.Value = 1f;
    if (!doMusic)
      return;
    scSoundManager.CheckPlaySound(scMusicController.TutorialMusicId(), ref this.mTutorialPausedMusic, 0.0f);
    if ((Object) this.mTutorialPausedMusic != (Object) null)
    {
      this.mTutorialPausedMusic.UnBlockPause();
      this.mTutorialPausedMusic.UnPause();
      this.mTutorialPausedMusic.BlockPause();
    }
    this.mTutorialPausedMusicVolume.Value = 1f;
  }

  public void EndTutorialPaused()
  {
    this.mTutorialPausedVolume.Value = 0.0f;
    this.mTutorialPausedMusicVolume.Value = 0.0f;
  }

  public void LoadToTitleScreen()
  {
  }

  public void LoadToRaceEvent()
  {
    this.SessionStart();
  }

  public void LoadToFrontend()
  {
    scMusicController.Start();
  }

  private void UpdateSliderSounds(float deltaTime)
  {
    this.mSliderRoundVolume.Update(deltaTime);
    this.mSliderSquareVolume.Update(deltaTime);
    this.mSliderRoundCoolOffTimer -= deltaTime;
    if ((double) this.mSliderRoundCoolOffTimer < 0.0)
    {
      this.mSliderRoundCoolOffTimer = 0.0f;
      this.mSliderRoundVolume.Value = 0.0f;
    }
    this.mSliderSquareCoolOffTimer -= deltaTime;
    if ((double) this.mSliderSquareCoolOffTimer < 0.0)
    {
      this.mSliderSquareCoolOffTimer = 0.0f;
      this.mSliderSquareVolume.Value = 0.0f;
    }
    if ((Object) this.mSliderRound != (Object) null && (Object) this.mSliderSquare != (Object) null)
    {
      this.mSliderRound.Volume = this.mSliderRoundVolume.Value;
      this.mSliderSquare.Volume = this.mSliderSquareVolume.Value;
    }
    else
    {
      if ((double) this.mSliderRoundVolume.Value <= 0.0 && (double) this.mSliderSquareVolume.Value <= 0.0)
        return;
      scSoundManager.CheckPlaySound(SoundID.Sfx_CircularSlider, ref this.mSliderRound, 0.0f);
      scSoundManager.CheckPlaySound(SoundID.Sfx_RectangleSlider, ref this.mSliderSquare, 0.0f);
    }
  }

  public void PlaySquareSlider()
  {
    if (scSoundManager.BlockSoundEvents)
      return;
    this.mSliderSquareCoolOffTimer = 0.05f;
    this.mSliderSquareVolume.Value = 1f;
  }

  public void PlayRoundSlider()
  {
    if (scSoundManager.BlockSoundEvents)
      return;
    this.mSliderRoundCoolOffTimer = 0.05f;
    this.mSliderRoundVolume.Value = 1f;
  }

  public void SetCameraZoom(float zoom)
  {
    float num1 = Mathf.Lerp(0.05f, 2f, zoom);
    float num2 = Mathf.Lerp(500f, 0.0f, zoom);
    float num3 = Mathf.Lerp(-1000f, 0.0f, zoom);
    this.m_EngineMixer.SetFloat("FrequencyGain", num1);
    this.m_EngineMixer.SetFloat("Reverb", num2);
    this.m_EngineMixer.SetFloat("DryLevel", num3);
  }

  public scSoundContainer PlaySound(SoundID soundID, float delay = 0.0f)
  {
    if (scSoundManager.BlockSoundEvents)
      return (scSoundContainer) null;
    if (soundID == SoundID.Unset || !this.mSoundTriggerLimits[(int) soundID].AllowSound())
      return (scSoundContainer) null;
    if (soundID == SoundID.Button_Select)
      soundID = SoundID.Button_Select;
    return this.m_SoundContainerManager.PlaySound(soundID, (Transform) null, 1f, delay);
  }

  public static bool CheckPlaySound(SoundID soundID, ref scSoundContainer scSoundContainer, float delay = 0.0f)
  {
    if (!((Object) scSoundContainer == (Object) null))
      return false;
    scSoundContainer = scSoundManager.Instance.PlaySound(soundID, delay);
    return true;
  }

  public static bool CheckStopSound(ref scSoundContainer scSoundContainer)
  {
    if (!((Object) scSoundContainer != (Object) null))
      return false;
    scSoundContainer.StopSound(false);
    scSoundContainer = (scSoundContainer) null;
    return true;
  }

  private float SliderValueToDb(int sliderValue)
  {
    return scSoundHelper.DbFromLinearVolume((float) sliderValue / 100f);
  }

  public void SetUserVolumes(bool inChangedValue)
  {
    PreferencesManager preferencesManager = App.instance.preferencesManager;
    float db1 = this.SliderValueToDb(preferencesManager.GetSettingInt(Preference.pName.Audio_MainVolume, inChangedValue));
    float db2 = this.SliderValueToDb(preferencesManager.GetSettingInt(Preference.pName.Audio_SfxVolume, inChangedValue));
    float db3 = this.SliderValueToDb(preferencesManager.GetSettingInt(Preference.pName.Audio_UIVolume, inChangedValue));
    float db4 = this.SliderValueToDb(preferencesManager.GetSettingInt(Preference.pName.Audio_MusicVolume, inChangedValue));
    if (!this.mHasReadInitialVolume)
    {
      this.m_EngineMixer.GetFloat("VolumeMaster", out this.mInitialVolumeMaster);
      this.m_EngineMixer.GetFloat("VolumePlayerCar", out this.mInitialVolumePlayerCar);
      this.m_EngineMixer.GetFloat("VolumeAmbience", out this.mInitialVolumeAmbience);
      this.m_EngineMixer.GetFloat("VolumeInGame", out this.mInitialVolumeInGame);
      this.m_EngineMixer.GetFloat("VolumeMusic", out this.mInitialVolumeMusic);
      this.m_EngineMixer.GetFloat("VolumeMenu", out this.mInitialVolumeMenu);
      this.m_EngineMixer.GetFloat("VolumeMovie", out this.mInitialVolumeMovie);
      this.mHasReadInitialVolume = true;
    }
    this.mMixerVolumeMaster = this.mInitialVolumeMaster + db1;
    float num1 = this.mInitialVolumePlayerCar + db2;
    float num2 = this.mInitialVolumeAmbience + db2;
    float num3 = this.mInitialVolumeInGame + db2;
    float num4 = this.mInitialVolumeMusic + db4;
    float num5 = this.mInitialVolumeMenu + db3;
    float num6 = this.mInitialVolumeMovie + db4;
    this.mMixerVolumeMaster = Mathf.Clamp(this.mMixerVolumeMaster, -80f, 0.0f);
    float num7 = Mathf.Clamp(num1, -80f, 0.0f);
    float num8 = Mathf.Clamp(num2, -80f, 0.0f);
    float num9 = Mathf.Clamp(num3, -80f, 0.0f);
    float num10 = Mathf.Clamp(num4, -80f, 0.0f);
    float num11 = Mathf.Clamp(num5, -80f, 0.0f);
    float num12 = Mathf.Clamp(num6, -80f, 0.0f);
    this.m_EngineMixer.SetFloat("VolumeMaster", this.mMixerVolumeMaster);
    this.m_EngineMixer.SetFloat("VolumePlayerCar", num7);
    this.m_EngineMixer.SetFloat("VolumeAmbience", num8);
    this.m_EngineMixer.SetFloat("VolumeInGame", num9);
    this.m_EngineMixer.SetFloat("VolumeMusic", num10);
    this.m_EngineMixer.SetFloat("VolumeMenu", num11);
    this.m_EngineMixer.SetFloat("VolumeMovie", num12);
  }

  private class SoundTriggerLimit
  {
    private int mFrameCount;
    private int mCountLimit;
    private scSoundManager.SoundTriggerLimit mSharedLimit;

    private int CountLimit
    {
      get
      {
        if (this.mSharedLimit != null)
          return this.mSharedLimit.mCountLimit;
        return this.mCountLimit;
      }
    }

    private int FrameCount
    {
      get
      {
        if (this.mSharedLimit != null)
          return this.mSharedLimit.mFrameCount;
        return this.mFrameCount;
      }
      set
      {
        if (this.mSharedLimit != null)
          this.mSharedLimit.mFrameCount = value;
        else
          this.mFrameCount = value;
      }
    }

    public SoundTriggerLimit(int limit)
    {
      this.mSharedLimit = (scSoundManager.SoundTriggerLimit) null;
      this.mFrameCount = 0;
      this.mCountLimit = limit;
    }

    public SoundTriggerLimit(scSoundManager.SoundTriggerLimit sharedLimit)
    {
      this.mSharedLimit = sharedLimit;
      this.mFrameCount = 0;
      this.mCountLimit = sharedLimit.mCountLimit;
    }

    public void UpdateFrame()
    {
      --this.FrameCount;
      this.FrameCount = Mathf.Max(this.FrameCount, 0);
    }

    public bool AllowSound()
    {
      if (this.FrameCount > 0)
        return false;
      this.FrameCount = this.CountLimit;
      return true;
    }
  }
}
