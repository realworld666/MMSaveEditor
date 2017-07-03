// Decompiled with JetBrains decompiler
// Type: UIImprovePartsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIImprovePartsWidget : MonoBehaviour
{
  public Button improveButton;
  public TextMeshProUGUI partImproveDescription;
  public GameObject conditionContainer;
  public Image conditionProgressBar;
  public GameObject otherBarsContainer;
  public GameObject reliabilityIdleContainer;
  public GameObject reliabilityWorkContainer;
  public GameObject performanceIdleContainer;
  public GameObject performanceWorkContainer;

  public void OnStart()
  {
    this.improveButton.onClick.RemoveAllListeners();
    this.improveButton.onClick.AddListener(new UnityAction(this.OnImproveButton));
  }

  public void OnEnter()
  {
    PartImprovement partImprovement = Game.instance.player.team.carManager.partImprovement;
    partImprovement.OnItemListsChangedForUI -= new Action(this.UpdateImprovementStatus);
    partImprovement.OnItemListsChangedForUI += new Action(this.UpdateImprovementStatus);
    this.improveButton.interactable = !partImprovement.FixingCondition();
    if (partImprovement.FixingCondition())
      this.partImproveDescription.text = Localisation.LocaliseID("PSG_10010376", (GameObject) null);
    else
      this.partImproveDescription.text = Localisation.LocaliseID("PSG_10008769", (GameObject) null);
    this.otherBarsContainer.SetActive(!partImprovement.FixingCondition());
    this.conditionContainer.SetActive(partImprovement.FixingCondition());
    this.UpdateImprovementStatus();
  }

  private void Update()
  {
    if (!Game.IsActive() || !this.conditionContainer.activeSelf)
      return;
    GameUtility.SetImageFillAmountIfDifferent(this.conditionProgressBar, Game.instance.player.team.carManager.partImprovement.GetNormalizedTimeToFinishWork(CarPartStats.CarPartStat.Condition), 1f / 512f);
    if ((double) this.conditionProgressBar.fillAmount < 1.0)
      return;
    this.OnEnter();
  }

  public void OnExit()
  {
    Game.instance.player.team.carManager.partImprovement.OnItemListsChangedForUI -= new Action(this.UpdateImprovementStatus);
  }

  private void UpdateImprovementStatus()
  {
    PartImprovement partImprovement = Game.instance.player.team.carManager.partImprovement;
    this.reliabilityIdleContainer.SetActive(!partImprovement.WorkOnStatActive(CarPartStats.CarPartStat.Reliability));
    this.reliabilityWorkContainer.SetActive(partImprovement.WorkOnStatActive(CarPartStats.CarPartStat.Reliability));
    this.performanceIdleContainer.SetActive(!partImprovement.WorkOnStatActive(CarPartStats.CarPartStat.Performance));
    this.performanceWorkContainer.SetActive(partImprovement.WorkOnStatActive(CarPartStats.CarPartStat.Performance));
  }

  private void OnImproveButton()
  {
    UIManager.instance.ChangeScreen("FactoryPartDevelopmentScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }
}
