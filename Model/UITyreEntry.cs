// Decompiled with JetBrains decompiler
// Type: UITyreEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITyreEntry : MonoBehaviour
{
  public UITyre uiTyre;
  private UITyreOptionsButton mParentButton;
  private bool mIsSelected;

  public bool isSelected
  {
    get
    {
      return this.mIsSelected;
    }
  }

  private void Awake()
  {
    this.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnClick));
  }

  public void OnClick()
  {
    this.mParentButton.SelectTyreOption(this);
  }

  public void SetParentButton(UITyreOptionsButton button)
  {
    this.mParentButton = button;
  }

  public void SetSelected(bool selected)
  {
    this.mIsSelected = selected;
  }

  private void Update()
  {
  }
}
