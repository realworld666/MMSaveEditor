// Decompiled with JetBrains decompiler
// Type: UISponsorsSlotEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UISponsorsSlotEntry : MonoBehaviour
{
  public TextMeshProUGUI slotName;
  public TextMeshProUGUI slotPart;

  public void Setup(int inSlotNumber, int inSlotID)
  {
    this.slotName.text = Localisation.LocaliseID(UISponsorsSlot.SlotTags[inSlotNumber - 1], (GameObject) null);
  }
}
