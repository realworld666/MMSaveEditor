// Decompiled with JetBrains decompiler
// Type: UIFinanceScreenFinanceWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIFinanceScreenFinanceWidget : MonoBehaviour
{
  public UIFinanceGraphWidget[] graphWidgets = new UIFinanceGraphWidget[0];

  public void OnEnter()
  {
    for (int index = 0; index < this.graphWidgets.Length; ++index)
      this.graphWidgets[index].OnEnter();
  }
}
