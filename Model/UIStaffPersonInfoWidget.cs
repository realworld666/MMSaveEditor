// Decompiled with JetBrains decompiler
// Type: UIStaffPersonInfoWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIStaffPersonInfoWidget : MonoBehaviour
{
  public UIStaffScreenDriverWidget driverWidget;
  public UICharacterPortrait portrait;
  public Flag flag;
  public UIAbilityStars ability;
  public TextMeshProUGUI personName;
  public TextMeshProUGUI age;
  public TextMeshProUGUI costToBreakContract;
  public TextMeshProUGUI expires;
  public TextMeshProUGUI wage;
  public Button detailsButton;
  public Button fireButton;
  public Button compareButton;
  public Button negotiateButton;
  public Button findButton;
  private Person mPerson;

  private void Awake()
  {
    this.detailsButton.onClick.AddListener(new UnityAction(this.OnCheckDetails));
    this.fireButton.onClick.AddListener(new UnityAction(this.OnFirePerson));
    this.compareButton.onClick.AddListener(new UnityAction(this.OnCompare));
    this.negotiateButton.onClick.AddListener(new UnityAction(this.OnNegotiate));
    this.findButton.onClick.AddListener(new UnityAction(this.OnFind));
  }

  private void OnCheckDetails()
  {
    UIManager.instance.ChangeScreen("StaffDetailsScreen", (Entity) this.mPerson, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }

  private void OnFirePerson()
  {
    FirePopup dialog = UIManager.instance.dialogBoxManager.GetDialog<FirePopup>();
    dialog.Setup(this.mPerson);
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
  }

  private void OnCompare()
  {
    UIManager.instance.ChangeScreen("CompareStaffScreen", (Entity) this.mPerson, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }

  private void OnNegotiate()
  {
    UIManager.instance.dialogBoxManager.GetDialog<ApproachDialogBox>().Show(this.mPerson, ApproachDialogBox.ApproachType.RenewContract);
  }

  private void OnFind()
  {
    if (this.mPerson is Mechanic)
      ScoutingScreen.ChangeScreen(UIScoutingFilterJobRole.Filter.Mechanics);
    else
      ScoutingScreen.ChangeScreen(UIScoutingFilterJobRole.Filter.Designers);
  }

  public void Setup(Person inPerson)
  {
    if (inPerson == null)
      return;
    this.mPerson = inPerson;
    this.portrait.SetPortrait(this.mPerson);
    this.flag.SetNationality(this.mPerson.nationality);
    this.ability.SetAbilityStarsData(this.mPerson);
    if (this.mPerson is Mechanic && (Object) this.driverWidget != (Object) null)
      this.driverWidget.SetupForDriver((Mechanic) this.mPerson);
    GameUtility.SetActive(this.negotiateButton.gameObject, this.mPerson.canNegotiateContract);
    GameUtility.SetActive(this.fireButton.gameObject, this.mPerson.canBeFired);
    GameUtility.SetActive(this.findButton.gameObject, this.mPerson.IsReplacementPerson());
    this.personName.text = this.mPerson.name;
    this.age.text = this.mPerson.GetAge().ToString();
    if (this.mPerson.contract.IsContractForReplacementPerson())
    {
      this.expires.text = Localisation.LocaliseID("PSG_10010607", (GameObject) null);
    }
    else
    {
      int monthsRemaining = this.mPerson.contract.GetMonthsRemaining();
      StringVariableParser.intValue1 = monthsRemaining;
      this.expires.text = Localisation.LocaliseID(monthsRemaining != 1 ? "PSG_10010608" : "PSG_10010609", (GameObject) null);
    }
    this.costToBreakContract.text = GameUtility.GetCurrencyString((long) -this.mPerson.contract.GetContractTerminationCost(), 0);
    this.costToBreakContract.color = GameUtility.GetCurrencyColor(-this.mPerson.contract.GetContractTerminationCost());
    this.wage.text = GameUtility.GetCurrencyString(-this.mPerson.contract.perRaceCost, 0);
    this.wage.color = GameUtility.GetCurrencyColor(-this.mPerson.contract.perRaceCost);
  }
}
