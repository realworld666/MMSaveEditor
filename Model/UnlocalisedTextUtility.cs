// Decompiled with JetBrains decompiler
// Type: UnlocalisedTextUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UnlocalisedTextUtility
{
  public static void SetUnlocalisedText(UnlocalisedTextUtility.Mode inMode, GameObject inRoot)
  {
    if (inMode != UnlocalisedTextUtility.Mode.Highlighted)
      return;
    foreach (TextMeshProUGUI componentsInChild in inRoot.GetComponentsInChildren<TextMeshProUGUI>(true))
    {
      if ((Object) componentsInChild.gameObject.GetComponent<UILocaliseLabel>() == (Object) null)
      {
        Color color = componentsInChild.color;
        VertexGradient colorGradient = componentsInChild.colorGradient;
        colorGradient.topLeft = color;
        componentsInChild.colorGradient = colorGradient;
        componentsInChild.color = Color.red;
        componentsInChild.fontStyle |= FontStyles.Bold | FontStyles.Italic | FontStyles.Underline;
      }
    }
  }

  public enum Mode
  {
    Unhighlighted,
    Highlighted,
  }
}
