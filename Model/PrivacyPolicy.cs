// Decompiled with JetBrains decompiler
// Type: PrivacyPolicy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PrivacyPolicy : UIDialogBox
{
  public const string HAS_ACCEPTED_PRIVACY_POLICY_FLAG = "AcceptedPrivacyPolicy";
  public Button acceptAgreementButton;
  public Button cancelAgreementButton;

  protected override void Awake()
  {
    base.Awake();
    this.acceptAgreementButton.onClick.AddListener(new UnityAction(this.OnAcceptAgreementButton));
    this.cancelAgreementButton.onClick.AddListener(new UnityAction(this.OnCancelAgreementButton));
  }

  public void Open()
  {
    this.gameObject.SetActive(true);
  }

  public void OnAcceptAgreementButton()
  {
    PlayerPrefs.SetInt("AcceptedPrivacyPolicy", 1);
    this.Hide();
    App.instance.gameStateManager.LoadToTitleScreen(GameStateManager.StateChangeType.CheckForFadedScreenChange);
  }

  public void OnCancelAgreementButton()
  {
    Application.Quit();
  }
}
