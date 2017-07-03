// Decompiled with JetBrains decompiler
// Type: UIScoutingFilterDriveableSeries
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIScoutingFilterDriveableSeries : MonoBehaviour
{
  public Toggle singleSeaterToggle;
  public Toggle GTToggle;
  public UIScoutingSearchResultsWidget widget;
  private Championship.Series mFilter;

  public Championship.Series filter
  {
    get
    {
      return this.mFilter;
    }
  }

  public void OnStart()
  {
    this.singleSeaterToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnFilter(this.singleSeaterToggle, Championship.Series.SingleSeaterSeries)));
    this.GTToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnFilter(this.GTToggle, Championship.Series.GTSeries)));
    if (Game.instance.player.IsUnemployed())
      return;
    this.SetFilter(Game.instance.player.team.championship.series);
  }

  public void SetFilter(Championship.Series inFilter)
  {
    this.GTToggle.isOn = inFilter == Championship.Series.GTSeries;
    this.singleSeaterToggle.isOn = inFilter == Championship.Series.SingleSeaterSeries;
  }

  private void OnFilter(Toggle inToggle, Championship.Series inFilter)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!inToggle.isOn)
      return;
    this.mFilter = inFilter;
    this.widget.filterDriveableSeries = inFilter;
    if (!this.gameObject.activeSelf)
      return;
    this.widget.Refresh();
  }
}
