// Decompiled with JetBrains decompiler
// Type: UIPartDevItemsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPartDevItemsWidget : MonoBehaviour
{
  private List<UIPartDevItemListContainerEntry> mListContainerEntries = new List<UIPartDevItemListContainerEntry>();
  public UIGridList gridList;
  public TextMeshProUGUI titleLabel;
  public TextMeshProUGUI driver1ToggleLabel;
  public TextMeshProUGUI driver2ToggleLabel;
  public Toggle showAllParts;
  public Toggle showDriver1Parts;
  public Toggle showDriver2Parts;
  private UIPartDevItemsWidget.ToggleType mToggleType;

  private void Awake()
  {
    this.showAllParts.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnToggleValueChange(value, UIPartDevItemsWidget.ToggleType.All)));
    this.showDriver1Parts.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnToggleValueChange(value, UIPartDevItemsWidget.ToggleType.Driver1)));
    this.showDriver2Parts.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnToggleValueChange(value, UIPartDevItemsWidget.ToggleType.Driver2)));
  }

  private void OnToggleValueChange(bool inBool, UIPartDevItemsWidget.ToggleType inType)
  {
    if (!inBool)
      scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!inBool)
      return;
    this.mToggleType = inType;
    this.ShowEntriesForType(inType);
  }

  private void ShowEntriesForType(UIPartDevItemsWidget.ToggleType inType)
  {
    for (int index = 0; index < this.mListContainerEntries.Count; ++index)
      this.mListContainerEntries[index].ActivateEntries(inType);
  }

  public void RefreshList()
  {
    for (int index = 0; index < this.mListContainerEntries.Count; ++index)
      this.mListContainerEntries[index].RefreshEntry();
  }

  public void Setup()
  {
    this.driver1ToggleLabel.text = Game.instance.player.team.GetDriver(0).name;
    this.driver2ToggleLabel.text = Game.instance.player.team.GetDriver(1).name;
    CarManager carManager = Game.instance.player.team.carManager;
    this.mListContainerEntries.Clear();
    CarPart.PartType[] partType1 = CarPart.GetPartType(carManager.team.championship.series, false);
    this.gridList.HideListItems();
    for (int inIndex = 0; inIndex < partType1.Length; ++inIndex)
    {
      CarPart.PartType partType2 = partType1[inIndex];
      this.CreateEntriesForPartType(carManager.partInventory.GetPartInventory(partType2), partType2, inIndex);
    }
    this.ShowEntriesForType(this.mToggleType);
  }

  private void CreateEntriesForPartType(List<CarPart> inList, CarPart.PartType inType, int inIndex)
  {
    UIPartDevItemListContainerEntry listContainerEntry = this.gridList.GetOrCreateItem<UIPartDevItemListContainerEntry>(inIndex);
    listContainerEntry.Setup(inType, inList);
    this.mListContainerEntries.Add(listContainerEntry);
  }

  public enum ToggleType
  {
    All,
    Driver1,
    Driver2,
  }
}
