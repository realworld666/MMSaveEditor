// Decompiled with JetBrains decompiler
// Type: SceneInstance2D
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;

public class SceneInstance2D : SceneInstance
{
  private bool mLoadingComplete;

  public override bool loadingComplete
  {
    get
    {
      return this.mLoadingComplete;
    }
  }

  public override bool is3DScene
  {
    get
    {
      return false;
    }
  }

  public SceneInstance2D(string inSceneName, int inScenePriority, SceneSet.SceneSetType inSceneSet)
    : base(inSceneName, inScenePriority, inSceneSet)
  {
  }

  public override void EnableScene()
  {
  }

  public override void DisableScene()
  {
  }

  [DebuggerHidden]
  public override IEnumerator LoadScene()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new SceneInstance2D.\u003CLoadScene\u003Ec__IteratorB() { \u003C\u003Ef__this = this };
  }

  [DebuggerHidden]
  public override IEnumerator UnloadScene()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new SceneInstance2D.\u003CUnloadScene\u003Ec__IteratorC() { \u003C\u003Ef__this = this };
  }
}
