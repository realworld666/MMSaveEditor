// Decompiled with JetBrains decompiler
// Type: SceneShadowCascades2Override
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SceneShadowCascades2Override : MonoBehaviour
{
  public bool updateRealTime;
  [Range(0.0f, 100f)]
  public float cascadeSplit;
  private float mSavedShadowCascades;

  private void OnEnable()
  {
    this.mSavedShadowCascades = QualitySettings.shadowCascade2Split;
    QualitySettings.shadowCascade2Split = Mathf.Clamp01(this.cascadeSplit / 100f);
  }

  private void OnDisable()
  {
    QualitySettings.shadowCascade2Split = this.mSavedShadowCascades;
  }
}
