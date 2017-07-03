// Decompiled with JetBrains decompiler
// Type: UIDriverScreenChampionshipFormWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIDriverScreenChampionshipFormWidget : MonoBehaviour
{
  public Button standingsButton;
  public UIGridList driverStandings;
  public TextMeshProUGUI driverPositionLabel;
  public TextMeshProUGUI driverFormLabel;
  public TextMeshProUGUI championshipLabel;
  public Image driverForm;
  public GameObject detailsObject;
  public GameObject noDetailsObject;
  public TextMeshProUGUI noDetailsLabel;
  public GameObject qualifyingHeader;
  private Driver mDriver;

  public void Setup(Driver inDriver)
  {
    if (inDriver == null)
      return;
    this.mDriver = inDriver;
    StringVariableParser.subject = (Person) this.mDriver;
    if (this.mDriver.IsFreeAgent())
    {
      GameUtility.SetActive(this.detailsObject, false);
      GameUtility.SetActive(this.noDetailsObject, true);
      this.noDetailsLabel.text = Localisation.LocaliseID("PSG_10008588", (GameObject) null);
    }
    else if (this.mDriver.IsReserveDriver() && this.mDriver.GetChampionshipEntry() == null)
    {
      GameUtility.SetActive(this.detailsObject, false);
      GameUtility.SetActive(this.noDetailsObject, true);
      this.noDetailsLabel.text = Localisation.LocaliseID("PSG_10001585", (GameObject) null);
    }
    else
    {
      GameUtility.SetActive(this.detailsObject, true);
      GameUtility.SetActive(this.noDetailsObject, false);
      this.driverStandings.DestroyListItems();
      Championship championship = this.mDriver.GetChampionshipEntry().championship;
      GameUtility.SetActive(this.qualifyingHeader, championship.rules.qualifyingBasedActive);
      int eventCount = championship.eventCount;
      for (int inEventIndex = 0; inEventIndex < eventCount; ++inEventIndex)
        this.driverStandings.CreateListItem<UIDriverFormEntry>().Setup(this.mDriver, inEventIndex);
      this.driverStandings.ResetScrollbar();
      Team team = this.mDriver.contract.GetTeam();
      int inIndex = team.GetDriverIndex(this.mDriver) != 0 ? 0 : 1;
      int wins1 = this.mDriver.GetChampionshipEntry().wins;
      int wins2 = team.GetDriver(inIndex).GetChampionshipEntry().wins;
      this.driverFormLabel.text = (wins1 <= wins2 ? (wins1 >= wins2 ? "DRAW" : "LOSING") : "WINNING") + " " + wins1.ToString() + "/" + wins2.ToString() + " AGAINST TEAM MATE";
      this.driverForm.color = wins1 <= wins2 ? (wins1 >= wins2 ? UIConstants.driverFormCompareDraw : UIConstants.driverFormCompareLosing) : UIConstants.driverFormCompareWinning;
      this.driverPositionLabel.text = GameUtility.FormatForPosition(this.mDriver.GetChampionshipEntry().GetCurrentChampionshipPosition(), (string) null);
      this.championshipLabel.text = championship.GetChampionshipName(false) + ":";
      this.standingsButton.onClick.RemoveAllListeners();
      this.standingsButton.onClick.AddListener(new UnityAction(this.OnStandingsButton));
    }
    StringVariableParser.subject = (Person) null;
  }

  private void OnStandingsButton()
  {
    if (this.mDriver.IsFreeAgent())
      return;
    UIManager.instance.ChangeScreen("StandingsScreen", (Entity) this.mDriver.contract.GetTeam().championship.standings, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }
}
