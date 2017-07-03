// Decompiled with JetBrains decompiler
// Type: UIItemListPartTypeEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIItemListPartTypeEntry : MonoBehaviour
{
  public GameObject[] partMissingContainer = new GameObject[0];
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
    for (int index = inList.Count - 1; index >= 0; --index)
    {
      CarPart inPart = inList[index];
      UIItemListPartEntry itemListPartEntry = this.list.GetOrCreateItem<UIItemListPartEntry>(this.mListItemCount);
      itemListPartEntry.Setup(inPart);
      itemListPartEntry.gameObject.name = "Part" + index.ToString();
      ++this.mListItemCount;
    }
    int itemCount = this.list.itemCount;
    for (int mListItemCount = this.mListItemCount; mListItemCount < itemCount; ++mListItemCount)
      GameUtility.SetActive(this.list.GetItem(mListItemCount), false);
    this.SetMissingPartsData();
  }

  public void SetMissingPartsData()
  {
    for (int inIndex = 0; inIndex < this.partMissingContainer.Length; ++inIndex)
    {
      bool inIsActive = !Game.instance.player.team.carManager.GetCar(inIndex).HasPartFitted(this.mPartType);
      GameUtility.SetActive(this.partMissingContainer[inIndex], inIsActive);
    }
    GameUtility.SetActive(this.partMissingSpacer, true);
  }
}
