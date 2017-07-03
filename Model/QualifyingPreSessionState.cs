// Decompiled with JetBrains decompiler
// Type: QualifyingPreSessionState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class QualifyingPreSessionState : PreSessionState
{
  public override GameState.Type type
  {
    get
    {
      return GameState.Type.QualifyingPreSession;
    }
  }

  public override void OnEnter(bool fromSave = false)
  {
    Game.instance.player.team.SelectMainDriversForSession();
    Game.instance.sessionManager.circuit.ClearGridCrowds();
    base.OnEnter(fromSave);
  }

  public override GameState.Type GetNextState()
  {
    RaceEventDetails eventDetails = Game.instance.sessionManager.eventDetails;
    return eventDetails.GetNextQualifyingActiveSession() != eventDetails.qualifyingSessions[0] ? GameState.Type.Qualifying : GameState.Type.PreSessionHUB;
  }
}
