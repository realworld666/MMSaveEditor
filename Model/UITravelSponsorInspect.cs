// Decompiled with JetBrains decompiler
// Type: UITravelSponsorInspect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;

public class UITravelSponsorInspect : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
  public RectTransform rectTransform;
  public TravelArrangementsScreen screen;
  private SponsorshipDeal mSponsorshipDeal;
  private UITravelSponsorBonusPopup mTooltip;

  public void Setup(SponsorshipDeal inSponsorshipDeal)
  {
    if (inSponsorshipDeal == null)
      return;
    this.mSponsorshipDeal = inSponsorshipDeal;
  }

  public void OnPointerEnter(PointerEventData inEventData)
  {
    if (this.mSponsorshipDeal == null)
      return;
    if ((Object) this.mTooltip == (Object) null)
      this.mTooltip = UIManager.instance.dialogBoxManager.GetDialog<UITravelSponsorBonusPopup>();
    this.mTooltip.Open(this.mSponsorshipDeal, this.rectTransform);
  }

  public void OnPointerExit(PointerEventData inEventData)
  {
    if ((Object) this.mTooltip == (Object) null)
      this.mTooltip = UIManager.instance.dialogBoxManager.GetDialog<UITravelSponsorBonusPopup>();
    this.mTooltip.Close();
  }
}
