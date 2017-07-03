// Decompiled with JetBrains decompiler
// Type: UIStaffComponentsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIStaffComponentsWidget : MonoBehaviour
{
  public UIGridList gridList;
  private Engineer mEngineer;

  public void Setup()
  {
    this.mEngineer = (Engineer) Game.instance.player.team.contractManager.GetPersonOnJob(Contract.Job.EngineerLead);
    this.gridList.DestroyListItems();
    GameUtility.SetActive(this.gridList.itemPrefab, true);
    for (int index = 0; index < 5; ++index)
    {
      UIPartComponentIcon listItem = this.gridList.CreateListItem<UIPartComponentIcon>();
      CarPartComponent componentForLevel = this.mEngineer.GetComponentForLevel(index + 1);
      listItem.Setup(componentForLevel);
      GameUtility.SetActive(listItem.gameObject, true);
    }
    GameUtility.SetActive(this.gridList.itemPrefab, false);
  }
}
