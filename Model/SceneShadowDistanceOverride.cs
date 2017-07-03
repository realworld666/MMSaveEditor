// Decompiled with JetBrains decompiler
// Type: SceneShadowDistanceOverride
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SceneShadowDistanceOverride : MonoBehaviour
{
  public float mShadowDistanceDivisor = 5f;
  private float mOverriddenDistance;

  private void OnEnable()
  {
    this.mOverriddenDistance = QualitySettings.shadowDistance;
    QualitySettings.shadowDistance = this.mOverriddenDistance / this.mShadowDistanceDivisor;
  }

  private void OnDisable()
  {
    QualitySettings.shadowDistance = this.mOverriddenDistance;
  }
}
