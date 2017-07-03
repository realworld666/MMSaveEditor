// Decompiled with JetBrains decompiler
// Type: UITopBarFinance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITopBarFinance : MonoBehaviour
{
  private long mTotalCostPerRace = -1;
  private int mLocaleID = -1;
  [SerializeField]
  private TextMeshProUGUI[] budget;
  [SerializeField]
  private TextMeshProUGUI costPerRace;
  [SerializeField]
  private Image backing;
  [SerializeField]
  private Button financeButton;
  [SerializeField]
  private GameObject tooltip;
  [SerializeField]
  private UIFinanceGraphWidget tooltipGraph;
  private long mCurrentBudget;

  public void SetListener()
  {
    GameUtility.SetActive(this.tooltip, false);
    this.financeButton.onClick.AddListener(new UnityAction(this.OnButtonPressed));
    UIManager.OnScreenChange += new Action(this.UpdateVisibility);
    Game.OnGameDataChanged += new Action(this.OnGameDataChanged);
    Game.OnNewGame += new Action(this.OnNewGame);
  }

  public void OnUnload()
  {
    UIManager.OnScreenChange -= new Action(this.UpdateVisibility);
    Game.OnGameDataChanged -= new Action(this.OnGameDataChanged);
    Game.OnNewGame -= new Action(this.OnNewGame);
    if (Game.instance == null)
      return;
    Game.instance.OnPlayerTeamChanged -= new Action<Team, Team>(this.OnPlayerTeamChanged);
    Game.instance.OnAboutToBeDestroyed -= new Action(this.OnAboutToBeDestroyed);
  }

  public void OnDestroy()
  {
    UIManager.OnScreenChange -= new Action(this.UpdateVisibility);
    Game.OnGameDataChanged -= new Action(this.OnGameDataChanged);
    Game.OnNewGame -= new Action(this.OnNewGame);
    if (Game.instance == null)
      return;
    Game.instance.OnPlayerTeamChanged -= new Action<Team, Team>(this.OnPlayerTeamChanged);
    Game.instance.OnAboutToBeDestroyed -= new Action(this.OnAboutToBeDestroyed);
  }

  private void OnGameDataChanged()
  {
    this.UpdateVisibility();
    if (Game.instance == null)
      return;
    Game.instance.OnPlayerTeamChanged += new Action<Team, Team>(this.OnPlayerTeamChanged);
    Game.instance.OnAboutToBeDestroyed += new Action(this.OnAboutToBeDestroyed);
  }

  private void OnAboutToBeDestroyed()
  {
    this.UpdateVisibility();
  }

  private void OnNewGame()
  {
    this.UpdateVisibility();
  }

  private void OnPlayerTeamChanged(Team oldTeam, Team newTeam)
  {
    this.UpdateVisibility();
  }

  private void UpdateVisibility()
  {
    if (!Game.IsActive() || Game.instance.player.IsUnemployed())
      GameUtility.SetActive(this.gameObject, false);
    else if (App.instance.gameStateManager.currentState is FrontendState || App.instance.gameStateManager.currentState is PostEventFrontendState || App.instance.gameStateManager.currentState is PreSeasonState)
      GameUtility.SetActive(this.gameObject, true);
    else
      GameUtility.SetActive(this.gameObject, false);
  }

  public void OnMouseEnter()
  {
    GameUtility.SetActive(this.tooltip, true);
    this.tooltipGraph.OnEnter();
  }

  public void OnMouseExit()
  {
    GameUtility.SetActive(this.tooltip, false);
  }

  private void Update()
  {
    if (!Game.IsActive() || Game.instance.player.IsUnemployed())
      return;
    TeamFinanceController financeController = Game.instance.player.team.financeController;
    if (this.tooltip.activeSelf)
    {
      long totalCostPerRace = financeController.GetTotalCostPerRace();
      if (this.mTotalCostPerRace != totalCostPerRace)
      {
        this.mTotalCostPerRace = totalCostPerRace;
        this.costPerRace.text = GameUtility.GetCurrencyStringWithSign(this.mTotalCostPerRace, 0);
        this.costPerRace.color = GameUtility.GetCurrencyColor(this.mTotalCostPerRace);
      }
    }
    long currentBudget = financeController.finance.currentBudget;
    int lcid = App.instance.preferencesManager.gamePreferences.GetCurrencyCultureFormat().LCID;
    if (currentBudget == this.mCurrentBudget && this.mLocaleID == lcid)
      return;
    this.mCurrentBudget = currentBudget;
    this.mLocaleID = lcid;
    for (int index = 0; index < this.budget.Length; ++index)
      this.budget[index].text = GameUtility.GetCurrencyString(this.mCurrentBudget, 0);
    this.backing.color = GameUtility.GetCurrencyBackingColor(this.mCurrentBudget);
  }

  private void OnButtonPressed()
  {
    if (UIManager.instance.IsScreenOpen("FinanceScreen") || App.instance.gameStateManager.currentState.group != GameState.Group.Frontend)
      return;
    UIManager.instance.ChangeScreen("FinanceScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    GameUtility.SetActive(this.tooltip, false);
  }
}
