// Decompiled with JetBrains decompiler
// Type: UIPanelDriverInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelDriverInfo : MonoBehaviour
{
  public TextMeshProUGUI driverStatus;
  public TextMeshProUGUI driverName;
  public TextMeshProUGUI driverPosition;
  public Flag flag;
  public UICharacterPortrait portrait;
  public TextMeshProUGUI driverHappinessLabel;
  public Image emoji;
  private Driver mDriver;

  public void Setup(Driver inDriver)
  {
    this.mDriver = inDriver;
    this.driverStatus.text = Localisation.LocaliseEnum((Enum) this.mDriver.contract.proposedStatus);
    this.driverName.text = this.mDriver.name;
    ChampionshipEntry_v1 championshipEntry = this.mDriver.GetChampionshipEntry();
    if (championshipEntry == null)
    {
      int num = 0 + 1;
    }
    this.driverPosition.text = championshipEntry.championship.GetAcronym(false) + ": " + GameUtility.FormatForPosition(championshipEntry.GetCurrentChampionshipPosition(), (string) null);
    this.flag.SetNationality(this.mDriver.nationality);
    this.portrait.SetPortrait((Person) this.mDriver);
    this.SetHappinessData(this.mDriver, false);
  }

  public void UpdateData()
  {
    this.SetHappinessData(this.mDriver, true);
  }

  public void SetHappinessData(Driver inDriver, bool inRefreshOpinion)
  {
    if (!inDriver.carOpinion.HasOpinion() || inRefreshOpinion)
      inDriver.carOpinion.CalculateDriverOpinions(inDriver);
    if (!inDriver.carOpinion.HasOpinion())
    {
      this.driverHappinessLabel.text = Localisation.LocaliseID("PSG_10010528", (GameObject) null);
      this.driverHappinessLabel.color = Color.grey;
      this.emoji.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-ThinkingSmileyLarge");
    }
    else
    {
      CarOpinion.Happiness averageHappiness = inDriver.carOpinion.GetDriverAverageHappiness();
      StringVariableParser.subject = (Person) inDriver;
      this.driverHappinessLabel.text = Localisation.LocaliseEnum((Enum) averageHappiness);
      this.driverHappinessLabel.color = CarOpinion.GetColor(averageHappiness);
      switch (averageHappiness)
      {
        case CarOpinion.Happiness.Angry:
          this.emoji.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-AngrySmileyLarge");
          break;
        case CarOpinion.Happiness.Unhappy:
          this.emoji.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-UnhappySmileyLarge");
          break;
        case CarOpinion.Happiness.Content:
          this.emoji.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-AverageSmileyLarge");
          break;
        case CarOpinion.Happiness.Happy:
        case CarOpinion.Happiness.Delighted:
          this.emoji.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-HappySmileyLarge");
          break;
      }
    }
  }

  public void OnMouseEnterSmiley()
  {
    UIManager.instance.dialogBoxManager.GetDialog<CarHappinessOverviewRollover>().ShowRollover(this.mDriver);
  }

  public void OnMouseExitSmiley()
  {
    UIManager.instance.dialogBoxManager.GetDialog<CarHappinessOverviewRollover>().Hide();
  }
}
