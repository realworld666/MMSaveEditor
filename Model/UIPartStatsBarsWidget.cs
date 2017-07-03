// Decompiled with JetBrains decompiler
// Type: UIPartStatsBarsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPartStatsBarsWidget : MonoBehaviour
{
  private List<UIPartStatsBarsEntry> mPartStats = new List<UIPartStatsBarsEntry>();
  public UIGridList gridList;
  public Dropdown dropdown;
  private CarPart mPart;
  private Circuit mSelectedCircuit;
  private PartDesignScreen mScreen;

  private void Start()
  {
    this.dropdown.onValueChanged.AddListener((UnityAction<int>) (value => this.OnDropdownValueChanged(value)));
  }

  private void OnDropdownValueChanged(int inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mSelectedCircuit = Game.instance.player.team.championship.calendar[inValue].circuit;
    this.UpdateCircuitOptions();
  }

  private void UpdateCircuitOptions()
  {
    for (int index = 0; index < this.mPartStats.Count; ++index)
      this.mPartStats[index].SetCircuitOptions(this.mSelectedCircuit);
  }

  public void Setup(CarPart inPart)
  {
    this.mPart = inPart;
    this.mScreen = UIManager.instance.GetScreen<PartDesignScreen>();
    this.mPartStats.Clear();
    this.dropdown.ClearOptions();
    Championship championship = Game.instance.player.team.championship;
    for (int eventNumber = championship.eventNumber; eventNumber < championship.calendar.Count; ++eventNumber)
    {
      string text = Localisation.LocaliseID(championship.calendar[eventNumber].circuit.locationNameID, (GameObject) null);
      this.dropdown.get_options().Add(new Dropdown.OptionData(text));
      if (eventNumber == 0)
      {
        this.mSelectedCircuit = championship.calendar[eventNumber].circuit;
        this.dropdown.captionText.text = text;
      }
    }
    this.gridList.DestroyListItems();
    CarStats.StatType statType = this.mPart.stats.statType;
    UIPartStatsBarsEntry listItem = this.gridList.CreateListItem<UIPartStatsBarsEntry>();
    float statWithPerformance = this.mPart.stats.statWithPerformance;
    float normalizedStatValue1 = CarPartStats.GetNormalizedStatValue(statWithPerformance - (float) this.mScreen.engineerAccuracy, this.mPart.GetPartType());
    float normalizedStatValue2 = CarPartStats.GetNormalizedStatValue(statWithPerformance + (float) this.mScreen.engineerAccuracy, this.mPart.GetPartType());
    listItem.Setup(statType, normalizedStatValue1, normalizedStatValue2);
    this.mPartStats.Add(listItem);
    this.UpdateCircuitOptions();
  }

  public void UpdateStats()
  {
    for (int index = 0; index < this.mPartStats.Count; ++index)
    {
      CarStats.StatType stat = this.mPartStats[index].stat;
      float statWithPerformance = this.mPart.stats.statWithPerformance;
      float normalizedStatValue1 = CarPartStats.GetNormalizedStatValue(statWithPerformance - (float) this.mScreen.engineerAccuracy, this.mPart.GetPartType());
      float normalizedStatValue2 = CarPartStats.GetNormalizedStatValue(statWithPerformance + (float) this.mScreen.engineerAccuracy, this.mPart.GetPartType());
      this.mPartStats[index].Setup(stat, normalizedStatValue1, normalizedStatValue2);
    }
  }
}
