// Decompiled with JetBrains decompiler
// Type: SceneInstance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInstance
{
  public string sceneName;
  public SceneSet.SceneSetType sceneSet;
  private AsyncOperation mAsyncOperation;
  private int mScenePriority;

  public GameObject sceneGameObject { get; private set; }

  public Scene scene { get; private set; }

  public bool isLoading
  {
    get
    {
      return this.mAsyncOperation != null;
    }
  }

  public virtual bool loadingComplete
  {
    get
    {
      if (this.scene.IsValid())
        return this.scene.isLoaded;
      return false;
    }
  }

  public virtual bool is3DScene
  {
    get
    {
      return true;
    }
  }

  public SceneInstance(string inSceneName, int inScenePriority, SceneSet.SceneSetType inSceneSet)
  {
    this.sceneName = inSceneName;
    this.mScenePriority = inScenePriority;
    this.sceneSet = inSceneSet;
  }

  public virtual void EnableScene()
  {
    if (!((Object) this.sceneGameObject != (Object) null) || this.sceneGameObject.activeSelf)
      return;
    GameUtility.SetActive(this.sceneGameObject, true);
    UnityEngine.SceneManagement.SceneManager.SetActiveScene(this.scene);
  }

  public virtual void DisableScene()
  {
    if (!((Object) this.sceneGameObject != (Object) null) || !this.sceneGameObject.activeSelf || !this.sceneGameObject.activeSelf)
      return;
    GameUtility.SetActive(this.sceneGameObject, false);
  }

  [DebuggerHidden]
  public virtual IEnumerator LoadScene()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new SceneInstance.\u003CLoadScene\u003Ec__Iterator8() { \u003C\u003Ef__this = this };
  }

  [DebuggerHidden]
  private IEnumerator LoadSceneDelegate(string inSceneName)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new SceneInstance.\u003CLoadSceneDelegate\u003Ec__Iterator9() { inSceneName = inSceneName, \u003C\u0024\u003EinSceneName = inSceneName, \u003C\u003Ef__this = this };
  }

  protected void OnLoadScene()
  {
    Debug.LogFormat("Scene {0} loaded. Warming up shaders", (object) this.sceneName);
    this.mAsyncOperation = (AsyncOperation) null;
    this.scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(this.sceneName);
    GameUtility.Assert(this.scene.IsValid(), "Scene loaded from SceneInstance is not valid - perhaps the name was wrong? Scene name: " + this.sceneName, (Object) null);
    GameUtility.Assert(this.scene.isLoaded, "Scene loaded from SceneInstance is apparently not loaded despite the async operation having finished. Scene name: " + this.sceneName, (Object) null);
    GameObject[] rootGameObjects = this.scene.GetRootGameObjects();
    GameUtility.Assert(rootGameObjects.Length > 0, "Loaded scene has no game objects!", (Object) null);
    GameUtility.Assert(rootGameObjects.Length <= 1, "Loaded scene has multiple game objects at the top level - should only have one", (Object) null);
    this.sceneGameObject = rootGameObjects[0];
    Shader.WarmupAllShaders();
    Debug.LogFormat("Scene {0} shaders warmed up", (object) this.sceneName);
    GameUtility.SetActive(this.sceneGameObject, false);
    BaseScene component = this.sceneGameObject.GetComponent<BaseScene>();
    if ((Object) component != (Object) null)
    {
      component.OnSceneLoad();
      component.DisableTraffic();
    }
    SceneManager.instance.SceneNotification(this, SceneManager.SceneNotificationType.LoadCompleted);
  }

  [DebuggerHidden]
  public virtual IEnumerator UnloadScene()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new SceneInstance.\u003CUnloadScene\u003Ec__IteratorA() { \u003C\u003Ef__this = this };
  }

  public float GetProgress()
  {
    if (this.loadingComplete)
      return 1f;
    if (this.mAsyncOperation != null)
      return this.mAsyncOperation.progress;
    return 0.0f;
  }
}
