// Decompiled with JetBrains decompiler
// Type: DialogSystemTestBox
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogSystemTestBox : UIDialogBox
{
  private MethodInfo[] mDialogMethods = new MethodInfo[0];
  private string mFilterText = string.Empty;
  private string mSelected = string.Empty;
  private List<object> mSelectedParameters = new List<object>();
  public UIGridList methodList;
  public UIGridList parametersList;
  public GameObject parametersObject;
  public Button activateMethod;
  public Button exitButton;
  public Text searchText;
  public TextMeshProUGUI parameterTitle;

  public string selectedMethod
  {
    set
    {
      this.mSelected = value;
    }
  }

  private void Start()
  {
    this.mDialogMethods = Game.instance.dialogSystem.GetType().GetMethods();
    for (int index = 0; index < this.mDialogMethods.Length; ++index)
    {
      this.methodList.CreateListItem<UIMethodEntry>().Setup(this.mDialogMethods[index].Name, this.mDialogMethods[index].GetParameters());
      Debug.Log((object) this.mDialogMethods[index].Name, (Object) null);
    }
    this.parametersObject.SetActive(false);
    this.methodList.itemPrefab.SetActive(false);
    this.parametersList.itemPrefab.SetActive(false);
    this.activateMethod.onClick.AddListener(new UnityAction(this.RunSelectedParameter));
    this.exitButton.onClick.AddListener((UnityAction) (() => this.gameObject.SetActive(false)));
  }

  private void Update()
  {
    this.activateMethod.interactable = this.mSelected != string.Empty || this.mSelected != string.Empty && this.mSelectedParameters.Count > 0 && this.parametersList.itemCount > 0;
    this.ApplyFilter();
  }

  private void ApplyFilter()
  {
    if (!(this.mFilterText != this.searchText.text))
      return;
    this.mFilterText = this.searchText.text;
    for (int inIndex = 0; inIndex < this.methodList.itemCount; ++inIndex)
    {
      UIMethodEntry uiMethodEntry = this.methodList.GetItem<UIMethodEntry>(inIndex);
      if (uiMethodEntry.name.text.Contains(this.mFilterText))
        uiMethodEntry.gameObject.SetActive(true);
      else
        uiMethodEntry.gameObject.SetActive(false);
    }
  }

  public void ClearParameters()
  {
    this.mSelectedParameters.Clear();
  }

  public void RunSelectedParameter()
  {
    if (this.mSelectedParameters.Count > 0)
      Game.instance.dialogSystem.GetType().GetMethod(this.mSelected).Invoke((object) Game.instance.dialogSystem, this.mSelectedParameters.ToArray());
    else
      Game.instance.dialogSystem.GetType().GetMethod(this.mSelected).Invoke((object) Game.instance.dialogSystem, (object[]) null);
  }

  public void SetParameterObject(object inObject)
  {
    this.ClearParameters();
    this.mSelectedParameters.Add(inObject);
  }

  public void OpenParametersBox(ParameterInfo[] inParameters)
  {
    this.mSelectedParameters.Clear();
    this.parametersObject.SetActive(true);
    this.parametersList.DestroyListItems();
    for (int index1 = 0; index1 < inParameters.Length; ++index1)
    {
      this.parameterTitle.text = "Select " + inParameters[index1].ParameterType.Name + " Parameter Option";
      if (inParameters[index1].ParameterType.Name == "Person")
      {
        for (int index2 = 0; index2 < Game.instance.player.team.contractManager.employeeSlotsCount; ++index2)
        {
          Person personHired = Game.instance.player.team.contractManager.GetEmployeeSlot(index2).personHired;
          if (personHired != null)
            this.parametersList.CreateListItem<UIParameterEntry>().Setup(personHired.name, (object) personHired);
        }
      }
      else if (inParameters[index1].ParameterType.Name == "HQsBuilding")
      {
        for (int index2 = 0; index2 < Game.instance.player.team.headquarters.hqBuildings.Count; ++index2)
        {
          HQsBuilding_v1 hqBuilding = Game.instance.player.team.headquarters.hqBuildings[index2];
          if (hqBuilding.isBuilt)
            this.parametersList.CreateListItem<UIParameterEntry>().Setup(hqBuilding.buildingName, (object) hqBuilding);
        }
      }
      else if (inParameters[index1].ParameterType.Name == "CarPart")
      {
        for (int index2 = 0; index2 < Game.instance.player.team.carManager.GetCar(0).seriesCurrentParts.Length; ++index2)
        {
          CarPart seriesCurrentPart = Game.instance.player.team.carManager.GetCar(0).seriesCurrentParts[index2];
          this.parametersList.CreateListItem<UIParameterEntry>().Setup(seriesCurrentPart.GetPartType().ToString(), (object) seriesCurrentPart);
        }
      }
      else if (inParameters[index1].ParameterType.Name == "Sponsor")
      {
        SponsorController sponsorController = Game.instance.player.team.sponsorController;
        int count = sponsorController.uniqueSponsors.Count;
        for (int index2 = 0; index2 < count; ++index2)
        {
          Sponsor uniqueSponsor = sponsorController.uniqueSponsors[index2];
          if (uniqueSponsor != null)
            this.parametersList.CreateListItem<UIParameterEntry>().Setup(uniqueSponsor.name, (object) uniqueSponsor);
        }
      }
      else if (inParameters[index1].ParameterType.Name == "Finance")
      {
        Finance finance = Game.instance.player.team.financeController.finance;
        if (finance != null)
          this.parametersList.CreateListItem<UIParameterEntry>().Setup(string.Empty, (object) finance);
      }
      else if (inParameters[index1].ParameterType.Name == "PartType")
      {
        for (CarPart.PartType partType = CarPart.PartType.Brakes; partType < CarPart.PartType.Last; ++partType)
          this.parametersList.CreateListItem<UIParameterEntry>().Setup(string.Empty, (object) partType);
      }
      else
        this.parameterTitle.text = inParameters[index1].ParameterType.Name + " NOT HOOKED UP";
    }
    if (this.parametersList.itemCount <= 0)
      return;
    this.parametersList.GetItem<UIParameterEntry>(0).toggle.isOn = true;
  }
}
