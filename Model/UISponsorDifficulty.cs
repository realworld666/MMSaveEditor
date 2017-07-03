// Decompiled with JetBrains decompiler
// Type: UISponsorDifficulty
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UISponsorDifficulty : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI difficultyText;

  public void SetDifficulty(int inTargetPosition)
  {
    if (inTargetPosition > 1)
    {
      StringVariableParser.ordinalNumberString = GameUtility.FormatForPosition(inTargetPosition, (string) null);
      this.difficultyText.text = Localisation.LocaliseID("PSG_10010361", (GameObject) null);
    }
    else
    {
      StringVariableParser.ordinalNumberString = GameUtility.FormatForPosition(inTargetPosition, (string) null);
      this.difficultyText.text = Localisation.LocaliseID("PSG_10010864", (GameObject) null);
    }
  }
}
