// Decompiled with JetBrains decompiler
// Type: SessionWinnerWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class SessionWinnerWidget : MonoBehaviour
{
  public GameObject qualifying;
  public GameObject race;
  public Flag flag;
  public UICharacterPortrait portrait;
  public TextMeshProUGUI driverNameLabel;
  public TextMeshProUGUI teamNameLabel;

  private void Start()
  {
  }

  private void OnEnable()
  {
    if (!Game.instance.sessionManager.isSessionActive)
      return;
    Driver driver = Game.instance.sessionManager.GetLeader().driver;
    if (Game.instance.sessionManager.eventDetails.currentSession.sessionType == SessionDetails.SessionType.Qualifying)
    {
      this.qualifying.SetActive(true);
      this.race.SetActive(false);
    }
    else
    {
      this.qualifying.SetActive(false);
      this.race.SetActive(true);
    }
    this.flag.SetNationality(driver.nationality);
    this.portrait.SetPortrait((Person) driver);
    this.driverNameLabel.text = driver.lastName;
    this.teamNameLabel.text = driver.contract.GetTeam().name;
    this.teamNameLabel.color = driver.contract.GetTeam().GetTeamColor().primaryUIColour.normal;
  }

  private void Update()
  {
  }
}
