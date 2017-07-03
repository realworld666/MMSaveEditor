// Decompiled with JetBrains decompiler
// Type: Loader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class Loader : MonoBehaviour
{
  private void Start()
  {
    this.StartCoroutine(this.DoMainLoad());
  }

  [DebuggerHidden]
  private IEnumerator DoMainLoad()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    Loader.\u003CDoMainLoad\u003Ec__Iterator6 mainLoadCIterator6 = new Loader.\u003CDoMainLoad\u003Ec__Iterator6();
    return (IEnumerator) mainLoadCIterator6;
  }
}
