// Decompiled with JetBrains decompiler
// Type: UIPartDevItemDragWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPartDevItemDragWidget : MonoBehaviour
{
  private Vector3 mInitialMousePosition = new Vector3(0.0f, 0.0f, 0.0f);
  public GameObject box;
  public GameObject icon;
  public UIPartDevItemDragWidget.State state;
  public UIPartDevItemDragWidget.Source source;
  public Transform iconParent;
  public Image iconLevelBacking;
  public TextMeshProUGUI levelLabel;
  public TextMeshProUGUI nameLabel;
  private CarPart mCarPart;
  private FactoryPartDevelopmentScreen mScreen;
  private UIPartDevItemEntry mEntry;

  public CarPart carPart
  {
    get
    {
      return this.mCarPart;
    }
  }

  public void OnEnter()
  {
    if ((UnityEngine.Object) this.mScreen == (UnityEngine.Object) null)
      this.mScreen = UIManager.instance.GetScreen<FactoryPartDevelopmentScreen>();
    if (!((UnityEngine.Object) this.mScreen != (UnityEngine.Object) null) || !this.mScreen.hasFocus)
      return;
    this.SetState(UIPartDevItemDragWidget.State.Idle);
  }

  private void SetPartIcon()
  {
    for (int index = 0; index < this.iconParent.childCount; ++index)
    {
      if ((CarPart.PartType) index == this.mCarPart.GetPartType())
        this.iconParent.GetChild(index).gameObject.SetActive(true);
      else
        this.iconParent.GetChild(index).gameObject.SetActive(false);
    }
  }

  public void SetSelectedPartFromList(UIPartDevItemEntry inEntry, CarPart inPart)
  {
    this.source = UIPartDevItemDragWidget.Source.ItemList;
    this.mInitialMousePosition = Input.mousePosition;
    this.mEntry = inEntry;
    this.mCarPart = inPart;
    this.transform.position = this.mEntry.transform.position;
    this.SetState(UIPartDevItemDragWidget.State.AsBox);
    this.SetPartIcon();
    this.nameLabel.text = Localisation.LocaliseEnum((Enum) inPart.GetPartType());
    StringVariableParser.intValue1 = inPart.stats.level;
    this.levelLabel.text = Localisation.LocaliseID("PSG_10010415", (GameObject) null);
    this.iconLevelBacking.color = UIConstants.GetPartLevelColor(inPart.stats.level);
  }

  private void OnMouseExit()
  {
    if (this.state == UIPartDevItemDragWidget.State.Idle)
      return;
    this.SetState(UIPartDevItemDragWidget.State.AsIcon);
  }

  private void OnReliabilityBox()
  {
    if (this.state == UIPartDevItemDragWidget.State.Idle)
      return;
    this.SetState(UIPartDevItemDragWidget.State.OverReliability);
  }

  private void OnConditionBox()
  {
    if (this.state == UIPartDevItemDragWidget.State.Idle)
      return;
    this.SetState(UIPartDevItemDragWidget.State.OverCondition);
  }

  private void OnPerformanceBox()
  {
    if (this.state == UIPartDevItemDragWidget.State.Idle)
      return;
    this.SetState(UIPartDevItemDragWidget.State.OverPerformance);
  }

  private void Update()
  {
    this.UpdateState();
    if (!Input.GetMouseButtonUp(0))
      return;
    PartImprovement partImprovement = Game.instance.player.team.carManager.partImprovement;
    if (this.state == UIPartDevItemDragWidget.State.OverCondition || this.state == UIPartDevItemDragWidget.State.OverPerformance || this.state == UIPartDevItemDragWidget.State.OverReliability)
    {
      switch (this.state)
      {
        case UIPartDevItemDragWidget.State.OverPerformance:
          partImprovement.AddPartToImprove(CarPartStats.CarPartStat.Performance, this.mCarPart);
          break;
        case UIPartDevItemDragWidget.State.OverReliability:
          partImprovement.AddPartToImprove(CarPartStats.CarPartStat.Reliability, this.mCarPart);
          break;
      }
    }
    this.SetState(UIPartDevItemDragWidget.State.Idle);
  }

  private void SetState(UIPartDevItemDragWidget.State inState)
  {
    switch (inState)
    {
      case UIPartDevItemDragWidget.State.Idle:
        this.box.SetActive(false);
        this.icon.SetActive(false);
        this.mCarPart = (CarPart) null;
        break;
      case UIPartDevItemDragWidget.State.AsBox:
        this.box.SetActive(true);
        this.icon.SetActive(false);
        break;
      case UIPartDevItemDragWidget.State.AsIcon:
        this.box.SetActive(false);
        this.icon.SetActive(true);
        break;
    }
    this.state = inState;
  }

  private void UpdateState()
  {
    if (this.state != UIPartDevItemDragWidget.State.Idle)
      UIManager.instance.SetMouseCursorState(CursorManager.State.Dragging);
    switch (this.state)
    {
      case UIPartDevItemDragWidget.State.AsBox:
        if ((double) Vector3.Distance(this.mInitialMousePosition, Input.mousePosition) > 15.0)
          this.SetState(UIPartDevItemDragWidget.State.AsIcon);
        this.transform.position = UIManager.instance.UICamera.ScreenToWorldPoint(this.mEntry.transform.position - (this.mInitialMousePosition - Input.mousePosition));
        break;
      case UIPartDevItemDragWidget.State.AsIcon:
        if (this.source == UIPartDevItemDragWidget.Source.ItemList && (double) Vector3.Distance(this.mInitialMousePosition, Input.mousePosition) < 15.0)
          this.SetState(UIPartDevItemDragWidget.State.AsBox);
        this.transform.position = UIManager.instance.UICamera.ScreenToWorldPoint(Input.mousePosition);
        break;
      case UIPartDevItemDragWidget.State.OverPerformance:
      case UIPartDevItemDragWidget.State.OverReliability:
      case UIPartDevItemDragWidget.State.OverCondition:
        this.transform.position = UIManager.instance.UICamera.ScreenToWorldPoint(Input.mousePosition);
        UIManager.instance.SetMouseCursorState(CursorManager.State.CanDrop);
        break;
    }
  }

  public enum Source
  {
    ItemList,
  }

  public enum State
  {
    Idle,
    AsBox,
    AsIcon,
    OverPerformance,
    OverReliability,
    OverCondition,
  }
}
