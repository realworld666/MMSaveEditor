// Decompiled with JetBrains decompiler
// Type: UICompareStaffButtonsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICompareStaffButtonsWidget : MonoBehaviour
{
  public Button detailsButton;
  public Button approachButton;
  public Button clearButton;
  public TextMeshProUGUI approachLabel;
  public UICompareStaffProfileWidget widget;

  public void Setup()
  {
    this.detailsButton.onClick.RemoveAllListeners();
    this.detailsButton.onClick.AddListener(new UnityAction(this.OnDetailsButton));
    this.approachButton.onClick.RemoveAllListeners();
    if (this.widget.person != null)
    {
      if (this.widget.screen.mode == CompareStaffScreen.Mode.Driver)
        GameUtility.SetActive(this.approachButton.gameObject, this.widget.person.canNegotiateContract);
      else
        GameUtility.SetActive(this.approachButton.gameObject, this.widget.person.canNegotiateContract);
    }
    if (this.approachButton.gameObject.activeSelf)
    {
      if (this.widget.person.IsFreeAgent() || !this.widget.person.contract.GetTeam().IsPlayersTeam())
      {
        if (this.widget.person is Driver)
          this.approachLabel.text = Localisation.LocaliseID("PSG_10007753", (GameObject) null);
        else if (this.widget.person is Mechanic)
          this.approachLabel.text = Localisation.LocaliseID("PSG_10007754", (GameObject) null);
        else if (this.widget.person is Engineer)
          this.approachLabel.text = Localisation.LocaliseID("PSG_10007755", (GameObject) null);
      }
      else
        this.approachLabel.text = Localisation.LocaliseID("PSG_10006855", (GameObject) null);
    }
    this.approachButton.onClick.AddListener(new UnityAction(this.OnApproachButton));
    this.clearButton.onClick.RemoveAllListeners();
    this.clearButton.onClick.AddListener(new UnityAction(this.OnClearButton));
  }

  public void OnDetailsButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.widget.screen.mode == CompareStaffScreen.Mode.Driver)
      UIManager.instance.ChangeScreen("DriverScreen", (Entity) this.widget.person, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
    else
      UIManager.instance.ChangeScreen("StaffDetailsScreen", (Entity) this.widget.person, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }

  public void OnApproachButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!this.widget.person.IsFreeAgent() && this.widget.person.contract.GetTeam().IsPlayersTeam())
      UIManager.instance.dialogBoxManager.GetDialog<ApproachDialogBox>().Show(this.widget.person, ApproachDialogBox.ApproachType.RenewContract);
    else
      UIManager.instance.dialogBoxManager.GetDialog<ApproachDialogBox>().Show(this.widget.person, ApproachDialogBox.ApproachType.SignNewContract);
  }

  public void OnClearButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.widget.Reset();
    this.widget.screen.Refresh();
  }
}
