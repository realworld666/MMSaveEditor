// Decompiled with JetBrains decompiler
// Type: UIDriverScreenMarketabilityWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDriverScreenMarketabilityWidget : MonoBehaviour
{
  public Slider marketability;
  public TextMeshProUGUI marketabilityLabel;
  public GameObject detailsObject;
  public GameObject noDetailsObject;
  public GameObject positiveIconModifier;
  public GameObject negativeIconModifier;
  private Driver mDriver;

  public void Setup(Driver inDriver)
  {
    if (inDriver == null)
      return;
    this.mDriver = inDriver;
    if (!this.mDriver.CanShowStats())
    {
      GameUtility.SetActive(this.detailsObject, false);
      GameUtility.SetActive(this.noDetailsObject, true);
      GameUtility.SetActive(this.positiveIconModifier, false);
      GameUtility.SetActive(this.negativeIconModifier, false);
    }
    else
    {
      GameUtility.SetActive(this.detailsObject, true);
      GameUtility.SetActive(this.noDetailsObject, false);
      this.marketability.value = this.mDriver.GetDriverStats().marketability;
      this.marketabilityLabel.text = GameUtility.GetPercentageText(this.mDriver.GetDriverStats().marketability, 1f, false, false);
      float singleModifierForStat = this.mDriver.personalityTraitController.GetSingleModifierForStat(PersonalityTrait.StatModified.Marketability);
      GameUtility.SetActive(this.positiveIconModifier, (double) singleModifierForStat > 0.0);
      GameUtility.SetActive(this.negativeIconModifier, (double) singleModifierForStat < 0.0);
    }
  }

  private void OnMouseEnter()
  {
    if (!this.mDriver.CanShowStats() || !this.mDriver.personalityTraitController.IsModifingStat(PersonalityTrait.StatModified.Marketability))
      return;
    UIManager.instance.dialogBoxManager.GetDialog<DriverStatsModifiersRollover>().ShowRollover(this.mDriver.GetDriverStats().marketability, this.mDriver, PersonalityTrait.StatModified.Marketability, string.Empty, false);
  }

  private void OnMouseExit()
  {
    UIManager.instance.dialogBoxManager.GetDialog<DriverStatsModifiersRollover>().HideRollover();
  }
}
