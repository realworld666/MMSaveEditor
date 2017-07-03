// Decompiled with JetBrains decompiler
// Type: UICompareStaffOverviewWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICompareStaffOverviewWidget : MonoBehaviour
{
  public Toggle favouriteToggle;
  public UICharacterPortrait driverPortrait;
  public UICharacterPortrait staffPortrait;
  public Flag flag;
  public UIDriverSeriesIcons driverSeriesIcon;
  public TextMeshProUGUI nameLabel;
  public TextMeshProUGUI ageLabel;
  public GameObject weightContainer;
  public TextMeshProUGUI weightLabel;
  public TextMeshProUGUI championshipName;
  public TextMeshProUGUI championshipPosition;
  public GameObject championshipParent;
  public UIAbilityStars stars;
  public CompareStaffScreen screen;
  private Person mPerson;

  public void Setup(Person inPerson)
  {
    if (inPerson == null)
      return;
    this.mPerson = inPerson;
    this.favouriteToggle.onValueChanged.RemoveAllListeners();
    this.favouriteToggle.isOn = this.mPerson.isShortlisted;
    this.favouriteToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle()));
    GameUtility.SetActive(this.driverPortrait.gameObject, this.mPerson is Driver);
    GameUtility.SetActive(this.staffPortrait.gameObject, !(this.mPerson is Driver));
    if (this.mPerson is Driver)
    {
      this.driverPortrait.SetPortrait(this.mPerson);
      if ((Object) this.driverSeriesIcon != (Object) null)
      {
        GameUtility.SetActive(this.driverSeriesIcon.gameObject, true);
        this.driverSeriesIcon.Setup(this.mPerson as Driver);
      }
    }
    else
    {
      GameUtility.SetActive(this.driverSeriesIcon.gameObject, false);
      this.staffPortrait.SetPortrait(this.mPerson);
    }
    this.flag.SetNationality(this.mPerson.nationality);
    this.nameLabel.text = this.mPerson.name;
    this.ageLabel.text = this.mPerson.GetAge().ToString();
    GameUtility.SetActive(this.weightContainer, this.mPerson.hasWeigthSet);
    this.weightLabel.text = GameUtility.GetWeightText((float) this.mPerson.weight, 2);
    this.SetStars();
    this.SetChampionship();
  }

  public void SetStars()
  {
    if (this.mPerson is Driver)
      this.stars.SetAbilityStarsData(this.mPerson as Driver);
    else
      this.stars.SetAbilityStarsData(this.mPerson);
  }

  public void SetChampionship()
  {
    Driver mPerson = this.mPerson as Driver;
    if (mPerson != null)
      GameUtility.SetActive(this.championshipParent, !mPerson.IsFreeAgent() && mPerson.IsInAChampionship());
    else
      GameUtility.SetActive(this.championshipParent, !this.mPerson.IsFreeAgent());
    if (!this.championshipParent.gameObject.activeSelf)
      return;
    this.championshipName.text = this.mPerson.contract.GetTeam().championship.GetChampionshipName(false) + ":";
    if (mPerson != null)
      this.championshipPosition.text = GameUtility.FormatForPosition(mPerson.GetChampionshipEntry().GetCurrentChampionshipPosition(), (string) null);
    else
      this.championshipPosition.text = GameUtility.FormatForPosition(this.mPerson.contract.GetTeam().GetChampionshipEntry().GetCurrentChampionshipPosition(), (string) null);
  }

  public void SetFavourite()
  {
    this.favouriteToggle.isOn = this.mPerson.isShortlisted;
  }

  private void OnToggle()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mPerson.ToggleShortlisted(this.favouriteToggle.isOn);
    this.screen.UpdateLists(this.mPerson);
  }
}
