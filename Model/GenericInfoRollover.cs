// Decompiled with JetBrains decompiler
// Type: GenericInfoRollover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class GenericInfoRollover : UIDialogBox
{
  public TextMeshProUGUI header;
  public TextMeshProUGUI description;
  private RectTransform mRectTransform;

  protected override void Awake()
  {
    base.Awake();
    this.mRectTransform = this.gameObject.GetComponent<RectTransform>();
  }

  public void Open(string inHeader, string inDescription)
  {
    this.header.text = inHeader;
    this.description.text = inDescription;
    this.gameObject.SetActive(true);
    this.Update();
  }

  public void Close()
  {
    this.gameObject.SetActive(false);
  }

  public void Update()
  {
    this.mRectTransform.pivot = (double) Input.mousePosition.x <= (double) (Screen.width / 2) ? new Vector2(0.0f, this.mRectTransform.pivot.y) : new Vector2(1f, this.mRectTransform.pivot.y);
    this.transform.position = UIManager.instance.UICamera.ScreenToWorldPoint(Input.mousePosition);
  }
}
