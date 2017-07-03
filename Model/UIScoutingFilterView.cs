// Decompiled with JetBrains decompiler
// Type: UIScoutingFilterView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIScoutingFilterView : MonoBehaviour
{
  private bool mApplyToggleEvent = true;
  public Toggle allToggle;
  public Toggle favouritestoggle;
  public Toggle scoutedToggle;
  public UIScoutingSearchResultsWidget widget;
  private UIScoutingFilterView.Filter mFilter;

  public UIScoutingFilterView.Filter filter
  {
    get
    {
      return this.mFilter;
    }
  }

  public void OnStart()
  {
    this.allToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnFilter(this.allToggle, UIScoutingFilterView.Filter.All)));
    this.favouritestoggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnFilter(this.favouritestoggle, UIScoutingFilterView.Filter.Favourites)));
    this.scoutedToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnFilter(this.scoutedToggle, UIScoutingFilterView.Filter.Scouted)));
  }

  private void OnFilter(Toggle inToggle, UIScoutingFilterView.Filter inFilter)
  {
    if (!this.mApplyToggleEvent)
      return;
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!inToggle.isOn)
      return;
    this.mFilter = inFilter;
    this.widget.filterView = inFilter;
    this.widget.Refresh();
  }

  public void UpdateState()
  {
    GameUtility.SetActive(this.scoutedToggle.gameObject, this.widget.jobRole.filter == UIScoutingFilterJobRole.Filter.Drivers);
    if (this.scoutedToggle.gameObject.activeSelf || !this.scoutedToggle.isOn)
      return;
    this.mApplyToggleEvent = false;
    this.scoutedToggle.isOn = false;
    this.allToggle.isOn = true;
    this.mFilter = UIScoutingFilterView.Filter.All;
    this.mApplyToggleEvent = true;
  }

  public enum Filter
  {
    All,
    Favourites,
    Scouted,
  }
}
