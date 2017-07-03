// Decompiled with JetBrains decompiler
// Type: UIDataCenterButtonWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIDataCenterButtonWidget : UIBaseSessionTopBarWidget
{
  public Button button;

  public override void OnEnter()
  {
    this.button.onClick.RemoveAllListeners();
    this.button.onClick.AddListener(new UnityAction(this.OnDataCenterButton));
  }

  private void OnDataCenterButton()
  {
    UIManager.instance.ChangeScreen("LiveTimingsScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    UIManager.instance.ClearBackStack();
  }

  public override bool ShouldBeEnabled()
  {
    return UIManager.instance.currentScreen is SessionHUD;
  }

  private void Update()
  {
  }
}
