// Decompiled with JetBrains decompiler
// Type: UIAbilityStar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UIAbilityStar : MonoBehaviour
{
  [SerializeField]
  private Image starFull;
  [SerializeField]
  private Image potentialFull;

  public void SetValue(float inPotential, float inStar)
  {
    this.potentialFull.fillAmount = inPotential;
    this.starFull.fillAmount = inStar;
  }
}
