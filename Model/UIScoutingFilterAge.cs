// Decompiled with JetBrains decompiler
// Type: UIScoutingFilterAge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIScoutingFilterAge : MonoBehaviour
{
  public Toggle allToggle;
  public Toggle youngToggle;
  public Toggle mediumToggle;
  public Toggle oldToggle;
  public Toggle olderToggle;
  public UIScoutingSearchResultsWidget widget;
  private UIScoutingFilterAge.Filter mFilter;

  public UIScoutingFilterAge.Filter filter
  {
    get
    {
      return this.mFilter;
    }
  }

  public void OnStart()
  {
    this.allToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnFilter(this.allToggle, UIScoutingFilterAge.Filter.All)));
    this.youngToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnFilter(this.youngToggle, UIScoutingFilterAge.Filter.Young)));
    this.mediumToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnFilter(this.mediumToggle, UIScoutingFilterAge.Filter.Medium)));
    this.oldToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnFilter(this.oldToggle, UIScoutingFilterAge.Filter.Old)));
    this.olderToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnFilter(this.olderToggle, UIScoutingFilterAge.Filter.Older)));
  }

  private void OnFilter(Toggle inToggle, UIScoutingFilterAge.Filter inFilter)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!inToggle.isOn)
      return;
    this.mFilter = inFilter;
    this.widget.filterAge = inFilter;
    this.widget.Refresh();
  }

  public enum Filter
  {
    All,
    Young,
    Medium,
    Old,
    Older,
  }
}
