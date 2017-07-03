// Decompiled with JetBrains decompiler
// Type: ChooseTeamOverview
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class ChooseTeamOverview : MonoBehaviour
{
  public ActivateForSeries.GameObjectData[] seriesSpecificData = new ActivateForSeries.GameObjectData[0];
  private Color mBudgetPositiveColor = new Color();
  public UITeamLogo teamLogo;
  public Flag teamFlag;
  public GameObject budgetContainer;
  public TextMeshProUGUI teamName;
  public TextMeshProUGUI teamLocation;
  public TextMeshProUGUI teamExpectedResult;
  public TextMeshProUGUI teamPressure;
  public TextMeshProUGUI teamDescription;
  public TextMeshProUGUI teamBudget;
  public ChooseTeamStats teamStats;
  public ChooseTeamScreen screen;
  private Team mTeam;

  public void Awake()
  {
    this.mBudgetPositiveColor = this.teamBudget.color;
  }

  public void Setup(Team inTeam)
  {
    if (inTeam == null)
      return;
    GameUtility.SetActiveForSeries(inTeam.championship, this.seriesSpecificData);
    this.mTeam = inTeam;
    this.SetDetails();
    this.teamStats.Setup(this.mTeam);
  }

  private void SetDetails()
  {
    this.teamLogo.SetTeam(this.mTeam);
    this.teamName.text = this.mTeam.name;
    this.teamLocation.text = Localisation.LocaliseID(this.mTeam.locationID, (GameObject) null);
    this.teamFlag.SetNationality(this.mTeam.nationality);
    this.teamDescription.text = this.mTeam.GetTeamStartDescription();
    this.teamExpectedResult.text = GameUtility.FormatForPosition(Game.instance.teamManager.CalculateExpectedPositionForChampionship(this.mTeam), (string) null);
    this.teamPressure.text = this.mTeam.GetPressureString();
    this.SetPressureColor();
    GameUtility.SetActive(this.budgetContainer, Game.instance.isCareer);
    if (!this.budgetContainer.activeSelf)
      return;
    this.teamBudget.text = GameUtility.GetCurrencyString(GameUtility.RoundCurrency(this.mTeam.financeController.availableFunds), 0);
    this.teamBudget.color = this.mTeam.financeController.availableFunds < 0L ? UIConstants.negativeColor : this.mBudgetPositiveColor;
  }

  private void SetPressureColor()
  {
    switch (this.mTeam.pressure)
    {
      case 1:
        this.teamPressure.color = UIConstants.colorBandGreen;
        break;
      case 2:
        this.teamPressure.color = UIConstants.colorBandYellow;
        break;
      case 3:
        this.teamPressure.color = UIConstants.colorBandRed;
        break;
    }
  }
}
