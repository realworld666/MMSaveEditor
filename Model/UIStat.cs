// Decompiled with JetBrains decompiler
// Type: UIStat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStat : MonoBehaviour
{
  private PersonalityTrait.StatModified mStatModified = PersonalityTrait.StatModified.Adaptability;
  [SerializeField]
  private TextMeshProUGUI headingLabel;
  [SerializeField]
  private TextMeshProUGUI statValueLabel;
  [SerializeField]
  private Image fill;
  [SerializeField]
  private Image statBacking;
  [SerializeField]
  private UIGridList modifierIcons;
  [SerializeField]
  private GameObject traitIcon;
  [SerializeField]
  private GameObject positiveModifierIcon;
  [SerializeField]
  private GameObject negativeModifierIcon;
  private Driver mDriver;
  private Person mPerson;
  private string mHeading;
  private bool mCanRollover;

  public void SetStat(string inHeading, float inValue, Person inPerson)
  {
    this.mHeading = inHeading;
    this.mDriver = (Driver) null;
    this.mPerson = inPerson;
    int num1 = (int) inValue;
    this.headingLabel.text = inHeading;
    if (inPerson.CanShowStats())
    {
      this.mCanRollover = true;
      float num2 = Mathf.Clamp01((float) num1 / 20f);
      this.statValueLabel.text = num1.ToString();
      this.fill.fillAmount = inValue - (float) num1;
      Color color = UIConstants.colorBandGreen;
      if ((double) num2 <= 0.25)
        color = UIConstants.colorBandRed;
      else if ((double) num2 <= 0.5)
        color = UIConstants.colorBandYellow;
      else if ((double) num2 <= 0.75)
        color = UIConstants.colorBandBlue;
      color.a = this.fill.color.a;
      this.fill.color = color;
      color.a = this.statBacking.color.a;
      this.statBacking.color = color;
    }
    else
    {
      this.mCanRollover = false;
      this.statValueLabel.text = inPerson.GetStats().GetStatRanges(inHeading);
      this.fill.fillAmount = 0.0f;
      Color whiteColor = UIConstants.whiteColor;
      whiteColor.a = this.fill.color.a;
      this.fill.color = whiteColor;
      whiteColor.a = this.statBacking.color.a;
      this.statBacking.color = whiteColor;
    }
  }

  public void SetupForDriverStatsModifiersRollover(Driver inDriver, PersonalityTrait.StatModified inStatModified)
  {
    this.modifierIcons.DestroyListItems();
    if (inDriver.CanShowStats())
    {
      this.mCanRollover = true;
      this.mDriver = inDriver;
      this.mStatModified = inStatModified;
      bool flag = this.mDriver.personalityTraitController.IsModifingStat(inStatModified);
      float num1 = 0.0f;
      if (inStatModified == PersonalityTrait.StatModified.Feedback && inDriver.IsPlayersDriver() && (double) Game.instance.player.driverFeedBackStatModifier > 0.0)
      {
        flag = true;
        num1 += Game.instance.player.driverFeedBackStatModifier;
      }
      if (flag)
      {
        float num2 = num1 + this.mDriver.personalityTraitController.GetSingleModifierForStat(inStatModified);
        GameUtility.SetActive(this.positiveModifierIcon, (double) num2 > 0.0);
        GameUtility.SetActive(this.negativeModifierIcon, (double) num2 < 0.0);
        GameUtility.SetActive(this.traitIcon, true);
      }
      else
      {
        GameUtility.SetActive(this.positiveModifierIcon, false);
        GameUtility.SetActive(this.negativeModifierIcon, false);
        GameUtility.SetActive(this.traitIcon, false);
      }
    }
    else
    {
      this.mCanRollover = false;
      GameUtility.SetActive(this.positiveModifierIcon, false);
      GameUtility.SetActive(this.negativeModifierIcon, false);
      GameUtility.SetActive(this.traitIcon, false);
      this.mDriver = (Driver) null;
    }
  }

  public void OnMouseEnter()
  {
    if (!this.mCanRollover)
      return;
    DriverStatsModifiersRollover dialog = UIManager.instance.dialogBoxManager.GetDialog<DriverStatsModifiersRollover>();
    if (this.mDriver != null)
      dialog.ShowRollover(this.fill.fillAmount, this.mDriver, this.mStatModified, this.mHeading, true);
    else
      dialog.ShowRollover(this.fill.fillAmount, this.mPerson, this.mHeading);
  }

  public void OnMouseExit()
  {
    if (!this.mCanRollover)
      return;
    UIManager.instance.dialogBoxManager.GetDialog<DriverStatsModifiersRollover>().HideRollover();
  }
}
