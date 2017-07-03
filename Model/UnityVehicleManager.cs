// Decompiled with JetBrains decompiler
// Type: UnityVehicleManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class UnityVehicleManager
{
  private List<UnityVehicle> mUnityVehicles = new List<UnityVehicle>();
  private GameObject mCarContainer;
  private GameObject mSingleSeaterVehiclePrefab;
  private GameObject mGTVehiclePrefab;
  private GameObject mUnitySafetyVehiclePrefab;
  private GameObject mUnityModdingVehiclePrefab;

  public UnityVehicleManager()
  {
    this.mSingleSeaterVehiclePrefab = Resources.Load("Prefabs/Simulation/RaceCar") as GameObject;
    this.mGTVehiclePrefab = Resources.Load("Prefabs/Simulation/RaceCarGT") as GameObject;
    this.mUnitySafetyVehiclePrefab = Resources.Load("Prefabs/Simulation/SafetyCar") as GameObject;
    this.mUnityModdingVehiclePrefab = Resources.Load("Prefabs/Simulation/RaceCarMod") as GameObject;
    this.mCarContainer = new GameObject("Cars");
    UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(this.mCarContainer, FirstActiveSceneHolder.firstActiveScene);
  }

  public void Destroy()
  {
    this.DestroyCars();
    Object.Destroy((Object) this.mCarContainer);
    this.mCarContainer = (GameObject) null;
    this.mSingleSeaterVehiclePrefab = (GameObject) null;
    this.mUnityVehicles.Clear();
    this.mUnityVehicles = (List<UnityVehicle>) null;
    App.instance.assetManager.ResetRaceCarModel();
  }

  public void DestroyCars()
  {
    Transform transform = this.mCarContainer.transform;
    for (int index = 0; index < transform.childCount; ++index)
      Object.Destroy((Object) transform.GetChild(index).gameObject);
    transform.DetachChildren();
    this.mUnityVehicles.Clear();
  }

  public void DestroyCar(UnityVehicle vehicle)
  {
    Debug.Assert(this.mUnityVehicles.Remove(vehicle), "Tried to remove vehicle that's not managed by UnityVehicleManager");
    Object.Destroy((Object) vehicle.gameObject);
  }

  public UnityVehicle CreateCar(string name, Vehicle vehicle)
  {
    GameObject gameObject = (GameObject) null;
    if (vehicle is RacingVehicle)
      gameObject = this.GetRaceVehicle(vehicle as RacingVehicle);
    else if (vehicle is SafetyVehicle)
      gameObject = Object.Instantiate<GameObject>(this.mUnitySafetyVehiclePrefab);
    gameObject.transform.SetParent(this.mCarContainer.transform);
    gameObject.name = name;
    UnityVehicle component = gameObject.GetComponent<UnityVehicle>();
    component.OnStart(vehicle);
    this.mUnityVehicles.Add(component);
    return component;
  }

  private GameObject GetRaceVehicle(RacingVehicle inRacingVehicle)
  {
    return this.GetRaceVehicle(inRacingVehicle.driver);
  }

  private GameObject GetRaceVehicle(Driver inDriver)
  {
    GameObject gameObject = (GameObject) null;
    Championship.Series series = inDriver.contract.GetTeam().championship.series;
    int championshipId = inDriver.contract.GetTeam().championship.championshipID;
    int teamId = inDriver.contract.GetTeam().teamID;
    GameObject raceCarModMesh = App.instance.assetManager.GetRaceCarModMesh(championshipId, teamId);
    if ((Object) raceCarModMesh != (Object) null)
    {
      gameObject = Object.Instantiate<GameObject>(this.mUnityModdingVehiclePrefab);
      raceCarModMesh.transform.SetParent(gameObject.transform, false);
    }
    else
    {
      switch (series)
      {
        case Championship.Series.SingleSeaterSeries:
          gameObject = Object.Instantiate<GameObject>(this.mSingleSeaterVehiclePrefab);
          break;
        case Championship.Series.GTSeries:
          gameObject = Object.Instantiate<GameObject>(this.mGTVehiclePrefab);
          break;
      }
    }
    return gameObject;
  }
}
