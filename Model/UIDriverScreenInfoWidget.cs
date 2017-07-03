// Decompiled with JetBrains decompiler
// Type: UIDriverScreenInfoWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIDriverScreenInfoWidget : MonoBehaviour
{
  public TextMeshProUGUI driverNameLabel;
  public TextMeshProUGUI driverNumberLabel;
  public TextMeshProUGUI ageLabel;
  public TextMeshProUGUI weightLabel;
  public UIGauge moraleGuage;
  public UICharacterPortrait driverPortrait;
  public UITeamLogo driverTeamLogo;
  public UIAbilityStars abilityStars;
  public UIDriverHelmet driverHelmet;
  public GameObject driverStatus;
  public Toggle favouriteToggle;
  public Flag flag;
  public UIContractWidget contractWidget;
  public UIScoutDriverWidget scoutDriverWidget;
  public Button teamButton;
  public Button leftDriverButton;
  public Button rightDriverButton;
  public GameObject positiveMoraleIconModifier;
  public GameObject negativeMoraleIconModifier;
  public GameObject consideringOffersIcon;
  public UIDriverSeriesIcons seriesIcons;
  private Driver mDriver;

  public void Setup(Driver inDriver)
  {
    if (inDriver == null)
      return;
    this.mDriver = inDriver;
    this.SetDetails();
  }

  private void SetDetails()
  {
    this.teamButton.onClick.RemoveAllListeners();
    this.teamButton.interactable = !this.mDriver.IsFreeAgent();
    this.teamButton.onClick.AddListener(new UnityAction(this.OnTeamButton));
    this.contractWidget.Setup(this.mDriver);
    if ((Object) this.seriesIcons != (Object) null)
      this.seriesIcons.Setup(this.mDriver);
    this.driverNameLabel.text = this.mDriver.shortName;
    GameUtility.SetActive(this.driverStatus.gameObject, !this.mDriver.IsFreeAgent());
    this.ageLabel.text = this.mDriver.GetAge().ToString();
    GameUtility.SetActive(this.weightLabel.gameObject, this.mDriver.hasWeigthSet);
    this.weightLabel.text = GameUtility.GetWeightText((float) this.mDriver.weight, 2);
    this.driverTeamLogo.SetTeam(this.mDriver.contract.GetTeam());
    this.driverHelmet.SetHelmet(this.mDriver);
    this.driverPortrait.SetPortrait((Person) this.mDriver);
    this.flag.SetNationality(this.mDriver.nationality);
    this.favouriteToggle.onValueChanged.RemoveAllListeners();
    this.favouriteToggle.onValueChanged.AddListener(new UnityAction<bool>(((Person) this.mDriver).ToggleShortlisted));
    this.favouriteToggle.isOn = this.mDriver.isShortlisted;
    this.moraleGuage.SetValueRange(0.0f, 1f);
    this.moraleGuage.SetValue(this.mDriver.GetMorale(), UIGauge.AnimationSetting.Animate);
    GameUtility.SetActive(this.scoutDriverWidget.gameObject, !Game.instance.player.IsUnemployed());
    if (this.scoutDriverWidget.gameObject.activeSelf)
      this.scoutDriverWidget.SetupScoutDriverWidget(this.mDriver);
    bool inIsActive = this.mDriver.CanShowStats();
    GameUtility.SetActive(this.abilityStars.gameObject, inIsActive);
    if (inIsActive)
    {
      this.abilityStars.SetAbilityStarsData(this.mDriver);
      float singleModifierForStat = this.mDriver.personalityTraitController.GetSingleModifierForStat(PersonalityTrait.StatModified.Morale);
      GameUtility.SetActive(this.positiveMoraleIconModifier, (double) singleModifierForStat > 0.0);
      GameUtility.SetActive(this.negativeMoraleIconModifier, (double) singleModifierForStat < 0.0);
      GameUtility.SetActive(this.consideringOffersIcon, this.mDriver.IsOpenToOffers() && !this.mDriver.WantsToRetire());
    }
    else
    {
      GameUtility.SetActive(this.positiveMoraleIconModifier, false);
      GameUtility.SetActive(this.negativeMoraleIconModifier, false);
      GameUtility.SetActive(this.consideringOffersIcon, false);
    }
    if (!this.mDriver.IsFreeAgent())
    {
      GameUtility.SetActive(this.leftDriverButton.gameObject, true);
      GameUtility.SetActive(this.rightDriverButton.gameObject, true);
      this.leftDriverButton.onClick.RemoveAllListeners();
      this.leftDriverButton.onClick.AddListener(new UnityAction(this.OnLeftDriverButton));
      this.rightDriverButton.onClick.RemoveAllListeners();
      this.rightDriverButton.onClick.AddListener(new UnityAction(this.OnRightDriverButton));
      this.driverNumberLabel.text = this.mDriver.contract.GetCurrentStatusText();
    }
    else
    {
      StringVariableParser.subject = (Person) this.mDriver;
      this.driverNumberLabel.text = Localisation.LocaliseID("PSG_10009320", (GameObject) null);
      StringVariableParser.subject = (Person) null;
      GameUtility.SetActive(this.leftDriverButton.gameObject, false);
      GameUtility.SetActive(this.rightDriverButton.gameObject, false);
    }
  }

  private void OnLeftDriverButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    int driverIndex = this.mDriver.contract.GetTeam().GetDriverIndex(this.mDriver);
    int inIndex = driverIndex >= 0 ? driverIndex - 1 : 1;
    if (inIndex < 0)
      inIndex = 2;
    UIManager.instance.ChangeScreen("DriverScreen", (Entity) (this.mDriver.contract.GetTeam().GetDriver(inIndex) ?? this.mDriver.contract.GetTeam().GetDriver(inIndex - 1)), UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }

  private void OnRightDriverButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    int inIndex = (this.mDriver.contract.GetTeam().GetDriverIndex(this.mDriver) + 1) % 3;
    UIManager.instance.ChangeScreen("DriverScreen", (Entity) (this.mDriver.contract.GetTeam().GetDriver(inIndex) ?? this.mDriver.contract.GetTeam().GetDriver((inIndex + 1) % 3)), UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }

  private void OnTeamButton()
  {
    if (this.mDriver.IsFreeAgent())
      return;
    UIManager.instance.ChangeScreen("TeamScreen", (Entity) this.mDriver.contract.GetTeam(), UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }

  private void OnMouseEnter()
  {
    if (!this.mDriver.CanShowStats() || !this.mDriver.personalityTraitController.IsModifingStat(PersonalityTrait.StatModified.Morale) && this.mDriver.moraleStatModificationHistory.historyEntryCount <= 0)
      return;
    UIManager.instance.dialogBoxManager.GetDialog<DriverStatsModifiersRollover>().ShowRollover(this.mDriver.GetMorale(), this.mDriver, PersonalityTrait.StatModified.Morale, string.Empty, false);
  }

  private void OnMouseExit()
  {
    UIManager.instance.dialogBoxManager.GetDialog<DriverStatsModifiersRollover>().HideRollover();
  }
}
