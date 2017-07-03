// Decompiled with JetBrains decompiler
// Type: UITopBarCurrentObjective
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITopBarCurrentObjective : MonoBehaviour
{
  public UITopBarChallengeEntry currentChallenge;
  public UITopBarObjectiveEntry chairmanObjective;
  public Button teamRolloverButton;
  public GameObject ArrowIcon;
  private bool mIsEnabled;

  public void SetListener()
  {
    EventTrigger eventTrigger = this.gameObject.AddComponent<EventTrigger>();
    EventTrigger.Entry entry1 = new EventTrigger.Entry();
    entry1.eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter;
    entry1.callback.AddListener((UnityAction<BaseEventData>) (eventData => this.OnMouseEnter()));
    eventTrigger.get_triggers().Add(entry1);
    EventTrigger.Entry entry2 = new EventTrigger.Entry();
    entry2.eventID = UnityEngine.EventSystems.EventTriggerType.PointerExit;
    entry2.callback.AddListener((UnityAction<BaseEventData>) (eventData => this.OnMouseExit()));
    eventTrigger.get_triggers().Add(entry2);
    this.TurnOffList();
  }

  private void Update()
  {
    if (Game.instance == null || Game.instance.player.team == null)
    {
      GameUtility.SetInteractable(this.teamRolloverButton, false);
      GameUtility.SetActive(this.ArrowIcon, false);
      this.mIsEnabled = false;
    }
    else
    {
      this.mIsEnabled = false;
      if (!(Game.instance.player.team is NullTeam))
        this.mIsEnabled = Game.instance.player.team.chairman.playerChosenExpectedTeamChampionshipPosition != 0;
      if (Game.instance.challengeManager.IsAttemptingChallenge())
        this.mIsEnabled = true;
      GameUtility.SetInteractable(this.teamRolloverButton, this.mIsEnabled);
      GameUtility.SetActive(this.ArrowIcon, this.mIsEnabled);
    }
  }

  private void TrySetup()
  {
    if (Game.instance.player.team != null && !(Game.instance.player.team is NullTeam))
    {
      int championshipPosition = Game.instance.player.team.chairman.playerChosenExpectedTeamChampionshipPosition;
      if (championshipPosition != 0)
      {
        bool inIsPositiveColor = Game.instance.player.team.GetExpectedChampionshipResult() <= championshipPosition;
        StringVariableParser.ordinalNumberString = GameUtility.FormatForPositionOrAbove(championshipPosition, (string) null);
        this.chairmanObjective.Setup(Localisation.LocaliseID("PSG_10010405", (GameObject) null), !inIsPositiveColor ? Localisation.LocaliseID("PSG_10008875", (GameObject) null) : Localisation.LocaliseID("PSG_10009312", (GameObject) null), inIsPositiveColor);
        GameUtility.SetActive(this.chairmanObjective.gameObject, true);
      }
      else
        GameUtility.SetActive(this.chairmanObjective.gameObject, false);
    }
    if (Game.instance.challengeManager.IsAttemptingChallenge())
    {
      this.currentChallenge.Setup(Game.instance.challengeManager.currentChallenge);
      GameUtility.SetActive(this.currentChallenge.gameObject, true);
    }
    else
      GameUtility.SetActive(this.currentChallenge.gameObject, false);
  }

  private void OnMouseEnter()
  {
    if (!this.mIsEnabled)
      return;
    this.TrySetup();
  }

  private void OnMouseExit()
  {
    this.TurnOffList();
  }

  private void TurnOffList()
  {
    GameUtility.SetActive(this.currentChallenge.gameObject, false);
    GameUtility.SetActive(this.chairmanObjective.gameObject, false);
  }
}
