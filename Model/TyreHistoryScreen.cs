// Decompiled with JetBrains decompiler
// Type: TyreHistoryScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

public class TyreHistoryScreen : DataCenterScreen
{
  public UIGridList driversGrid;

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.SetupEntries();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
  }

  public void SetupEntries()
  {
    List<RacingVehicle> standings = Game.instance.sessionManager.standings;
    int count = standings.Count;
    this.driversGrid.DestroyListItems();
    for (int inIndex = 0; inIndex < count; ++inIndex)
    {
      RacingVehicle inVehicle = standings[inIndex];
      UITyreHistoryEntry tyreHistoryEntry = this.driversGrid.GetOrCreateItem<UITyreHistoryEntry>(inIndex);
      tyreHistoryEntry.barType = this.GetBarType(inVehicle, inIndex);
      tyreHistoryEntry.OnStart();
      tyreHistoryEntry.Setup(inVehicle);
    }
    GameUtility.SetActive(this.driversGrid.itemPrefab, false);
    this.driversGrid.ResetScrollbar();
  }

  public UITyreHistoryEntry.BarType GetBarType(RacingVehicle inVehicle, int inIndex)
  {
    if (inVehicle.driver.IsPlayersDriver())
      return UITyreHistoryEntry.BarType.PlayerOwned;
    return inIndex % 2 == 0 ? UITyreHistoryEntry.BarType.Lighter : UITyreHistoryEntry.BarType.Darker;
  }
}
