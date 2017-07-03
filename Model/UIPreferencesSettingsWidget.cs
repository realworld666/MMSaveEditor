// Decompiled with JetBrains decompiler
// Type: UIPreferencesSettingsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class UIPreferencesSettingsWidget : MonoBehaviour
{
  public UIPreferencesEntry[] preferencesEntries = new UIPreferencesEntry[0];
  private Dictionary<CarChassisStats.Stats, float> mSliderValues = new Dictionary<CarChassisStats.Stats, float>();
  private bool mLocked;

  public Dictionary<CarChassisStats.Stats, float> sliderStats
  {
    get
    {
      return this.mSliderValues;
    }
  }

  public void OnEnter()
  {
    for (int index = 0; index < this.preferencesEntries.Length; ++index)
      this.preferencesEntries[index].OnEnter();
    this.mLocked = Game.instance.championshipManager.GetMainChampionship(Championship.Series.SingleSeaterSeries) != Game.instance.player.team.championship;
    this.UpdateSliderValues();
  }

  public void UpdateSliderValues()
  {
    float num1 = GameStatsConstants.chassisBaseStat[Game.instance.player.team.championship.championshipID];
    float chassisSliderAmmount = GameStatsConstants.chassisSliderAmmount;
    float num2 = num1 - chassisSliderAmmount / 2f;
    for (int index = 0; index < this.preferencesEntries.Length; ++index)
    {
      UIPreferencesEntry preferencesEntry = this.preferencesEntries[index];
      switch (preferencesEntry.preferenceType)
      {
        case Supplier.CarAspect.RearPackage:
          this.mSliderValues[CarChassisStats.Stats.Improvability] = !this.mLocked ? num2 + preferencesEntry.slider.normalizedValue * chassisSliderAmmount : num1;
          this.mSliderValues[CarChassisStats.Stats.TyreHeating] = !this.mLocked ? num2 + (1f - preferencesEntry.slider.normalizedValue) * chassisSliderAmmount : num1;
          break;
        case Supplier.CarAspect.NoseHeight:
          this.mSliderValues[CarChassisStats.Stats.FuelEfficiency] = !this.mLocked ? num2 + preferencesEntry.slider.normalizedValue * chassisSliderAmmount : num1;
          this.mSliderValues[CarChassisStats.Stats.TyreWear] = !this.mLocked ? num2 + (1f - preferencesEntry.slider.normalizedValue) * chassisSliderAmmount : num1;
          break;
      }
    }
  }

  public UIPreferencesEntry GetEntryOfCarAspect(Supplier.CarAspect inAspect)
  {
    for (int index = 0; index < this.preferencesEntries.Length; ++index)
    {
      if (this.preferencesEntries[index].preferenceType == inAspect)
        return this.preferencesEntries[index];
    }
    return (UIPreferencesEntry) null;
  }

  public void UpdateBounds()
  {
    CarDesignScreen screen = UIManager.instance.GetScreen<CarDesignScreen>();
    for (int index = 0; index < this.preferencesEntries.Length; ++index)
      this.preferencesEntries[index].ResetBoundaries();
    for (int index = 0; index < screen.supplierList.Length; ++index)
    {
      Supplier supplier = screen.supplierList[index];
      if (supplier != null)
      {
        using (Dictionary<Supplier.CarAspect, float>.Enumerator enumerator = supplier.carAspectMaxBoundary.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<Supplier.CarAspect, float> current = enumerator.Current;
            this.GetEntryOfCarAspect(current.Key).AddToMaxBoundary(current.Value);
          }
        }
        using (Dictionary<Supplier.CarAspect, float>.Enumerator enumerator = supplier.carAspectMinBoundary.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<Supplier.CarAspect, float> current = enumerator.Current;
            this.GetEntryOfCarAspect(current.Key).AddToMinBoundary(current.Value);
          }
        }
      }
    }
  }
}
