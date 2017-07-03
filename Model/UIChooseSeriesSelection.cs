// Decompiled with JetBrains decompiler
// Type: UIChooseSeriesSelection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIChooseSeriesSelection : MonoBehaviour
{
  public UIChooseSeriesEntry[] series;
  public ToggleGroup toggleGroup;
  public Button getGtSeriesButton;
  private List<Championship> mChampionships;

  public void Setup()
  {
    scSoundManager.BlockSoundEvents = true;
    this.toggleGroup.SetAllTogglesOff();
    this.getGtSeriesButton.onClick.RemoveAllListeners();
    this.getGtSeriesButton.onClick.AddListener(new UnityAction(this.OnGetGtSeriesClicked));
    bool flag = false;
    this.mChampionships = Game.instance.championshipManager.GetEntityList();
    for (int index = 0; index < this.series.Length; ++index)
    {
      GameUtility.SetActive(this.series[index].gameObject, index < this.mChampionships.Count);
      if (index < this.mChampionships.Count)
      {
        this.series[index].Setup(this.mChampionships[index]);
        if (this.series[index].isChoosable && !flag)
        {
          this.series[index].toggle.isOn = true;
          flag = true;
        }
      }
    }
    scSoundManager.BlockSoundEvents = false;
  }

  private void OnGetGtSeriesClicked()
  {
    DLCManager.HandleGetDlcButton(DLCManager.GetDlcByName("New Championship").appId);
  }
}
