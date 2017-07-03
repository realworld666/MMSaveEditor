// Decompiled with JetBrains decompiler
// Type: UIAdditionalBonusEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAdditionalBonusEntry : MonoBehaviour
{
  public TextMeshProUGUI text;
  public Image icon;

  public void Setup(CarPartComponent inComponent)
  {
    if (!((Object) this.icon != (Object) null))
      return;
    string iconPath = inComponent.GetIconPath();
    this.icon.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Frontend1, "PartDevIcons-" + iconPath);
  }
}
