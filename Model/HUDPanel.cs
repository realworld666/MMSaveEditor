// Decompiled with JetBrains decompiler
// Type: HUDPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HUDPanel : MonoBehaviour
{
  public bool autoHide = true;
  public Action OnShowPanel;
  public Action OnHidePanel;
  private Toggle mToggle;
  private RacingVehicle mVehicle;
  private RectTransform mActionButtonPanelTransform;

  public Toggle toggle
  {
    get
    {
      return this.mToggle;
    }
  }

  public RacingVehicle vehicle
  {
    get
    {
      return this.mVehicle;
    }
  }

  public virtual HUDPanel.Type type
  {
    get
    {
      return HUDPanel.Type.Count;
    }
  }

  public bool isVisible
  {
    get
    {
      return this.gameObject.activeSelf;
    }
  }

  public void Show()
  {
    if (!this.isVisible)
    {
      this.gameObject.SetActive(true);
      if (this.OnShowPanel != null)
        this.OnShowPanel();
    }
    if (!((UnityEngine.Object) this.mToggle != (UnityEngine.Object) null))
      return;
    this.mToggle.isOn = true;
  }

  public void Hide()
  {
    if (this.isVisible)
    {
      this.gameObject.SetActive(false);
      if (this.OnHidePanel != null)
        this.OnHidePanel();
    }
    if (!((UnityEngine.Object) this.mToggle != (UnityEngine.Object) null))
      return;
    this.mToggle.isOn = false;
  }

  protected virtual void Update()
  {
  }

  private void LateUpdate()
  {
    this.CheckForClickAwayFromPanel();
  }

  private void CheckForClickAwayFromPanel()
  {
    if (!this.autoHide || !InputManager.instance.GetKey(KeyBinding.Name.MouseLeft) || !this.gameObject.activeSelf)
      return;
    bool flag = false;
    List<RaycastResult> objectsAtMousePosition = UIManager.instance.UIObjectsAtMousePosition;
    for (int index = 0; index < objectsAtMousePosition.Count && !flag; ++index)
    {
      if (!flag && GameUtility.IsParentInHierarchy(objectsAtMousePosition[index].gameObject.transform, (Transform) this.mActionButtonPanelTransform))
        flag = true;
    }
    if (flag || Game.instance.tutorialSystem.isTutorialOnScreen)
      return;
    this.Hide();
  }

  public void SetVehicle(RacingVehicle inVehicle)
  {
    this.mVehicle = inVehicle;
  }

  public void SetActionButtonPanel(RectTransform inActionButtonPanelTransform)
  {
    this.mActionButtonPanelTransform = inActionButtonPanelTransform;
  }

  public void SetToggle(Toggle inToggle)
  {
    this.mToggle = inToggle;
  }

  public enum Type
  {
    Strategy,
    CarCondition,
    Penalties,
    ERSBattery,
    Count,
  }
}
