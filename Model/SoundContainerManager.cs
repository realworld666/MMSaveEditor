// Decompiled with JetBrains decompiler
// Type: SoundContainerManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class SoundContainerManager
{
  private Dictionary<SoundID, SoundContainerManager.SoundType> m_AllSoundTypes = new Dictionary<SoundID, SoundContainerManager.SoundType>((IEqualityComparer<SoundID>) new SoundContainerManager.SoundIDComparer());
  private List<scSoundContainer> m_AllSounds = new List<scSoundContainer>();
  private bool mLastSolo;
  private bool mSolo;

  private void CopySettingsToAllInstances(scSoundContainer copySound)
  {
    if (!this.m_AllSoundTypes.ContainsKey(copySound.SoundID))
      return;
    using (List<scSoundContainer>.Enumerator enumerator = this.m_AllSoundTypes[copySound.SoundID].m_Instances.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        scSoundContainer current = enumerator.Current;
        current.m_Volume = copySound.m_Volume;
        current.m_Solo = copySound.m_Solo;
        current.m_Mute = copySound.m_Mute;
      }
    }
  }

  public scSoundContainer PlaySound(SoundID name, Transform parent, float volume, float delay)
  {
    if (this.m_AllSoundTypes.ContainsKey(name))
    {
      scSoundContainer scSoundContainer1 = (scSoundContainer) null;
      SoundContainerManager.SoundType allSoundType = this.m_AllSoundTypes[name];
      scSoundContainer scSoundContainer2 = (scSoundContainer) null;
      int num = 0;
      int count = allSoundType.m_Instances.Count;
      for (int index = 0; index < count; ++index)
      {
        if (!allSoundType.m_Instances[index].IsPlaying)
          scSoundContainer1 = allSoundType.m_Instances[index];
        else if (allSoundType.m_Instances[index].UpdateCount > num)
        {
          scSoundContainer2 = allSoundType.m_Instances[index];
          num = allSoundType.m_Instances[index].UpdateCount;
        }
      }
      if ((Object) scSoundContainer2 != (Object) null && (Object) scSoundContainer1 == (Object) null && scSoundContainer2.m_CullingBehaviour == scSoundContainer.CullingBehaviour.Steal)
      {
        scSoundContainer2.StopSound(false);
        scSoundContainer1 = scSoundContainer2;
      }
      if ((Object) scSoundContainer1 != (Object) null)
        scSoundContainer1.PlaySound(volume, parent, delay);
      return scSoundContainer1;
    }
    Debug.LogWarning((object) ("Sound not found " + (object) name), (Object) null);
    return (scSoundContainer) null;
  }

  public void Update(float dt)
  {
    this.mSolo = this.mLastSolo;
    int count = this.m_AllSounds.Count;
    for (int index = 0; index < count; ++index)
    {
      if (this.m_AllSounds[index].IsPlaying)
      {
        this.m_AllSounds[index].UpdateSound(dt, this.mSolo);
        if (this.m_AllSounds[index].m_Solo)
          this.mLastSolo = true;
        if (this.m_AllSounds[index].m_Mute || this.m_AllSounds[index].m_Solo || (double) this.m_AllSounds[index].m_Volume != (double) this.m_AllSounds[index].m_InitialVolume)
          this.CopySettingsToAllInstances(this.m_AllSounds[index]);
      }
    }
  }

  private void AddSoundReference(SoundContainerManager.SoundType soundType, scSoundContainer soundContainer)
  {
    soundType.m_Instances.Add(soundContainer);
    this.m_AllSounds.Add(soundContainer);
  }

  public void AddSoundContainer(scSoundContainer soundContainer, SoundID soundID)
  {
    if (this.m_AllSoundTypes.ContainsKey(soundID))
      return;
    SoundContainerManager.SoundType soundType = new SoundContainerManager.SoundType();
    this.AddSoundReference(soundType, soundContainer);
    for (int index = 1; index < soundContainer.m_MaxInstances; ++index)
    {
      scSoundContainer soundContainer1 = Object.Instantiate<scSoundContainer>(soundContainer);
      scSoundHelper.SetParent(soundContainer1.transform, scSoundManager.Instance.gameObject.transform);
      soundContainer1.name = soundContainer.name;
      this.AddSoundReference(soundType, soundContainer1);
    }
    this.m_AllSoundTypes.Add(soundID, soundType);
  }

  private class SoundType
  {
    public List<scSoundContainer> m_Instances = new List<scSoundContainer>();
  }

  public class SoundIDComparer : IEqualityComparer<SoundID>
  {
    public bool Equals(SoundID x, SoundID y)
    {
      return x == y;
    }

    public int GetHashCode(SoundID codeh)
    {
      return (int) codeh;
    }
  }
}
