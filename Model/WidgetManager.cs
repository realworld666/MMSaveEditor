// Decompiled with JetBrains decompiler
// Type: WidgetManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class WidgetManager
{
  private List<GameObject> widgets = new List<GameObject>();

  public void RegisterWidget(GameObject inWidget)
  {
    if (this.CheckWidget(inWidget) != -1)
      return;
    this.widgets.Add(inWidget);
  }

  public void UnregisterWidget(GameObject inWidget)
  {
    int index = this.CheckWidget(inWidget);
    if (index < 0)
      return;
    this.widgets.RemoveAt(index);
  }

  public void DisableAllWidgets()
  {
    int count = this.widgets.Count;
    for (int index = 0; index < count; ++index)
      GameUtility.SetActive(this.widgets[index], false);
  }

  public GameObject[] GetActiveWidgets()
  {
    List<GameObject> gameObjectList = new List<GameObject>();
    int count = this.widgets.Count;
    for (int index = 0; index < count; ++index)
    {
      GameObject widget = this.widgets[index];
      if (widget.gameObject.activeSelf)
        gameObjectList.Add(widget);
    }
    return gameObjectList.ToArray();
  }

  private int CheckWidget(GameObject inWidget)
  {
    int count = this.widgets.Count;
    for (int index = 0; index < count; ++index)
    {
      if ((Object) this.widgets[index] == (Object) inWidget)
        return index;
    }
    return -1;
  }
}
