// Decompiled with JetBrains decompiler
// Type: UIPlayerJobApplicationsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class UIPlayerJobApplicationsWidget : MonoBehaviour
{
  public UIGridList grid;

  public void Setup()
  {
    this.grid.DestroyListItems();
    List<PlayerJobApplication> jobApplications = Game.instance.player.jobApplications;
    GameUtility.SetActive(this.grid.itemPrefab, true);
    for (int index = jobApplications.Count - 1; index >= 0; --index)
      this.grid.CreateListItem<UIPlayerApplicationEntry>().Setup(jobApplications[index]);
    GameUtility.SetActive(this.grid.itemPrefab, false);
  }
}
