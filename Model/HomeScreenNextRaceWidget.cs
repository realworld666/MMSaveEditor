// Decompiled with JetBrains decompiler
// Type: HomeScreenNextRaceWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HomeScreenNextRaceWidget : MonoBehaviour
{
  public Button eventCalendarButton;
  public UICircuitImage eventCircuitImage;
  public Flag eventFlag;
  public TextMeshProUGUI eventNumberLabel;
  public TextMeshProUGUI eventNameLabel;
  public RoundInfoPip[] eventPips;
  private Circuit mCircuit;
  private Championship mChampionship;
  private int mEventID;

  public void OnStart()
  {
    this.eventCalendarButton.onClick.AddListener(new UnityAction(this.OnEventCalendarButton));
  }

  public void Setup()
  {
    this.mChampionship = Game.instance.player.team.championship;
    this.mEventID = this.mChampionship.eventNumber;
    this.mCircuit = this.mChampionship.calendar[this.mEventID].circuit;
    StringVariableParser.intValue1 = this.mEventID + 1;
    this.eventNumberLabel.text = Localisation.LocaliseID("PSG_10004153", (GameObject) null);
    this.eventNameLabel.text = Localisation.LocaliseID(this.mCircuit.locationNameID, (GameObject) null);
    this.eventCircuitImage.SetCircuitIcon(this.mCircuit);
    this.eventFlag.SetNationality(this.mCircuit.nationality);
    this.SetPips();
    GameUtility.SetActive(this.gameObject, !this.mChampionship.calendar[this.mEventID].hasEventEnded);
  }

  private void SetPips()
  {
    int eventCount = this.mChampionship.eventCount;
    for (int index = 0; index < this.eventPips.Length; ++index)
    {
      GameUtility.SetActive(this.eventPips[index].gameObject, index < eventCount);
      if (this.eventPips[index].gameObject.activeSelf)
        this.eventPips[index].SetRaceEventDetails(this.mChampionship, this.mChampionship.calendar[index]);
    }
  }

  public void OnEventCalendarButton()
  {
    UIManager.instance.ChangeScreen("EventCalendarScreen", (Entity) Game.instance.player.team.championship, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }
}
