// Decompiled with JetBrains decompiler
// Type: StudioScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class StudioScene : BackgroundScene
{
  [SerializeField]
  private Vector3[] carPositions = new Vector3[CarManager.carCount];
  [SerializeField]
  private Quaternion[] carRotations = new Quaternion[CarManager.carCount];
  public CameraControllerUI studioSponsorCameraController;
  [SerializeField]
  private GameCamera studioTeamSelectCamera;
  public GameObject carCameraTarget;
  public GameObject carCameraTargetGT;
  [SerializeField]
  private Light tuneSpotlight;
  public ActivateForSeries.GameObjectData[] shadows;
  private FrontendCar generatedCar;
  private FrontendCar mFrontendCar;
  private StudioScene.Mode modeToSetOnceEnabled;
  private StudioScene.Mode mode;
  private StudioScene.Car carType;
  private FrontendCarData[] savedData;
  private int mChampionshipID;
  private int mTeamID;
  private float mIntensity;
  private bool mTargetTracksAlongCar;
  private scSoundContainer mAmbienceSound;
  private Championship.Series mSelectedSeries;

  private void Update()
  {
    if (this.mTargetTracksAlongCar)
      this.UpdateCameraTargetPosition();
    if (this.mFrontendCar == null)
      return;
    this.mFrontendCar.Update();
  }

  public void SetSeries(Championship.Series inSeries)
  {
    this.mSelectedSeries = inSeries;
    GameUtility.SetActiveForSeries(inSeries, this.shadows);
  }

  private void changeToSavedMode()
  {
    this.changeMode(this.modeToSetOnceEnabled);
    this.modeToSetOnceEnabled = StudioScene.Mode.NotSet;
  }

  private void changeMode(StudioScene.Mode newMode)
  {
    if (newMode == this.mode)
      return;
    this.exitCurrentMode();
    this.mode = newMode;
    this.enterCurrentMode();
  }

  private void enterCurrentMode()
  {
    if (this.mode != StudioScene.Mode.NotSet && Game.IsActive() && (this.savedData == null && Game.instance.player.IsUnemployed()))
      this.mode = StudioScene.Mode.Default;
    switch (this.mode)
    {
      case StudioScene.Mode.CurrentGame:
        GameUtility.Assert(Game.instance.player != null, "Can't set studio car visuals to current game without a current game in progress!", (Object) null);
        FrontendCar frontendCar1 = (FrontendCar) null;
        switch (this.carType)
        {
          case StudioScene.Car.CurrentCar:
            frontendCar1 = Game.IsActive() ? Game.instance.player.team.carManager.frontendCar : CreateTeamManager.newTeam.carManager.frontendCar;
            break;
          case StudioScene.Car.NextYearCar:
            frontendCar1 = Game.IsActive() ? Game.instance.player.team.carManager.nextFrontendCar : CreateTeamManager.newTeam.carManager.nextFrontendCar;
            break;
        }
        frontendCar1.transform.position = this.carPositions[0];
        frontendCar1.transform.rotation = this.carRotations[0];
        frontendCar1.gameObject.SetActive(true);
        if (frontendCar1.isCarModded())
          GameUtility.SetActiveForSeries(Championship.Series.Count, this.shadows);
        this.mFrontendCar = frontendCar1;
        break;
      case StudioScene.Mode.Saved:
        if (this.savedData == null)
        {
          Debug.LogError((object) "Trying to set saved car visuals in studio scene, but haven't been given any", (Object) null);
          break;
        }
        if (this.savedData.Length < CarManager.carCount)
        {
          Debug.LogError((object) "Studio trying to load wrong number of cars - perhaps bad save data?", (Object) null);
          break;
        }
        FrontendCar frontendCar2 = new FrontendCar();
        frontendCar2.Start(this.mTeamID, this.mChampionshipID, App.instance.frontendCarManager);
        frontendCar2.gameObject.name += 0.ToString();
        frontendCar2.gameObject.transform.SetParent(this.transform);
        frontendCar2.gameObject.transform.position = this.carPositions[0];
        frontendCar2.gameObject.transform.rotation = this.carRotations[0];
        frontendCar2.gameObject.SetActive(true);
        frontendCar2.SetData(this.savedData[0]);
        if (frontendCar2.isCarModded())
          GameUtility.SetActiveForSeries(Championship.Series.Count, this.shadows);
        this.generatedCar = frontendCar2;
        this.mFrontendCar = frontendCar2;
        break;
      case StudioScene.Mode.Default:
        TeamColor defaultColour = App.instance.uiTeamColourManager.defaultColour;
        LiveryData defaultLivery = LiveryData.defaultLivery;
        FrontendCarData.ModelData modelData = FrontendCarData.ModelData.defaultModelData;
        FrontendCarData.BlendShapeData defaultBlendShapeData = FrontendCarData.BlendShapeData.defaultBlendShapeData;
        FrontendCarData.SponsorData defaultSponsorData = FrontendCarData.SponsorData.defaultSponsorData;
        FrontendCar frontendCar3 = new FrontendCar();
        frontendCar3.Start(this.mTeamID, this.mChampionshipID, App.instance.frontendCarManager);
        frontendCar3.gameObject.name += 0.ToString();
        frontendCar3.gameObject.transform.position = this.carPositions[0];
        frontendCar3.gameObject.transform.rotation = this.carRotations[0];
        frontendCar3.gameObject.SetActive(true);
        if (Game.instance != null)
          modelData = FrontendCar.DefaultPartsForChampionship(this.mChampionshipID, App.instance.carPartModelDatabase);
        frontendCar3.Setup(defaultColour, defaultLivery, modelData, defaultBlendShapeData, defaultSponsorData);
        if (frontendCar3.isCarModded())
          GameUtility.SetActiveForSeries(Championship.Series.Count, this.shadows);
        this.generatedCar = frontendCar3;
        this.mFrontendCar = frontendCar3;
        break;
    }
  }

  private void exitCurrentMode()
  {
    switch (this.mode)
    {
      case StudioScene.Mode.CurrentGame:
        if (this.mFrontendCar != null && (Object) this.mFrontendCar.gameObject != (Object) null)
          this.mFrontendCar.gameObject.SetActive(false);
        this.SetCarsToNull();
        break;
      case StudioScene.Mode.Saved:
        this.DestroyGeneratedCars();
        this.SetCarsToNull();
        this.savedData = (FrontendCarData[]) null;
        break;
      case StudioScene.Mode.Default:
        this.DestroyGeneratedCars();
        this.SetCarsToNull();
        break;
    }
  }

  public void SetCarType(StudioScene.Car inCar)
  {
    if (this.carType == inCar)
      return;
    this.carType = inCar;
    this.ResetMode();
  }

  public void SetCarVisualsToCurrentGame()
  {
    this.modeToSetOnceEnabled = StudioScene.Mode.CurrentGame;
    if (!this.isActiveAndEnabled)
      return;
    this.changeToSavedMode();
  }

  public void SetSavedCarVisuals(int inTeamID, int championshipID, params FrontendCarData[] data)
  {
    this.mChampionshipID = championshipID;
    this.mTeamID = inTeamID;
    this.modeToSetOnceEnabled = StudioScene.Mode.Saved;
    this.savedData = data;
    if (!this.isActiveAndEnabled)
      return;
    this.changeToSavedMode();
  }

  public void SetDefaultCarVisuals(int inTeamID, int championshipID)
  {
    this.mChampionshipID = championshipID;
    this.mTeamID = inTeamID;
    this.modeToSetOnceEnabled = StudioScene.Mode.Default;
    if (!this.isActiveAndEnabled)
      return;
    this.changeToSavedMode();
  }

  public FrontendCar GetGeneratedCar(int inIndex)
  {
    return this.generatedCar;
  }

  public void SetCameraTargetToTrackAlongCar(bool enable)
  {
    this.mTargetTracksAlongCar = enable;
    if (!enable)
      return;
    this.UpdateCameraTargetPosition();
  }

  public void TuneSpotlight(bool inDefault)
  {
    if ((Object) this.tuneSpotlight == (Object) null)
      return;
    if (inDefault)
    {
      this.tuneSpotlight.intensity = this.mIntensity;
    }
    else
    {
      this.mIntensity = this.tuneSpotlight.intensity;
      this.tuneSpotlight.intensity = 1.6f;
    }
  }

  private void UpdateCameraTargetPosition()
  {
    if (!((Object) this.studioTeamSelectCamera != (Object) null))
      return;
    this.GetCameraTArget().transform.position = EasingUtility.EaseByType(EasingUtility.Easing.InOutCubic, this.carPositions[0] + this.carRotations[0] * Vector3.forward * 1.05f, this.carPositions[0] - this.carRotations[0] * Vector3.forward * 1.45f, Mathf.Clamp01(Mathf.Abs((float) (((double) Mathf.Repeat((float) ((double) this.studioTeamSelectCamera.transform.eulerAngles.y - (double) this.carRotations[0].eulerAngles.y + 360.0), 360f) - 180.0) / 180.0))));
  }

  public GameObject GetCameraTArget()
  {
    switch (this.mSelectedSeries)
    {
      case Championship.Series.GTSeries:
        return this.carCameraTargetGT;
      default:
        return this.carCameraTarget;
    }
  }

  private void OnEnable()
  {
    if (this.modeToSetOnceEnabled == StudioScene.Mode.NotSet)
      return;
    this.changeToSavedMode();
  }

  public void ResetMode()
  {
    this.changeMode(StudioScene.Mode.NotSet);
  }

  private void OnDisable()
  {
    scSoundManager.CheckStopSound(ref this.mAmbienceSound);
    this.changeMode(StudioScene.Mode.NotSet);
  }

  private void SetCarsToNull()
  {
    this.mFrontendCar = (FrontendCar) null;
  }

  private void DestroyGeneratedCars()
  {
    if (this.generatedCar == null)
      return;
    this.generatedCar.Destroy();
  }

  public string GetTeamSelectCameraString(Championship.Series inSeries)
  {
    switch (inSeries)
    {
      case Championship.Series.GTSeries:
        return "TeamSelectCameraGT";
      default:
        return "TeamSelectCamera";
    }
  }

  public enum Car
  {
    CurrentCar,
    NextYearCar,
  }

  private enum Mode
  {
    NotSet,
    CurrentGame,
    Saved,
    Default,
  }
}
