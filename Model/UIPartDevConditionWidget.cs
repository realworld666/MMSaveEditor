// Decompiled with JetBrains decompiler
// Type: UIPartDevConditionWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPartDevConditionWidget : MonoBehaviour
{
  public CarPartStats.CarPartStat stat = CarPartStats.CarPartStat.Condition;
  public UICharacterPortrait[] driverPortraits = new UICharacterPortrait[0];
  public TextMeshProUGUI[] driverNames = new TextMeshProUGUI[0];
  public TextMeshProUGUI[] driverPositions = new TextMeshProUGUI[0];
  public TextMeshProUGUI[] conditionLoss = new TextMeshProUGUI[0];
  public Flag[] driverFlags = new Flag[0];
  public List<UIPartDevStatImprovementPartEntry> car1Entries = new List<UIPartDevStatImprovementPartEntry>();
  public List<UIPartDevStatImprovementPartEntry> car2Entries = new List<UIPartDevStatImprovementPartEntry>();
  private List<UIPartDevStatImprovementPartEntry> mAllEntries = new List<UIPartDevStatImprovementPartEntry>();
  public TextMeshProUGUI finishDateDescription;
  public TextMeshProUGUI finishDateTrack;
  public TextMeshProUGUI devUnlocksLabel;
  public Slider workCompleteSlider;
  public bool labelsSet;
  private PartImprovement mPartImprovement;

  private void Awake()
  {
    this.mAllEntries.AddRange((IEnumerable<UIPartDevStatImprovementPartEntry>) this.car1Entries);
    this.mAllEntries.AddRange((IEnumerable<UIPartDevStatImprovementPartEntry>) this.car2Entries);
  }

  public void Setup()
  {
    this.mPartImprovement = Game.instance.player.team.carManager.partImprovement;
    this.RefreshTimeLabels();
    this.SetDriversData();
    this.RefreshItemsPartStatus();
  }

  private void SetDriversData()
  {
    for (int inIndex = 0; inIndex < this.driverPortraits.Length; ++inIndex)
    {
      Person driver = (Person) Game.instance.player.team.GetDriver(inIndex);
      Championship championship = Game.instance.player.team.championship;
      this.driverPortraits[inIndex].SetPortrait(driver);
      this.driverNames[inIndex].text = driver.name;
      this.driverPositions[inIndex].text = championship.GetChampionshipName(false) + ": " + GameUtility.FormatForPosition(championship.standings.GetEntry((Entity) driver).GetCurrentChampionshipPosition(), (string) null);
      this.driverFlags[inIndex].SetNationality(driver.nationality);
    }
  }

  public void SetConditionLossLabels()
  {
    if (this.labelsSet)
      return;
    for (int inIndex = 0; inIndex < this.driverPortraits.Length; ++inIndex)
    {
      Person driver = (Person) Game.instance.player.team.GetDriver(inIndex);
      CarManager carManager = Game.instance.player.team.carManager;
      string str = GameUtility.ColorToRichTextHex(UIConstants.staffScreenOrange) + (object) Mathf.Round(carManager.GetCarForDriver((Driver) driver).carConditionAfterEvent * 100f) + "%</color>";
      this.conditionLoss[inIndex].text = "Condition lost over race weekend: " + str;
    }
    this.labelsSet = true;
  }

  private void UpdateEntries()
  {
    for (int index = 0; index < this.mAllEntries.Count; ++index)
    {
      if (this.mAllEntries[index].part != null)
        this.mAllEntries[index].UpdateStats();
    }
  }

  public void RefreshItemsPartStatus()
  {
    List<CarPart> carPartList = new List<CarPart>();
    for (int index1 = 0; index1 < this.car1Entries.Count; ++index1)
    {
      for (int index2 = 0; index2 < this.mPartImprovement.partsToImprove[(int) this.stat].Count; ++index2)
      {
        CarPart inPart = this.mPartImprovement.partsToImprove[(int) this.stat][index2];
        if (!carPartList.Contains(inPart) && inPart != null && (inPart.fittedCar != null && inPart.fittedCar.identifier == 0))
        {
          this.car1Entries[index1].Setup(inPart, UIPartDevStatImprovementPartEntry.State.Used, CarPartStats.CarPartStat.Condition);
          carPartList.Add(inPart);
          break;
        }
        this.car1Entries[index1].Setup((CarPart) null, UIPartDevStatImprovementPartEntry.State.Free, CarPartStats.CarPartStat.Condition);
      }
    }
    for (int index1 = 0; index1 < this.car2Entries.Count; ++index1)
    {
      for (int index2 = 0; index2 < this.mPartImprovement.partsToImprove[(int) this.stat].Count; ++index2)
      {
        CarPart inPart = this.mPartImprovement.partsToImprove[(int) this.stat][index2];
        if (!carPartList.Contains(inPart) && inPart != null && (inPart.fittedCar != null && inPart.fittedCar.identifier == 1))
        {
          this.car2Entries[index1].Setup(inPart, UIPartDevStatImprovementPartEntry.State.Used, CarPartStats.CarPartStat.Condition);
          carPartList.Add(inPart);
          break;
        }
        this.car2Entries[index1].Setup((CarPart) null, UIPartDevStatImprovementPartEntry.State.Free, CarPartStats.CarPartStat.Condition);
      }
    }
  }

  public void UpdateWidget()
  {
    this.RefreshTimeLabels();
    this.UpdateEntries();
  }

  private void RefreshTimeLabels()
  {
    this.finishDateDescription.color = Color.white;
    DateTime inEndDate = this.mPartImprovement.partWorkEndDate[(int) this.stat];
    this.devUnlocksLabel.text = "PART DEV UNLOCKS IN " + (object) UIPartDevStatImprovementWidget.GetHoursToCompletion(inEndDate) + " HOURS";
    this.finishDateDescription.text = UIPartDevStatImprovementWidget.GetDateText(inEndDate);
    this.finishDateTrack.text = UIPartDevStatImprovementWidget.GetEventText(inEndDate);
    this.workCompleteSlider.value = this.mPartImprovement.GetNormalizedTimeToFinishWork(this.stat);
  }
}
