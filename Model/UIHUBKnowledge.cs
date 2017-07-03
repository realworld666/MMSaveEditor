// Decompiled with JetBrains decompiler
// Type: UIHUBKnowledge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class UIHUBKnowledge : UIHUBStepOption
{
  public UIHUBKnowledgeEntry[] entries;
  public UIHUBSelection widget;

  public override void OnStart()
  {
    for (int index = 0; index < this.entries.Length; ++index)
      this.entries[index].OnStart();
  }

  public override void Setup()
  {
    RacingVehicle[] vehicles = this.widget.vehicles;
    int length1 = vehicles.Length;
    int length2 = this.entries.Length;
    for (int index = 0; index < length1; ++index)
    {
      if (index < length2)
      {
        this.entries[index].Setup(vehicles[index]);
        if (Game.instance.gameType == Game.GameType.SingleEvent && ((QuickRaceSetupState) App.instance.gameStateManager.GetState(GameState.Type.QuickRaceSetup)).raceWeekend == QuickRaceSetupState.RaceWeekend.RaceOnly)
          this.entries[index].OnAutoPickButton();
      }
    }
  }

  public void UpdateEntries()
  {
    for (int index = 0; index < this.entries.Length; ++index)
      this.entries[index].UpdateKnowledgeSlotsWithSelectedBonus();
  }

  public override bool IsReady()
  {
    for (int index = 0; index < this.entries.Length; ++index)
    {
      if (!this.entries[index].isReady)
        return false;
    }
    return true;
  }
}
