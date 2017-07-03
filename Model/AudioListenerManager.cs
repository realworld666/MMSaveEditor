// Decompiled with JetBrains decompiler
// Type: AudioListenerManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class AudioListenerManager
{
  private List<StealableAudioListener> audioListeners = new List<StealableAudioListener>();

  public void RegisterAudioListener(StealableAudioListener inListener)
  {
    if (this.CheckAudioListener(inListener) != -1)
      return;
    this.audioListeners.Add(inListener);
  }

  public void UnRegisterAudioListener(StealableAudioListener inListener)
  {
    int index = this.CheckAudioListener(inListener);
    if (index < 0)
      return;
    this.audioListeners.RemoveAt(index);
  }

  public StealableAudioListener GetActiveAudioListener()
  {
    if (this.audioListeners.Count > 0)
      return this.audioListeners[0];
    return (StealableAudioListener) null;
  }

  private int CheckAudioListener(StealableAudioListener inListener)
  {
    int count = this.audioListeners.Count;
    for (int index = 0; index < count; ++index)
    {
      if ((Object) this.audioListeners[index] == (Object) inListener)
        return index;
    }
    return -1;
  }
}
