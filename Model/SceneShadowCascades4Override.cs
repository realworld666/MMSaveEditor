// Decompiled with JetBrains decompiler
// Type: SceneShadowCascades4Override
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SceneShadowCascades4Override : MonoBehaviour
{
  private Vector3 mSavedShadowCascades = Vector3.zero;
  public bool updateRealTime;
  [Range(0.0f, 100f)]
  public float cascadeSplit1;
  [Range(0.0f, 100f)]
  public float cascadeSplit2;
  [Range(0.0f, 100f)]
  public float cascadeSplit3;

  private void OnEnable()
  {
    this.mSavedShadowCascades.Set(QualitySettings.shadowCascade4Split.x, QualitySettings.shadowCascade4Split.y, QualitySettings.shadowCascade4Split.z);
    QualitySettings.shadowCascade4Split = new Vector3(Mathf.Clamp01(this.cascadeSplit1 / 100f), Mathf.Clamp01(this.cascadeSplit2 / 100f), Mathf.Clamp01(this.cascadeSplit3 / 100f));
  }

  private void OnDisable()
  {
    QualitySettings.shadowCascade4Split = new Vector3(this.mSavedShadowCascades.x, this.mSavedShadowCascades.y, this.mSavedShadowCascades.z);
  }
}
