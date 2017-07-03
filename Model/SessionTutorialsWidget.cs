// Decompiled with JetBrains decompiler
// Type: SessionTutorialsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class SessionTutorialsWidget : MonoBehaviour
{
  public int maxActiveTutorials = 3;
  private List<string> mTutorials = new List<string>();
  public UIGridList tutorialsGrid;

  public void Setup()
  {
    this.ClearTutorialsGrid();
    if (Game.instance == null || Game.instance.tutorialInfo == null)
      return;
    this.mTutorials = Game.instance.tutorialInfo.GetTutorialRules(this.maxActiveTutorials, TutorialInfo.RuleStatus.Triggered);
    this.UpdateTutorialsGrid();
  }

  public void Update()
  {
    this.UpdateTutorialsGrid();
  }

  public bool ValidateGridTutorial(string inRuleGameArea)
  {
    for (int index = 0; index < this.mTutorials.Count; ++index)
    {
      if (this.mTutorials[index] == inRuleGameArea)
        return true;
    }
    return false;
  }

  public bool ValidateRule(string inRuleGameArea)
  {
    for (int inIndex = 0; inIndex < this.tutorialsGrid.itemCount; ++inIndex)
    {
      SessionTutorialEntry sessionTutorialEntry = this.tutorialsGrid.GetItem<SessionTutorialEntry>(inIndex);
      if (sessionTutorialEntry.rule != null && sessionTutorialEntry.rule.gameArea == inRuleGameArea)
        return true;
    }
    return false;
  }

  public void ValidateGridTutorials()
  {
    if (this.tutorialsGrid.itemCount <= 0)
      return;
    for (int inIndex = 0; inIndex < this.tutorialsGrid.itemCount; ++inIndex)
    {
      if (!this.ValidateGridTutorial(this.tutorialsGrid.GetItem<SessionTutorialEntry>(inIndex).rule.gameArea))
        this.tutorialsGrid.DestroyListItem(inIndex);
    }
  }

  public void AddNewGridTutorials()
  {
    if (this.tutorialsGrid.itemCount >= this.maxActiveTutorials)
      return;
    this.tutorialsGrid.itemPrefab.gameObject.SetActive(true);
    for (int index = 0; index < this.mTutorials.Count; ++index)
    {
      if (!this.ValidateRule(this.mTutorials[index]))
      {
        DialogRule simulationTutorialRule = Game.instance.tutorialInfo.GetSimulationTutorialRule(this.mTutorials[index], TutorialInfo.RuleStatus.Triggered);
        if (simulationTutorialRule != null)
          this.tutorialsGrid.CreateListItem<SessionTutorialEntry>().Setup(simulationTutorialRule, this);
        if (this.tutorialsGrid.itemCount >= this.maxActiveTutorials)
          break;
      }
    }
    this.tutorialsGrid.itemPrefab.gameObject.SetActive(false);
  }

  public void UpdateTutorialsGrid()
  {
    this.mTutorials = Game.instance.tutorialInfo.GetTutorialRules(this.maxActiveTutorials, TutorialInfo.RuleStatus.Triggered);
    this.ValidateGridTutorials();
    this.AddNewGridTutorials();
  }

  public void ClearTutorialsGrid()
  {
    this.tutorialsGrid.DestroyListItems();
  }
}
