// Decompiled with JetBrains decompiler
// Type: TeamStatsRollover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class TeamStatsRollover : UIDialogBox
{
  public RectTransform rectTransform;
  public TextMeshProUGUI headerLabel;
  public TextMeshProUGUI status;
  public TeamStatsEntry[] entries;
  public GameObject specPartContainer;
  public GameObject hqIconContainer;
  public UIHQGroupIcon hqCategoryIcon;
  public GameObject carStatIconContainer;
  public UICarStatIcon carstatIcon;
  private bool mFlipRollover;

  public static void ShowRollover(ChampionshipStatistics.StatisticsReport inReport, bool inFlipRollover = true)
  {
    scSoundManager.BlockSoundEvents = true;
    UIManager.instance.dialogBoxManager.GetDialog<TeamStatsRollover>().Setup(inReport, inFlipRollover);
    scSoundManager.BlockSoundEvents = false;
  }

  public static void ShowRolloverHQ(ChampionshipStatistics.StatisticsReport inReport, bool inFlipRollover = true)
  {
    scSoundManager.BlockSoundEvents = true;
    UIManager.instance.dialogBoxManager.GetDialog<TeamStatsRollover>().SetupHQ(inReport, inFlipRollover);
    scSoundManager.BlockSoundEvents = false;
  }

  public static void ShowRolloverCar(ChampionshipStatistics.StatisticsReport inReport, bool inFlipRollover = true)
  {
    scSoundManager.BlockSoundEvents = true;
    UIManager.instance.dialogBoxManager.GetDialog<TeamStatsRollover>().SetupCar(inReport, inFlipRollover);
    scSoundManager.BlockSoundEvents = false;
  }

  public static void HideRollover()
  {
    UIManager.instance.dialogBoxManager.GetDialog<TeamStatsRollover>().Close();
  }

  public void Setup(ChampionshipStatistics.StatisticsReport inReport, bool inFlipRollover)
  {
    this.mFlipRollover = inFlipRollover;
    this.headerLabel.text = Localisation.LocaliseEnum((Enum) inReport.stat);
    GameUtility.SetActive(this.hqIconContainer, false);
    GameUtility.SetActive(this.carStatIconContainer, false);
    GameUtility.SetActive(this.specPartContainer, false);
    this.SetTeamsList(inReport);
    this.SetDetails(inReport);
    this.UpdatePosition();
    GameUtility.SetActive(this.gameObject, true);
  }

  public void SetupHQ(ChampionshipStatistics.StatisticsReport inReport, bool inFlipRollover)
  {
    this.mFlipRollover = inFlipRollover;
    this.headerLabel.text = Localisation.LocaliseEnum((Enum) inReport.category);
    GameUtility.SetActive(this.hqIconContainer, true);
    GameUtility.SetActive(this.carStatIconContainer, false);
    GameUtility.SetActive(this.specPartContainer, false);
    this.hqCategoryIcon.SetIcon(inReport.category);
    this.SetTeamsList(inReport);
    this.SetDetails(inReport);
    this.UpdatePosition();
    GameUtility.SetActive(this.gameObject, true);
  }

  public void SetupCar(ChampionshipStatistics.StatisticsReport inReport, bool inFlipRollover)
  {
    this.mFlipRollover = inFlipRollover;
    this.headerLabel.text = Localisation.LocaliseEnum((Enum) inReport.carStat);
    GameUtility.SetActive(this.hqIconContainer, false);
    GameUtility.SetActive(this.carStatIconContainer, true);
    this.carstatIcon.SetIcon(inReport.carStat, Game.instance.player.team.championship.series);
    GameUtility.SetActive(this.specPartContainer, Game.instance.player.team.championship.rules.specParts.Contains(CarPart.GetPartForStatType(inReport.carStat, Game.instance.player.team.championship.series)));
    if (!this.specPartContainer.activeSelf)
    {
      this.SetTeamsList(inReport);
      this.SetDetails(inReport);
    }
    this.UpdatePosition();
    GameUtility.SetActive(this.gameObject, true);
  }

  private void SetTeamsList(ChampionshipStatistics.StatisticsReport inReport)
  {
    int count = inReport.teamsOrdered.Count;
    for (int index = 0; index < count; ++index)
      this.entries[index].Setup(inReport.teamsOrdered[index], index + 1);
  }

  private void SetDetails(ChampionshipStatistics.StatisticsReport inReport)
  {
    GameUtility.SetActive(this.status.gameObject, inReport.championship.isPlayerChampionship);
    this.status.text = GameUtility.FormatForBestOnGrid(inReport.teamPosition, (string) null);
  }

  private void SetDetailsCar(ChampionshipStatistics.StatisticsReport inReport)
  {
    StringVariableParser.subject = (Person) inReport.bestDriver;
    StringVariableParser.ordinalNumberString = GameUtility.FormatForPosition(inReport.teamPosition, (string) null);
    StringVariableParser.stringValue1 = inReport.teamPosition != 1 ? "<color=white>" : GameUtility.ColorToRichTextHex(UIConstants.sectorSessionFastestColor);
    this.status.text = Localisation.LocaliseID("PSG_10010859", (GameObject) null);
    StringVariableParser.subject = (Person) null;
  }

  public void Close()
  {
    GameUtility.SetActive(this.gameObject, false);
  }

  private void Update()
  {
    this.UpdatePosition();
  }

  private void UpdatePosition()
  {
    GameUtility.SetTooltipTransformInsideScreen(this.rectTransform, (RectTransform) null, Vector3.zero, !this.mFlipRollover, (RectTransform) null);
  }
}
