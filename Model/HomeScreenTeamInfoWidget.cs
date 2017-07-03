// Decompiled with JetBrains decompiler
// Type: HomeScreenTeamInfoWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HomeScreenTeamInfoWidget : MonoBehaviour
{
  public UITeamLogo teamLogo;
  public TextMeshProUGUI championship;
  public Button[] driverButtons;
  public UICharacterPortrait[] driverPortraits;
  public TextMeshProUGUI[] driverNames;
  public TextMeshProUGUI[] driverPositions;
  public Flag[] driverFlags;

  public void Setup()
  {
    Team team = Game.instance.player.team;
    this.teamLogo.SetTeam(team);
    this.championship.text = team.championship.GetChampionshipName(false);
    for (int inIndex = 0; inIndex < Team.mainDriverCount; ++inIndex)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      HomeScreenTeamInfoWidget.\u003CSetup\u003Ec__AnonStorey83 setupCAnonStorey83 = new HomeScreenTeamInfoWidget.\u003CSetup\u003Ec__AnonStorey83();
      // ISSUE: reference to a compiler-generated field
      setupCAnonStorey83.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      setupCAnonStorey83.driver = team.GetDriver(inIndex);
      // ISSUE: reference to a compiler-generated field
      this.driverNames[inIndex].text = setupCAnonStorey83.driver.shortName;
      // ISSUE: reference to a compiler-generated field
      this.driverPortraits[inIndex].SetPortrait((Person) setupCAnonStorey83.driver);
      // ISSUE: reference to a compiler-generated field
      this.driverFlags[inIndex].SetNationality(setupCAnonStorey83.driver.nationality);
      string str = "-";
      // ISSUE: reference to a compiler-generated field
      if (setupCAnonStorey83.driver.GetChampionshipEntry().GetCurrentChampionshipPosition() > 0)
      {
        // ISSUE: reference to a compiler-generated field
        str = GameUtility.FormatForPosition(setupCAnonStorey83.driver.GetChampionshipEntry().GetCurrentChampionshipPosition(), (string) null);
      }
      this.driverPositions[inIndex].text = str;
      this.driverButtons[inIndex].onClick.RemoveAllListeners();
      // ISSUE: reference to a compiler-generated method
      this.driverButtons[inIndex].onClick.AddListener(new UnityAction(setupCAnonStorey83.\u003C\u003Em__17D));
    }
  }

  public void OnDriverButton(Driver inDriver)
  {
    UIManager.instance.ChangeScreen("DriverScreen", (Entity) inDriver, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }
}
