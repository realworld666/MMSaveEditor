// Decompiled with JetBrains decompiler
// Type: UIDriverPerformanceWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDriverPerformanceWidget : MonoBehaviour
{
  public TextMeshProUGUI championshipName;
  public TextMeshProUGUI championshipPosition;
  public Image championshipPositionOutcomePip;
  public TextMeshProUGUI points;
  public Image pointsOutcomePip;
  public TextMeshProUGUI seasonForm;
  public Image seasonFormOutcomePip;
  public TextMeshProUGUI podiums;
  public Image podiumsOutcomePip;
  public TextMeshProUGUI qualifying;
  public Image qualifyingOutcomePip;
  public TextMeshProUGUI racePosition;
  public Image racePositionOutcomePip;
  public TextMeshProUGUI wins;
  public Image winsOutcomePip;
  public TextMeshProUGUI total;
  public Image totalOutcomePip;

  public void Setup(Driver inCurrentDriver, Driver inOtherDriver)
  {
    int currentTotal = 0;
    int otherTotal = 0;
    ChampionshipEntry_v1 championshipEntry1 = inCurrentDriver.GetChampionshipEntry();
    ChampionshipEntry_v1 championshipEntry2 = inOtherDriver.GetChampionshipEntry();
    this.CompareChampionshipPosition(championshipEntry1, championshipEntry2, ref currentTotal, ref otherTotal);
    this.ComparePoints(championshipEntry1, championshipEntry2, ref currentTotal, ref otherTotal);
    this.ComparePodiums(championshipEntry1, championshipEntry2, ref currentTotal, ref otherTotal);
    this.CompareSeasonForm(inCurrentDriver, inOtherDriver, ref currentTotal, ref otherTotal);
    this.CompareWins(championshipEntry1, championshipEntry2, ref currentTotal, ref otherTotal);
    this.CompareOutQualifying(championshipEntry1, championshipEntry2, ref currentTotal, ref otherTotal);
    this.CompareOutRace(championshipEntry1, championshipEntry2, ref currentTotal, ref otherTotal);
    this.total.text = currentTotal.ToString() + "/7";
    int num = 0;
    this.ColorPipGreater(this.totalOutcomePip, currentTotal, otherTotal, ref num, ref num);
  }

  private void ColorPipGreater(Image inPip, int inCurrentValue, int inOtherValue, ref int currentTotal, ref int otherTotal)
  {
    this.ColorPipGreater(inPip, (float) inCurrentValue, (float) inOtherValue, ref currentTotal, ref otherTotal);
  }

  private void ColorPipGreater(Image inPip, float inCurrentValue, float inOtherValue, ref int currentTotal, ref int otherTotal)
  {
    if ((double) inCurrentValue > (double) inOtherValue)
    {
      inPip.color = UIConstants.driverPerformanceOutcomeGreen;
      currentTotal = currentTotal + 1;
    }
    else if ((double) inCurrentValue == (double) inOtherValue)
    {
      inPip.color = UIConstants.driverPerformanceOutcomeGrey;
    }
    else
    {
      inPip.color = UIConstants.driverPerformanceOutcomeRed;
      otherTotal = otherTotal + 1;
    }
  }

  private void ColorPipSmaller(Image inPip, int inCurrentValue, int inOtherValue, ref int currentTotal, ref int otherTotal)
  {
    this.ColorPipSmaller(inPip, (float) inCurrentValue, (float) inOtherValue, ref currentTotal, ref otherTotal);
  }

  private void ColorPipSmaller(Image inPip, float inCurrentValue, float inOtherValue, ref int currentTotal, ref int otherTotal)
  {
    if ((double) inCurrentValue < (double) inOtherValue)
    {
      inPip.color = UIConstants.driverPerformanceOutcomeGreen;
      currentTotal = currentTotal + 1;
    }
    else if ((double) inCurrentValue == (double) inOtherValue)
    {
      inPip.color = UIConstants.driverPerformanceOutcomeGrey;
    }
    else
    {
      inPip.color = UIConstants.driverPerformanceOutcomeRed;
      otherTotal = otherTotal + 1;
    }
  }

  private void CompareChampionshipPosition(ChampionshipEntry_v1 inEntry, ChampionshipEntry_v1 inOtherEntry, ref int currentTotal, ref int otherTotal)
  {
    int championshipPosition1 = inEntry.GetCurrentChampionshipPosition();
    int championshipPosition2 = inOtherEntry.GetCurrentChampionshipPosition();
    this.championshipName.SetText(inEntry.championship.GetChampionshipName(false));
    this.championshipPosition.text = GameUtility.FormatForPosition(championshipPosition1, (string) null);
    this.ColorPipSmaller(this.championshipPositionOutcomePip, championshipPosition1, championshipPosition2, ref currentTotal, ref otherTotal);
  }

  private void CompareSeasonForm(Driver inDriver, Driver inOtherDriver, ref int currentTotal, ref int otherTotal)
  {
    float average1 = inDriver.careerForm.GetAverage();
    float average2 = inOtherDriver.careerForm.GetAverage();
    this.seasonForm.text = average1.ToString("0.0", (IFormatProvider) Localisation.numberFormatter);
    this.ColorPipGreater(this.seasonFormOutcomePip, average1, average2, ref currentTotal, ref otherTotal);
  }

  private void ComparePodiums(ChampionshipEntry_v1 inEntry, ChampionshipEntry_v1 inOtherEntry, ref int currentTotal, ref int otherTotal)
  {
    int podiums1 = inEntry.podiums;
    int podiums2 = inOtherEntry.podiums;
    this.podiums.text = podiums1.ToString();
    this.ColorPipGreater(this.podiumsOutcomePip, podiums1, podiums2, ref currentTotal, ref otherTotal);
  }

  private void CompareWins(ChampionshipEntry_v1 inEntry, ChampionshipEntry_v1 inOtherEntry, ref int currentTotal, ref int otherTotal)
  {
    int wins1 = inEntry.wins;
    int wins2 = inOtherEntry.wins;
    this.wins.text = wins1.ToString();
    this.ColorPipGreater(this.winsOutcomePip, wins1, wins2, ref currentTotal, ref otherTotal);
  }

  private void ComparePoints(ChampionshipEntry_v1 inEntry, ChampionshipEntry_v1 inOtherEntry, ref int currentTotal, ref int otherTotal)
  {
    int currentPoints1 = inEntry.GetCurrentPoints();
    int currentPoints2 = inOtherEntry.GetCurrentPoints();
    this.points.text = currentPoints1.ToString();
    this.ColorPipGreater(this.pointsOutcomePip, currentPoints1, currentPoints2, ref currentTotal, ref otherTotal);
  }

  private void CompareOutQualifying(ChampionshipEntry_v1 inEntry, ChampionshipEntry_v1 inOtherEntry, ref int currentTotal, ref int otherTotal)
  {
    ChampionshipEntry_v1 championshipEntryV1_1 = inEntry;
    ChampionshipEntry_v1 championshipEntryV1_2 = inOtherEntry;
    int inCurrentValue = 0;
    int inOtherValue = 0;
    for (int inEventNumber = 0; inEventNumber < championshipEntryV1_1.championship.eventNumber; ++inEventNumber)
    {
      if (championshipEntryV1_1.GetQualifyingPositionForEvent(inEventNumber) < championshipEntryV1_2.GetQualifyingPositionForEvent(inEventNumber))
        ++inCurrentValue;
      else
        ++inOtherValue;
    }
    this.qualifying.text = inCurrentValue.ToString();
    this.ColorPipGreater(this.qualifyingOutcomePip, inCurrentValue, inOtherValue, ref currentTotal, ref otherTotal);
  }

  private void CompareOutRace(ChampionshipEntry_v1 inEntry, ChampionshipEntry_v1 inOtherEntry, ref int currentTotal, ref int otherTotal)
  {
    ChampionshipEntry_v1 championshipEntryV1_1 = inEntry;
    ChampionshipEntry_v1 championshipEntryV1_2 = inOtherEntry;
    int inCurrentValue = 0;
    int inOtherValue = 0;
    for (int inEventNumber = 0; inEventNumber < championshipEntryV1_1.championship.eventNumber; ++inEventNumber)
    {
      if (championshipEntryV1_1.GetRacePositionForEvent(inEventNumber) < championshipEntryV1_2.GetRacePositionForEvent(inEventNumber))
        ++inCurrentValue;
      else
        ++inOtherValue;
    }
    this.racePosition.text = inCurrentValue.ToString();
    this.ColorPipGreater(this.racePositionOutcomePip, inCurrentValue, inOtherValue, ref currentTotal, ref otherTotal);
  }
}
