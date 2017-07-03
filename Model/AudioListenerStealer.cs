// Decompiled with JetBrains decompiler
// Type: AudioListenerStealer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class AudioListenerStealer : MonoBehaviour
{
  private GameObject container;
  private Transform listener;
  private Transform originalParent;

  private void Awake()
  {
    this.container = new GameObject("AudioListenerContainer");
    this.container.transform.SetParent(this.transform);
  }

  private void Start()
  {
    StealableAudioListener activeAudioListener = App.instance.audioListenerManager.GetActiveAudioListener();
    if ((Object) activeAudioListener != (Object) null)
    {
      this.listener = activeAudioListener.transform;
      this.originalParent = this.listener.parent;
    }
    else
    {
      GameObject gameObject = new GameObject();
      gameObject.AddComponent<AudioListener>();
      gameObject.AddComponent<StealableAudioListener>();
      gameObject.name = "StealableAudioListener";
      this.listener = gameObject.transform;
      this.originalParent = (Transform) null;
    }
    this.listener.SetParent(this.container.transform, true);
  }

  private void OnApplicationQuit()
  {
    this.container = (GameObject) null;
    this.listener = (Transform) null;
    this.originalParent = (Transform) null;
  }

  private void OnDisable()
  {
    if (!((Object) this.listener != (Object) null) || !((Object) this.listener.parent == (Object) this.container.transform))
      return;
    this.listener.SetParent(!((Object) this.originalParent != (Object) null) ? (Transform) null : this.originalParent);
  }
}
