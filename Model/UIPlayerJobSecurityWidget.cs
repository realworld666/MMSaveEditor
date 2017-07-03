// Decompiled with JetBrains decompiler
// Type: UIPlayerJobSecurityWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIPlayerJobSecurityWidget : MonoBehaviour
{
  public UIGauge security;
  private Person mPerson;
  private float mSecurity;
  private float mPreviousSecurity;

  private void Update()
  {
    if (this.mPerson == null || (double) this.mPreviousSecurity == (double) this.mSecurity)
      return;
    this.mPreviousSecurity = this.mSecurity;
    this.security.SetValue(this.mSecurity, UIGauge.AnimationSetting.Animate);
  }

  public void Setup(Person inPerson)
  {
    if (inPerson == null)
      return;
    this.mPerson = inPerson;
    this.mSecurity = (float) Random.Range(10, 101);
    this.mPreviousSecurity = this.mSecurity;
    this.security.SetValueRange(0.0f, 100f);
    this.security.SetValue(this.mSecurity, UIGauge.AnimationSetting.Animate);
  }
}
