// Decompiled with JetBrains decompiler
// Type: PoliticsPlayerVoteStage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PoliticsPlayerVoteStage : MonoBehaviour
{
  public PoliticsPlayerVotePopup.Stage stage;
  public PoliticsPlayerVotePopup widget;

  public virtual void OnStart()
  {
  }

  public virtual void Setup()
  {
    GameUtility.SetActive(this.gameObject, true);
  }

  public virtual void Reset()
  {
  }

  public virtual void Hide()
  {
    GameUtility.SetActive(this.gameObject, false);
  }

  public virtual bool isReady()
  {
    return true;
  }
}
