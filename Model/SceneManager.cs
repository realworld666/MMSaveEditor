// Decompiled with JetBrains decompiler
// Type: SceneManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[Serializable]
public class SceneManager : MonoBehaviour
{
  private List<SceneInstance> sceneList = new List<SceneInstance>();
  private List<SceneSet> sceneSetsList = new List<SceneSet>();
  private static SceneManager sInstance;
  [SerializeField]
  private TextAsset[] scenesData;
  private SceneInstance mCurrentScene;
  private BaseScene mCurrentBaseScene;
  private bool mHasLoaded3DGeometry;

  public static SceneManager instance
  {
    get
    {
      SceneManager.EnsureInstanceExists();
      return SceneManager.sInstance;
    }
  }

  public SceneInstance currentScene
  {
    get
    {
      return this.mCurrentScene;
    }
  }

  public BaseScene currentBaseScene
  {
    get
    {
      return this.mCurrentBaseScene;
    }
  }

  public bool hasLoaded3DGeometry
  {
    get
    {
      return this.mHasLoaded3DGeometry;
    }
  }

  private static void Create()
  {
    GameObject go = (GameObject) UnityEngine.Object.Instantiate(Resources.Load("Prefabs/UI/Managers/SceneManager"));
    go.name = go.name.Replace("(Clone)", string.Empty);
    UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(go, FirstActiveSceneHolder.firstActiveScene);
    SceneManager.sInstance = go.GetComponent<SceneManager>();
  }

  public static void EnsureInstanceExists()
  {
    if (!((UnityEngine.Object) SceneManager.sInstance == (UnityEngine.Object) null))
      return;
    SceneManager.Create();
  }

  private void Awake()
  {
    this.sceneList.Clear();
    this.sceneSetsList.Clear();
    for (int index = 0; index < this.scenesData.Length; ++index)
    {
      SceneSet sceneSet = new SceneSet();
      switch (index)
      {
        case 0:
          sceneSet.Setup(SceneSet.SceneSetType.Core, this.scenesData[index]);
          break;
        case 1:
          sceneSet.Setup(SceneSet.SceneSetType.Title, this.scenesData[index]);
          break;
        case 2:
          sceneSet.Setup(SceneSet.SceneSetType.Shared, this.scenesData[index]);
          break;
        case 3:
          sceneSet.Setup(SceneSet.SceneSetType.FrontEnd, this.scenesData[index]);
          break;
        case 4:
          sceneSet.Setup(SceneSet.SceneSetType.RaceEvent, this.scenesData[index]);
          break;
      }
      this.sceneSetsList.Add(sceneSet);
    }
  }

  private void CreateSetSceneInstances(SceneSet.SceneSetType inSceneSet)
  {
    SceneSet sceneSet = this.GetSceneSet(inSceneSet);
    if (sceneSet.scenesList.Length > 0)
    {
      string[] scenesList = sceneSet.scenesList;
      int inScenePriority = scenesList.Length + 1;
      for (int index = 0; index < scenesList.Length; ++index)
      {
        --inScenePriority;
        if (scenesList[index] != string.Empty)
          this.CreateScene(scenesList[index], inScenePriority, sceneSet.sceneSetType);
      }
    }
    this.mHasLoaded3DGeometry = App.instance.preferencesManager.GetSettingBool(Preference.pName.Video_3D, true);
  }

  private void CreateScene(string inSceneName, int inScenePriority, SceneSet.SceneSetType inSceneSet)
  {
    SceneInstance sceneInstance;
    if (App.instance.preferencesManager.GetSettingBool(Preference.pName.Video_3D, true))
    {
      sceneInstance = new SceneInstance(inSceneName, inScenePriority, inSceneSet);
      App.instance.preferencesManager.videoPreferences.SetRunning2DMode(false);
    }
    else
    {
      sceneInstance = (SceneInstance) new SceneInstance2D(inSceneName, inScenePriority, inSceneSet);
      App.instance.preferencesManager.videoPreferences.SetRunning2DMode(true);
    }
    this.sceneList.Add(sceneInstance);
  }

  private SceneSet GetSceneSet(SceneSet.SceneSetType inSceneSet)
  {
    for (int index = 0; index < this.sceneSetsList.Count; ++index)
    {
      if (this.sceneSetsList[index].sceneSetType == inSceneSet)
        return this.sceneSetsList[index];
    }
    return (SceneSet) null;
  }

  private SceneInstance GetScene(string inSceneName)
  {
    for (int index = 0; index < this.sceneList.Count; ++index)
    {
      if (this.sceneList[index].loadingComplete && this.sceneList[index].sceneName == inSceneName)
        return this.sceneList[index];
    }
    return (SceneInstance) null;
  }

  public GameObject GetSceneGameObject(string inSceneName)
  {
    SceneInstance scene = this.GetScene(inSceneName);
    if (scene != null)
      return scene.sceneGameObject;
    return (GameObject) null;
  }

  public void LeaveCurrentScene()
  {
    if ((UnityEngine.Object) this.mCurrentBaseScene != (UnityEngine.Object) null)
      this.mCurrentBaseScene.DisableTraffic();
    if (this.mCurrentScene != null)
      this.mCurrentScene.DisableScene();
    this.mCurrentBaseScene = (BaseScene) null;
    this.mCurrentScene = (SceneInstance) null;
  }

  public bool SwitchScene(string inSceneName)
  {
    SceneInstance scene = this.GetScene(inSceneName);
    if (scene == null || !scene.loadingComplete || (!scene.is3DScene || this.mCurrentScene == scene))
      return false;
    this.LeaveCurrentScene();
    if (!App.instance.preferencesManager.videoPreferences.isRunning2DMode)
    {
      this.mCurrentScene = scene;
      this.mCurrentBaseScene = scene.sceneGameObject.GetComponent<BaseScene>();
      if ((UnityEngine.Object) this.mCurrentBaseScene != (UnityEngine.Object) null)
        this.mCurrentBaseScene.EnableTraffic();
      scene.EnableScene();
    }
    return true;
  }

  public float GetSceneProgress(string inSceneName)
  {
    SceneInstance scene = this.GetScene(inSceneName);
    if (scene != null)
      return scene.GetProgress();
    return 0.0f;
  }

  [DebuggerHidden]
  public IEnumerator LoadScenesSet(SceneSet.SceneSetType inSceneSet)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new SceneManager.\u003CLoadScenesSet\u003Ec__IteratorD() { inSceneSet = inSceneSet, \u003C\u0024\u003EinSceneSet = inSceneSet, \u003C\u003Ef__this = this };
  }

  [DebuggerHidden]
  private IEnumerator DoSceneLoad(SceneSet.SceneSetType inSceneSet)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new SceneManager.\u003CDoSceneLoad\u003Ec__IteratorE() { inSceneSet = inSceneSet, \u003C\u0024\u003EinSceneSet = inSceneSet, \u003C\u003Ef__this = this };
  }

  [DebuggerHidden]
  public IEnumerator UnloadScenesSet(SceneSet.SceneSetType inSceneSet)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new SceneManager.\u003CUnloadScenesSet\u003Ec__IteratorF() { inSceneSet = inSceneSet, \u003C\u0024\u003EinSceneSet = inSceneSet, \u003C\u003Ef__this = this };
  }

  public void DisableAllScenes()
  {
    for (int index = 0; index < this.sceneList.Count; ++index)
    {
      if (this.sceneList[index].loadingComplete)
        this.sceneList[index].DisableScene();
    }
    this.mCurrentScene = (SceneInstance) null;
  }

  public bool isSceneActive(BaseScene inBaseScene)
  {
    return (UnityEngine.Object) this.mCurrentBaseScene == (UnityEngine.Object) inBaseScene;
  }

  public bool HasSceneSetLoaded(SceneSet.SceneSetType inSceneSet)
  {
    SceneSet sceneSet = this.GetSceneSet(inSceneSet);
    return sceneSet != null && sceneSet.HasAllScenesLoaded();
  }

  private void SceneCountIncrease(SceneInstance inScene)
  {
    SceneSet sceneSet = this.GetSceneSet(inScene.sceneSet);
    if (sceneSet == null)
      return;
    ++sceneSet.scenesLoaded;
  }

  private void SceneCountDecrease(SceneInstance inScene)
  {
    SceneSet sceneSet = this.GetSceneSet(inScene.sceneSet);
    if (sceneSet == null)
      return;
    --sceneSet.scenesLoaded;
  }

  public void SceneNotification(SceneInstance inScene, SceneManager.SceneNotificationType inSceneNotificationType)
  {
    switch (inSceneNotificationType)
    {
      case SceneManager.SceneNotificationType.LoadCompleted:
        this.SceneCountIncrease(inScene);
        break;
      case SceneManager.SceneNotificationType.Unloaded:
        this.SceneCountDecrease(inScene);
        break;
    }
  }

  public enum SceneNotificationType
  {
    LoadCompleted,
    Unloaded,
  }
}
