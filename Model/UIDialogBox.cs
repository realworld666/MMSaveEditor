// Decompiled with JetBrains decompiler
// Type: UIDialogBox
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIDialogBox : MonoBehaviour
{
  public bool useBlur = true;
  public Button yesButton;
  public Button noButton;
  public Button okButton;
  public Button cancelButton;
  private Animator mAnimator;

  public virtual bool pauseOnEnable
  {
    get
    {
      return true;
    }
  }

  public event Action OnYesButton;

  public event Action OnNoButton;

  public event Action OnOKButton;

  public event Action OnCancelButton;

  protected virtual void Awake()
  {
    this.mAnimator = this.GetComponentInChildren<Animator>();
    if ((bool) ((UnityEngine.Object) this.mAnimator) && !GameUtility.HasParameterOfType(this.mAnimator, "closeDialogBox", AnimatorControllerParameterType.Bool))
      this.mAnimator = (Animator) null;
    if ((bool) ((UnityEngine.Object) this.mAnimator) && !GameUtility.HasStateWithName(this.mAnimator, "Exit"))
      this.mAnimator = (Animator) null;
    if (this.HasYesButton())
      this.yesButton.onClick.AddListener(new UnityAction(this.OnYesButtonClicked));
    if (this.HasNoButton())
      this.noButton.onClick.AddListener(new UnityAction(this.OnNoButtonClicked));
    if (this.HasOKButton())
      this.okButton.onClick.AddListener(new UnityAction(this.OnOKButtonClicked));
    if (!this.HasCancelButton())
      return;
    this.cancelButton.onClick.AddListener(new UnityAction(this.OnCancelButtonClicked));
  }

  private void Update()
  {
    if (!((UnityEngine.Object) this.mAnimator != (UnityEngine.Object) null) || this.mAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash != AnimationHashes.Exit)
      return;
    GameUtility.SetActive(this.gameObject, false);
  }

  public virtual void OnQuitToMainMenu()
  {
    GameUtility.SetActive(this.gameObject, false);
  }

  public virtual void Hide()
  {
    if ((UnityEngine.Object) this.mAnimator != (UnityEngine.Object) null)
      this.mAnimator.SetBool("closeDialogBox", true);
    else
      GameUtility.SetActive(this.gameObject, false);
    UIManager.instance.blur.Hide(this.gameObject);
    if (UIManager.instance.dialogBoxManager.GetActiveDialogBoxCount() != 0)
      return;
    UIManager.instance.OnScreenRegainingFocus();
  }

  public void OnYesButtonClicked()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.OnYesButton != null)
      this.OnYesButton();
    this.Hide();
  }

  public void OnNoButtonClicked()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Back, 0.0f);
    if (this.OnNoButton != null)
      this.OnNoButton();
    this.Hide();
  }

  public void OnOKButtonClicked()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.OnOKButton != null)
      this.OnOKButton();
    this.Hide();
  }

  public virtual void OnCancelButtonClicked()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Back, 0.0f);
    this.CallOnCancelButton();
    this.Hide();
  }

  public void CallOnCancelButton()
  {
    if (this.OnCancelButton == null)
      return;
    this.OnCancelButton();
  }

  public bool HasYesButton()
  {
    return (UnityEngine.Object) this.yesButton != (UnityEngine.Object) null;
  }

  public bool HasNoButton()
  {
    return (UnityEngine.Object) this.noButton != (UnityEngine.Object) null;
  }

  public bool HasOKButton()
  {
    return (UnityEngine.Object) this.okButton != (UnityEngine.Object) null;
  }

  public bool HasCancelButton()
  {
    return (UnityEngine.Object) this.cancelButton != (UnityEngine.Object) null;
  }

  protected virtual void OnEnable()
  {
    if ((UnityEngine.Object) this.mAnimator != (UnityEngine.Object) null)
      this.mAnimator.SetBool("closeDialogBox", false);
    if (!this.HasYesButton() && !this.HasNoButton() && (!this.HasOKButton() && !this.HasCancelButton()))
      return;
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionScreen, 0.0f);
  }

  protected virtual void OnDisable()
  {
    this.OnYesButton = (Action) null;
    this.OnNoButton = (Action) null;
    this.OnOKButton = (Action) null;
    this.OnCancelButton = (Action) null;
  }
}
