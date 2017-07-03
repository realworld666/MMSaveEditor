// Decompiled with JetBrains decompiler
// Type: UICharacterToolGlasses
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class UICharacterToolGlasses : MonoBehaviour
{
  public UIGridList grid;
  public CharacterCreatorToolScreen screen;

  public void Setup()
  {
    this.screen.profilesWidget.OnSelection -= new Action(this.SetGrid);
    this.screen.profilesWidget.OnSelection += new Action(this.SetGrid);
    this.SetGrid();
  }

  private void SetGrid()
  {
    GameUtility.SetActive(this.grid.gameObject, this.screen.profilesWidget.selectedGender != UICharacterToolProfiles.Gender.Both);
    if (!this.grid.gameObject.activeSelf)
      return;
    this.AddItemsToGrid();
  }

  private void AddItemsToGrid()
  {
    this.grid.DestroyListItems();
    GameUtility.SetActive(this.grid.itemPrefab, true);
    int num = this.screen.profilesWidget.selectedGender != UICharacterToolProfiles.Gender.Male ? Portrait.glassesFemale.Length : Portrait.glassesMale.Length;
    Person.Gender inGender = this.screen.profilesWidget.selectedGender != UICharacterToolProfiles.Gender.Male ? Person.Gender.Female : Person.Gender.Male;
    bool inDriver = this.screen.profilesWidget.selectedPerson is Driver;
    for (int inNum = 0; inNum < num; ++inNum)
    {
      UICharacterToolTrait listItem = this.grid.CreateListItem<UICharacterToolTrait>();
      listItem.OnStart();
      listItem.Setup(inNum, inGender, inDriver);
    }
    GameUtility.SetActive(this.grid.itemPrefab, false);
  }
}
