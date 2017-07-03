// Decompiled with JetBrains decompiler
// Type: scSoundHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class scSoundHelper
{
  public static void SetParent(Transform child, Transform parent)
  {
    child.parent = parent;
    child.rotation = Quaternion.identity;
    child.position = Vector3.zero;
  }

  public static float LinearVolumeFromDb(float dB)
  {
    return Mathf.Pow(2f, dB / 6f);
  }

  public static float DbFromLinearVolume(float volume)
  {
    return Mathf.Log10(volume) * 20f;
  }
}
