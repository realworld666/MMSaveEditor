// Decompiled with JetBrains decompiler
// Type: UIBlurGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIBlurGroup : MonoBehaviour
{
  public bool activateInSimulation = true;
  private Canvas mCanvas;

  private void Awake()
  {
    this.mCanvas = this.GetComponent<Canvas>();
    if (!((Object) this.mCanvas == (Object) null))
      return;
    if (!Game.IsActive())
    {
      this.mCanvas = this.GetComponentInParent<Canvas>();
    }
    else
    {
      if (Game.instance.tutorialSystem.isTutorialOnScreen)
        return;
      this.mCanvas = this.GetComponentInParent<Canvas>();
    }
  }

  private void OnEnable()
  {
    if (!this.activateInSimulation && UIManager.instance.currentScreen_name == "SessionHUD")
      return;
    if ((Object) this.mCanvas == (Object) null)
      this.Awake();
    if (!((Object) this.mCanvas != (Object) null))
      return;
    UIManager.instance.blur.Show(this.gameObject, this.mCanvas);
  }

  private void OnDisable()
  {
    if (!UIManager.InstanceExists)
      return;
    UIManager.instance.blur.Hide(this.gameObject);
  }
}
