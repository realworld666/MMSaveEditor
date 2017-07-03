// Decompiled with JetBrains decompiler
// Type: UIFitPartsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIFitPartsWidget : MonoBehaviour
{
  public Button fitPartsButton;
  public TextMeshProUGUI partFittingDescription;
  public GameObject noWorkContainer;
  public GameObject buildingPartContainer;
  public UIGridList partsList;

  public void OnStart()
  {
    this.fitPartsButton.onClick.RemoveAllListeners();
    this.fitPartsButton.onClick.AddListener(new UnityAction(this.OnFitPartsButton));
  }

  public void OnEnter()
  {
    CarPartDesign carPartDesign = Game.instance.player.team.carManager.carPartDesign;
    carPartDesign.OnPartBuilt -= new Action(this.UpdateFitPartsData);
    carPartDesign.OnPartBuilt += new Action(this.UpdateFitPartsData);
    this.UpdateFitPartsData();
  }

  public void OnExit()
  {
    Game.instance.player.team.carManager.carPartDesign.OnPartBuilt -= new Action(this.UpdateFitPartsData);
  }

  private void UpdateFitPartsData()
  {
    this.partsList.HideListItems();
    List<CarPart> withNotification = Game.instance.player.team.carManager.partInventory.GetPartsWithNotification();
    for (int inIndex = 0; inIndex < withNotification.Count && inIndex < 2; ++inIndex)
      this.partsList.GetOrCreateItem<UIFitPartsWidgetCarPartEntry>(inIndex).Setup(withNotification[inIndex]);
    StringVariableParser.intValue1 = Mathf.Min(2, withNotification.Count);
    if (withNotification.Count == 1)
      this.partFittingDescription.text = Localisation.LocaliseID("PSG_10010427", (GameObject) null);
    else if (withNotification.Count > 1)
      this.partFittingDescription.text = Localisation.LocaliseID("PSG_10010375", (GameObject) null);
    else
      this.partFittingDescription.text = Localisation.LocaliseID("PSG_10010374", (GameObject) null);
    this.noWorkContainer.SetActive(withNotification.Count == 0);
    this.buildingPartContainer.SetActive(withNotification.Count != 0);
  }

  private void OnFitPartsButton()
  {
    UIManager.instance.ChangeScreen("CarPartFittingScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }
}
