// Decompiled with JetBrains decompiler
// Type: UISponsorsEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISponsorsEntry : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler
{
  [SerializeField]
  private Toggle toggle;
  [SerializeField]
  private UISponsorLogo logo;
  [SerializeField]
  private TextMeshProUGUI remainingLength;
  [SerializeField]
  private UISponsorsSlot sponsorSlot;
  [SerializeField]
  private SponsorsScreen screen;
  private SponsorshipDeal mSponsorshipDeal;

  protected void Awake()
  {
    GameUtility.Assert((Object) this.toggle != (Object) null, "UISponsorsEntry.toggle != null", (Object) this);
    GameUtility.Assert((Object) this.logo != (Object) null, "UISponsorsEntry.logo != null", (Object) this);
    GameUtility.Assert((Object) this.remainingLength != (Object) null, "UISponsorsEntry.remainingLength != null", (Object) this);
    GameUtility.Assert((Object) this.screen != (Object) null, "UISponsorsEntry.screen != null", (Object) this);
  }

  public void Setup(SponsorshipDeal inSponsorshipDeal)
  {
    if (inSponsorshipDeal == null)
      return;
    this.toggle.onValueChanged.RemoveAllListeners();
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle()));
    this.mSponsorshipDeal = inSponsorshipDeal;
    this.SetSponsorDetails();
  }

  private void SetSponsorDetails()
  {
    this.logo.SetSponsor(this.mSponsorshipDeal.sponsor);
    StringVariableParser.intValue1 = this.mSponsorshipDeal.contract.contractRacesLeft;
    this.remainingLength.text = Localisation.LocaliseID(this.mSponsorshipDeal.contract.contractRacesLeft != 1 ? "PSG_10010469" : "PSG_10010929", (GameObject) null);
  }

  public void SetSponsorSlots()
  {
    UISponsorsPanel.SponsorData sponsorData = this.screen.panelWidget.GetSponsorData(this.mSponsorshipDeal);
    if (sponsorData == null)
      return;
    this.sponsorSlot.Setup(sponsorData.slot.number, sponsorData.slot.index, this.screen);
  }

  public void OnPointerEnter(PointerEventData inEventData)
  {
    UIManager.instance.dialogBoxManager.GetDialog<UISponsorsOverview>().Open(this.mSponsorshipDeal);
  }

  private void OnToggle()
  {
  }
}
