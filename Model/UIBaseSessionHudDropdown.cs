// Decompiled with JetBrains decompiler
// Type: UIBaseSessionHudDropdown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIBaseSessionHudDropdown : MonoBehaviour
{
  public bool autoHide = true;
  public GameObject widgetToDisable;
  public RectTransform[] autoHideTransforms;
  private bool mHasMouseEnteredRect;

  private void Awake()
  {
  }

  protected virtual void OnEnable()
  {
    if (Game.IsActive())
    {
      if ((Object) this.widgetToDisable != (Object) null)
        this.widgetToDisable.SetActive(false);
    }
    else
      this.gameObject.SetActive(false);
    this.mHasMouseEnteredRect = false;
    App.instance.widgetManager.DisableAllWidgets();
    App.instance.widgetManager.RegisterWidget(this.gameObject);
  }

  protected virtual void OnDisable()
  {
    if (Game.IsActive() && (Object) this.widgetToDisable != (Object) null)
      this.widgetToDisable.SetActive(true);
    App.instance.widgetManager.UnregisterWidget(this.gameObject);
  }

  protected virtual void Update()
  {
    this.CheckForClickAwayFromPanel();
  }

  private void CheckForClickAwayFromPanel()
  {
    if (!this.autoHide || this.autoHideTransforms == null || this.autoHideTransforms.Length <= 0)
      return;
    bool flag = false;
    List<RaycastResult> objectsAtMousePosition = UIManager.instance.UIObjectsAtMousePosition;
    for (int index1 = 0; index1 < objectsAtMousePosition.Count && !flag; ++index1)
    {
      for (int index2 = 0; index2 < this.autoHideTransforms.Length && !flag; ++index2)
      {
        if (GameUtility.IsParentInHierarchy(objectsAtMousePosition[index1].gameObject.transform, (Transform) this.autoHideTransforms[index2]))
          flag = true;
      }
    }
    if (!this.mHasMouseEnteredRect && flag)
      this.mHasMouseEnteredRect = true;
    if (!this.mHasMouseEnteredRect || flag)
      return;
    this.gameObject.SetActive(false);
  }
}
