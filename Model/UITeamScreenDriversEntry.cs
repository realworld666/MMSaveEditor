// Decompiled with JetBrains decompiler
// Type: UITeamScreenDriversEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITeamScreenDriversEntry : MonoBehaviour
{
  public UICharacterPortrait portrait;
  public Flag nationalityFlag;
  public TextMeshProUGUI name;
  public TextMeshProUGUI age;
  public TextMeshProUGUI driverStatus;
  public TextMeshProUGUI poachability;
  public TextMeshProUGUI wage;
  public TextMeshProUGUI expireDate;
  public TextMeshProUGUI breakClause;
  public Button personsPageButton;
  private Person mPerson;

  private void Awake()
  {
    this.personsPageButton.onClick.AddListener(new UnityAction(this.GoToPersonsPage));
  }

  private void GoToPersonsPage()
  {
    if (this.mPerson == null)
      return;
    UIManager.instance.ChangeScreen("DriverScreen", (Entity) this.mPerson, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }

  public void Setup(Driver inDriver)
  {
    this.mPerson = (Person) inDriver;
    this.gameObject.SetActive(this.mPerson != null);
    if (this.mPerson == null)
      return;
    this.portrait.SetPortrait(this.mPerson);
    this.nationalityFlag.SetNationality(this.mPerson.nationality);
    this.name.text = this.mPerson.shortName;
    this.age.text = this.mPerson.GetAge().ToString();
    this.driverStatus.text = Localisation.LocaliseEnum((Enum) this.mPerson.contract.proposedStatus);
    int inResult = UnityEngine.Random.Range(0, 5);
    this.poachability.color = this.mPerson.GetPoachabilityColor(inResult);
    this.poachability.text = this.mPerson.GetPoachabilityString(inResult);
    this.wage.text = GameUtility.GetCurrencyString((long) this.mPerson.contract.GetMonthlyWageCost(), 0);
    this.expireDate.text = this.mPerson.contract.endDate.Year.ToString() + " END";
    this.breakClause.text = GameUtility.GetCurrencyString((long) this.mPerson.contract.GetContractTerminationCost(), 0);
  }
}
