using FullSerializer;
using MM2;
using System;
using System.Collections.Generic;
using System.Text;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Chairman : Person
{
  public ChairmanUltimatum ultimatum = new ChairmanUltimatum();
  private StatModificationHistory mHappinessModificationHistory = new StatModificationHistory();
  public int ultimatumsGeneratedThisSeason;
  public int costFocus;
  public int patience;
  public int patienceStrikesCount;
  public int playerChosenExpectedTeamChampionshipPosition;
  private float mHappiness;
  public int happinessBeforeEvent;

  public enum EstimatedPosition
  {
    Low,
    Medium,
    High,
  }

  public enum RequestFundsAnswer
  {
    Accepted,
    DeclinedLowHappiness,
    DeclinedPreSeason,
    DeclinedSeasonStart,
    Declined,
  }
}
