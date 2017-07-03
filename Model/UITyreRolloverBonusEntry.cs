// Decompiled with JetBrains decompiler
// Type: UITyreRolloverBonusEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UITyreRolloverBonusEntry : MonoBehaviour
{
  public TextMeshProUGUI bonusNumber;
  public TextMeshProUGUI bonusName;
  public TextMeshProUGUI bonusDescription;

  public void SetupBonusEntry(int inListEntryNumber, string inBonusName, string inBonusDescription)
  {
    this.bonusNumber.SetText(inListEntryNumber.ToString());
    this.bonusName.SetText(inBonusName);
    this.bonusDescription.SetText(inBonusDescription);
  }
}
