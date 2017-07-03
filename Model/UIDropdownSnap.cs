// Decompiled with JetBrains decompiler
// Type: UIDropdownSnap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDropdownSnap : MonoBehaviour
{
  public Dropdown dropdown;
  public ScrollRect scrollrect;
  public Transform content;
  private bool mIsSnapping;
  private GameObject mSelected;
  private GameObject mLastSelected;
  private RectTransform mSelectedRect;

  private void OnEnable()
  {
    if ((Object) this.dropdown == (Object) null)
      return;
    this.mIsSnapping = true;
    this.StartCoroutine(this.SnapDropdown());
  }

  private void Update()
  {
    if (this.mIsSnapping || Mathf.Approximately(Input.GetAxis("Vertical"), 0.0f))
      return;
    this.mSelected = EventSystem.current.currentSelectedGameObject;
    if ((Object) this.mSelected == (Object) null || (Object) this.mSelected.transform.parent != (Object) this.scrollrect.content.transform || (Object) this.mSelected == (Object) this.mLastSelected)
      return;
    this.mSelectedRect = this.mSelected.GetComponent<RectTransform>();
    GameUtility.SnapScrollRectTo(this.mSelectedRect, this.scrollrect, GameUtility.AnchorType.Y, GameUtility.AnchorLocation.Top);
    this.mLastSelected = this.mSelected;
  }

  [DebuggerHidden]
  private IEnumerator SnapDropdown()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new UIDropdownSnap.\u003CSnapDropdown\u003Ec__Iterator1E()
    {
      \u003C\u003Ef__this = this
    };
  }

  private void OnDisable()
  {
    this.mLastSelected = (GameObject) null;
    this.mIsSnapping = false;
  }
}
