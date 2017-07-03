// Decompiled with JetBrains decompiler
// Type: PersonCareerWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PersonCareerWidget : MonoBehaviour
{
  public UIGridList grid;
  public GameObject noData;
  private Person mPerson;

  public void Setup(Person inPerson)
  {
    if (inPerson == null)
      return;
    this.mPerson = inPerson;
    this.SetGrid();
  }

  private void SetGrid()
  {
    this.grid.DestroyListItems();
    GameUtility.SetActive(this.grid.itemPrefab, true);
    int careerCount = this.mPerson.careerHistory.careerCount;
    for (int index = careerCount - 1; index >= 0; --index)
    {
      PersonCareerEntry listItem = this.grid.CreateListItem<PersonCareerEntry>();
      listItem.Setup(this.mPerson.careerHistory.career[index]);
      GameUtility.SetActive(listItem.gameObject, true);
    }
    GameUtility.SetActive(this.grid.itemPrefab, false);
    GameUtility.SetActive(this.noData, careerCount <= 0);
  }
}
