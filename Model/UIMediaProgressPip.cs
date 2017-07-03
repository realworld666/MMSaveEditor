// Decompiled with JetBrains decompiler
// Type: UIMediaProgressPip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIMediaProgressPip : MonoBehaviour
{
  public GameObject emptyContainer;
  public GameObject filledContainer;
  public GameObject currentStepContainer;

  public void SetState(UIMediaProgressPip.State inState)
  {
    this.emptyContainer.SetActive(false);
    this.filledContainer.SetActive(false);
    this.currentStepContainer.SetActive(false);
    switch (inState)
    {
      case UIMediaProgressPip.State.Empty:
        this.emptyContainer.SetActive(true);
        break;
      case UIMediaProgressPip.State.Filled:
        this.filledContainer.SetActive(true);
        break;
      case UIMediaProgressPip.State.Current:
        this.currentStepContainer.SetActive(true);
        break;
    }
  }

  public enum State
  {
    Empty,
    Filled,
    Current,
  }
}
