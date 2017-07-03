// Decompiled with JetBrains decompiler
// Type: DriverStatsModifiersRollover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class DriverStatsModifiersRollover : UIDialogBox
{
  public TextMeshProUGUI improvementToNextLevel;
  public TextMeshProUGUI improvementRate;
  public TextMeshProUGUI notImprovingLabel;
  public UIGridList improvementRateBreakdown;
  public TextMeshProUGUI totalModifier;
  public UIGridList driverStatsModifierEntries;
  public GameObject personalityTraitsObject;
  public GameObject improvementRateObject;
  public GameObject historyStatObject;
  public UIGridList historyStatGrid;
  private RectTransform mTransform;

  private void CommonSetup()
  {
    this.mTransform = this.GetComponent<RectTransform>();
    GameUtility.SetTooltipTransformInsideScreen(this.mTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
    GameUtility.SetActive(this.gameObject, true);
  }

  public void ShowRollover(float inPercentage01, Person inPerson, string inHeading)
  {
    scSoundManager.BlockSoundEvents = true;
    this.CommonSetup();
    GameUtility.SetActive(this.personalityTraitsObject, false);
    GameUtility.SetActive(this.improvementRateObject, true);
    GameUtility.SetActive(this.historyStatObject, false);
    this.SetupIncreasingStat(inPercentage01, inPerson, inHeading);
    scSoundManager.BlockSoundEvents = false;
  }

  public void ShowRollover(float inPercentage01, Driver inDriver, PersonalityTrait.StatModified inStatModified, string inHeading, bool inShowImprovementRate)
  {
    scSoundManager.BlockSoundEvents = true;
    this.CommonSetup();
    GameUtility.SetActive(this.personalityTraitsObject, true);
    GameUtility.SetActive(this.improvementRateObject, inShowImprovementRate);
    this.SetupDriverPersonalityTraits(inDriver, inStatModified);
    if (inShowImprovementRate)
      this.SetupIncreasingStat(inPercentage01, (Person) inDriver, inHeading);
    if (inStatModified == PersonalityTrait.StatModified.Morale)
      this.SetupStatModificationHistory(inDriver.moraleStatModificationHistory);
    else if (inStatModified == PersonalityTrait.StatModified.MechanicRelationship)
      this.SetupStatModificationHistory(inDriver.contract.GetTeam().GetMechanicOfDriver(inDriver).currentDriverRelationshipModificationHistory);
    else
      GameUtility.SetActive(this.historyStatObject, false);
    scSoundManager.BlockSoundEvents = false;
  }

  public void ShowRollover(Chairman inChairman)
  {
    scSoundManager.BlockSoundEvents = true;
    this.CommonSetup();
    GameUtility.SetActive(this.improvementRateObject, false);
    this.SetupChairmanHappinessModifiers(inChairman);
    this.SetupStatModificationHistory(inChairman.happinessModificationHistory);
    scSoundManager.BlockSoundEvents = false;
  }

  public void HideRollover()
  {
    GameUtility.SetActive(this.gameObject, false);
  }

  private void Update()
  {
    GameUtility.SetTooltipTransformInsideScreen(this.mTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
  }

  private void SetupDriverPersonalityTraits(Driver inDriver, PersonalityTrait.StatModified inStatModified)
  {
    this.driverStatsModifierEntries.DestroyListItems();
    List<PersonalityTrait> modifierTraitsForStat = inDriver.personalityTraitController.GetModifierTraitsForStat(inStatModified);
    bool flag = inDriver.IsPlayersDriver() && (double) Game.instance.player.driverFeedBackStatModifier > 0.0 && inStatModified == PersonalityTrait.StatModified.Feedback;
    if (modifierTraitsForStat.Count > 0 || flag)
    {
      GameUtility.SetActive(this.personalityTraitsObject, true);
      this.driverStatsModifierEntries.itemPrefab.SetActive(true);
      for (int index = 0; index < modifierTraitsForStat.Count; ++index)
      {
        UIDriverStatsModifierEntry listItem = this.driverStatsModifierEntries.CreateListItem<UIDriverStatsModifierEntry>();
        if (!modifierTraitsForStat[index].IsOwnedByDriver(inDriver))
        {
          string empty = string.Empty;
          using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
          {
            StringBuilder stringBuilder = builderSafe.stringBuilder;
            stringBuilder.Append(modifierTraitsForStat[index].ownerDriver.name);
            stringBuilder.Append(": ");
            stringBuilder.Append(modifierTraitsForStat[index].name);
            empty = stringBuilder.ToString();
          }
          if (inStatModified == PersonalityTrait.StatModified.Morale)
            listItem.Setup(empty, modifierTraitsForStat[index], PersonalityTrait.StatModified.TeammateMorale);
          else
            listItem.Setup(empty, modifierTraitsForStat[index], inStatModified);
        }
        else
          listItem.Setup(modifierTraitsForStat[index], inStatModified);
      }
      if (flag)
        this.AddPlayerExDriverStoryFeedBackModifierEntry();
      this.driverStatsModifierEntries.itemPrefab.SetActive(false);
      if (flag)
      {
        float inTotalModifierValue = Mathf.Round(inDriver.personalityTraitController.GetSingleModifierForStat(inStatModified) + Game.instance.player.driverFeedBackStatModifier);
        string empty = string.Empty;
        using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
        {
          StringBuilder stringBuilder = builderSafe.stringBuilder;
          if ((double) inTotalModifierValue > 0.0)
            stringBuilder.Append("+");
          stringBuilder.Append(inTotalModifierValue.ToString((IFormatProvider) Localisation.numberFormatter));
          empty = stringBuilder.ToString();
        }
        this.SetupTotalModifierLabel(inTotalModifierValue, empty);
      }
      else
        this.SetupTotalModifierLabel(inDriver.personalityTraitController.GetSingleModifierForStat(inStatModified), inDriver.personalityTraitController.GetSingleModifierForStatText(inStatModified));
    }
    else
      GameUtility.SetActive(this.personalityTraitsObject, false);
  }

  private void AddPlayerExDriverStoryFeedBackModifierEntry()
  {
    UIDriverStatsModifierEntry listItem = this.driverStatsModifierEntries.CreateListItem<UIDriverStatsModifierEntry>();
    string empty = string.Empty;
    using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
    {
      StringBuilder stringBuilder = builderSafe.stringBuilder;
      stringBuilder.Append(Game.instance.player.name);
      stringBuilder.Append(": ");
      stringBuilder.Append(Game.instance.player.playerBackStoryString);
      empty = stringBuilder.ToString();
    }
    listItem.Setup(empty, Game.instance.player.driverFeedBackStatModifier);
  }

  private void SetupChairmanHappinessModifiers(Chairman inChairman)
  {
    List<PersonalityTrait> happinessModifiers = inChairman.GetPersonalityTraitHappinessModifiers();
    GameUtility.SetActive(this.personalityTraitsObject, happinessModifiers.Count > 0);
    if (happinessModifiers.Count <= 0)
      return;
    this.driverStatsModifierEntries.DestroyListItems();
    this.driverStatsModifierEntries.itemPrefab.SetActive(true);
    for (int index = 0; index < happinessModifiers.Count; ++index)
    {
      UIDriverStatsModifierEntry listItem = this.driverStatsModifierEntries.CreateListItem<UIDriverStatsModifierEntry>();
      StringBuilder builder = GameUtility.GlobalStringBuilderPool.GetBuilder();
      builder.Append(happinessModifiers[index].ownerDriver.name);
      builder.Append(": ");
      builder.Append(happinessModifiers[index].name);
      string inModifierName = builder.ToString();
      GameUtility.GlobalStringBuilderPool.ReturnBuilder(builder);
      listItem.Setup(inModifierName, happinessModifiers[index], PersonalityTrait.StatModified.ChairmanHappiness);
    }
    this.SetupTotalModifierLabel(inChairman.GetHappinessModifier(), inChairman.GetHappinessModifierText());
    this.driverStatsModifierEntries.itemPrefab.SetActive(false);
  }

  private void SetupTotalModifierLabel(float inTotalModifierValue, string inModifierText)
  {
    if ((double) inTotalModifierValue > 0.0)
      this.totalModifier.color = UIConstants.colorBandGreen;
    else if ((double) inTotalModifierValue < 0.0)
      this.totalModifier.color = UIConstants.colorBandRed;
    else
      this.totalModifier.color = UIConstants.whiteColor;
    this.totalModifier.text = inModifierText;
  }

  private void SetupIncreasingStat(float inNormalizedProgress, Person inPerson, string inHeading)
  {
    Dictionary<string, float> dictionary = new Dictionary<string, float>();
    if (inPerson is Driver)
    {
      Driver driver = inPerson as Driver;
      driver.lastAccumulatedStats.Clear();
      driver.lastAccumulatedStats.CopyImprovementRates(driver.GetDriverStats());
      driver.CalculateAccumulatedStatsForDay(driver.lastAccumulatedStats);
      dictionary = PersonStats.GetStatDictionary((PersonStats) driver.lastAccumulatedStats, Game.instance.player.team.championship.series);
    }
    else if (inPerson is Mechanic)
    {
      Mechanic mechanic = inPerson as Mechanic;
      mechanic.lastAccumulatedStats.Clear();
      mechanic.CalculateAccumulatedStatsForDay(mechanic.lastAccumulatedStats);
      dictionary = PersonStats.GetStatDictionary((PersonStats) mechanic.lastAccumulatedStats, Game.instance.player.team.championship.series);
    }
    else if (inPerson is Engineer)
    {
      Engineer engineer = inPerson as Engineer;
      engineer.lastAccumulatedStats.Clear();
      engineer.CalculateAccumulatedStatsForDay(engineer.lastAccumulatedStats);
      dictionary = PersonStats.GetStatDictionary((PersonStats) engineer.lastAccumulatedStats, Game.instance.player.team.championship.series);
    }
    float inAccumulatedStat = 0.0f;
    if (!dictionary.TryGetValue(inHeading, out inAccumulatedStat))
      Debug.LogWarningFormat("Couldn't Find stat accumulation for {0} for person {1}", new object[2]
      {
        (object) inHeading,
        (object) inPerson.name
      });
    this.improvementRate.text = this.GetStringForImprovementPercentage(inAccumulatedStat);
    this.improvementToNextLevel.text = inNormalizedProgress.ToString("P0", (IFormatProvider) Localisation.numberFormatter);
    this.PopulateStatImprovementBreakdown(inPerson, inHeading);
  }

  private void PopulateStatImprovementBreakdown(Person inPerson, string inHeading)
  {
    Dictionary<string, float> dictionary = new Dictionary<string, float>();
    this.improvementRateBreakdown.DestroyListItems();
    if (inPerson.IsAtPeak())
    {
      GameUtility.SetActive(this.notImprovingLabel.gameObject, true);
      StringVariableParser.subject = inPerson;
      this.notImprovingLabel.text = Localisation.LocaliseID("PSG_10011069", (GameObject) null);
    }
    else
    {
      Dictionary<string, float> statDictionary1 = PersonStats.GetStatDictionary(inPerson.GetStats(), Game.instance.player.team.championship.series);
      float num = 0.0f;
      if (!statDictionary1.TryGetValue(inHeading, out num))
        Debug.LogWarningFormat("Couldn't Find stat accumulation for {0}", (object) inHeading);
      if ((double) num >= 20.0)
      {
        GameUtility.SetActive(this.notImprovingLabel.gameObject, true);
        this.notImprovingLabel.text = Localisation.LocaliseID("PSG_10011070", (GameObject) null);
      }
      else
      {
        GameUtility.SetActive(this.notImprovingLabel.gameObject, false);
        GameUtility.SetActive(this.improvementRateBreakdown.itemPrefab, true);
        if (inPerson is Driver)
        {
          Driver driver1 = inPerson as Driver;
          driver1.lastAccumulatedStats.Clear();
          driver1.UpdateStatsForAge(driver1.lastAccumulatedStats, PersonConstants.statIncreaseTimePerDay);
          Dictionary<string, float> statDictionary2 = PersonStats.GetStatDictionary((PersonStats) driver1.lastAccumulatedStats, Game.instance.player.team.championship.series);
          this.ShowImprovementBreakdown((Person) driver1, Localisation.LocaliseID("PSG_10001306", (GameObject) null), statDictionary2, inHeading);
          if (!driver1.IsFreeAgent())
          {
            Team team = driver1.contract.GetTeam();
            if (team != null && !(team is NullTeam) && team.headquarters != null)
            {
              for (int inIndex = 0; inIndex < Team.driverCount; ++inIndex)
              {
                Driver driver2 = team.GetDriver(inIndex);
                List<PersonalityTrait> personalityTraits = driver2.personalityTraitController.GetAllPersonalityTraits();
                for (int index = 0; index < personalityTraits.Count; ++index)
                {
                  PersonalityTrait inTrait = personalityTraits[index];
                  if (driver1 != driver2 && (inTrait.HasSpecialCase(PersonalityTrait.SpecialCaseType.MentorImproveabilityBoost) || inTrait.HasSpecialCase(PersonalityTrait.SpecialCaseType.MentorImproveabilityDebuff)) && (double) inTrait.teamDailyImprovementModifier != 0.0)
                  {
                    driver1.lastAccumulatedStats.Clear();
                    driver1.AddTraitImprovementBonus(inTrait, driver1.lastAccumulatedStats, PersonConstants.statIncreaseTimePerDay);
                    Dictionary<string, float> statDictionary3 = PersonStats.GetStatDictionary((PersonStats) driver1.lastAccumulatedStats, Game.instance.player.team.championship.series);
                    this.ShowImprovementBreakdown((Person) driver1, driver2.name + " " + inTrait.name, statDictionary3, inHeading);
                  }
                }
              }
              using (List<HQsBuilding_v1>.Enumerator enumerator = team.headquarters.hqBuildings.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  HQsBuilding_v1 current = enumerator.Current;
                  if (current.activeDriverStatProgression && current.driverStatProgression.IsImprovingStat(inHeading))
                  {
                    driver1.lastAccumulatedStats.Clear();
                    float improvementRateForAge = driver1.GetImprovementRateForAge(Game.instance.time.now);
                    driver1.CalculateAccumulatedStatsForBuilding(improvementRateForAge, driver1.lastAccumulatedStats, current);
                    Dictionary<string, float> statDictionary3 = PersonStats.GetStatDictionary((PersonStats) driver1.lastAccumulatedStats, Game.instance.player.team.championship.series);
                    this.ShowImprovementBreakdown((Person) driver1, current.buildingName, statDictionary3, inHeading);
                  }
                }
              }
              if (driver1.IsPlayersDriver() && (double) Game.instance.player.driverImprovementRateModifier != 0.0)
              {
                driver1.lastAccumulatedStats.Clear();
                driver1.CalculateAccumulatedStatsForBackstory(Game.instance.player.driverImprovementRateModifier, driver1.lastAccumulatedStats);
                Dictionary<string, float> statDictionary3 = PersonStats.GetStatDictionary((PersonStats) driver1.lastAccumulatedStats, Game.instance.player.team.championship.series);
                string empty = string.Empty;
                using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
                {
                  StringBuilder stringBuilder = builderSafe.stringBuilder;
                  stringBuilder.Append(Game.instance.player.name);
                  stringBuilder.Append(": ");
                  stringBuilder.Append(Game.instance.player.playerBackStoryString);
                  empty = stringBuilder.ToString();
                }
                this.ShowImprovementBreakdown((Person) driver1, empty, statDictionary3, inHeading);
              }
            }
          }
          dictionary = PersonStats.GetStatDictionary((PersonStats) driver1.lastAccumulatedStats, Game.instance.player.team.championship.series);
        }
        else if (inPerson is Mechanic)
        {
          Mechanic mechanic = inPerson as Mechanic;
          mechanic.lastAccumulatedStats.Clear();
          mechanic.UpdateStatsForAge(mechanic.lastAccumulatedStats, PersonConstants.statIncreaseTimePerDay);
          Dictionary<string, float> statDictionary2 = PersonStats.GetStatDictionary((PersonStats) mechanic.lastAccumulatedStats, Game.instance.player.team.championship.series);
          this.ShowImprovementBreakdown((Person) mechanic, Localisation.LocaliseID("PSG_10001306", (GameObject) null), statDictionary2, inHeading);
          if (!mechanic.IsFreeAgent())
          {
            Team team = mechanic.contract.GetTeam();
            if (team != null && !(team is NullTeam) && team.headquarters != null)
            {
              using (List<HQsBuilding_v1>.Enumerator enumerator = team.headquarters.hqBuildings.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  HQsBuilding_v1 current = enumerator.Current;
                  if (current.activeMechanicStatProgression && current.mechanicStatProgression.IsImprovingStat(inHeading))
                  {
                    mechanic.lastAccumulatedStats.Clear();
                    float improvementRateForAge = mechanic.GetImprovementRateForAge(Game.instance.time.now);
                    mechanic.CalculateAccumulatedStatsForBuilding(improvementRateForAge, mechanic.lastAccumulatedStats, current);
                    Dictionary<string, float> statDictionary3 = PersonStats.GetStatDictionary((PersonStats) mechanic.lastAccumulatedStats, Game.instance.player.team.championship.series);
                    this.ShowImprovementBreakdown((Person) mechanic, current.buildingName, statDictionary3, inHeading);
                  }
                }
              }
            }
          }
          dictionary = PersonStats.GetStatDictionary((PersonStats) mechanic.lastAccumulatedStats, Game.instance.player.team.championship.series);
        }
        else if (inPerson is Engineer)
        {
          Engineer engineer = inPerson as Engineer;
          engineer.lastAccumulatedStats.Clear();
          engineer.UpdateStatsForAge(engineer.lastAccumulatedStats, PersonConstants.statIncreaseTimePerDay);
          Dictionary<string, float> statDictionary2 = PersonStats.GetStatDictionary((PersonStats) engineer.lastAccumulatedStats, Game.instance.player.team.championship.series);
          this.ShowImprovementBreakdown((Person) engineer, Localisation.LocaliseID("PSG_10001306", (GameObject) null), statDictionary2, inHeading);
          if (!engineer.IsFreeAgent())
          {
            Team team = engineer.contract.GetTeam();
            if (team != null && !(team is NullTeam) && team.headquarters != null)
            {
              using (List<HQsBuilding_v1>.Enumerator enumerator = team.headquarters.hqBuildings.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  HQsBuilding_v1 current = enumerator.Current;
                  if (current.activeEngineerStatProgression && current.engineerStatProgression.IsImprovingStat(inHeading))
                  {
                    engineer.lastAccumulatedStats.Clear();
                    float improvementRateForAge = engineer.GetImprovementRateForAge(Game.instance.time.now);
                    engineer.CalculateAccumulatedStatsForBuilding(improvementRateForAge, engineer.lastAccumulatedStats, current);
                    Dictionary<string, float> statDictionary3 = PersonStats.GetStatDictionary((PersonStats) engineer.lastAccumulatedStats, Game.instance.player.team.championship.series);
                    this.ShowImprovementBreakdown((Person) engineer, current.buildingName, statDictionary3, inHeading);
                  }
                }
              }
            }
          }
          dictionary = PersonStats.GetStatDictionary((PersonStats) engineer.lastAccumulatedStats, Game.instance.player.team.championship.series);
        }
        GameUtility.SetActive(this.improvementRateBreakdown.itemPrefab, false);
      }
    }
  }

  private void ShowImprovementBreakdown(Person inPerson, string breakdownName, Dictionary<string, float> stats, string inHeading)
  {
    float num = 0.0f;
    if (!stats.TryGetValue(inHeading, out num))
      Debug.LogWarningFormat("Couldn't Find stat accumulation for {0}", (object) inHeading);
    if (!Mathf.Approximately(num, 0.0f))
    {
      this.improvementRateBreakdown.CreateListItem<UIDriverStatsModifierEntry>().Setup(breakdownName, this.GetStringForImprovementPercentage(num));
    }
    else
    {
      if (!inPerson.HasPassedPeakAge())
        return;
      this.improvementRateBreakdown.CreateListItem<UIDriverStatsModifierEntry>().Setup(breakdownName, Localisation.LocaliseID("PSG_10011090", (GameObject) null));
    }
  }

  private string GetStringForImprovementPercentage(float inAccumulatedStat)
  {
    StringBuilder builder = GameUtility.GlobalStringBuilderPool.GetBuilder();
    if ((double) inAccumulatedStat >= 0.0)
      builder.Append(GameUtility.ColorToRichTextHex(UIConstants.colorBandGreen));
    else
      builder.Append(GameUtility.ColorToRichTextHex(UIConstants.colorBandRed));
    if ((double) inAccumulatedStat >= 0.0)
      builder.Append("+");
    inAccumulatedStat *= 700f;
    builder.Append(inAccumulatedStat.ToString("0.00", (IFormatProvider) Localisation.numberFormatter));
    StringVariableParser.ordinalNumberString = builder.ToString();
    GameUtility.GlobalStringBuilderPool.ReturnBuilder(builder);
    return Localisation.LocaliseID("PSG_10010956", (GameObject) null);
  }

  private void SetupStatModificationHistory(StatModificationHistory inStatModificationHistory)
  {
    bool inIsActive = inStatModificationHistory.historyEntryCount > 0;
    GameUtility.SetActive(this.historyStatObject, inIsActive);
    if (!inIsActive)
      return;
    this.historyStatGrid.DestroyListItems();
    GameUtility.SetActive(this.historyStatGrid.itemPrefab, true);
    for (int index = 0; index < inStatModificationHistory.historyEntryCount; ++index)
    {
      UIDriverStatsModifierEntry listItem = this.historyStatGrid.CreateListItem<UIDriverStatsModifierEntry>();
      StatModificationHistory.StatModificationEntry modificationHistoryEntry = inStatModificationHistory.statModificationHistoryEntries[index];
      listItem.Setup(modificationHistoryEntry.modificationNameLocalised, modificationHistoryEntry.colorModifierText);
    }
    GameUtility.SetActive(this.historyStatGrid.itemPrefab, false);
  }
}
