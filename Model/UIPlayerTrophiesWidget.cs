// Decompiled with JetBrains decompiler
// Type: UIPlayerTrophiesWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIPlayerTrophiesWidget : MonoBehaviour
{
  public UIGridList grid;
  public GameObject noTrophies;

  public void Setup(Player inPlayer)
  {
    this.grid.DestroyListItems();
    GameUtility.SetActive(this.grid.itemPrefab, true);
    int count = Game.instance.player.trophyHistory.trophies.Count;
    for (int index = count - 1; index >= 0; --index)
      this.grid.CreateListItem<UIPlayerTrophyEntry>().Setup(Game.instance.player.trophyHistory.trophies[index]);
    GameUtility.SetActive(this.grid.itemPrefab, false);
    GameUtility.SetActive(this.noTrophies, count <= 0);
  }
}
