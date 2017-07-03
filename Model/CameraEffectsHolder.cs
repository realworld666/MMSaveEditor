// Decompiled with JetBrains decompiler
// Type: CameraEffectsHolder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CameraEffectsHolder : MonoBehaviour
{
  public CameraEffectsScriptableObject mEffectsScriptableObject;

  public void SetScriptableObject(ref CameraEffectsScriptableObject so)
  {
    if (!((Object) this.mEffectsScriptableObject != (Object) so))
      return;
    this.mEffectsScriptableObject = so;
    this.LoadSettingsFromScriptableObject();
  }

  public void LoadSettingsFromScriptableObject()
  {
    if (!((Object) this.mEffectsScriptableObject != (Object) null))
      return;
    this.LoadSettingsFromScriptableObject(ref this.mEffectsScriptableObject);
  }

  protected void LoadSettingsFromScriptableObject(ref CameraEffectsScriptableObject so)
  {
    if (!((Object) so != (Object) null))
      return;
    AmplifyColorEffect component1 = this.gameObject.GetComponent<AmplifyColorEffect>();
    if ((Object) component1 != (Object) null)
    {
      component1.enabled = so.UseAmplifyColor;
      component1.QualityLevel = so.QualityLevel;
      component1.BlendAmount = so.BlendAmount;
      component1.LutTexture = so.LutTexture;
      component1.LutBlendTexture = so.LutBlendTexture;
      component1.MaskTexture = so.MaskTexture;
      component1.UseVolumes = so.UseVolumes;
      component1.ExitVolumeBlendTime = so.ExitVolumeBlendTime;
      component1.TriggerVolumeProxy = so.TriggerVolumeProxy;
      component1.VolumeCollisionMask = so.VolumeCollisionMask;
    }
    else if (so.UseAmplifyColor)
      Debug.LogWarning((object) ("Please add the Amplify Color Effect Component to " + this.gameObject.name), (Object) null);
    UltimateBloom component2 = this.gameObject.GetComponent<UltimateBloom>();
    if ((Object) component2 != (Object) null)
    {
      component2.enabled = so.UseUltimateBloom;
      component2.m_HDR = so.m_HDR;
      component2.m_InvertImage = so.m_InvertImage;
      component2.m_BloomIntensity = so.m_BloomIntensity;
      component2.m_DownscaleCount = so.m_DownscaleCount;
      component2.m_BloomIntensities = so.m_BloomIntensities;
      component2.m_BloomColors = so.m_BloomColors;
      component2.m_BloomUsages = so.m_BloomUsages;
      component2.m_TemporalStableDownsampling = so.m_TemporalStableDownsampling;
      component2.m_SamplingMode = so.m_SamplingMode;
      component2.m_UpsamplingQuality = so.m_UpsamplingQuality;
      component2.m_SamplingMinHeight = so.m_SamplingMinHeight;
      component2.m_IntensityManagement = so.m_IntensityManagement;
      component2.m_BloomThreshhold = so.m_BloomThreshhold;
      component2.m_BloomCurve = so.m_BloomCurve;
      component2.m_DirectDownSample = so.m_DirectDownSample;
      component2.m_DirectUpsample = so.m_DirectUpsample;
      component2.m_UseLensDust = so.m_UseLensDust;
      component2.m_DustIntensity = so.m_DustIntensity;
      component2.m_DustTexture = so.m_DustTexture;
      component2.m_DirtLightIntensity = so.m_DirtLightIntensity;
      component2.m_UseLensFlare = so.m_UseLensFlare;
      component2.m_FlareRendering = so.m_FlareRendering;
      component2.m_FlareIntensity = so.m_FlareIntensity;
      component2.m_FlareTreshold = so.m_FlareTreshold;
      component2.m_FlareScales = so.m_FlareScales;
      component2.m_FlareScalesNear = so.m_FlareScalesNear;
      component2.m_FlareTint0 = so.m_FlareTint0;
      component2.m_FlareTint1 = so.m_FlareTint1;
      component2.m_FlareTint2 = so.m_FlareTint2;
      component2.m_FlareTint3 = so.m_FlareTint3;
      component2.m_FlareTint4 = so.m_FlareTint4;
      component2.m_FlareTint5 = so.m_FlareTint5;
      component2.m_FlareTint6 = so.m_FlareTint6;
      component2.m_FlareTint7 = so.m_FlareTint7;
      component2.m_FlareMask = so.m_FlareMask;
      component2.m_UseBokehFlare = so.m_UseBokehFlare;
      component2.m_BokehFlareQuality = so.m_BokehFlareQuality;
      component2.m_BokehScale = so.m_BokehScale;
      component2.m_FlareShape = so.m_FlareShape;
      component2.m_UseAnamorphicFlare = so.m_UseAnamorphicFlare;
      component2.m_AnamorphicDownscaleCount = so.m_AnamorphicDownscaleCount;
      component2.m_AnamorphicFlareIntensity = so.m_AnamorphicFlareIntensity;
      component2.m_AnamorphicScale = so.m_AnamorphicScale;
      component2.m_AnamorphicBlurPass = so.m_AnamorphicBlurPass;
      component2.m_AnamorphicSmallVerticalBlur = so.m_AnamorphicSmallVerticalBlur;
      component2.m_AnamorphicDirection = so.m_AnamorphicDirection;
      component2.m_AnamorphicBloomIntensities = so.m_AnamorphicBloomIntensities;
      component2.m_AnamorphicBloomColors = so.m_AnamorphicBloomColors;
      component2.m_AnamorphicBloomUsages = so.m_AnamorphicBloomUsages;
      component2.m_UseStarFlare = so.m_UseStarFlare;
      component2.m_StarDownscaleCount = so.m_StarDownscaleCount;
      component2.m_StarFlareIntensity = so.m_StarFlareIntensity;
      component2.m_StarScale = so.m_StarScale;
      component2.m_StarBlurPass = so.m_StarBlurPass;
      component2.m_StarBloomIntensities = so.m_StarBloomIntensities;
      component2.m_StarBloomColors = so.m_StarBloomColors;
      component2.m_StarBloomUsages = so.m_StarBloomUsages;
    }
    else
    {
      if (!so.UseUltimateBloom)
        return;
      Debug.LogWarning((object) ("Please add the Ultimate Bloom Component to " + this.gameObject.name), (Object) null);
    }
  }
}
