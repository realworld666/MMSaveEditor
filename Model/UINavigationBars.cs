// Decompiled with JetBrains decompiler
// Type: UINavigationBars
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UINavigationBars : MonoBehaviour
{
  public Canvas tutorialCanvas;
  public UITutorial navigationBarsTutorial;
  private UITopBar mTopBar;
  private UIBottomBar mBottomBar;

  public UITopBar topBar
  {
    get
    {
      return this.mTopBar;
    }
  }

  public UIBottomBar bottomBar
  {
    get
    {
      return this.mBottomBar;
    }
  }

  private void Awake()
  {
    this.mTopBar = this.GetComponentInChildren<UITopBar>(true);
    this.mBottomBar = this.GetComponentInChildren<UIBottomBar>(true);
    this.ShowNavigationBars(false);
    this.StartUIComponents();
  }

  public void ShowNavigationBars(bool inShow)
  {
    GameUtility.SetActive(this.mTopBar.gameObject, inShow);
    GameUtility.SetActive(this.mBottomBar.gameObject, inShow);
  }

  public void SetContinueActive(bool inValue)
  {
    this.mBottomBar.SetContinueActive(inValue);
  }

  public void SetContinueInteractable(bool inValue)
  {
    this.mBottomBar.SetContinueInteractable(inValue);
  }

  public void SetPlayerActionContinueInteractable(bool inValue)
  {
    this.mBottomBar.SetPlayerActionContinueInteractable(inValue);
  }

  private void StartUIComponents()
  {
    ActivateForGameState[] componentsInChildren1 = this.GetComponentsInChildren<ActivateForGameState>(true);
    if (componentsInChildren1 != null)
    {
      for (int index = 0; index < componentsInChildren1.Length; ++index)
        componentsInChildren1[index].OnStart();
    }
    ActivateForSession[] componentsInChildren2 = this.GetComponentsInChildren<ActivateForSession>(true);
    if (componentsInChildren2 != null)
    {
      for (int index = 0; index < componentsInChildren2.Length; ++index)
        componentsInChildren2[index].OnStart();
    }
    UIGridList[] componentsInChildren3 = this.GetComponentsInChildren<UIGridList>(true);
    if (componentsInChildren3 == null)
      return;
    for (int index = 0; index < componentsInChildren3.Length; ++index)
      componentsInChildren3[index].OnStart();
  }

  public void OnUnload()
  {
    this.mTopBar.OnUnload();
    ActivateForGameState[] componentsInChildren1 = this.GetComponentsInChildren<ActivateForGameState>(true);
    if (componentsInChildren1 != null)
    {
      for (int index = 0; index < componentsInChildren1.Length; ++index)
        componentsInChildren1[index].OnUnload();
    }
    ActivateForSession[] componentsInChildren2 = this.GetComponentsInChildren<ActivateForSession>(true);
    if (componentsInChildren2 == null)
      return;
    for (int index = 0; index < componentsInChildren2.Length; ++index)
      componentsInChildren2[index].OnUnload();
  }
}
