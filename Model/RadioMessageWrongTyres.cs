// Decompiled with JetBrains decompiler
// Type: RadioMessageWrongTyres
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RadioMessageWrongTyres : RadioMessage
{
  private float mWaitTimer;

  public RadioMessageWrongTyres(RacingVehicle inVehicle, TeamRadio inTeamRadio)
    : base(inVehicle, inTeamRadio)
  {
    this.dilemmaType = RadioMessage.DilemmaType.Tyres;
  }

  public override void OnLoad()
  {
  }

  protected override void OnSimulationUpdate()
  {
  }

  public void OnTyreChange()
  {
    this.mHasPlayerBeenInformedAlready[0] = false;
    this.mWaitTimer = 0.0f;
  }

  public override bool DontDelayForDriverFeedback()
  {
    return true;
  }

  protected override bool CheckForDilemmaAndSendToPlayer()
  {
    if (Game.instance.sessionManager.sessionType == SessionDetails.SessionType.Race && !this.IsChangingTyres() && (!this.mVehicle.pathState.IsInPitlaneArea() && !this.mVehicle.timer.hasSeenChequeredFlag) && !this.mVehicle.behaviourManager.isOutOfRace)
      return this.CheckWeatherAgainstCompound();
    return false;
  }

  private bool CheckWeatherAgainstCompound()
  {
    if (!this.mHasPlayerBeenInformedAlready[0] && this.mVehicle.setup.tyreSet.GetTread() != SessionStrategy.GetRecommendedTreadForTrackConditions())
    {
      this.mWaitTimer += GameTimer.simulationDeltaTime;
      if ((double) this.mWaitTimer > 30.0)
      {
        this.mWaitTimer = 0.0f;
        this.mHasPlayerBeenInformedAlready[0] = true;
        this.CreateDialogQuery();
        return true;
      }
    }
    return false;
  }

  private bool IsChangingTyres()
  {
    if (this.mVehicle.strategy.IsGoingToPit())
      return this.mVehicle.setup.IsChangingTyres();
    return false;
  }

  private void CreateDialogQuery()
  {
    DialogQuery inQuery = new DialogQuery();
    this.AddSourceCriteria(inQuery);
    this.AddPersonCriteria(inQuery, (Person) this.mVehicle.driver);
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(inQuery, false);
    if (this.dialogRule == null)
      return;
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    this.SendDilemma();
  }

  private void AddSourceCriteria(DialogQuery inQuery)
  {
    switch (SessionStrategy.GetRecommendedTreadForTrackConditions())
    {
      case TyreSet.Tread.Slick:
        if (this.mVehicle.setup.tyreSet.GetCompound() == TyreSet.Compound.Wet)
        {
          inQuery.AddCriteria("Source", "WrongTyresWets");
          break;
        }
        if (this.mVehicle.setup.tyreSet.GetCompound() != TyreSet.Compound.Intermediate)
          break;
        inQuery.AddCriteria("Source", "WrongTyresInters");
        break;
      case TyreSet.Tread.LightTread:
        inQuery.AddCriteria("Source", "WrongTyresNeedInters");
        break;
      case TyreSet.Tread.HeavyTread:
        inQuery.AddCriteria("Source", "WrongTyresNeedWets");
        break;
    }
  }
}
