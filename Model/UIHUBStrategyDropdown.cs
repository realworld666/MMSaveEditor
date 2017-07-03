// Decompiled with JetBrains decompiler
// Type: UIHUBStrategyDropdown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHUBStrategyDropdown : MonoBehaviour
{
  public RectTransform rectTransform;

  private void Update()
  {
    if (!this.HasUserClickedAwayFromObject())
      return;
    this.gameObject.SetActive(false);
  }

  private bool HasUserClickedAwayFromObject()
  {
    if (!InputManager.instance.GetKeyDown(KeyBinding.Name.MouseLeft))
      return false;
    bool flag = false;
    List<RaycastResult> objectsAtMousePosition = UIManager.instance.UIObjectsAtMousePosition;
    for (int index = 0; index < objectsAtMousePosition.Count && !flag; ++index)
    {
      if (GameUtility.IsParentInHierarchy(objectsAtMousePosition[index].gameObject.transform, (Transform) this.rectTransform))
        flag = true;
    }
    return !flag;
  }
}
