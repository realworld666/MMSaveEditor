// Decompiled with JetBrains decompiler
// Type: UICarStatIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class UICarStatIcon : MonoBehaviour
{
  private CarStats.StatType mStat = CarStats.StatType.Acceleration;
  private Animator mAnimator;
  private TextMeshProUGUI mLabel;

  public CarStats.StatType stat
  {
    get
    {
      return this.mStat;
    }
  }

  public Animator animator
  {
    get
    {
      return this.mAnimator;
    }
  }

  public void SetIcon(CarStats.StatType inStat, Championship.Series inSeries)
  {
    this.mStat = inStat;
    this.mAnimator = this.gameObject.GetComponent<Animator>();
    Transform transform = (Transform) null;
    switch (inSeries)
    {
      case Championship.Series.SingleSeaterSeries:
        transform = this.transform.GetChild(2);
        GameUtility.SetActive(this.transform.GetChild(1).gameObject, false);
        break;
      case Championship.Series.GTSeries:
        transform = this.transform.GetChild(1);
        GameUtility.SetActive(this.transform.GetChild(2).gameObject, false);
        break;
    }
    GameUtility.SetActive(transform.gameObject, true);
    for (int index = 0; index < transform.childCount; ++index)
    {
      if ((CarStats.StatType) index == this.mStat)
        transform.GetChild(index).gameObject.SetActive(true);
      else
        transform.GetChild(index).gameObject.SetActive(false);
    }
    if (this.transform.childCount <= 1 || this.transform.GetChild(0).childCount <= 0)
      return;
    this.mLabel = this.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
    if (!((UnityEngine.Object) this.mLabel != (UnityEngine.Object) null))
      return;
    this.mLabel.text = Localisation.LocaliseEnum((Enum) this.mStat);
  }
}
