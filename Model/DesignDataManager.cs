// Decompiled with JetBrains decompiler
// Type: DesignDataManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.IO;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("DesignData")]
public class DesignDataManager
{
  [XmlElement("RaceLength")]
  public RaceLengthDesignData raceLengthData = new RaceLengthDesignData();
  [XmlElement("CarStats")]
  public CarStatsDesignData carStatsData = new CarStatsDesignData();
  [XmlElement("Tyre")]
  public TyreDesignData tyreData = new TyreDesignData();
  [XmlElement("ShortGameLength")]
  public DesignData shortGameLength = new DesignData();
  [XmlElement("MediumGameLength")]
  public DesignData mediumGameLength = new DesignData();
  [XmlElement("LongGameLength")]
  public DesignData longGameLength = new DesignData();
  public static DesignDataManager instance;

  public static void LoadSingleSeaterDesignData()
  {
    DesignDataManager.Load("SingleSeaterDesignData");
  }

  public static void LoadGTDesignData()
  {
    DesignDataManager.Load("GTDesignData");
  }

  public static void Load(string inFileName)
  {
    DesignDataManager.instance = new XmlSerializer(typeof (DesignDataManager)).Deserialize((Stream) new MemoryStream(Resources.Load<TextAsset>("Design/" + inFileName).bytes)) as DesignDataManager;
  }

  public DesignData GetDesignData()
  {
    DesignData designData = (DesignData) null;
    switch (DesignDataManager.GetGameLength(false))
    {
      case PrefGameRaceLength.Type.Short:
        designData = this.shortGameLength;
        break;
      case PrefGameRaceLength.Type.Medium:
        designData = this.mediumGameLength;
        break;
      case PrefGameRaceLength.Type.Long:
        designData = this.longGameLength;
        break;
    }
    return designData;
  }

  public static int CalculateRaceLapCount(Championship inChampionship, float inlapDistance, bool inGetPreferencesSessionLengthOnly = false)
  {
    PrefGameRaceLength.Type gameLength = DesignDataManager.GetGameLength(inGetPreferencesSessionLengthOnly);
    int num = Mathf.CeilToInt(DesignDataManager.GetMinimumRaceDistance(gameLength) / inlapDistance);
    ChampionshipRules.SessionLength sessionLength = inChampionship.rules.raceLength[0];
    if (gameLength == PrefGameRaceLength.Type.Short)
    {
      switch (sessionLength)
      {
        case ChampionshipRules.SessionLength.Short:
          num += Mathf.CeilToInt((float) DesignDataManager.instance.raceLengthData.shortRaceLengthShortSessionRulesLapDelta / inlapDistance);
          break;
        case ChampionshipRules.SessionLength.Long:
          num += Mathf.CeilToInt((float) DesignDataManager.instance.raceLengthData.shortRaceLengthLongSessionRulesLapDelta / inlapDistance);
          break;
      }
    }
    else if (gameLength == PrefGameRaceLength.Type.Medium)
    {
      switch (sessionLength)
      {
        case ChampionshipRules.SessionLength.Short:
          num += Mathf.CeilToInt((float) DesignDataManager.instance.raceLengthData.mediumRaceLengthShortSessionRulesLapDelta / inlapDistance);
          break;
        case ChampionshipRules.SessionLength.Long:
          num += Mathf.CeilToInt((float) DesignDataManager.instance.raceLengthData.mediumRaceLengthLongSessionRulesLapDelta / inlapDistance);
          break;
      }
    }
    else
    {
      switch (sessionLength)
      {
        case ChampionshipRules.SessionLength.Short:
          num += Mathf.CeilToInt((float) DesignDataManager.instance.raceLengthData.longRaceLengthShortSessionRulesLapDelta / inlapDistance);
          break;
        case ChampionshipRules.SessionLength.Long:
          num += Mathf.CeilToInt((float) DesignDataManager.instance.raceLengthData.longRaceLengthLongSessionRulesLapDelta / inlapDistance);
          break;
      }
    }
    return num;
  }

  public static float GetRaceDistanceFromRuleType(PrefGameRaceLength.Type inGameLength, ChampionshipRules.SessionLength inRulesBasedLength)
  {
    float num = 0.0f;
    if (inGameLength == PrefGameRaceLength.Type.Short)
    {
      switch (inRulesBasedLength)
      {
        case ChampionshipRules.SessionLength.Short:
          num = (float) DesignDataManager.instance.raceLengthData.shortRaceLengthShortSessionRulesLapDelta;
          break;
        case ChampionshipRules.SessionLength.Long:
          num = (float) DesignDataManager.instance.raceLengthData.shortRaceLengthLongSessionRulesLapDelta;
          break;
      }
    }
    else if (inGameLength == PrefGameRaceLength.Type.Medium)
    {
      switch (inRulesBasedLength)
      {
        case ChampionshipRules.SessionLength.Short:
          num = (float) DesignDataManager.instance.raceLengthData.mediumRaceLengthShortSessionRulesLapDelta;
          break;
        case ChampionshipRules.SessionLength.Long:
          num = (float) DesignDataManager.instance.raceLengthData.mediumRaceLengthLongSessionRulesLapDelta;
          break;
      }
    }
    else
    {
      switch (inRulesBasedLength)
      {
        case ChampionshipRules.SessionLength.Short:
          num = (float) DesignDataManager.instance.raceLengthData.longRaceLengthShortSessionRulesLapDelta;
          break;
        case ChampionshipRules.SessionLength.Long:
          num = (float) DesignDataManager.instance.raceLengthData.longRaceLengthLongSessionRulesLapDelta;
          break;
      }
    }
    return num;
  }

  public static float GetMinimumRaceDistance(PrefGameRaceLength.Type inGameLength)
  {
    float num = 0.0f;
    switch (inGameLength)
    {
      case PrefGameRaceLength.Type.Short:
        num = DesignDataManager.instance.raceLengthData.shortRaceLength;
        break;
      case PrefGameRaceLength.Type.Medium:
        num = DesignDataManager.instance.raceLengthData.mediumRaceLength;
        break;
      case PrefGameRaceLength.Type.Long:
        num = DesignDataManager.instance.raceLengthData.longRaceLength;
        break;
    }
    return num;
  }

  public static PrefGameRaceLength.Type GetGameLength(bool inGetPreferencesSessionLengthOnly = false)
  {
    PrefGameRaceLength.Type type = App.instance.preferencesManager.gamePreferences.GetRaceLength();
    if (Game.instance.gameType == Game.GameType.SingleEvent)
    {
      switch (((QuickRaceSetupState) App.instance.gameStateManager.GetState(GameState.Type.QuickRaceSetup)).raceLength)
      {
        case QuickRaceSetupState.RaceLength.Short:
          type = PrefGameRaceLength.Type.Short;
          break;
        case QuickRaceSetupState.RaceLength.Medium:
          type = PrefGameRaceLength.Type.Medium;
          break;
        case QuickRaceSetupState.RaceLength.Long:
          type = PrefGameRaceLength.Type.Long;
          break;
      }
    }
    else if (Game.instance.sessionManager != null && !inGetPreferencesSessionLengthOnly)
      type = Game.instance.sessionManager.prefSessionLength;
    return type;
  }
}
