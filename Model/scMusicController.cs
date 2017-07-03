// Decompiled with JetBrains decompiler
// Type: scMusicController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class scMusicController
{
  private static scSoundContainer mSound = (scSoundContainer) null;
  private static scSoundContainer mSound2 = (scSoundContainer) null;
  private static SoundID[] mLoop1A = new SoundID[5]
  {
    SoundID.Music_Track01_BlindingLight_Loop1A,
    SoundID.Music_Track02_GatheringData_Loop1A,
    SoundID.Music_Track03_HelpIsOnItsWay_Loop1A,
    SoundID.Music_Track04_LeadingEdge_Loop1A,
    SoundID.Music_Track05_SearchBegins_Loop1A
  };
  private static float[] mLoop1A_Duration = new float[5]
  {
    25.285f,
    5.189f,
    4.924f,
    1.105f,
    0.0f
  };
  private static SoundID[] mLoop1S = new SoundID[5]
  {
    SoundID.Music_Track01_BlindingLight_Loop1S,
    SoundID.Music_Track02_GatheringData_Loop1S,
    SoundID.Music_Track03_HelpIsOnItsWay_Loop1S,
    SoundID.Music_Track04_LeadingEdge_Loop1S,
    SoundID.Music_Track05_SearchBegins_Loop1S
  };
  private static SoundID[] mTransition = new SoundID[5]
  {
    SoundID.Music_Track01_BlindingLight_Transition,
    SoundID.Music_Track02_GatheringData_Transition,
    SoundID.Music_Track03_HelpIsOnItsWay_Transition,
    SoundID.Music_Track04_LeadingEdge_Transition,
    SoundID.Music_Track05_SearchBegins_Transition
  };
  private static float[] mTransition_Duration = new float[5]
  {
    29.918f,
    21.433f,
    12f,
    54.229f,
    13.382f
  };
  private static SoundID[] mLoop2 = new SoundID[5]
  {
    SoundID.Music_Track01_BlindingLight_Loop2,
    SoundID.Music_Track02_GatheringData_Loop2,
    SoundID.Music_Track03_HelpIsOnItsWay_Loop2,
    SoundID.Music_Track04_LeadingEdge_Loop2,
    SoundID.Music_Track05_SearchBegins_Loop2
  };
  private static SoundID[] mRace = new SoundID[5]
  {
    SoundID.Music_Track01_BlindingLight_RaceStart,
    SoundID.Music_Track02_GatheringData_RaceStart,
    SoundID.Music_Track03_HelpIsOnItsWay_RaceStart,
    SoundID.Music_Track04_LeadingEdge_RaceStart,
    SoundID.Music_Track05_SearchBegins_RaceStart
  };
  private static SoundID[] mPostRaceLoop2A = new SoundID[5]
  {
    SoundID.Music_Track01_BlindingLight_Loop2Start,
    SoundID.Music_Track02_GatheringData_Loop2Start,
    SoundID.Music_Track03_HelpIsOnItsWay_Loop2Start,
    SoundID.Music_Track04_LeadingEdge_Loop2Start,
    SoundID.Music_Track05_SearchBegins_Loop2Start
  };
  private static float[] mPostRaceLoop2A_Duration = new float[5]
  {
    17.265f,
    7.692f,
    6.837f,
    1.105f,
    2.8f
  };
  private static scMusicController.State mState = scMusicController.State.Stopped;
  private static int mTrackIndex = 0;

  public static void Update(float deltaTime)
  {
  }

  public static void Start()
  {
    if (scMusicController.mState == scMusicController.State.Loop1)
      return;
    ++scMusicController.mTrackIndex;
    if (scMusicController.mTrackIndex >= 5)
      scMusicController.mTrackIndex = 0;
    scMusicController.Stop();
    scSoundManager.CheckPlaySound(scMusicController.mLoop1A[scMusicController.mTrackIndex], ref scMusicController.mSound, 0.0f);
    if ((double) scMusicController.mLoop1A_Duration[scMusicController.mTrackIndex] != 0.0)
      scSoundManager.CheckPlaySound(scMusicController.mLoop1S[scMusicController.mTrackIndex], ref scMusicController.mSound2, scMusicController.mLoop1A_Duration[scMusicController.mTrackIndex]);
    scMusicController.mState = scMusicController.State.Loop1;
  }

  public static void Transition()
  {
    if (scMusicController.mState == scMusicController.State.Loop2)
      return;
    scMusicController.Stop();
    scSoundManager.CheckPlaySound(scMusicController.mTransition[scMusicController.mTrackIndex], ref scMusicController.mSound, 0.0f);
    if ((double) scMusicController.mTransition_Duration[scMusicController.mTrackIndex] != 0.0)
      scSoundManager.CheckPlaySound(scMusicController.mLoop2[scMusicController.mTrackIndex], ref scMusicController.mSound2, scMusicController.mTransition_Duration[scMusicController.mTrackIndex]);
    scMusicController.mState = scMusicController.State.Loop2;
  }

  public static void Race()
  {
    if (scMusicController.mState == scMusicController.State.Race)
      return;
    scMusicController.Stop();
    scSoundManager.CheckPlaySound(scMusicController.mRace[scMusicController.mTrackIndex], ref scMusicController.mSound, 0.0f);
    scMusicController.mState = scMusicController.State.Race;
  }

  public static void PostRaceLoop2()
  {
    if (scMusicController.mState == scMusicController.State.PostRaceLoop2 || scMusicController.mState == scMusicController.State.Loop2 || (scMusicController.mState == scMusicController.State.Loop1 || scSoundManager.Instance.SessionActive))
      return;
    scMusicController.Stop();
    scSoundManager.CheckPlaySound(scMusicController.mPostRaceLoop2A[scMusicController.mTrackIndex], ref scMusicController.mSound, 0.0f);
    if ((double) scMusicController.mPostRaceLoop2A_Duration[scMusicController.mTrackIndex] != 0.0)
      scSoundManager.CheckPlaySound(scMusicController.mLoop2[scMusicController.mTrackIndex], ref scMusicController.mSound2, scMusicController.mPostRaceLoop2A_Duration[scMusicController.mTrackIndex]);
    scMusicController.mState = scMusicController.State.PostRaceLoop2;
  }

  public static void Stop()
  {
    scSoundManager.CheckStopSound(ref scMusicController.mSound);
    scSoundManager.CheckStopSound(ref scMusicController.mSound2);
    scMusicController.mState = scMusicController.State.Stopped;
  }

  public static SoundID TutorialMusicId()
  {
    return scMusicController.mLoop1S[scMusicController.mTrackIndex];
  }

  private enum State
  {
    Stopped,
    Loop1,
    Loop2,
    Race,
    PostRaceLoop2,
  }
}
