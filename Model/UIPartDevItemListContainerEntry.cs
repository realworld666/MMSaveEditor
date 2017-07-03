// Decompiled with JetBrains decompiler
// Type: UIPartDevItemListContainerEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPartDevItemListContainerEntry : MonoBehaviour
{
  public GameObject[] partMissingContainer = new GameObject[0];
  public TextMeshProUGUI[] partMissingText = new TextMeshProUGUI[0];
  private List<UIPartDevItemEntry> mEntries = new List<UIPartDevItemEntry>();
  public TextMeshProUGUI partType;
  public UIGridList list;
  public Transform iconParent;
  public GameObject partMissingSpacer;
  private int mListItemCount;
  private CarPart.PartType mPartType;

  private void Awake()
  {
    this.list.OnStart();
  }

  public void Setup(CarPart.PartType inType, List<CarPart> inList)
  {
    this.mPartType = inType;
    CarPart.SetIcon(this.iconParent, this.mPartType);
    this.mListItemCount = 0;
    GameUtility.SetActive(this.gameObject, true);
    if ((UnityEngine.Object) this.partType != (UnityEngine.Object) null)
      this.partType.text = Localisation.LocaliseEnum((Enum) this.mPartType);
    GameUtility.SetActive(this.list.itemPrefab, false);
    this.mEntries.Clear();
    for (int inIndex = 0; inIndex < CarManager.carCount; ++inIndex)
    {
      StringVariableParser.subject = (Person) Game.instance.player.team.GetDriver(inIndex);
      this.partMissingText[inIndex].text = Localisation.LocaliseID("PSG_10012173", (GameObject) null);
    }
    for (int index = inList.Count - 1; index >= 0; --index)
    {
      CarPart inPart = inList[index];
      UIPartDevItemEntry partDevItemEntry = this.list.GetOrCreateItem<UIPartDevItemEntry>(this.mListItemCount);
      partDevItemEntry.Setup(inPart);
      partDevItemEntry.gameObject.name = "Part" + index.ToString();
      ++this.mListItemCount;
      this.mEntries.Add(partDevItemEntry);
    }
    int itemCount = this.list.itemCount;
    for (int mListItemCount = this.mListItemCount; mListItemCount < itemCount; ++mListItemCount)
      GameUtility.SetActive(this.list.GetItem(mListItemCount), false);
  }

  public void RefreshEntry()
  {
    for (int index = 0; index < this.mEntries.Count; ++index)
    {
      if (this.mEntries[index].gameObject.activeSelf)
        this.mEntries[index].RefreshEntry();
    }
  }

  public void ActivateEntries(UIPartDevItemsWidget.ToggleType inToggleType)
  {
    Car car1 = Game.instance.player.team.carManager.GetCar(0);
    Car car2 = Game.instance.player.team.carManager.GetCar(1);
    for (int index = 0; index < this.mEntries.Count; ++index)
    {
      UIPartDevItemEntry mEntry = this.mEntries[index];
      switch (inToggleType)
      {
        case UIPartDevItemsWidget.ToggleType.All:
          GameUtility.SetActive(mEntry.gameObject, true);
          break;
        case UIPartDevItemsWidget.ToggleType.Driver1:
          GameUtility.SetActive(mEntry.gameObject, mEntry.carPart.fittedCar == car1);
          break;
        case UIPartDevItemsWidget.ToggleType.Driver2:
          GameUtility.SetActive(mEntry.gameObject, mEntry.carPart.fittedCar == car2);
          break;
      }
      if (mEntry.gameObject.activeSelf)
        mEntry.RefreshEntry();
    }
    this.SetMissingPartsData(inToggleType);
  }

  public void SetMissingPartsData(UIPartDevItemsWidget.ToggleType inType)
  {
    switch (inType)
    {
      case UIPartDevItemsWidget.ToggleType.All:
        for (int index = 0; index < this.partMissingContainer.Length; ++index)
          GameUtility.SetActive(this.partMissingContainer[index], false);
        break;
      case UIPartDevItemsWidget.ToggleType.Driver1:
        this.ActivateForIndex(0);
        break;
      case UIPartDevItemsWidget.ToggleType.Driver2:
        this.ActivateForIndex(1);
        break;
    }
    GameUtility.SetActive(this.partMissingSpacer, true);
  }

  private void ActivateForIndex(int inIndex)
  {
    for (int inIndex1 = 0; inIndex1 < this.partMissingContainer.Length; ++inIndex1)
    {
      if (inIndex1 == inIndex)
      {
        bool inIsActive = !Game.instance.player.team.carManager.GetCar(inIndex1).HasPartFitted(this.mPartType);
        GameUtility.SetActive(this.partMissingContainer[inIndex1], inIsActive);
      }
      else
        GameUtility.SetActive(this.partMissingContainer[inIndex1], false);
    }
  }
}
