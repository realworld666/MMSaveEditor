// Decompiled with JetBrains decompiler
// Type: UITooltipTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;

public class UITooltipTrigger : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
  private UITooltip mTooltip;

  public void Setup(UITooltip inToolTip)
  {
    if (!((Object) inToolTip != (Object) null))
      return;
    this.mTooltip = inToolTip;
  }

  private void Update()
  {
    if (this.mTooltip.useMouseObjectStack)
      this.CheckForCursor();
    if (!this.mTooltip.gameObject.activeSelf)
      return;
    UIManager.instance.cursorManager.SetState(this.mTooltip.cursorToUse);
  }

  private void CheckForCursor()
  {
    bool flag = false;
    for (int index = 0; index < this.mTooltip.otherTargets.Length; ++index)
    {
      if (UIManager.instance.IsObjectAtMousePosition(this.mTooltip.otherTargets[index].gameObject))
      {
        flag = true;
        this.ActivateToolTip();
      }
      else if (!flag)
        this.mTooltip.Hide();
    }
    if (flag)
      return;
    if (UIManager.instance.IsObjectAtMousePosition(this.mTooltip.target.gameObject))
      this.ActivateToolTip();
    else
      this.mTooltip.Hide();
  }

  private void ActivateToolTip()
  {
    if (this.mTooltip.gameObject.activeSelf)
      return;
    this.mTooltip.Show();
  }

  public void OnPointerEnter(PointerEventData inEventData)
  {
    if (!((Object) this.mTooltip != (Object) null))
      return;
    this.mTooltip.Show();
  }

  public void OnPointerExit(PointerEventData inEventData)
  {
    if (!((Object) this.mTooltip != (Object) null))
      return;
    this.mTooltip.Hide();
  }

  private void OnDisable()
  {
    if (!((Object) this.mTooltip != (Object) null))
      return;
    this.mTooltip.Hide();
  }
}
