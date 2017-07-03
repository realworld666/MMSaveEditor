// Decompiled with JetBrains decompiler
// Type: CameraEffectsScriptableObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using AmplifyColor;
using System;
using UnityEngine;

[Serializable]
public class CameraEffectsScriptableObject : ScriptableObject
{
  public bool UseAmplifyColor = true;
  public bool UseUltimateBloom = true;
  public bool UseSonicEtherSSAO = true;
  [Space(10f)]
  [Header("Amplify Color")]
  public Quality QualityLevel;
  public float BlendAmount;
  public Texture LutTexture;
  public Texture LutBlendTexture;
  public Texture MaskTexture;
  public bool UseVolumes;
  public float ExitVolumeBlendTime;
  public Transform TriggerVolumeProxy;
  public LayerMask VolumeCollisionMask;
  [Space(10f)]
  [Header("Ultimate Bloom")]
  public UltimateBloom.HDRBloomMode m_HDR;
  public bool m_InvertImage;
  public float m_BloomIntensity;
  public int m_DownscaleCount;
  public float[] m_BloomIntensities;
  public Color[] m_BloomColors;
  public bool[] m_BloomUsages;
  public bool m_TemporalStableDownsampling;
  public UltimateBloom.SamplingMode m_SamplingMode;
  public UltimateBloom.BloomSamplingQuality m_UpsamplingQuality;
  public float m_SamplingMinHeight;
  public UltimateBloom.BloomIntensityManagement m_IntensityManagement;
  public float m_BloomThreshhold;
  public DeluxeFilmicCurve m_BloomCurve;
  public bool m_DirectDownSample;
  public bool m_DirectUpsample;
  public bool m_UseLensDust;
  public float m_DustIntensity;
  public Texture2D m_DustTexture;
  public float m_DirtLightIntensity;
  public bool m_UseLensFlare;
  public UltimateBloom.FlareRendering m_FlareRendering;
  public float m_FlareIntensity;
  public float m_FlareTreshold;
  public Vector4 m_FlareScales;
  public Vector4 m_FlareScalesNear;
  public Color m_FlareTint0;
  public Color m_FlareTint1;
  public Color m_FlareTint2;
  public Color m_FlareTint3;
  public Color m_FlareTint4;
  public Color m_FlareTint5;
  public Color m_FlareTint6;
  public Color m_FlareTint7;
  public Texture2D m_FlareMask;
  public bool m_UseBokehFlare;
  public UltimateBloom.BokehFlareQuality m_BokehFlareQuality;
  public float m_BokehScale;
  public Texture2D m_FlareShape;
  public bool m_UseAnamorphicFlare;
  public int m_AnamorphicDownscaleCount;
  public float m_AnamorphicFlareIntensity;
  public float m_AnamorphicScale;
  public int m_AnamorphicBlurPass;
  public bool m_AnamorphicSmallVerticalBlur;
  public UltimateBloom.AnamorphicDirection m_AnamorphicDirection;
  public float[] m_AnamorphicBloomIntensities;
  public Color[] m_AnamorphicBloomColors;
  public bool[] m_AnamorphicBloomUsages;
  public bool m_UseStarFlare;
  public int m_StarDownscaleCount;
  public float m_StarFlareIntensity;
  public float m_StarScale;
  public int m_StarBlurPass;
  public float[] m_StarBloomIntensities;
  public Color[] m_StarBloomColors;
  public bool[] m_StarBloomUsages;
  [Header("Sonic Ether SSAO")]
  [Space(10f)]
  public float occlusionIntensity;
  public float colorBleedAmount;
  public float radius;
  public float sampleDistributionCurve;
  public float drawDistance;
  public float drawDistanceFadeSize;
  public float bias;
  public float zThickness;
  public float bilateralDepthTolerance;
  public float brightnessThreshold;
  public bool reduceSelfBleeding;
  public bool preserveDetails;
  public bool useDownsampling;
  public bool halfSampling;
}
