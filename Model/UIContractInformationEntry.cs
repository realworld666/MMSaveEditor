// Decompiled with JetBrains decompiler
// Type: UIContractInformationEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIContractInformationEntry : MonoBehaviour
{
  public TextMeshProUGUI expiresLabel;
  public TextMeshProUGUI breakCost;
  public TextMeshProUGUI wageLabel;
  public TextMeshProUGUI statusLabel;
  public Button renew;
  public Button fire;
  public Button promote;
  public Button find;
  private Driver mDriver;

  public void Setup(Driver inDriver)
  {
    this.mDriver = inDriver;
    if (this.mDriver != null)
    {
      if (this.mDriver.contract.IsContractForReplacementPerson())
      {
        this.expiresLabel.text = Localisation.LocaliseID("PSG_10010607", (GameObject) null);
      }
      else
      {
        int monthsRemaining = this.mDriver.contract.GetMonthsRemaining();
        StringVariableParser.intValue1 = monthsRemaining;
        this.expiresLabel.text = Localisation.LocaliseID(monthsRemaining != 1 ? "PSG_10010608" : "PSG_10010609", (GameObject) null);
      }
      this.statusLabel.text = this.mDriver.contract.GetProposedStatusText();
      this.breakCost.text = GameUtility.GetCurrencyString((long) -this.mDriver.contract.GetContractTerminationCost(), 0);
      this.breakCost.color = GameUtility.GetCurrencyColor(-this.mDriver.contract.GetContractTerminationCost());
      this.wageLabel.text = GameUtility.GetCurrencyString(-this.mDriver.contract.perRaceCost, 0);
      this.wageLabel.color = GameUtility.GetCurrencyColor(-this.mDriver.contract.perRaceCost);
    }
    else
    {
      this.statusLabel.text = "-";
      this.expiresLabel.text = "-";
      this.wageLabel.text = "-";
      this.breakCost.text = "-";
    }
    this.SetButtons();
  }

  private void SetButtons()
  {
    this.renew.onClick.RemoveAllListeners();
    this.renew.onClick.AddListener(new UnityAction(this.OnRenewButtonPress));
    this.fire.onClick.RemoveAllListeners();
    this.fire.onClick.AddListener(new UnityAction(this.OnFireButtonPress));
    this.find.onClick.RemoveAllListeners();
    this.find.onClick.AddListener(new UnityAction(this.OnFindButtonPress));
    bool flag = this.mDriver == null || this.mDriver is NullDriver;
    GameUtility.SetActive(this.renew.gameObject, !flag && this.mDriver.canNegotiateContract);
    GameUtility.SetActive(this.fire.gameObject, !flag && this.mDriver.canBeFired);
    GameUtility.SetActive(this.find.gameObject, flag || this.mDriver.IsReplacementPerson());
    if (!((Object) this.promote != (Object) null))
      return;
    GameUtility.SetActive(this.promote.gameObject, !flag && this.mDriver.canBePromoted);
    this.promote.onClick.RemoveAllListeners();
    this.promote.onClick.AddListener(new UnityAction(this.OnPromoteButtonPress));
  }

  private void OnRenewButtonPress()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    UIManager.instance.dialogBoxManager.GetDialog<ApproachDialogBox>().Show((Person) this.mDriver, ApproachDialogBox.ApproachType.RenewContract);
  }

  private void OnFindButtonPress()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    ScoutingScreen.ChangeScreen(UIScoutingFilterJobRole.Filter.Drivers);
  }

  private void OnFireButtonPress()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    FirePopup dialog = UIManager.instance.dialogBoxManager.GetDialog<FirePopup>();
    dialog.Setup(this.mDriver);
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
  }

  private void OnPromoteButtonPress()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    PromotePopup dialog = UIManager.instance.dialogBoxManager.GetDialog<PromotePopup>();
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
    dialog.Setup((Person) this.mDriver);
  }
}
