// Decompiled with JetBrains decompiler
// Type: scOneShotSounds
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

internal class scOneShotSounds
{
  private SoundID mSoundID;
  private scSoundContainer mSoundContainer;
  private float mTimer;
  private float mMinDelay;
  private float mMaxDelay;

  public float Volume
  {
    set
    {
      if (!((Object) this.mSoundContainer != (Object) null) || !this.mSoundContainer.IsPlaying)
        return;
      this.mSoundContainer.Volume = value;
    }
  }

  public scOneShotSounds(SoundID soundID, float minDelay, float maxDelay)
  {
    this.Initialise(soundID, minDelay, maxDelay, minDelay, maxDelay);
  }

  public scOneShotSounds(SoundID soundID, float minDelay, float maxDelay, float initialMinDelay, float initialMaxDelay)
  {
    this.Initialise(soundID, minDelay, maxDelay, initialMinDelay, initialMaxDelay);
  }

  private void Initialise(SoundID soundID, float minDelay, float maxDelay, float initialMinDelay, float initialMaxDelay)
  {
    this.mTimer = Random.Range(initialMinDelay, initialMaxDelay);
    this.mMinDelay = minDelay;
    this.mMaxDelay = maxDelay;
    this.mSoundID = soundID;
  }

  public void Update(float deltaTime)
  {
    if ((Object) this.mSoundContainer != (Object) null)
    {
      if (this.mSoundContainer.IsPlaying)
        return;
      this.mSoundContainer = (scSoundContainer) null;
    }
    else
    {
      this.mTimer -= deltaTime;
      if ((double) this.mTimer >= 0.0)
        return;
      this.mTimer = Random.Range(this.mMinDelay, this.mMaxDelay);
      this.mSoundContainer = scSoundManager.Instance.PlaySound(this.mSoundID, 0.0f);
    }
  }

  public void Stop()
  {
    scSoundManager.CheckStopSound(ref this.mSoundContainer);
  }
}
