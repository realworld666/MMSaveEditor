// Decompiled with JetBrains decompiler
// Type: UIPracticeSessionWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class UIPracticeSessionWidget : UIBaseSessionTopBarWidget
{
  public UIPracticeKnowledgeWidget[] knowledgeWidgets;
  private RacingVehicle mDriverOneVehicle;
  private RacingVehicle mDriverTwoVehicle;

  public override void OnEnter()
  {
    this.SetupKnowledgeSessionWidgets();
  }

  private void SetupKnowledgeSessionWidgets()
  {
    Team team = Game.instance.player.team;
    Driver selectedDriver1 = team.GetSelectedDriver(0);
    Driver selectedDriver2 = team.GetSelectedDriver(1);
    this.mDriverOneVehicle = Game.instance.vehicleManager.GetVehicle(selectedDriver1);
    this.mDriverTwoVehicle = Game.instance.vehicleManager.GetVehicle(selectedDriver2);
    if (this.mDriverOneVehicle.practiceKnowledge.GetCurrentTyreKnowledge() != this.mDriverTwoVehicle.practiceKnowledge.GetCurrentTyreKnowledge())
    {
      this.knowledgeWidgets[0].gameObject.SetActive(true);
      this.knowledgeWidgets[0].SetupForKnowldgeType(this.mDriverOneVehicle.practiceKnowledge.GetCurrentTyreKnowledge());
      this.knowledgeWidgets[1].gameObject.SetActive(true);
      this.knowledgeWidgets[1].SetupForKnowldgeType(this.mDriverTwoVehicle.practiceKnowledge.GetCurrentTyreKnowledge());
    }
    else
    {
      this.knowledgeWidgets[0].gameObject.SetActive(true);
      this.knowledgeWidgets[0].SetupForKnowldgeType(this.mDriverOneVehicle.practiceKnowledge.GetCurrentTyreKnowledge());
      this.knowledgeWidgets[1].gameObject.SetActive(false);
    }
    if (this.mDriverOneVehicle.practiceKnowledge.knowledgeType != this.mDriverTwoVehicle.practiceKnowledge.knowledgeType)
    {
      this.knowledgeWidgets[2].gameObject.SetActive(true);
      this.knowledgeWidgets[2].SetupForKnowldgeType(this.mDriverOneVehicle.practiceKnowledge.knowledgeType);
      this.knowledgeWidgets[3].gameObject.SetActive(true);
      this.knowledgeWidgets[3].SetupForKnowldgeType(this.mDriverTwoVehicle.practiceKnowledge.knowledgeType);
    }
    else
    {
      this.knowledgeWidgets[2].gameObject.SetActive(true);
      this.knowledgeWidgets[2].SetupForKnowldgeType(this.mDriverOneVehicle.practiceKnowledge.knowledgeType);
      this.knowledgeWidgets[3].gameObject.SetActive(false);
    }
  }

  private void Update()
  {
    if (this.mDriverOneVehicle.pathState.previousState != null && (this.mDriverOneVehicle.pathState.previousState.stateType == PathStateManager.StateType.GarageExit && this.mDriverOneVehicle.pathState.currentState.stateType != PathStateManager.StateType.GarageExit))
      this.SetupKnowledgeSessionWidgets();
    if (this.mDriverTwoVehicle.pathState.previousState == null || (this.mDriverTwoVehicle.pathState.previousState.stateType != PathStateManager.StateType.GarageExit || this.mDriverTwoVehicle.pathState.currentState.stateType == PathStateManager.StateType.GarageExit))
      return;
    this.SetupKnowledgeSessionWidgets();
  }

  public override bool ShouldBeEnabled()
  {
    return Game.instance.sessionManager.eventDetails.currentSession.sessionType == SessionDetails.SessionType.Practice;
  }
}
