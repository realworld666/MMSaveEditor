// Decompiled with JetBrains decompiler
// Type: UIPromotePopupStaffOption
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPromotePopupStaffOption : MonoBehaviour
{
  public Toggle toggle;
  public TextMeshProUGUI driverNumber;
  public TextMeshProUGUI championshipName;
  public TextMeshProUGUI championshipPosition;
  public TextMeshProUGUI driverAge;
  public TextMeshProUGUI driverName;
  public Flag driverFlag;
  public UICharacterPortrait driverPortrait;
  public UIAbilityStars abilityStars;
  public PromotePopup widget;
  private Driver mDriver;

  public Person person
  {
    get
    {
      return (Person) this.mDriver;
    }
  }

  public void Setup(Driver inDriver)
  {
    this.mDriver = inDriver;
    if ((Object) this.toggle != (Object) null)
    {
      this.toggle.onValueChanged.RemoveAllListeners();
      this.toggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetReplacementPerson()));
    }
    this.driverNumber.text = this.mDriver.contract.GetCurrentStatusText();
    this.championshipName.text = this.mDriver.contract.GetTeam().championship.GetChampionshipName(false);
    if (this.mDriver.GetChampionshipEntry() != null)
      this.championshipPosition.text = GameUtility.FormatForPosition(this.mDriver.GetChampionshipEntry().GetCurrentChampionshipPosition(), (string) null);
    else
      this.championshipPosition.text = "-";
    this.driverAge.text = this.mDriver.GetAge().ToString();
    this.driverName.text = this.mDriver.name;
    this.driverFlag.SetNationality(this.mDriver.nationality);
    this.driverPortrait.SetPortrait((Person) this.mDriver);
    this.abilityStars.SetAbilityStarsData(this.mDriver);
  }

  private void SetReplacementPerson()
  {
    if (!this.toggle.isOn)
      return;
    this.widget.SelectPersonToReplace((Person) this.mDriver);
  }
}
