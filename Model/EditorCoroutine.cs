// Decompiled with JetBrains decompiler
// Type: EditorCoroutine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections;

public class EditorCoroutine
{
  private IEnumerator mRoutine;

  private EditorCoroutine(IEnumerator inRoutine)
  {
    this.mRoutine = inRoutine;
  }

  public static EditorCoroutine Start(IEnumerator inRoutine)
  {
    EditorCoroutine editorCoroutine = new EditorCoroutine(inRoutine);
    editorCoroutine.Start();
    return editorCoroutine;
  }

  private void Start()
  {
  }

  public void Stop()
  {
  }

  private void Update()
  {
    if (this.mRoutine.MoveNext())
      return;
    this.Stop();
  }
}
