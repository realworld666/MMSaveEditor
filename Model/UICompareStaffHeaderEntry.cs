// Decompiled with JetBrains decompiler
// Type: UICompareStaffHeaderEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICompareStaffHeaderEntry : MonoBehaviour
{
  public Toggle toggle;
  public TextMeshProUGUI title;
  public UICompareStaffListWidget widget;
  private UICompareStaffHeaderEntry.Status mStatus;

  public UICompareStaffHeaderEntry.Status status
  {
    get
    {
      return this.mStatus;
    }
  }

  public void Setup(string inTitle, string inLocalisedTitle)
  {
    this.toggle.onValueChanged.RemoveAllListeners();
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle()));
    if (string.IsNullOrEmpty(inLocalisedTitle))
      return;
    this.title.text = inLocalisedTitle;
  }

  public void OnToggle()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.toggle.isOn && this.mStatus == UICompareStaffHeaderEntry.Status.Open)
    {
      this.widget.SetGroup(this.title.text, false);
      this.mStatus = UICompareStaffHeaderEntry.Status.Closed;
    }
    else
    {
      if (this.toggle.isOn || this.mStatus != UICompareStaffHeaderEntry.Status.Closed)
        return;
      this.widget.SetGroup(this.title.text, true);
      this.mStatus = UICompareStaffHeaderEntry.Status.Open;
    }
  }

  public enum Status
  {
    Open,
    Closed,
  }
}
