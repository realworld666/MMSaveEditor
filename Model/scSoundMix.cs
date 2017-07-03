// Decompiled with JetBrains decompiler
// Type: scSoundMix
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class scSoundMix : MonoBehaviour
{
  private static scSoundMix m_scSoundMix;
  public List<scSoundMix.Submix> m_Submixers;

  public static scSoundMix Instance
  {
    get
    {
      if ((UnityEngine.Object) scSoundMix.m_scSoundMix == (UnityEngine.Object) null)
        Debug.LogError((object) "scSoundMix not initialised", (UnityEngine.Object) null);
      return scSoundMix.m_scSoundMix;
    }
  }

  private void Awake()
  {
    if ((UnityEngine.Object) scSoundMix.m_scSoundMix != (UnityEngine.Object) null)
      Debug.LogError((object) "scSoundMix singleton initialised already", (UnityEngine.Object) null);
    scSoundMix.m_scSoundMix = this;
  }

  public AudioMixerGroup GetMixGroup(scSoundMix.SubmixID submixID)
  {
    using (List<scSoundMix.Submix>.Enumerator enumerator = this.m_Submixers.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        scSoundMix.Submix current = enumerator.Current;
        if (current.m_SubmixID == submixID)
          return current.m_MixerGroup;
      }
    }
    return (AudioMixerGroup) null;
  }

  public enum SubmixID
  {
    PlayerCar,
    InGame,
    Ambience,
    Music,
    Menu,
    Movie,
    Master,
    GT_PlayerCar,
    GT_Ambience,
    Unset,
  }

  [Serializable]
  public class Submix
  {
    public AudioMixerGroup m_MixerGroup;
    public scSoundMix.SubmixID m_SubmixID;
  }
}
