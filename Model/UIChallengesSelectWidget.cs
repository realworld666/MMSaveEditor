// Decompiled with JetBrains decompiler
// Type: UIChallengesSelectWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIChallengesSelectWidget : MonoBehaviour
{
  public TextMeshProUGUI challengesCountLabel;
  public UIChallengeEntry[] entries;

  public void Setup()
  {
    Challenge[] array = Game.instance.challengeManager.challenges.ToArray();
    StringVariableParser.intValue1 = array.Length;
    StringVariableParser.intValue2 = array.Length;
    this.challengesCountLabel.text = Localisation.LocaliseID("PSG_10010652", (GameObject) null);
    for (int index = 0; index < this.entries.Length; ++index)
    {
      GameUtility.SetActive(this.entries[index].gameObject, index < array.Length);
      if (this.entries[index].gameObject.activeSelf)
      {
        this.entries[index].Setup(array[index]);
        if (index == 0)
        {
          this.entries[index].toggle.isOn = true;
          this.entries[index].OnToggle(true);
        }
      }
    }
  }
}
