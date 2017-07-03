// Decompiled with JetBrains decompiler
// Type: UIContractWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIContractWidget : MonoBehaviour
{
  public TextMeshProUGUI negotiateLabel;
  public UIPersonContractDetailsWidget details;
  public Button fireButton;
  public Button compareButton;
  public Button negotiateButton;
  private Driver mDriver;

  private void Start()
  {
    this.negotiateButton.onClick.AddListener(new UnityAction(this.OnNegotiateButton));
    this.compareButton.onClick.AddListener(new UnityAction(this.OnCompareButton));
    this.fireButton.onClick.AddListener(new UnityAction(this.OnFireButton));
  }

  public void Setup(Driver inDriver)
  {
    this.mDriver = inDriver;
    this.details.Setup((Person) this.mDriver);
    this.negotiateLabel.text = !this.mDriver.isNegotiatingContract ? (!this.mDriver.IsPlayersDriver() ? Localisation.LocaliseID("PSG_10007753", (GameObject) null) : Localisation.LocaliseID("PSG_10006855", (GameObject) null)) : Localisation.LocaliseID("PSG_10010603", (GameObject) null);
    bool flag1 = App.instance.gameStateManager.currentState.group != GameState.Group.Frontend;
    bool flag2 = !Game.instance.player.IsUnemployed();
    this.negotiateButton.interactable = !flag1 && !this.mDriver.isNegotiatingContract;
    this.compareButton.interactable = !flag1;
    this.fireButton.interactable = !flag1;
    GameUtility.SetActive(this.negotiateButton.gameObject, flag2 && (this.mDriver.canNegotiateContract || this.mDriver.isNegotiatingContract));
    GameUtility.SetActive(this.compareButton.gameObject, flag2 && this.mDriver.CanShowStats());
    GameUtility.SetActive(this.fireButton.gameObject, this.mDriver.canBeFired);
  }

  private void OnNegotiateButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!this.mDriver.canNegotiateContract)
      return;
    bool transferWindowPreseason = Game.instance.player.team.championship.rules.staffTransferWindowPreseason;
    bool flag1 = !transferWindowPreseason || transferWindowPreseason && App.instance.gameStateManager.currentState is PreSeasonState;
    bool flag2 = this.mDriver.IsFreeAgent() || !this.mDriver.contract.GetTeam().IsPlayersTeam();
    if (flag1 && flag2)
      UIManager.instance.dialogBoxManager.GetDialog<ApproachDialogBox>().Show((Person) this.mDriver, ApproachDialogBox.ApproachType.SignNewContract);
    else if (!flag2)
    {
      UIManager.instance.dialogBoxManager.GetDialog<ApproachDialogBox>().Show((Person) this.mDriver, ApproachDialogBox.ApproachType.RenewContract);
    }
    else
    {
      GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
      string inTitle = Localisation.LocaliseID("PSG_10010846", (GameObject) null);
      string inText = Localisation.LocaliseID("PSG_10010847", (GameObject) null);
      string inCancelString = Localisation.LocaliseID("PSG_10009081", (GameObject) null);
      string empty = string.Empty;
      dialog.Show((Action) null, inCancelString, (Action) null, empty, inText, inTitle);
    }
  }

  private void OnCompareButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    UIManager.instance.ChangeScreen("CompareStaffScreen", (Entity) this.mDriver, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }

  private void OnFireButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    FirePopup dialog = UIManager.instance.dialogBoxManager.GetDialog<FirePopup>();
    dialog.Setup(this.mDriver);
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
  }
}
