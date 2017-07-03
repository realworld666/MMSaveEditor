// Decompiled with JetBrains decompiler
// Type: TextMeshProTypedAnimation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class TextMeshProTypedAnimation : MonoBehaviour
{
  public bool playOnEnable = true;
  public float animationDuration = 1f;
  public TextMeshProUGUI textLabel;
  private float mTimer;
  private bool mIsPlaying;

  private void OnEnable()
  {
    this.textLabel.maxVisibleCharacters = 0;
    if (!this.playOnEnable)
      return;
    this.Play();
  }

  private void Update()
  {
    if (!this.mIsPlaying)
      return;
    this.mTimer += GameTimer.deltaTime;
    this.textLabel.maxVisibleCharacters = Mathf.RoundToInt(Mathf.Lerp(0.0f, (float) this.textLabel.text.Length, Mathf.Clamp01(this.mTimer / this.animationDuration)));
  }

  public void Play()
  {
    this.mIsPlaying = true;
    this.mTimer = 0.0f;
  }
}
