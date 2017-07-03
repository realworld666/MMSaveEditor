// Decompiled with JetBrains decompiler
// Type: MouseEvents
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (RectTransform))]
public class MouseEvents : MonoBehaviour
{
  public string onMouseClickFunction = "OnMouseClick";
  public string onMouseHoverFunction = "OnMouseHover";
  public string onMouseHoldFunction = "OnMouseHold";
  public string onMouseEnterFunction = "OnMouseEnter";
  public string onMouseExitFunction = "OnMouseExit";
  public bool ignoreRaycast = true;
  public GameObject targetObject;
  public bool targetAndSelf;
  public bool ignoreBlur;
  private RectTransform mRectTransform;
  private bool mPreviousContainsMouseCheck;
  private bool mPreviousObjectBlocked;
  private bool mIsObjectBlocked;
  private bool mRectContainsMouseCursor;

  private void OnEnable()
  {
    this.mRectTransform = this.GetComponent<RectTransform>();
    this.mPreviousContainsMouseCheck = false;
    if (!((Object) this.targetObject == (Object) null))
      return;
    this.targetObject = this.gameObject;
  }

  private void Update()
  {
    if (UIManager.instance.blur.isActive && !this.ignoreBlur)
      return;
    this.mIsObjectBlocked = this.IsRaycastTarget();
    this.mRectContainsMouseCursor = this.mRectTransform.rect.Contains((Vector2) this.mRectTransform.InverseTransformPoint(UIManager.instance.UICamera.ScreenToWorldPoint(Input.mousePosition)));
    this.CheckMouseEnterAndExit();
    this.CheckMouseHold();
    this.CheckMouseHover();
    this.mPreviousContainsMouseCheck = this.mRectContainsMouseCursor;
    this.mPreviousObjectBlocked = this.mIsObjectBlocked;
  }

  private void CheckMouseEnterAndExit()
  {
    if (!this.mIsObjectBlocked && this.mRectContainsMouseCursor && !this.mPreviousContainsMouseCheck)
    {
      this.targetObject.SendMessage(this.onMouseEnterFunction, SendMessageOptions.DontRequireReceiver);
      if (!this.targetAndSelf)
        return;
      this.gameObject.SendMessage(this.onMouseEnterFunction, SendMessageOptions.DontRequireReceiver);
    }
    else
    {
      if (this.mPreviousObjectBlocked || this.mRectContainsMouseCursor || !this.mPreviousContainsMouseCheck)
        return;
      this.targetObject.SendMessage(this.onMouseExitFunction, SendMessageOptions.DontRequireReceiver);
      if (!this.targetAndSelf)
        return;
      this.gameObject.SendMessage(this.onMouseExitFunction, SendMessageOptions.DontRequireReceiver);
    }
  }

  private void CheckMouseHold()
  {
    if (this.mIsObjectBlocked || !this.mRectContainsMouseCursor || !InputManager.instance.GetKeyDown(KeyBinding.Name.MouseLeft))
      return;
    this.targetObject.SendMessage(this.onMouseHoldFunction, SendMessageOptions.DontRequireReceiver);
    if (!this.targetAndSelf)
      return;
    this.gameObject.SendMessage(this.onMouseHoldFunction, SendMessageOptions.DontRequireReceiver);
  }

  private void CheckMouseHover()
  {
    if (this.mIsObjectBlocked || !this.mRectContainsMouseCursor)
      return;
    this.targetObject.SendMessage(this.onMouseHoverFunction, SendMessageOptions.DontRequireReceiver);
    if (Input.GetMouseButtonDown(0))
      this.OnPointerClick();
    if (!this.targetAndSelf)
      return;
    this.gameObject.SendMessage(this.onMouseHoverFunction, SendMessageOptions.DontRequireReceiver);
  }

  private bool IsRaycastTarget()
  {
    return !this.ignoreRaycast && !this.IsRaycastFirstTarget();
  }

  public bool IsRaycastFirstTarget()
  {
    if (UIManager.instance.UIObjectsAtMousePosition.Count > 0)
      return (Object) UIManager.instance.UIObjectsAtMousePosition[0].gameObject == (Object) this.gameObject;
    return false;
  }

  public void OnPointerClick()
  {
    if ((Object) this.targetObject == (Object) null)
      this.targetObject = this.gameObject;
    this.targetObject.SendMessage(this.onMouseClickFunction, SendMessageOptions.DontRequireReceiver);
    if (!this.targetAndSelf)
      return;
    this.gameObject.SendMessage(this.onMouseClickFunction, SendMessageOptions.DontRequireReceiver);
  }
}
