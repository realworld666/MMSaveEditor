// Decompiled with JetBrains decompiler
// Type: BaseScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FluffyUnderware.Curvy;
using System.Collections.Generic;
using UnityEngine;
using uSky;

public class BaseScene : MonoBehaviour
{
  private Dictionary<Light, LightShadows> mLights = new Dictionary<Light, LightShadows>();
  private PreferenceTag[] mTags;
  private FollowSpline[] mTrafficComponents;
  private PreferencesManager mPreferencesManager;
  private uSkyManager[] mSkyManagers;
  private uSkyFog mSkyFog;
  private uSkyFogCamera[] mSkyCameras;

  private void OnEnable()
  {
    if (!((Object) App.instance != (Object) null))
      return;
    App.instance.baseSceneManager.RegisterBaseScene(this);
    this.SetPreferences();
  }

  private void OnDisable()
  {
    if (!((Object) App.instance != (Object) null))
      return;
    App.instance.baseSceneManager.UnregisterBaseScene(this);
  }

  public void OnSceneLoad()
  {
    if (this.mLights.Count <= 0)
      this.CreateLightDictionary();
    if (this.mTags == null)
      this.mTags = this.gameObject.GetComponentsInChildren<PreferenceTag>(true);
    if (this.mTrafficComponents == null)
      this.mTrafficComponents = this.gameObject.GetComponentsInChildren<FollowSpline>(true);
    if (this.mSkyManagers == null)
      this.mSkyManagers = this.gameObject.GetComponentsInChildren<uSkyManager>();
    if ((Object) this.mSkyFog == (Object) null)
      this.mSkyFog = this.gameObject.GetComponentInChildren<uSkyFog>(true);
    if (this.mSkyCameras == null)
      this.mSkyCameras = this.gameObject.GetComponentsInChildren<uSkyFogCamera>(true);
    this.LoadSky();
    this.SetPreferences();
  }

  public void SetPreferences()
  {
    this.mPreferencesManager = App.instance.preferencesManager;
    this.SetEnvPreference(Preference.pName.Video_WorldDetail, this.mPreferencesManager.GetSettingBool(Preference.pName.Video_WorldDetail, false));
    this.SetEnvPreference(Preference.pName.Video_Traffic, this.mPreferencesManager.GetSettingBool(Preference.pName.Video_Traffic, false));
    this.SetEnvPreference(Preference.pName.Video_Crowds, this.mPreferencesManager.GetSettingBool(Preference.pName.Video_Crowds, false));
    this.SetEnvPreference(Preference.pName.Video_Decals, this.mPreferencesManager.GetSettingBool(Preference.pName.Video_Decals, false));
    this.SetEnvPreference(Preference.pName.Video_VFX, this.mPreferencesManager.GetSettingBool(Preference.pName.Video_VFX, false));
    this.SetEnvPreference(Preference.pName.Video_WaterQuality, this.mPreferencesManager.GetSettingBool(Preference.pName.Video_WaterQuality, false));
    this.SetEnvPreference(Preference.pName.Video_DynamicLights, this.mPreferencesManager.GetSettingBool(Preference.pName.Video_DynamicLights, false));
  }

  public void EnableTraffic()
  {
    if (this.mTrafficComponents == null)
      return;
    for (int index = 0; index < this.mTrafficComponents.Length; ++index)
      this.mTrafficComponents[index].enabled = true;
  }

  public void DisableTraffic()
  {
    if (this.mTrafficComponents == null)
      return;
    for (int index = 0; index < this.mTrafficComponents.Length; ++index)
      this.mTrafficComponents[index].enabled = false;
  }

  private void LoadSky()
  {
    if (this.mSkyManagers != null)
    {
      for (int index = 0; index < this.mSkyManagers.Length; ++index)
        this.mSkyManagers[index].OnLoad();
    }
    if ((Object) this.mSkyFog != (Object) null)
    {
      this.mSkyFog.uSkyCameras = this.mSkyCameras;
      this.mSkyFog.OnLoad();
    }
    if (this.mSkyCameras == null)
      return;
    for (int index = 0; index < this.mSkyCameras.Length; ++index)
    {
      this.mSkyCameras[index].fog = this.mSkyFog;
      this.mSkyCameras[index].OnLoad();
    }
  }

  public void LoadMainCameraFog()
  {
    uSkyFogCamera component = App.instance.cameraManager.gameCamera.freeRoamCamera.GetComponent<uSkyFogCamera>();
    component.fog = this.mSkyFog;
    component.OnLoad();
  }

  private void CreateLightDictionary()
  {
    this.mLights.Clear();
    foreach (Light componentsInChild in this.gameObject.GetComponentsInChildren<Light>(true))
      this.mLights.Add(componentsInChild, componentsInChild.shadows);
  }

  public void SetShadows(bool inValue)
  {
    if (this.mLights.Count <= 0)
      return;
    Light[] array = new Light[this.mLights.Count];
    this.mLights.Keys.CopyTo(array, 0);
    for (int index1 = 0; index1 < array.Length; ++index1)
    {
      Light index2 = array[index1];
      index2.shadows = !inValue ? LightShadows.None : this.mLights[index2];
    }
  }

  public virtual void SetEnvPreference(Preference.pName inName, bool inValue)
  {
    switch (inName)
    {
      case Preference.pName.Video_WorldDetail:
        this.ToggleTags(PreferenceTag.Type.WorldDetail, inValue);
        break;
      case Preference.pName.Video_Traffic:
        this.ToggleTags(PreferenceTag.Type.Traffic, inValue);
        break;
      case Preference.pName.Video_Crowds:
        this.ToggleTags(PreferenceTag.Type.Crowds, inValue);
        break;
      case Preference.pName.Video_Decals:
        this.ToggleTags(PreferenceTag.Type.Decals, inValue);
        break;
      case Preference.pName.Video_VFX:
        this.ToggleTags(PreferenceTag.Type.VFX, inValue);
        break;
      case Preference.pName.Video_WaterQuality:
        this.ToggleTags(PreferenceTag.Type.WaterHigh, inValue);
        this.ToggleTags(PreferenceTag.Type.WaterLow, !inValue);
        break;
    }
  }

  private void ToggleTags(PreferenceTag.Type inType, bool inValue)
  {
    if (this.mTags == null)
      return;
    for (int index = 0; index < this.mTags.Length; ++index)
    {
      PreferenceTag mTag = this.mTags[index];
      if (mTag.type == inType)
        GameUtility.SetActive(mTag.gameObject, inValue);
    }
  }
}
