// Decompiled with JetBrains decompiler
// Type: UIDialogBoxManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class UIDialogBoxManager
{
  private UIDialogBox[] mDialogBoxes;
  private Canvas mCanvas;

  public bool isReady { get; private set; }

  public Canvas canvas
  {
    get
    {
      return this.mCanvas;
    }
  }

  public UIDialogBoxManager()
  {
    App.instance.StartCoroutine(this.LoadScene());
  }

  [DebuggerHidden]
  private IEnumerator LoadScene()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new UIDialogBoxManager.\u003CLoadScene\u003Ec__Iterator13()
    {
      \u003C\u003Ef__this = this
    };
  }

  public void Update()
  {
    if (!this.isReady)
      ;
  }

  public void Show(string inName)
  {
    if (!this.isReady)
      return;
    this.Show(this.GetDialog(inName));
  }

  public void Show(UIDialogBox inDialog)
  {
    if (!this.isReady)
      return;
    for (int index = 0; index < this.mDialogBoxes.Length; ++index)
    {
      if ((Object) this.mDialogBoxes[index] == (Object) inDialog)
      {
        GameUtility.SetActive(this.mDialogBoxes[index].gameObject, true);
        UIManager.instance.blur.Show(inDialog.gameObject, this.mCanvas);
        if (!Game.IsActive() || !inDialog.pauseOnEnable)
          break;
        Game.instance.time.Pause(GameTimer.PauseType.UI);
        break;
      }
    }
  }

  public UIDialogBox GetDialog(string inName)
  {
    if (!this.isReady)
      return (UIDialogBox) null;
    for (int index = 0; index < this.mDialogBoxes.Length; ++index)
    {
      if (this.mDialogBoxes[index].name == inName)
        return this.mDialogBoxes[index];
    }
    return (UIDialogBox) null;
  }

  public T GetDialog<T>() where T : UIDialogBox
  {
    if (!this.isReady)
      return (T) null;
    for (int index = 0; index < this.mDialogBoxes.Length; ++index)
    {
      if (this.mDialogBoxes[index] is T)
        return (T) this.mDialogBoxes[index];
    }
    return (T) null;
  }

  public void HideAll()
  {
    if (!this.isReady)
      return;
    this.HideDialogs();
  }

  public void OnQuitToMainMenu()
  {
    this.HideDialogs();
  }

  private void HideDialogs()
  {
    if (this.mDialogBoxes != null)
    {
      for (int index = 0; index < this.mDialogBoxes.Length; ++index)
      {
        if (this.mDialogBoxes[index] is GenericConfirmation)
          this.mDialogBoxes[index].CallOnCancelButton();
        GameUtility.SetActive(this.mDialogBoxes[index].gameObject, false);
      }
    }
    UIManager.instance.blur.ForceHide();
    UIManager.instance.OnScreenRegainingFocus();
  }

  public int GetActiveDialogBoxCount()
  {
    if (!this.isReady)
      return 0;
    int num = 0;
    for (int index = 0; index < this.mDialogBoxes.Length; ++index)
    {
      if ((Object) this.mDialogBoxes[index] != (Object) null && this.mDialogBoxes[index].useBlur && this.mDialogBoxes[index].gameObject.activeSelf)
        ++num;
    }
    return num;
  }
}
