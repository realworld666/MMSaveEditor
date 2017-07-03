// Decompiled with JetBrains decompiler
// Type: UITravelSponsorBonusPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UITravelSponsorBonusPopup : UIDialogBox
{
  public RectTransform rectTransform;
  public TextMeshProUGUI qualifyingBonus;
  public TextMeshProUGUI qualifyingTarget;
  public TextMeshProUGUI raceBonus;
  public TextMeshProUGUI raceTarget;
  public TextMeshProUGUI combinedBonus;
  private SponsorshipDeal mSponsorshipDeal;

  public void Open(SponsorshipDeal inSponsorshipDeal, RectTransform inRectTransform)
  {
    if (inSponsorshipDeal == null)
      return;
    this.mSponsorshipDeal = inSponsorshipDeal;
    this.gameObject.SetActive(true);
    GameUtility.SetTooltipTransformInsideScreen(this.rectTransform, inRectTransform, new Vector3(), false, (RectTransform) null);
    Vector2 anchoredPosition = this.rectTransform.anchoredPosition;
    anchoredPosition.x += 20f;
    this.rectTransform.anchoredPosition = anchoredPosition;
    this.SetDetails();
  }

  private void SetDetails()
  {
    this.qualifyingBonus.text = GameUtility.GetCurrencyString((long) this.mSponsorshipDeal.qualifyingObjective.financialReward, 0);
    this.qualifyingTarget.text = this.mSponsorshipDeal.qualifyingObjective.objectiveText;
    this.raceBonus.text = GameUtility.GetCurrencyString((long) this.mSponsorshipDeal.raceObjective.financialReward, 0);
    this.raceTarget.text = this.mSponsorshipDeal.raceObjective.objectiveText;
    this.combinedBonus.text = GameUtility.GetCurrencyString((long) this.mSponsorshipDeal.GetObjectivesTotalBonus(), 0);
  }

  public void Close()
  {
    this.gameObject.SetActive(false);
  }
}
