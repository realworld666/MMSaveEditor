// Decompiled with JetBrains decompiler
// Type: GridBox
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class GridBox : MonoBehaviour
{
  [SerializeField]
  private GameObject[] gridCrowd;
  [SerializeField]
  private GameObject[] crowds;

  public Vector3 position
  {
    get
    {
      return this.transform.position;
    }
  }

  public void Setup(Vector3 inPosition, Vector3 inForward)
  {
    this.transform.position = inPosition;
    this.transform.forward = inForward;
    this.HideCrowds();
  }

  public void PreparForRaceStart(RacingVehicle inVehicle)
  {
    int random = RandomUtility.GetRandom(0, this.gridCrowd.Length);
    Renderer[] componentsInChildren = this.GetComponentsInChildren<Renderer>(true);
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      if (componentsInChildren[index].sharedMaterial.name.Contains("Crowd"))
        componentsInChildren[index].material.SetColor(MaterialPropertyHashes._Color, inVehicle.driver.contract.GetTeam().GetTeamColor().livery.primary);
    }
    for (int index = 0; index < this.gridCrowd.Length; ++index)
      this.gridCrowd[index].SetActive(inVehicle != null && index == random);
    if (inVehicle != null)
      this.ShowCrowds();
    else
      this.HideCrowds();
  }

  public void ShowCrowds()
  {
    for (int index = 0; index < this.crowds.Length; ++index)
      this.crowds[index].SetActive(true);
    this.UpdatePosition();
  }

  public void HideCrowds()
  {
    for (int index = 0; index < this.crowds.Length; ++index)
      this.crowds[index].SetActive(false);
    this.UpdatePosition();
  }

  private void UpdatePosition()
  {
    Vector3 position = this.transform.position;
    RaycastHit hitInfo;
    if (Physics.Raycast(position + Vector3.up * 2f, Vector3.down, out hitInfo, float.PositiveInfinity, 1 << LayerMask.NameToLayer("Env_Track")))
      position.y = hitInfo.point.y;
    this.transform.position = position;
  }
}
