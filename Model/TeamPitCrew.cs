// Decompiled with JetBrains decompiler
// Type: TeamPitCrew
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class TeamPitCrew : MonoBehaviour
{
  private GameObject mPitCrewArt;
  private Team mTeam;
  private RacingVehicle[] mVehicles;
  private int mLayerMask;

  private void Start()
  {
    this.mPitCrewArt = (GameObject) Object.Instantiate(Resources.Load("Prefabs/Simulation/PitCrew"));
    this.mPitCrewArt.transform.position = Vector3.zero;
    this.mPitCrewArt.transform.rotation = Quaternion.identity;
    this.mPitCrewArt.transform.SetParent(this.transform, false);
    this.mPitCrewArt.SetActive(false);
    this.mLayerMask = 1 << LayerMask.NameToLayer("Env_Track");
  }

  public void SetTeam(Team inTeam)
  {
    this.mTeam = inTeam;
    this.mVehicles = (RacingVehicle[]) null;
    Renderer[] componentsInChildren = this.mPitCrewArt.GetComponentsInChildren<Renderer>(true);
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      if (componentsInChildren[index].sharedMaterial.name.Contains("Pit_Crew"))
      {
        componentsInChildren[index].material = new Material(componentsInChildren[index].sharedMaterial);
        componentsInChildren[index].material.SetColor(MaterialPropertyHashes._Color, this.mTeam.GetTeamColor().staffColor.primary);
      }
    }
    this.mPitCrewArt.SetActive(false);
  }

  public void ClearTeam()
  {
    this.mTeam = (Team) null;
    this.mVehicles = (RacingVehicle[]) null;
    this.mPitCrewArt.SetActive(false);
  }

  private void Update()
  {
    if (Game.IsActive() && Game.instance.sessionManager.sessionType == SessionDetails.SessionType.Race)
    {
      if (this.mTeam == null)
        return;
      if (this.mVehicles == null)
        this.mVehicles = Game.instance.vehicleManager.GetVehiclesByTeam(this.mTeam);
      bool flag = false;
      for (int index = 0; index < this.mVehicles.Length; ++index)
      {
        if (!this.mVehicles[index].behaviourManager.isOutOfRace && this.mVehicles[index].strategy.status != SessionStrategy.Status.PitThruPenalty && !this.mVehicles[index].timer.hasSeenChequeredFlag && (this.mVehicles[index].strategy.IsGoingToPit() || this.mVehicles[index].pathState.IsInPitlaneArea()))
          flag = true;
      }
      if (flag)
      {
        if (!this.mPitCrewArt.activeSelf)
        {
          PathData.Gate lastGate = this.mVehicles[0].pathController.GetPathData(PathController.PathType.PitboxEntry).GetLastGate();
          Vector3 position = lastGate.position;
          RaycastHit hitInfo;
          if (Physics.Raycast(position + Vector3.up * 2f, Vector3.down, out hitInfo, float.PositiveInfinity, this.mLayerMask))
            position.y = hitInfo.point.y;
          this.transform.position = position;
          this.transform.rotation = Quaternion.LookRotation(lastGate.normal);
        }
        GameUtility.SetActive(this.mPitCrewArt, true);
      }
      else
      {
        if (flag || !this.mPitCrewArt.activeSelf || this.IsRendered())
          return;
        GameUtility.SetActive(this.mPitCrewArt, false);
      }
    }
    else
      GameUtility.SetActive(this.mPitCrewArt, false);
  }

  public bool IsRendered()
  {
    GameCamera firstActiveCamera = App.instance.gameCameraManager.GetFirstActiveCamera();
    if ((Object) firstActiveCamera != (Object) null)
    {
      Vector3 viewportPoint = firstActiveCamera.GetCamera().WorldToViewportPoint(this.mPitCrewArt.transform.position);
      if ((double) viewportPoint.z > 0.0 && (double) viewportPoint.x > 0.0 && ((double) viewportPoint.x < 1.0 && (double) viewportPoint.y > 0.0) && (double) viewportPoint.y < 1.0)
        return true;
    }
    return false;
  }
}
