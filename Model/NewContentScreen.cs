// Decompiled with JetBrains decompiler
// Type: NewContentScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;

public class NewContentScreen : UIScreen
{
  public UIDlcButton[] dlcButtonArray;

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.SetTopBarMode(UITopBar.Mode.Core);
    this.SetBottomBarMode(UIBottomBar.Mode.Core);
    UIManager.instance.navigationBars.SetContinueInteractable(false);
    UIManager.instance.ClearForwardStack();
    UIManager.instance.navigationBars.SetContinueActive(false);
    this.RefreshButtons();
    App.instance.dlcManager.OnOwnedDlcChanged += new Action(this.RefreshButtons);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionScreen, 0.0f);
  }

  private void RefreshButtons()
  {
    for (int index = 0; index < this.dlcButtonArray.Length; ++index)
      this.dlcButtonArray[index].Setup();
  }

  public override void OnExit()
  {
    base.OnExit();
    UIManager.instance.navigationBars.SetContinueActive(true);
    App.instance.dlcManager.OnOwnedDlcChanged += new Action(this.RefreshButtons);
  }
}
