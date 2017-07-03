// Decompiled with JetBrains decompiler
// Type: UIScrollRectControls
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScrollRectControls : MonoBehaviour
{
  public bool useSelectionParent = true;
  public ScrollRect scrollrect;
  public Transform container;
  private GameObject mSelected;
  private GameObject mLastSelected;
  private RectTransform mSelectedRect;

  private void Update()
  {
    if (Mathf.Approximately(Input.GetAxis("Vertical"), 0.0f))
      return;
    this.mSelected = EventSystem.current.currentSelectedGameObject;
    if ((Object) this.mSelected == (Object) null || (Object) this.mSelected.transform.parent != (Object) this.container && ((Object) this.mSelected.transform.parent == (Object) null || (Object) this.mSelected.transform.parent.parent != (Object) this.container) || (Object) this.mSelected == (Object) this.mLastSelected)
      return;
    this.mSelectedRect = !this.useSelectionParent ? this.mSelected.GetComponent<RectTransform>() : this.mSelected.transform.parent.GetComponent<RectTransform>();
    GameUtility.SnapScrollRectTo(this.mSelectedRect, this.scrollrect, GameUtility.AnchorType.Y, GameUtility.AnchorLocation.Top);
    this.mLastSelected = this.mSelected;
  }
}
