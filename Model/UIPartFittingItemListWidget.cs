// Decompiled with JetBrains decompiler
// Type: UIPartFittingItemListWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPartFittingItemListWidget : MonoBehaviour
{
  public UICarPartHeader header;
  public TextMeshProUGUI heading;
  public UIGridList gridList;
  public ScrollRect scrollRect;
  public Button close;
  private int mListItemCount;

  private void Awake()
  {
    this.close.onClick.AddListener(new UnityAction(this.Hide));
  }

  public void Setup()
  {
    this.mListItemCount = 0;
    CarManager carManager = Game.instance.player.team.carManager;
    foreach (CarPart.PartType partType in CarPart.GetPartType(carManager.team.championship.series, false))
      this.CreateEntriesForPartType(new List<CarPart>((IEnumerable<CarPart>) carManager.partInventory.GetPartInventory(partType)), partType);
    this.heading.text = Localisation.LocaliseID("PSG_10008013", (GameObject) null);
    this.header.Setup(CarPart.PartType.None);
  }

  public void UpdateUnfitedParts()
  {
    for (int inIndex = 0; inIndex < this.gridList.itemCount; ++inIndex)
      this.gridList.GetItem<UIItemListPartTypeEntry>(inIndex).SetMissingPartsData();
  }

  public void Open(CarPart.PartType inType)
  {
    UIManager.instance.GetScreen<CarPartFittingScreen>().panelsAnimator.SetTrigger(AnimationHashes.ShowPartFittingPanel);
  }

  public void Hide()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    UIManager.instance.GetScreen<CarPartFittingScreen>().panelsAnimator.SetTrigger(AnimationHashes.ShowDriversPanel);
  }

  public void EnableScrolling()
  {
    this.scrollRect.enabled = true;
  }

  public void StopScrolling()
  {
    this.scrollRect.StopMovement();
    this.scrollRect.enabled = false;
  }

  private void CreateEntriesForPartType(List<CarPart> inList, CarPart.PartType inType)
  {
    UIItemListPartTypeEntry listPartTypeEntry = this.gridList.GetOrCreateItem<UIItemListPartTypeEntry>(this.mListItemCount);
    GameUtility.SetActive(listPartTypeEntry.gameObject, true);
    listPartTypeEntry.Setup(inType, inList);
    ++this.mListItemCount;
  }
}
