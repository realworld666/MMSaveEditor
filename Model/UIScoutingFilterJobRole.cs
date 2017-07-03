// Decompiled with JetBrains decompiler
// Type: UIScoutingFilterJobRole
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIScoutingFilterJobRole : MonoBehaviour
{
  public Toggle driversToggle;
  public Toggle designersToggle;
  public Toggle mechanicsToggle;
  public UIScoutingSearchResultsWidget widget;
  private UIScoutingFilterJobRole.Filter mFilter;

  public UIScoutingFilterJobRole.Filter filter
  {
    get
    {
      return this.mFilter;
    }
  }

  public void OnStart()
  {
    this.driversToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnFilter(this.driversToggle, UIScoutingFilterJobRole.Filter.Drivers)));
    this.designersToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnFilter(this.designersToggle, UIScoutingFilterJobRole.Filter.Designers)));
    this.mechanicsToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnFilter(this.mechanicsToggle, UIScoutingFilterJobRole.Filter.Mechanics)));
  }

  public void SetFilter(UIScoutingFilterJobRole.Filter inFilter)
  {
    this.designersToggle.isOn = inFilter == UIScoutingFilterJobRole.Filter.Designers;
    this.mechanicsToggle.isOn = inFilter == UIScoutingFilterJobRole.Filter.Mechanics;
    this.driversToggle.isOn = inFilter == UIScoutingFilterJobRole.Filter.Drivers;
  }

  private void OnFilter(Toggle inToggle, UIScoutingFilterJobRole.Filter inFilter)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!inToggle.isOn)
      return;
    this.mFilter = inFilter;
    this.widget.filterJobRole = inFilter;
    if (!this.gameObject.activeSelf)
      return;
    this.widget.Refresh();
  }

  public enum Filter
  {
    Drivers,
    Designers,
    Mechanics,
  }
}
