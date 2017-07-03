// Decompiled with JetBrains decompiler
// Type: LoopAnimation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class LoopAnimation : MonoBehaviour
{
  public float[] delays;
  public string AnimOne;
  public string AnimTwo;

  private void Start()
  {
    this.StartCoroutine(this.PlayAnim(15, this.AnimOne));
  }

  [DebuggerHidden]
  private IEnumerator PlayAnim(int delay, string anim)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new LoopAnimation.\u003CPlayAnim\u003Ec__Iterator0() { delay = delay, \u003C\u0024\u003Edelay = delay, \u003C\u003Ef__this = this };
  }
}
