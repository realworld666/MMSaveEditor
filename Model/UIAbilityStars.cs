// Decompiled with JetBrains decompiler
// Type: UIAbilityStars
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UIAbilityStars : MonoBehaviour
{
  private const float scaleFactor = 4f;
  private const float scaleMax = 5f;
  [SerializeField]
  private Image potential;
  [SerializeField]
  private Image starsfull;

  public void SetAbilityStarsData(Driver inDriver)
  {
    if (inDriver == null)
      return;
    if (inDriver.CanShowStats())
    {
      GameUtility.SetActive(this.gameObject, true);
      float fillAmount = Mathf.Clamp01(MathsUtility.RoundToScale(inDriver.GetDriverStats().GetAbility(), 4f) / 5f);
      GameUtility.SetImageFillAmountIfDifferent(this.potential, Mathf.Clamp01(MathsUtility.RoundToScale(inDriver.GetDriverStats().GetAbilityPotential(), 4f) / 5f), 1f / 512f);
      GameUtility.SetImageFillAmountIfDifferent(this.starsfull, fillAmount, 1f / 512f);
    }
    else
      GameUtility.SetActive(this.gameObject, false);
  }

  public void SetStarsValue(float inStarsValue, float inAbilityValue)
  {
    GameUtility.SetActive(this.gameObject, true);
    inStarsValue = Mathf.Clamp01(inStarsValue / 5f);
    inAbilityValue = Mathf.Clamp01(inAbilityValue / 5f);
    GameUtility.SetImageFillAmountIfDifferent(this.potential, inAbilityValue, 1f / 512f);
    GameUtility.SetImageFillAmountIfDifferent(this.starsfull, inStarsValue, 1f / 512f);
  }

  public void SetAbilityStarsData(Person inPerson)
  {
    if (inPerson == null || inPerson.GetStats() == null)
      return;
    GameUtility.SetActive(this.gameObject, true);
    float fillAmount = Mathf.Clamp01(MathsUtility.RoundToScale(inPerson.GetStats().GetAbility(), 4f) / 5f);
    GameUtility.SetImageFillAmountIfDifferent(this.potential, Mathf.Clamp01(MathsUtility.RoundToScale(inPerson.GetStats().GetAbilityPotential(), 4f) / 5f), 1f / 512f);
    GameUtility.SetImageFillAmountIfDifferent(this.starsfull, fillAmount, 1f / 512f);
  }
}
