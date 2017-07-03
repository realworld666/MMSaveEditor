// Decompiled with JetBrains decompiler
// Type: GenericConfirmation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class GenericConfirmation : UIDialogBox
{
  public Button cancel;
  public Button confirm;
  public Button ignore;
  public TextMeshProUGUI title;
  public TextMeshProUGUI text;
  public TextMeshProUGUI cancelLabel;
  public TextMeshProUGUI confirmlabel;
  public TextMeshProUGUI ignoreLabel;
  private Action OnCancel;
  private Action OnConfirm;
  private Action OnIgnore;

  protected override void Awake()
  {
    base.Awake();
    this.cancel.onClick.AddListener(new UnityAction(this.OnCancelButtonBlah));
    this.confirm.onClick.AddListener(new UnityAction(this.OnConfirmButtonClicked));
    if (!((UnityEngine.Object) this.ignore != (UnityEngine.Object) null))
      return;
    this.ignore.onClick.AddListener(new UnityAction(this.OnIgnoreButtonClicked));
  }

  public void Show(Action inCancelAction, string inCancelString, Action inConfirmAction, string inConfirmString, string inText, string inTitle)
  {
    this.title.text = inTitle;
    this.text.text = inText;
    if (App.instance.preferencesManager.GetSettingBool(Preference.pName.Game_Gamepad, false))
    {
      this.cancelLabel.text = inCancelString + " (O)";
      this.confirmlabel.text = inConfirmString + " (X)";
    }
    else
    {
      this.cancelLabel.text = inCancelString;
      this.confirmlabel.text = inConfirmString;
    }
    this.OnCancel = inCancelAction;
    this.OnConfirm = inConfirmAction;
    this.confirm.gameObject.SetActive(inConfirmAction != null);
    GameUtility.SetActive(this.cancel.gameObject, (!(inCancelString == string.Empty) ? 0 : (inCancelAction == null ? 1 : 0)) == 0);
    if ((UnityEngine.Object) this.ignore != (UnityEngine.Object) null)
      GameUtility.SetActive(this.ignore.gameObject, false);
    this.gameObject.SetActive(true);
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) this);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionScreen, 0.0f);
  }

  public void SetIgnoreButton(Action inIgnoreAction, string inIgnoreString)
  {
    if (!((UnityEngine.Object) this.ignore != (UnityEngine.Object) null) || !((UnityEngine.Object) this.ignoreLabel != (UnityEngine.Object) null))
      return;
    this.OnIgnore = inIgnoreAction;
    GameUtility.SetActive(this.ignore.gameObject, inIgnoreAction != null);
    this.ignoreLabel.text = inIgnoreString;
  }

  private void OnCancelButtonBlah()
  {
    if (this.OnCancel != null)
      this.OnCancel();
    this.Hide();
    scSoundManager.Instance.PlaySound(SoundID.Button_Back, 0.0f);
  }

  private void OnConfirmButtonClicked()
  {
    if (this.OnConfirm != null)
      this.OnConfirm();
    this.Hide();
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  private void OnIgnoreButtonClicked()
  {
    if (this.OnIgnore != null)
      this.OnIgnore();
    this.Hide();
    scSoundManager.Instance.PlaySound(SoundID.Button_Back, 0.0f);
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    if (Game.instance != null && !UIManager.instance.dialogBoxManager.GetDialog<SaveLoadDialog>().gameObject.activeSelf)
      Game.instance.time.UnPause(GameTimer.PauseType.UI);
    this.OnCancel = (Action) null;
    this.OnConfirm = (Action) null;
  }
}
