// Decompiled with JetBrains decompiler
// Type: VideoPreferences
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityStandardAssets.CinematicEffects;
using UnityStandardAssets.ImageEffects;

public class VideoPreferences
{
  public static float targetAspectRatio = 1.777778f;
  private List<UnityEngine.Resolution> availableResolutions = new List<UnityEngine.Resolution>();
  private int[] currentResolution = new int[2];
  public bool currentFullscreenMode = true;
  private UnityEngine.Resolution[] mScreenResolutions;
  private PreferencesManager mManager;
  private bool mIsRunning2DMode;
  private bool mSimulationIsRunning2DMode;

  public bool isRunning2DMode
  {
    get
    {
      return this.mIsRunning2DMode;
    }
  }

  public bool isSimulationRunning2DMode
  {
    get
    {
      return this.mSimulationIsRunning2DMode;
    }
  }

  public VideoPreferences()
  {
    this.GetAllScreenResolutions();
  }

  public void Start(PreferencesManager inManager)
  {
    this.mManager = inManager;
    int width = Screen.width;
    int height = Screen.height;
    string settingString = this.mManager.GetSettingString(Preference.pName.Video_Resolution, false);
    if (settingString.Length > 0)
    {
      int[] videoResolution = this.GetVideoResolution(settingString);
      width = videoResolution[0];
      height = videoResolution[1];
    }
    this.SetCurrentResolution(width, height, false);
    bool videoDisplay = this.GetVideoDisplay(this.mManager.GetSettingString(Preference.pName.Video_Display, false));
    bool? nullable = VideoPreferences.CommandLineForcedFullScreen();
    if (nullable.HasValue)
    {
      Debug.LogFormat("Command line forcing fullscreen to {0}", (object) nullable.Value);
      videoDisplay = nullable.Value;
    }
    this.SetVideoDisplay(videoDisplay);
    this.SetVideoResolution(width, height, videoDisplay);
    this.SetRunInBackground(App.instance.preferencesManager.GetSettingBool(Preference.pName.Video_RunInBackground, false));
  }

  public void Load()
  {
    this.LoadQualitySettings();
  }

  private static bool? CommandLineForcedFullScreen()
  {
    string commandLine = Environment.CommandLine;
    if (Regex.IsMatch(commandLine, "-screen-fullscreen[\\s]+0"))
      return new bool?(false);
    if (Regex.IsMatch(commandLine, "-screen-fullscreen[\\s]+1"))
      return new bool?(true);
    return new bool?();
  }

  private void LoadQualitySettings()
  {
    this.SetQuality(this.mManager.GetSettingEnum<PrefVideoUnityQuality.Type>(Preference.pName.Video_UnityQuality, false));
    this.SetVsync(this.mManager.GetSettingBool(Preference.pName.Video_Vsync, false));
    this.SetAntiAliasing(this.mManager.GetSettingBool(Preference.pName.Video_AntiAliasing, false));
    this.SetPostProcessing(Preference.pName.Video_AmbientOcclusion, this.mManager.GetSettingBool(Preference.pName.Video_AmbientOcclusion, false));
    this.SetPostProcessing(Preference.pName.Video_SSR, this.mManager.GetSettingBool(Preference.pName.Video_SSR, false));
    this.SetPostProcessing(Preference.pName.Video_ToneMapping, this.mManager.GetSettingBool(Preference.pName.Video_ToneMapping, false));
    this.SetPostProcessing(Preference.pName.Video_Bloom, this.mManager.GetSettingBool(Preference.pName.Video_Bloom, false));
    this.SetPostProcessing(Preference.pName.Video_VignetteChromaticAbberation, this.mManager.GetSettingBool(Preference.pName.Video_VignetteChromaticAbberation, false));
    this.SetPostProcessing(Preference.pName.Video_DOF, this.mManager.GetSettingBool(Preference.pName.Video_DOF, false));
  }

  private List<UnityEngine.Resolution> GetAllScreenResolutions()
  {
    this.availableResolutions.Clear();
    this.mScreenResolutions = Screen.resolutions;
    for (int index = this.mScreenResolutions.Length - 1; index >= 0; --index)
    {
      if (this.IsValidResolution((float) this.mScreenResolutions[index].width, (float) this.mScreenResolutions[index].height))
        this.availableResolutions.Add(this.mScreenResolutions[index]);
    }
    if (this.availableResolutions.Count == 0)
      this.availableResolutions.Add(this.mScreenResolutions[this.mScreenResolutions.Length - 1]);
    return this.availableResolutions;
  }

  private bool IsValidResolution(float width, float height)
  {
    if ((double) height < 720.0)
      return false;
    float a = width / height;
    return Mathf.Approximately(a, 1.333333f) || Mathf.Approximately(a, 1.6f) || (Mathf.Approximately(a, 1.25f) || Mathf.Approximately(a, 9f / 32f)) || (double) a > 1.70000004768372;
  }

  public string[] GetScreenResolutionList()
  {
    List<string> stringList = new List<string>();
    for (int index = 0; index < this.availableResolutions.Count; ++index)
    {
      string resolutionString = this.GetVideoResolutionString(this.availableResolutions[index].width, this.availableResolutions[index].height);
      stringList.Add(resolutionString);
    }
    return stringList.ToArray();
  }

  public string GetBestScreenResolution()
  {
    UnityEngine.Resolution currentResolution = Screen.currentResolution;
    if (this.availableResolutions.Count <= 0)
      return this.GetVideoResolutionString(currentResolution.width, currentResolution.height);
    int index1 = 0;
    float b = (float) currentResolution.width / (float) currentResolution.height;
    for (int index2 = 0; index2 < this.availableResolutions.Count; ++index2)
    {
      if (this.availableResolutions[index2].height <= currentResolution.height && MathsUtility.Approximately((float) this.availableResolutions[index2].width / (float) this.availableResolutions[index2].height, b, 0.02f))
      {
        index1 = index2;
        break;
      }
    }
    return this.GetVideoResolutionString(this.availableResolutions[index1].width, this.availableResolutions[index1].height);
  }

  public PrefVideoUnityQuality.Type GetBestQualitySetting()
  {
    PrefVideoUnityQuality.Type type = PrefVideoUnityQuality.Type.MediumLow;
    int num1 = 2000;
    int num2 = 4000;
    int num3 = 1000;
    int num4 = 6000;
    int num5 = 41;
    int graphicsShaderLevel = SystemInfo.graphicsShaderLevel;
    int graphicsMemorySize = SystemInfo.graphicsMemorySize;
    int systemMemorySize = SystemInfo.systemMemorySize;
    Debug.Log((object) string.Format("Shader Level {0}, VRam {1}, Main Mem {2}", (object) graphicsShaderLevel, (object) graphicsMemorySize, (object) systemMemorySize), (UnityEngine.Object) null);
    if (graphicsMemorySize >= num1)
      type = PrefVideoUnityQuality.Type.MediumHigh;
    if (graphicsMemorySize >= num2)
      type = PrefVideoUnityQuality.Type.High;
    if (graphicsShaderLevel < num5 || graphicsMemorySize < num3 || systemMemorySize < num4)
      type = PrefVideoUnityQuality.Type.Low;
    return type;
  }

  private void SetCurrentResolution(UnityEngine.Resolution inResolution, bool inSave)
  {
    this.currentResolution[0] = inResolution.width;
    this.currentResolution[1] = inResolution.height;
    if (!inSave)
      return;
    this.mManager.SetSetting(Preference.pName.Video_Resolution, (object) this.GetVideoCurrentResolution(), true);
    this.mManager.SetSetting(Preference.pName.Video_Resolution, (object) this.GetVideoCurrentResolution(), false);
  }

  private void SetCurrentResolution(int inResolutionWidth, int inResolutionHeight, bool inSave)
  {
    this.currentResolution[0] = inResolutionWidth;
    this.currentResolution[1] = inResolutionHeight;
    if (!inSave)
      return;
    this.mManager.SetSetting(Preference.pName.Video_Resolution, (object) this.GetVideoCurrentResolution(), true);
    this.mManager.SetSetting(Preference.pName.Video_Resolution, (object) this.GetVideoCurrentResolution(), false);
  }

  public bool CheckResolution(UnityEngine.Resolution inResolution)
  {
    for (int index = 0; index < this.availableResolutions.Count; ++index)
    {
      if (this.availableResolutions[index].width == inResolution.width && this.availableResolutions[index].height == inResolution.height)
        return true;
    }
    return false;
  }

  private bool CheckResolution(int inResolutionWidth, int inResolutionHeight)
  {
    for (int index = 0; index < this.availableResolutions.Count; ++index)
    {
      if (this.availableResolutions[index].width == inResolutionWidth && this.availableResolutions[index].height == inResolutionHeight)
        return true;
    }
    return false;
  }

  public bool GetVideoDisplay(string inDropdownValue)
  {
    string key = inDropdownValue;
    if (key != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (VideoPreferences.\u003C\u003Ef__switch\u0024map2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        VideoPreferences.\u003C\u003Ef__switch\u0024map2 = new Dictionary<string, int>(2)
        {
          {
            "Windowed",
            0
          },
          {
            "Fullscreen",
            1
          }
        };
      }
      int num;
      // ISSUE: reference to a compiler-generated field
      if (VideoPreferences.\u003C\u003Ef__switch\u0024map2.TryGetValue(key, out num))
      {
        if (num == 0)
          return false;
        if (num == 1)
          return true;
      }
    }
    return true;
  }

  public string GetVideoCurrentDisplay()
  {
    return Screen.fullScreen ? "Fullscreen" : "Windowed";
  }

  public void SetVideoDisplay(bool inFullScreenMode)
  {
    this.currentFullscreenMode = inFullScreenMode;
    Screen.fullScreen = inFullScreenMode;
  }

  public int[] GetVideoResolution(string inDropdownValue)
  {
    string[] strArray = inDropdownValue.Trim().ToLower().Split('x');
    return new int[2]{ int.Parse(strArray[0]), int.Parse(strArray[1]) };
  }

  private string GetVideoResolutionString(int inResolutionWidth, int inResolutionHeight)
  {
    return inResolutionWidth.ToString() + " x " + inResolutionHeight.ToString();
  }

  private string GetVideoCurrentResolution()
  {
    return this.GetVideoResolutionString(this.currentResolution[0], this.currentResolution[1]);
  }

  public static string GetVideoResolution()
  {
    return Screen.width.ToString() + " x " + Screen.height.ToString();
  }

  public void SetQuality(PrefVideoUnityQuality.Type ql)
  {
    switch (ql)
    {
      case PrefVideoUnityQuality.Type.High:
        UnityEngine.QualitySettings.SetQualityLevel(0, true);
        break;
      case PrefVideoUnityQuality.Type.MediumHigh:
        UnityEngine.QualitySettings.SetQualityLevel(1, true);
        break;
      case PrefVideoUnityQuality.Type.MediumLow:
        UnityEngine.QualitySettings.SetQualityLevel(2, true);
        break;
      case PrefVideoUnityQuality.Type.Low:
        UnityEngine.QualitySettings.SetQualityLevel(3, true);
        break;
    }
  }

  public void SetVsync(bool inBool)
  {
    Application.targetFrameRate = 60;
    UnityEngine.QualitySettings.vSyncCount = !inBool ? 0 : 1;
  }

  public void SetAntiAliasing(bool inBoolEnabled)
  {
    Antialiasing.DisableAA = !inBoolEnabled;
  }

  public void SetRunInBackground(bool inBool)
  {
    if (Application.runInBackground == inBool)
      return;
    Application.runInBackground = inBool;
  }

  public void SetPostProcessing(Preference.pName inName, bool inValue)
  {
    switch (inName)
    {
      case Preference.pName.Video_SSR:
        ScreenSpaceReflection.DisableSSR = !inValue;
        foreach (ScreenSpaceReflection screenSpaceReflection in Resources.FindObjectsOfTypeAll<ScreenSpaceReflection>())
        {
          if (screenSpaceReflection.gameObject.activeSelf)
            screenSpaceReflection.enabled = inValue;
        }
        break;
      case Preference.pName.Video_ToneMapping:
        AmplifyColorBase.DisableColourGrading = !inValue;
        if (!inValue)
          break;
        AmplifyColorEffect[] objectsOfTypeAll = Resources.FindObjectsOfTypeAll<AmplifyColorEffect>();
        for (int index = 0; index < objectsOfTypeAll.Length; ++index)
        {
          if (objectsOfTypeAll[index].gameObject.activeSelf)
            objectsOfTypeAll[index].enabled = inValue;
        }
        break;
      case Preference.pName.Video_Bloom:
        UltimateBloom.DisableBloom = !inValue;
        if (!inValue)
          break;
        foreach (UltimateBloom ultimateBloom in Resources.FindObjectsOfTypeAll<UltimateBloom>())
        {
          if (ultimateBloom.gameObject.activeSelf)
            ultimateBloom.enabled = inValue;
        }
        break;
      case Preference.pName.Video_VignetteChromaticAbberation:
        VignetteAndChromaticAberration.DisableVCA = !inValue;
        break;
      case Preference.pName.Video_DOF:
        UnityStandardAssets.ImageEffects.DepthOfField.DisableDOF = !inValue;
        UnityStandardAssets.CinematicEffects.DepthOfField.DisableDOF = !inValue;
        foreach (UnityEngine.Behaviour behaviour in Resources.FindObjectsOfTypeAll<UnityStandardAssets.CinematicEffects.DepthOfField>())
          behaviour.enabled = inValue;
        foreach (UnityEngine.Behaviour behaviour in Resources.FindObjectsOfTypeAll<UnityStandardAssets.ImageEffects.DepthOfField>())
          behaviour.enabled = inValue;
        break;
      case Preference.pName.Video_TiltShift:
        TiltShift.DisableTS = !inValue;
        break;
    }
  }

  public void SetBasicDetail(Preference.pName inName, bool inValue)
  {
    if (!Game.IsActive())
      return;
    foreach (BaseScene activeBaseScene in App.instance.baseSceneManager.GetActiveBaseScenes())
    {
      if ((UnityEngine.Object) activeBaseScene != (UnityEngine.Object) null)
        activeBaseScene.SetEnvPreference(inName, inValue);
    }
  }

  public void SetVideoResolution(int inResolutionWidth, int inResolutionHeight, bool inFullScreenMode)
  {
    Screen.fullScreen = inFullScreenMode;
    App.instance.StartCoroutine(this.WaitSetResolution(inResolutionWidth, inResolutionHeight, inFullScreenMode));
  }

  [DebuggerHidden]
  private IEnumerator WaitSetResolution(int inResolutionWidth, int inResolutionHeight, bool inFullScreenMode)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new VideoPreferences.\u003CWaitSetResolution\u003Ec__Iterator1() { inResolutionWidth = inResolutionWidth, inResolutionHeight = inResolutionHeight, inFullScreenMode = inFullScreenMode, \u003C\u0024\u003EinResolutionWidth = inResolutionWidth, \u003C\u0024\u003EinResolutionHeight = inResolutionHeight, \u003C\u0024\u003EinFullScreenMode = inFullScreenMode };
  }

  public void SetRunning2DMode(bool inEnabled)
  {
    this.mIsRunning2DMode = inEnabled;
    if (this.mIsRunning2DMode)
      SceneManager.instance.DisableAllScenes();
    this.SetSimulationRunningMode2D(inEnabled);
  }

  public void SetSimulationRunningMode2D(bool inEnabled)
  {
    this.mSimulationIsRunning2DMode = inEnabled;
  }
}
