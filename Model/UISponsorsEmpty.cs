// Decompiled with JetBrains decompiler
// Type: UISponsorsEmpty
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISponsorsEmpty : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler
{
  private SponsorSlot.SlotType mSlotType = SponsorSlot.SlotType.AirIntake;
  public Button slotButton;
  public TextMeshProUGUI slotTitle;
  public TextMeshProUGUI slotPart;
  public TextMeshProUGUI offersAvailable;
  public UISponsorsCamera sponsorCamera;
  public GameObject noOffers;
  public SponsorsScreen screen;
  public UINotification notificationNumber;
  private bool mHasOffer;

  public void Setup(int inSlotNumber, int inSlotID, SponsorsScreen inSponsorsScreen)
  {
    this.mSlotType = (SponsorSlot.SlotType) inSlotID;
    this.mHasOffer = Game.instance.player.team.sponsorController.GetOffersCountForSlot(this.mSlotType) > 0;
    GameUtility.SetActive(this.slotButton.gameObject, this.mHasOffer);
    if (this.mHasOffer)
    {
      this.slotButton.onClick.RemoveAllListeners();
      this.slotButton.onClick.AddListener(new UnityAction(this.OnButton));
      int offersCountForSlot = Game.instance.player.team.sponsorController.GetOffersCountForSlot(this.mSlotType);
      StringVariableParser.intValue1 = offersCountForSlot;
      this.offersAvailable.text = offersCountForSlot != 1 ? Localisation.LocaliseID("PSG_10010492", (GameObject) null) : Localisation.LocaliseID("PSG_10010493", (GameObject) null);
    }
    else
      this.offersAvailable.text = Localisation.LocaliseID("PSG_10008582", (GameObject) null);
    GameUtility.SetActive(this.noOffers, !this.mHasOffer);
    this.slotTitle.text = Localisation.LocaliseID(UISponsorsSlot.SlotTags[inSlotNumber - 1], (GameObject) null);
    this.slotPart.text = SponsorSlot.GetSlotNameString(this.mSlotType);
    this.notificationNumber.notificationName = "NewSponsorOffer" + this.mSlotType.ToString();
    this.notificationNumber.FindNotification();
    this.sponsorCamera.Setup(inSlotID);
    GameUtility.SetActive(this.sponsorCamera.gameObject, inSponsorsScreen.screenMode == UIScreen.ScreenMode.Mode3D);
  }

  public void OnButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    UIManager.instance.dialogBoxManager.GetDialog<UISponsorsOffers>().Open(this.mSlotType);
  }

  public void OnPointerEnter(PointerEventData inEventData)
  {
    UIManager.instance.dialogBoxManager.GetDialog<UISponsorsOverview>().Close();
  }
}
