// Decompiled with JetBrains decompiler
// Type: PenaltyEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PenaltyEntry : MonoBehaviour
{
  public Image levelBacking;
  public TextMeshProUGUI levelLabel;
  public TextMeshProUGUI partNameLabel;
  public TextMeshProUGUI penaltyText;
  public Transform partIcon;
  private CarPart mPart;

  public void Setup(PenaltyPartRulesBroken inPenalty)
  {
    this.mPart = inPenalty.part;
    StringVariableParser.intValue1 = inPenalty.placesLost;
    this.penaltyText.text = string.Format("{0}\n{1}", (object) GameUtility.GetCurrencyString(inPenalty.penaltyCashAmount, 0), (object) Localisation.LocaliseID("PSG_10011157", (GameObject) null));
    this.levelBacking.color = UIConstants.GetPartLevelColor(this.mPart.stats.level);
    this.levelLabel.text = this.mPart.GetLevelUIString();
    this.partNameLabel.text = Localisation.LocaliseEnum((Enum) this.mPart.GetPartType());
    CarPart.SetIcon(this.partIcon, this.mPart.GetPartType());
  }

  private void OpenPopup()
  {
    if (this.mPart == null)
      return;
    UIManager.instance.dialogBoxManager.GetDialog<UIPartInfoPopupWidget>().ShowTooltip(this.mPart, (RectTransform) null);
  }

  private void ClosePopup()
  {
    UIManager.instance.dialogBoxManager.GetDialog<UIPartInfoPopupWidget>().HideTooltip();
  }
}
