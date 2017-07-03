// Decompiled with JetBrains decompiler
// Type: UISponsorsSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UISponsorsSlot : MonoBehaviour
{
  public static readonly string[] SlotTags = new string[6]
  {
    "PSG_10005756",
    "PSG_10005757",
    "PSG_10005758",
    "PSG_10005759",
    "PSG_10005760",
    "PSG_10005761"
  };
  public TextMeshProUGUI slotTitle;
  public TextMeshProUGUI slotPart;
  public UISponsorsCamera sponsorCamera;

  public void Setup(int inSlotNumber, int inSlotID, SponsorsScreen inSponsorsScreen)
  {
    this.slotTitle.text = Localisation.LocaliseID(UISponsorsSlot.SlotTags[inSlotNumber - 1], (GameObject) null);
    this.slotPart.text = SponsorSlot.GetSlotNameString((SponsorSlot.SlotType) inSlotID);
    this.sponsorCamera.Setup(inSlotID);
    GameUtility.SetActive(this.sponsorCamera.gameObject, inSponsorsScreen.screenMode == UIScreen.ScreenMode.Mode3D);
  }
}
