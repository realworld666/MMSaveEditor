// Decompiled with JetBrains decompiler
// Type: BinkWrapper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using BinkPlugin;
using System;
using UnityEngine;

public class BinkWrapper : MonoBehaviour
{
  public BinkWrapper.DrawStyle drawStyle = BinkWrapper.DrawStyle.OverlayFillScreenWithAspectRatio;
  private Vector2 destinationUpperLeft = new Vector2(0.0f, 0.0f);
  private Vector2 destinationLowerRight = new Vector2(1f, 1f);
  [SerializeField]
  private Bink.SoundTrackTypes soundOutput = Bink.SoundTrackTypes.SndSimple;
  private IntPtr mBink = IntPtr.Zero;
  private Bink.Info mBinkInfo = new Bink.Info();
  private string mCurrentMovieName = string.Empty;
  private static BinkWrapper sInstance;
  [SerializeField]
  private int layerDepth;
  [SerializeField]
  private Bink.BufferingTypes ioBuffering;
  [SerializeField]
  private int soundTrackOffset;
  private static int mInstanceCount;
  private BinkWrapper.PlaybackStatus mPlaybackStatus;
  private Action mOnVideoFinished;
  private string mStreamingAssetsPath;

  public static BinkWrapper instance
  {
    get
    {
      BinkWrapper.EnsureInstanceExists();
      return BinkWrapper.sInstance;
    }
  }

  public static bool HasInstance
  {
    get
    {
      return (UnityEngine.Object) BinkWrapper.sInstance != (UnityEngine.Object) null;
    }
  }

  public string currentMovieName
  {
    get
    {
      return this.mCurrentMovieName;
    }
  }

  public static void EnsureInstanceExists()
  {
    if (!((UnityEngine.Object) BinkWrapper.sInstance == (UnityEngine.Object) null) || !Application.isPlaying)
      return;
    GameObject go = UnityEngine.Object.Instantiate<GameObject>((GameObject) Resources.Load("Prefabs/UI/MoviePlayer/BinkPlayer"));
    go.name = go.name.Replace("(Clone)", string.Empty);
    UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(go, FirstActiveSceneHolder.firstActiveScene);
    BinkWrapper.sInstance = go.GetComponent<BinkWrapper>();
  }

  public static Vector2 get_viewport_size()
  {
    return new Vector2((float) Screen.width, (float) Screen.height);
  }

  private void Start()
  {
    Bink.UseDirectSound();
    ++BinkWrapper.mInstanceCount;
  }

  public void EmptyInit()
  {
  }

  private void OnPostRender()
  {
    if (!(this.mBink != IntPtr.Zero) || this.mPlaybackStatus != BinkWrapper.PlaybackStatus.Playing)
      return;
    Bink.GetInfo(this.mBink, ref this.mBinkInfo);
    if ((int) this.mBinkInfo.PlaybackState == 3)
    {
      this.VideoFinished();
    }
    else
    {
      float x0 = this.destinationUpperLeft.x;
      float y0 = this.destinationUpperLeft.y;
      float x1 = this.destinationLowerRight.x;
      float y1 = this.destinationLowerRight.y;
      if (this.drawStyle == BinkWrapper.DrawStyle.OverlayFillScreenWithAspectRatio)
      {
        Vector2 viewportSize = BinkWrapper.get_viewport_size();
        float num1 = (float) this.mBinkInfo.Width / viewportSize.x;
        float num2 = (float) this.mBinkInfo.Height / viewportSize.y;
        float num3;
        float num4;
        if ((double) num1 > (double) num2)
        {
          num3 = num2 / num1;
          num4 = 1f;
        }
        else
        {
          num4 = num1 / num2;
          num3 = 1f;
        }
        x0 = (float) ((1.0 - (double) num4) / 2.0);
        y0 = (float) ((1.0 - (double) num3) / 2.0);
        x1 = num4 + x0;
        y1 = num3 + y0;
      }
      else
      {
        Vector2 viewportSize = BinkWrapper.get_viewport_size();
        float num1 = viewportSize.x / viewportSize.y;
        float num2 = (float) this.mBinkInfo.Width / (float) this.mBinkInfo.Height;
        if ((double) num1 < (double) num2)
        {
          float f = (num2 - num1) * 0.5f;
          x0 -= f;
          x1 += Mathf.Abs(f);
        }
      }
      Bink.ScheduleOverlay(this.mBink, x0, y0, x1, y1, this.layerDepth);
      Bink.AllScheduled();
      Bink.SetOverlayRenderBuffer(Display.main.colorBuffer.GetNativeRenderBufferPtr());
      RenderTexture active = RenderTexture.active;
      RenderTexture.active = (RenderTexture) null;
      GL.IssuePluginEvent(Bink.GetUnityEventFunc(), 411601028);
      RenderTexture.active = active;
    }
  }

  private void OnEnable()
  {
    this.mStreamingAssetsPath = Application.streamingAssetsPath;
  }

  private void OnDisable()
  {
    if (this.mPlaybackStatus == BinkWrapper.PlaybackStatus.Paused)
      return;
    Bink.Close(this.mBink);
    this.mBink = IntPtr.Zero;
    this.mPlaybackStatus = BinkWrapper.PlaybackStatus.Stopped;
  }

  private void OnDestroy()
  {
    --BinkWrapper.mInstanceCount;
    if (BinkWrapper.mInstanceCount != 0)
      return;
    GL.IssuePluginEvent(Bink.GetUnityEventFunc(), 411601026);
  }

  public void PrepareMovie(string movie_name)
  {
    if (string.IsNullOrEmpty(movie_name))
      return;
    string name = App.instance.assetManager.GetCustomMoviePath(movie_name + ".bik");
    if (string.IsNullOrEmpty(name))
      name = System.IO.Path.Combine(this.mStreamingAssetsPath, movie_name + ".bk2");
    ulong file_byte_offset = 0;
    if (!string.IsNullOrEmpty(this.mCurrentMovieName) && this.mCurrentMovieName != movie_name)
      this.Stop();
    this.mBink = Bink.Open(name, this.soundOutput, this.soundTrackOffset, this.ioBuffering, file_byte_offset);
    if (this.mBink == IntPtr.Zero)
      UnityEngine.Debug.Log((object) Bink.GetError());
    this.mCurrentMovieName = movie_name;
    this.mPlaybackStatus = BinkWrapper.PlaybackStatus.Stopped;
  }

  public void Play(int loop_count = 1, Action on_video_finished = null)
  {
    if (this.mBink == IntPtr.Zero)
    {
      UnityEngine.Debug.Log((object) Bink.GetError());
    }
    else
    {
      if (this.mPlaybackStatus == BinkWrapper.PlaybackStatus.Paused)
      {
        Bink.Pause(this.mBink, 0);
      }
      else
      {
        if (on_video_finished != null)
          this.mOnVideoFinished = on_video_finished;
        Bink.Loop(this.mBink, (uint) loop_count);
      }
      this.mPlaybackStatus = BinkWrapper.PlaybackStatus.Playing;
    }
  }

  public void Pause()
  {
    if (!(this.mBink != IntPtr.Zero) || this.mPlaybackStatus != BinkWrapper.PlaybackStatus.Playing)
      return;
    Bink.Pause(this.mBink, -1);
    this.mPlaybackStatus = BinkWrapper.PlaybackStatus.Paused;
  }

  public void Stop()
  {
    if (this.mPlaybackStatus == BinkWrapper.PlaybackStatus.Stopped)
      return;
    Bink.Close(this.mBink);
    this.mBink = IntPtr.Zero;
    this.mPlaybackStatus = BinkWrapper.PlaybackStatus.Stopped;
  }

  public bool IsPlaying()
  {
    return this.mPlaybackStatus == BinkWrapper.PlaybackStatus.Playing;
  }

  public bool IsStopped()
  {
    return this.mPlaybackStatus == BinkWrapper.PlaybackStatus.Stopped;
  }

  private void VideoFinished()
  {
    this.mPlaybackStatus = BinkWrapper.PlaybackStatus.Finished;
    if (this.mOnVideoFinished == null)
      return;
    this.mOnVideoFinished();
  }

  public enum DrawStyle
  {
    FillScreen,
    OverlayFillScreenWithAspectRatio,
  }

  private enum PlaybackStatus
  {
    Stopped,
    Playing,
    Paused,
    Finished,
  }

  public enum UnityPluginCommands
  {
    ProcessOnly = 411601024,
    ProcessOnlyNoWait = 411601025,
    ProcessCloses = 411601026,
    ProcessAndDraw = 411601027,
    ProcessAndDrawNoWait = 411601028,
    DrawOnly = 411601029,
    DrawToTexturesOnly = 411601030,
    DrawOverlaysOnly = 411601031,
  }
}
