// Decompiled with JetBrains decompiler
// Type: scSessionAmbience
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class scSessionAmbience
{
  private static scSoundContainer mAmbienceWindSound;
  private static scSoundContainer mTrackCrowdAmbience;
  private static scSoundLFO mModulator1;
  private static scSoundLFO mModulator2;
  private static List<scOneShotSounds> mOneShotSounds;
  private static List<scOneShotSounds> mNoRainOneShotSounds;
  private static scOneShotSounds mRaceCrowdOneShots;
  private static bool mIsPlaying;
  private static bool mPlayingSound;

  public static bool IsPlaying
  {
    get
    {
      return scSessionAmbience.mIsPlaying;
    }
  }

  public static bool IsPaused { get; set; }

  public static bool PlayingSound
  {
    get
    {
      return scSessionAmbience.mPlayingSound;
    }
  }

  private static void StartOneShot(SoundID soundID, float initialMinDelay, float initialMaxDelay, float minDelay, float maxDelay)
  {
    scSessionAmbience.mOneShotSounds.Add(new scOneShotSounds(soundID, minDelay, maxDelay, initialMinDelay, initialMaxDelay));
  }

  private static void StartNoRainOneShot(SoundID soundID, float initialMinDelay, float initialMaxDelay, float minDelay, float maxDelay)
  {
    scSessionAmbience.mNoRainOneShotSounds.Add(new scOneShotSounds(soundID, minDelay, maxDelay, initialMinDelay, initialMaxDelay));
  }

  public static void Start(string locationName, SessionDetails.SessionType sessionType, bool preRace)
  {
    scSessionAmbience.mPlayingSound = true;
    scSessionAmbience.mModulator1 = new scSoundLFO(21f);
    scSessionAmbience.mModulator2 = new scSoundLFO(33f);
    scSessionAmbience.mOneShotSounds = new List<scOneShotSounds>();
    scSessionAmbience.mNoRainOneShotSounds = new List<scOneShotSounds>();
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    bool flag4 = false;
    bool flag5 = false;
    bool flag6 = false;
    bool flag7 = false;
    bool flag8 = false;
    bool flag9 = false;
    if (preRace)
      scSoundManager.Instance.SetCameraZoom(1f);
    SoundID soundID = SoundID.Ambience_LowWind;
    string key = locationName;
    if (key != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (scSessionAmbience.\u003C\u003Ef__switch\u0024map35 == null)
      {
        // ISSUE: reference to a compiler-generated field
        scSessionAmbience.\u003C\u003Ef__switch\u0024map35 = new Dictionary<string, int>(16)
        {
          {
            "Ardennes",
            0
          },
          {
            "Beijing",
            1
          },
          {
            "Black Sea",
            2
          },
          {
            "Cape Town",
            3
          },
          {
            "Doha",
            4
          },
          {
            "Dubai",
            5
          },
          {
            "Guildford",
            6
          },
          {
            "Milan",
            7
          },
          {
            "Munich",
            8
          },
          {
            "Phoenix",
            9
          },
          {
            "Rio de Janeiro",
            10
          },
          {
            "Singapore",
            11
          },
          {
            "Sydney",
            12
          },
          {
            "Tondela",
            13
          },
          {
            "Vancouver",
            14
          },
          {
            "Yokohama",
            15
          }
        };
      }
      int num;
      // ISSUE: reference to a compiler-generated field
      if (scSessionAmbience.\u003C\u003Ef__switch\u0024map35.TryGetValue(key, out num))
      {
        switch (num)
        {
          case 0:
            flag2 = true;
            soundID = SoundID.Ambience_Ardennes;
            break;
          case 1:
            flag1 = true;
            flag9 = true;
            soundID = SoundID.Ambience_Bejing;
            break;
          case 2:
            flag3 = true;
            soundID = SoundID.Ambience_BlackSea;
            break;
          case 3:
            flag5 = true;
            soundID = SoundID.Ambience_CapeTown;
            break;
          case 4:
            flag3 = true;
            flag1 = true;
            flag4 = true;
            flag5 = true;
            soundID = SoundID.Ambience_Doha;
            break;
          case 5:
            flag4 = true;
            flag5 = true;
            flag1 = true;
            flag9 = true;
            soundID = SoundID.Ambience_Dubai;
            break;
          case 6:
            flag1 = true;
            flag2 = true;
            flag6 = true;
            soundID = SoundID.Ambience_Guildford;
            break;
          case 7:
            flag2 = true;
            soundID = SoundID.Ambience_Milan;
            break;
          case 8:
            flag2 = true;
            soundID = SoundID.Ambience_Munich;
            break;
          case 9:
            flag2 = true;
            soundID = SoundID.Ambience_Phoenix;
            break;
          case 10:
            flag2 = true;
            flag1 = true;
            flag9 = true;
            soundID = SoundID.Ambience_RioDeJaneiro;
            break;
          case 11:
            flag1 = true;
            flag3 = true;
            flag9 = true;
            soundID = SoundID.Ambience_Singapore;
            break;
          case 12:
            flag2 = true;
            flag5 = true;
            soundID = SoundID.Ambience_Sydney;
            break;
          case 14:
            flag2 = true;
            soundID = SoundID.Ambience_Vancouver;
            break;
          case 15:
            flag8 = true;
            flag7 = true;
            soundID = SoundID.Ambience_Yokohama;
            break;
        }
      }
    }
    switch (sessionType)
    {
      case SessionDetails.SessionType.Practice:
        if (preRace)
        {
          scSoundManager.CheckPlaySound(SoundID.Ambience_CrowdPracticePreRace, ref scSessionAmbience.mTrackCrowdAmbience, 0.0f);
          break;
        }
        scSoundManager.CheckPlaySound(SoundID.Ambience_CrowdPracticeCheer, ref scSessionAmbience.mTrackCrowdAmbience, 0.0f);
        break;
      case SessionDetails.SessionType.Qualifying:
        if (preRace)
        {
          scSoundManager.CheckPlaySound(SoundID.Ambience_CrowdQualifyPreRace, ref scSessionAmbience.mTrackCrowdAmbience, 0.0f);
          break;
        }
        scSoundManager.CheckPlaySound(SoundID.Ambience_CrowdQualifyCheer, ref scSessionAmbience.mTrackCrowdAmbience, 0.0f);
        break;
      case SessionDetails.SessionType.Race:
        if (preRace)
        {
          scSoundManager.CheckPlaySound(SoundID.Ambience_CrowdRacePreRace, ref scSessionAmbience.mTrackCrowdAmbience, 0.0f);
          break;
        }
        scSoundManager.CheckPlaySound(SoundID.Ambience_CrowdRaceCheer, ref scSessionAmbience.mTrackCrowdAmbience, 0.0f);
        scSessionAmbience.mRaceCrowdOneShots = new scOneShotSounds(SoundID.Sfx_CrowdRaceCheerOneShot, 8f, 15f);
        break;
    }
    scSoundManager.CheckPlaySound(soundID, ref scSessionAmbience.mAmbienceWindSound, 0.0f);
    scSessionAmbience.StartOneShot(SoundID.Sfx_TannoyOneShot, 10f, 20f, 10f, 20f);
    scSessionAmbience.StartNoRainOneShot(SoundID.Ambience_Helicopter, 15f, 25f, 30f, 50f);
    if (flag1)
      scSessionAmbience.StartOneShot(SoundID.Sfx_AirplaneJet, 25f, 35f, 60f, 100f);
    if (flag2)
      scSessionAmbience.StartNoRainOneShot(SoundID.Sfx_AirplaneProp, 20f, 30f, 40f, 60f);
    if (!preRace)
      return;
    if (flag3)
      scSessionAmbience.StartOneShot(SoundID.Sfx_BoatHorn, 15f, 30f, 50f, 80f);
    if (flag4)
      scSessionAmbience.StartNoRainOneShot(SoundID.Sfx_BoatRace, 5f, 10f, 20f, 30f);
    if (flag5)
      scSessionAmbience.StartNoRainOneShot(SoundID.Sfx_JetSki, 5f, 10f, 20f, 30f);
    if (flag6)
      scSessionAmbience.StartOneShot(SoundID.Sfx_AirplaneJetGuildford, 5f, 15f, 60f, 100f);
    if (flag8)
      scSessionAmbience.StartNoRainOneShot(SoundID.Sfx_Trainpass, 5f, 10f, 20f, 30f);
    if (flag7)
      scSessionAmbience.StartNoRainOneShot(SoundID.Sfx_TrainpassHorn, 10f, 20f, 50f, 80f);
    if (!flag9)
      return;
    scSessionAmbience.StartNoRainOneShot(SoundID.Sfx_CarPass, 5f, 10f, 12f, 20f);
  }

  public static void Update(float deltaTime, float trackWindAmbienceVolume = 1f)
  {
    if (scSessionAmbience.IsPaused || !scSessionAmbience.mPlayingSound)
      return;
    if (Game.instance != null && Game.instance.sessionManager != null)
    {
      SessionWeatherDetails currentSessionWeather = Game.instance.sessionManager.currentSessionWeather;
      if (currentSessionWeather != null && currentSessionWeather.GetCurrentWeather() != null && currentSessionWeather.GetCurrentWeather().rainType == Weather.RainType.None)
      {
        for (int index = 0; index < scSessionAmbience.mNoRainOneShotSounds.Count; ++index)
          scSessionAmbience.mNoRainOneShotSounds[index].Update(deltaTime);
      }
    }
    for (int index = 0; index < scSessionAmbience.mOneShotSounds.Count; ++index)
      scSessionAmbience.mOneShotSounds[index].Update(deltaTime);
    double num1 = (double) scSessionAmbience.mModulator1.Update(GameTimer.deltaTime);
    double num2 = (double) scSessionAmbience.mModulator2.Update(GameTimer.deltaTime);
    float num3 = Mathf.Lerp(0.2f, 1f, (float) (((double) scSessionAmbience.mModulator1.Value + (double) scSessionAmbience.mModulator2.Value) * 0.5));
    scSessionAmbience.mTrackCrowdAmbience.Volume = num3;
    if (scSessionAmbience.mRaceCrowdOneShots != null)
    {
      scSessionAmbience.mRaceCrowdOneShots.Update(deltaTime);
      scSessionAmbience.mRaceCrowdOneShots.Volume = num3;
    }
    scSessionAmbience.mAmbienceWindSound.Volume = trackWindAmbienceVolume;
  }

  public static void Stop()
  {
    scSessionAmbience.IsPaused = false;
    scSessionAmbience.mPlayingSound = false;
    if (scSessionAmbience.mOneShotSounds != null)
    {
      for (int index = 0; index < scSessionAmbience.mOneShotSounds.Count; ++index)
        scSessionAmbience.mOneShotSounds[index].Stop();
      scSessionAmbience.mOneShotSounds = (List<scOneShotSounds>) null;
    }
    if (scSessionAmbience.mNoRainOneShotSounds != null)
    {
      for (int index = 0; index < scSessionAmbience.mNoRainOneShotSounds.Count; ++index)
        scSessionAmbience.mNoRainOneShotSounds[index].Stop();
      scSessionAmbience.mNoRainOneShotSounds = (List<scOneShotSounds>) null;
    }
    if (scSessionAmbience.mRaceCrowdOneShots != null)
    {
      scSessionAmbience.mRaceCrowdOneShots.Stop();
      scSessionAmbience.mRaceCrowdOneShots = (scOneShotSounds) null;
    }
    scSoundManager.CheckStopSound(ref scSessionAmbience.mAmbienceWindSound);
    scSoundManager.CheckStopSound(ref scSessionAmbience.mTrackCrowdAmbience);
  }
}
