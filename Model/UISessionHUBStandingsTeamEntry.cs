// Decompiled with JetBrains decompiler
// Type: UISessionHUBStandingsTeamEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class UISessionHUBStandingsTeamEntry : UISessionHUBStandingsEntry
{
  public UICar car;

  public override void OnStart()
  {
    base.OnStart();
  }

  public override void Setup(SessionHUBStandingsScreen.HUBStanding inStanding)
  {
    if (inStanding.vehicle != null && this.vehicle != inStanding.vehicle)
      this.car.SetTeamColor(inStanding.team.GetTeamColor().primaryUIColour.normal);
    if (this.position != inStanding.predictedPosition)
    {
      this.position = inStanding.predictedPosition;
      this.racePosition.text = inStanding.predictedPosition.ToString();
    }
    base.Setup(inStanding);
  }
}
