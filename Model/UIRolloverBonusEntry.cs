// Decompiled with JetBrains decompiler
// Type: UIRolloverBonusEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRolloverBonusEntry : MonoBehaviour
{
  public TextMeshProUGUI bonusName;
  public TextMeshProUGUI bonusDescription;
  public GameObject bonusIconContainer;
  public GameObject traitIconContainer;
  public Image bonusIcon;
  public Image bonusIconBackground;

  public void SetupBonusEntry(string inBonusName, string inBonusDescription, Sprite inBonusSprite = null, int inBonusLevel = 0)
  {
    this.bonusName.text = inBonusName;
    this.bonusName.ForceMeshUpdate();
    this.bonusDescription.text = inBonusDescription;
    if ((Object) inBonusSprite != (Object) null)
    {
      this.bonusIcon.sprite = inBonusSprite;
      if ((Object) this.traitIconContainer != (Object) null && (Object) this.bonusIconContainer != (Object) null)
      {
        GameUtility.SetActive(this.traitIconContainer, false);
        GameUtility.SetActive(this.bonusIconContainer, true);
      }
    }
    else
    {
      if ((Object) this.traitIconContainer != (Object) null && (Object) this.bonusIconContainer != (Object) null)
      {
        GameUtility.SetActive(this.traitIconContainer, true);
        GameUtility.SetActive(this.bonusIconContainer, false);
      }
      if ((Object) this.bonusIcon != (Object) null)
        GameUtility.SetActive(this.bonusIcon.gameObject, false);
    }
    if (inBonusLevel <= 0 || !((Object) this.bonusIconBackground != (Object) null))
      return;
    this.bonusIconBackground.color = PracticeKnowledge.GetKnowledgeLevelColor(inBonusLevel);
  }
}
