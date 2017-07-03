// Decompiled with JetBrains decompiler
// Type: FirstActiveSceneHolder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine.SceneManagement;

public class FirstActiveSceneHolder
{
  private static bool initialised;
  private static Scene _firstActiveScene;

  public static Scene firstActiveScene
  {
    get
    {
      if (!FirstActiveSceneHolder.initialised)
      {
        FirstActiveSceneHolder._firstActiveScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        FirstActiveSceneHolder.initialised = true;
      }
      return FirstActiveSceneHolder._firstActiveScene;
    }
  }
}
