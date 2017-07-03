// Decompiled with JetBrains decompiler
// Type: AudioCircuitCarsTrackWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class AudioCircuitCarsTrackWidget : MonoBehaviour
{
  [SerializeField]
  private SoundCameraZoom cameraZoom;
  private SessionDetails mSessionDetails;
  private bool mPlay;
  private scSoundContainer mRaceTrackAmbience;
  private scSoundContainer mPitLaneTannoy;
  private scSoundContainer mPitLaneMechanics;
  private scSoundModulator mPitLaneTannoyModulator;
  private scSoundModulator mPitLaneMechanicsModulator;
  private scOneShotSounds m_SfxAirdrill;
  private List<scSoundContainer> mSoundContainerPool;
  private float mRivalSoundTimer;
  private float mNearSoundTimer;
  private float mDistantSoundTimer;
  private static Vehicle mLastCameraTargetVehicle;

  private void Awake()
  {
    this.mSoundContainerPool = new List<scSoundContainer>(20);
    this.mPitLaneTannoyModulator = new scSoundModulator(17f, 0.25f, 1f);
    this.mPitLaneMechanicsModulator = new scSoundModulator(25f, 0.5f, 1f);
    this.m_SfxAirdrill = new scOneShotSounds(SoundID.Sfx_Airdrill, 5f, 10f);
  }

  private void AddSound(SoundID soundId)
  {
    scSoundContainer scSoundContainer = scSoundManager.Instance.PlaySound(soundId, 0.0f);
    if (!((Object) scSoundContainer != (Object) null))
      return;
    this.mSoundContainerPool.Add(scSoundContainer);
  }

  private void UpdateSounds(int rivalSoundTarget, int distantSoundTarget, float nearestRivalVolume, bool triggerNearRivalBy)
  {
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    SoundID soundId1 = SoundID.Ambience_F1_RivalNear;
    SoundID soundId2 = SoundID.Ambience_F1_DistantBy;
    SoundID soundId3 = SoundID.Ambience_F1_NearBy;
    switch (Game.instance.sessionManager.championship.series)
    {
      case Championship.Series.GTSeries:
        soundId1 = SoundID.GT_Ambience_F1_RivalNear;
        soundId2 = SoundID.GT_Ambience_F1_DistantBy;
        soundId3 = SoundID.GT_Ambience_F1_NearBy;
        break;
    }
    for (int index = this.mSoundContainerPool.Count - 1; index >= 0; --index)
    {
      scSoundContainer scSoundContainer = this.mSoundContainerPool[index];
      if (!scSoundContainer.IsPlaying)
        this.mSoundContainerPool.RemoveAt(index);
      else if (scSoundContainer.SoundID == soundId2)
        ++num2;
      else if (scSoundContainer.SoundID == soundId1)
      {
        scSoundContainer.Volume = nearestRivalVolume;
        ++num1;
      }
      else if (scSoundContainer.SoundID == soundId3)
        ++num3;
    }
    this.mRivalSoundTimer = Mathf.Clamp01(this.mRivalSoundTimer - GameTimer.deltaTime);
    if (num1 < rivalSoundTarget && (double) this.mRivalSoundTimer <= 0.0)
    {
      this.AddSound(soundId1);
      this.mRivalSoundTimer = Random.Range(1f, 2f);
    }
    this.mDistantSoundTimer = Mathf.Clamp01(this.mDistantSoundTimer - GameTimer.deltaTime);
    if (num2 < distantSoundTarget && (double) this.mDistantSoundTimer <= 0.0)
    {
      this.AddSound(soundId2);
      this.mDistantSoundTimer = Random.Range(1f, 2f);
    }
    this.mNearSoundTimer = Mathf.Clamp01(this.mNearSoundTimer - GameTimer.deltaTime);
    if (!triggerNearRivalBy || num3 >= 4 || (double) this.mNearSoundTimer > 0.0)
      return;
    this.AddSound(soundId3);
    this.mNearSoundTimer = Random.Range(1f, 2f);
  }

  private void Update()
  {
    if (Game.instance == null)
    {
      scSoundManager.Instance.SessionEnd(false);
    }
    else
    {
      this.mSessionDetails = Game.instance.sessionManager.eventDetails.currentSession;
      this.mPlay = this.mSessionDetails != null && Game.instance.sessionManager.isSessionActive && scSoundManager.Instance.SessionActive;
      if (this.mPlay && !Game.instance.time.isPaused)
      {
        List<RacingVehicle> standings = Game.instance.sessionManager.standings;
        int count = standings.Count;
        this.mPlay = count > 0;
        if (this.mPlay)
        {
          int num1 = 0;
          int rivalSoundTarget = 0;
          float nearestRivalVolume = 0.0f;
          Vehicle vehicle1 = (Vehicle) null;
          bool flag = false;
          if ((Object) App.instance.cameraManager.target != (Object) null)
          {
            for (int index = 0; index < standings.Count; ++index)
            {
              Vehicle vehicle2 = (Vehicle) standings[index];
              if ((Object) App.instance.cameraManager.target.transform == (Object) vehicle2.unityTransform)
              {
                if (vehicle2.pathState.pathStateGroup == PathStateManager.PathStateGroup.InGarage)
                  flag = true;
                vehicle1 = vehicle2;
                break;
              }
            }
          }
          bool triggerNearRivalBy = false;
          for (int index = 0; index < standings.Count; ++index)
          {
            Vehicle vehicle2 = (Vehicle) standings[index];
            if (vehicle2.pathState.pathStateGroup != PathStateManager.PathStateGroup.InGarage && vehicle2.pathState.pathStateGroup != PathStateManager.PathStateGroup.OnGrid)
            {
              ++num1;
              if (vehicle1 != null && vehicle1 != vehicle2)
              {
                float magnitude = (vehicle1.transform.position - vehicle2.transform.position).magnitude;
                if ((double) magnitude < 50.0)
                {
                  if (flag)
                    triggerNearRivalBy = true;
                  else
                    nearestRivalVolume = Mathf.Clamp01(nearestRivalVolume + (float) ((50.0 - (double) magnitude) / 50.0));
                  ++rivalSoundTarget;
                }
              }
            }
          }
          float num2 = (float) num1 / (float) count;
          float num3 = 1f;
          float zoom = 1f;
          if ((Object) this.cameraZoom != (Object) null)
          {
            num3 = this.cameraZoom.volume;
            zoom = this.cameraZoom.zoom;
          }
          SoundID soundID = SoundID.Ambience_DistantCarLoop;
          switch (Game.instance.sessionManager.championship.series)
          {
            case Championship.Series.GTSeries:
              soundID = SoundID.GT_Ambience_DistantCarLoop;
              break;
          }
          if (scSoundManager.CheckPlaySound(soundID, ref this.mRaceTrackAmbience, 0.0f))
            scSessionAmbience.Start(Game.instance.sessionManager.eventDetails.circuit.locationName, Game.instance.sessionManager.eventDetails.currentSession.sessionType, false);
          if (flag && num1 > 0)
          {
            scSoundManager.CheckPlaySound(SoundID.Ambience_PitLaneTannoy, ref this.mPitLaneTannoy, 0.0f);
            scSoundManager.CheckPlaySound(SoundID.Ambience_PitLaneMechanics, ref this.mPitLaneMechanics, 0.0f);
            this.m_SfxAirdrill.Update(GameTimer.deltaTime);
            this.mPitLaneTannoy.Volume = this.mPitLaneTannoyModulator.Update(GameTimer.deltaTime);
            this.mPitLaneMechanics.Volume = this.mPitLaneMechanicsModulator.Update(GameTimer.deltaTime);
          }
          else
          {
            scSoundManager.CheckStopSound(ref this.mPitLaneTannoy);
            scSoundManager.CheckStopSound(ref this.mPitLaneMechanics);
          }
          scSoundManager.Instance.SetCameraZoom(zoom);
          float num4 = num2 * num3;
          scSessionAmbience.Update(GameTimer.deltaTime, 1f - num4);
          this.mRaceTrackAmbience.Volume = num4;
          this.UpdateSounds(rivalSoundTarget, num1 / 4, nearestRivalVolume, triggerNearRivalBy);
          if (vehicle1 != AudioCircuitCarsTrackWidget.mLastCameraTargetVehicle && AudioCircuitCarsTrackWidget.mLastCameraTargetVehicle != null)
          {
            scSoundManager.Instance.PlaySound(SoundID.Sfx_CameraMove, 0.0f);
            scSoundManager.Instance.PlaySound(SoundID.Sfx_CameraMoveSweetner, 0.0f);
          }
          AudioCircuitCarsTrackWidget.mLastCameraTargetVehicle = vehicle1;
        }
      }
      if (this.mPlay || !scSoundManager.CheckStopSound(ref this.mRaceTrackAmbience))
        return;
      this.StopAllSound();
    }
  }

  private void OnDisable()
  {
    this.StopAllSound();
  }

  private void StopAllSound()
  {
    scSoundManager.CheckStopSound(ref this.mPitLaneTannoy);
    scSoundManager.CheckStopSound(ref this.mPitLaneMechanics);
    scSoundManager.CheckStopSound(ref this.mRaceTrackAmbience);
    scSessionAmbience.Stop();
    CameraRain.StopSound();
    using (List<scSoundContainer>.Enumerator enumerator = this.mSoundContainerPool.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.StopSound(false);
    }
    this.mSoundContainerPool.Clear();
    AudioCircuitCarsTrackWidget.mLastCameraTargetVehicle = (Vehicle) null;
  }
}
