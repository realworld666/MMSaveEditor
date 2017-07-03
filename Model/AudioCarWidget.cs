// Decompiled with JetBrains decompiler
// Type: AudioCarWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class AudioCarWidget : MonoBehaviour
{
  private static scSoundCarEngine mSoundCarEngine = (scSoundCarEngine) null;
  private static scEngineController mEngineController = (scEngineController) null;
  private static scSoundContainer mRoadWindLow = (scSoundContainer) null;
  private static scSoundContainer mRoadWindHigh = (scSoundContainer) null;
  private static scSoundModulator mModulator = new scSoundModulator(25f, 0.6f, 1f);
  private static float mDeltaTimeTotal = 0.0f;
  private static float mRpmScaler = 1f;
  private static bool mF1EngineLoaded = false;
  private float mLastSpeed;
  private float mAverageSpeedDelta;
  private Vehicle mVehicle;

  public static bool HeadPhonesUnplugged { get; set; }

  public void OnStart(Vehicle inVehicle)
  {
    this.mVehicle = inVehicle;
    switch (Game.instance.sessionManager.championship.series)
    {
      case Championship.Series.SingleSeaterSeries:
        if (!AudioCarWidget.mF1EngineLoaded)
        {
          AudioCarWidget.mSoundCarEngine = (scSoundCarEngine) null;
          break;
        }
        break;
      case Championship.Series.GTSeries:
        if (AudioCarWidget.mF1EngineLoaded)
        {
          AudioCarWidget.mSoundCarEngine = (scSoundCarEngine) null;
          break;
        }
        break;
    }
    if (!((Object) AudioCarWidget.mSoundCarEngine == (Object) null))
      return;
    switch (Game.instance.sessionManager.championship.series)
    {
      case Championship.Series.SingleSeaterSeries:
        this.LoadEngineAudio("Prefabs/Audio/F1EngineAudio");
        AudioCarWidget.mRpmScaler = 1f;
        scEngineController.RpmDeltaScale = 1f;
        scEngineController.DeltaGearChangeScale = 1f;
        AudioCarWidget.mF1EngineLoaded = true;
        break;
      case Championship.Series.GTSeries:
        this.LoadEngineAudio("Prefabs/Audio/GTEngineAudio");
        AudioCarWidget.mRpmScaler = 0.75f;
        scEngineController.RpmDeltaScale = 0.8f;
        scEngineController.DeltaGearChangeScale = 0.5f;
        AudioCarWidget.mF1EngineLoaded = false;
        break;
    }
    AudioCarWidget.mSoundCarEngine.InitialiseEngine();
    AudioCarWidget.mEngineController = new scEngineController();
  }

  private void LoadEngineAudio(string inPath)
  {
    GameObject go = (GameObject) Object.Instantiate(Resources.Load(inPath));
    go.name = go.name.Replace("(Clone)", string.Empty);
    UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(go, FirstActiveSceneHolder.firstActiveScene);
    AudioCarWidget.mSoundCarEngine = go.GetComponent<scSoundCarEngine>();
  }

  private void Update()
  {
    if (AudioCarWidget.HeadPhonesUnplugged)
    {
      Debug.LogWarning((object) "Head Phones Unplugged - restarting engine audio", (Object) null);
      AudioCarWidget.mSoundCarEngine.Stop();
      AudioCarWidget.HeadPhonesUnplugged = false;
    }
    if (!Game.IsActive() || this.mVehicle == null)
      return;
    if (UIManager.instance.currentScreen is SessionHUD && Game.instance.sessionManager.isSessionActive && (scSoundManager.Instance.SessionActive && App.instance.cameraManager.target.transform.name != "Safety Vehicle"))
    {
      if (!((Object) App.instance.cameraManager.target != (Object) null) || !((Object) App.instance.cameraManager.target.transform == (Object) this.mVehicle.unityTransform) || Game.instance.time.isPaused)
        return;
      AudioCarWidget.mSoundCarEngine.Play();
      float speedDelta = this.mVehicle.speed - (this.mLastSpeed - 1f / 1000f);
      if ((double) speedDelta < 0.0)
        speedDelta *= 2f;
      this.mAverageSpeedDelta += speedDelta;
      this.mAverageSpeedDelta *= 0.8f;
      this.mLastSpeed = this.mVehicle.speed;
      scEngineController.State state = (double) this.mVehicle.speed < 10.0 || (double) this.mAverageSpeedDelta < -1.0 && (double) this.mVehicle.speed < 20.0 ? scEngineController.State.Idle : ((double) this.mAverageSpeedDelta < 1.0 ? ((double) this.mAverageSpeedDelta < 0.0 ? ((double) this.mAverageSpeedDelta > -1.0 ? scEngineController.State.BrakeSteady : scEngineController.State.BrakeFast) : scEngineController.State.AccelerateSteady) : scEngineController.State.AccelerateFast);
      AudioCarWidget.mDeltaTimeTotal += Time.fixedDeltaTime;
      AudioCarWidget.mDeltaTimeTotal = Mathf.Clamp(AudioCarWidget.mDeltaTimeTotal, 0.0f, 0.1333333f);
      while ((double) AudioCarWidget.mDeltaTimeTotal > 0.0166666675359011)
      {
        AudioCarWidget.mDeltaTimeTotal -= 0.01666667f;
        AudioCarWidget.mEngineController.SetState(state, speedDelta);
        AudioCarWidget.mEngineController.Update();
      }
      AudioCarWidget.mSoundCarEngine.StateRef.m_Load.Value = AudioCarWidget.mEngineController.Load;
      AudioCarWidget.mSoundCarEngine.StateRef.m_RPM.Value = AudioCarWidget.mEngineController.Rpm * AudioCarWidget.mRpmScaler;
      AudioCarWidget.mSoundCarEngine.UpdateState(GameTimer.deltaTime);
      AudioCarWidget.mSoundCarEngine.Volume = AudioCarWidget.mModulator.Update(GameTimer.deltaTime);
      scSoundManager.CheckPlaySound(SoundID.Sfx_RoadWindLoop, ref AudioCarWidget.mRoadWindLow, 0.0f);
      scSoundManager.CheckPlaySound(SoundID.Sfx_RoadWindLoopFast, ref AudioCarWidget.mRoadWindHigh, 0.0f);
      if (!((Object) AudioCarWidget.mRoadWindLow != (Object) null) || !((Object) AudioCarWidget.mRoadWindHigh != (Object) null))
        return;
      float num1 = Mathf.Clamp01(Mathf.Abs(this.mVehicle.speed) / 20f);
      float num2 = Mathf.Clamp01(Mathf.Abs(this.mVehicle.speed) / 40f);
      float num3 = num1 * (1f - Mathf.Clamp01((float) (((double) Mathf.Abs(this.mVehicle.speed) - 40.0) / 20.0)));
      float num4 = Mathf.Clamp01((float) (((double) Mathf.Abs(this.mVehicle.speed) - 40.0) / 40.0));
      float num5 = Mathf.Clamp01((float) (((double) Mathf.Abs(this.mVehicle.speed) - 40.0) / 100.0));
      AudioCarWidget.mRoadWindLow.Volume = num3;
      AudioCarWidget.mRoadWindLow.Pitch = 1f + num2;
      AudioCarWidget.mRoadWindHigh.Volume = num4;
      AudioCarWidget.mRoadWindHigh.Pitch = 1f + num5;
    }
    else
      this.StopAudio();
  }

  private void OnDisable()
  {
    this.StopAudio();
  }

  private void StopAudio()
  {
    scSoundManager.CheckStopSound(ref AudioCarWidget.mRoadWindLow);
    scSoundManager.CheckStopSound(ref AudioCarWidget.mRoadWindHigh);
    AudioCarWidget.mSoundCarEngine.Stop();
  }
}
