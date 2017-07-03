// Decompiled with JetBrains decompiler
// Type: RequestFundsPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RequestFundsPopup : UIDialogBox
{
  private Chairman.RequestFundsAnswer mRequestType = Chairman.RequestFundsAnswer.Declined;
  public Button buttonCancel;
  public Button buttonConfirm;
  public TextMeshProUGUI confirmLabel;
  public TextMeshProUGUI cancelLabel;
  public UICharacterPortrait portrait;
  public TextMeshProUGUI chairmanName;
  public Flag flag;
  public UIGauge chairmanHappiness;
  public GameObject positiveHappinessIconModifier;
  public GameObject negativeHappinessIconModifier;
  public TextMeshProUGUI label;
  public GameObject impactContainer;
  public TextMeshProUGUI impactLabel;
  public TextMeshProUGUI incomeLabel;

  protected override void Awake()
  {
    base.Awake();
    this.buttonCancel.onClick.AddListener(new UnityAction(this.OnButtonCancel));
    this.buttonConfirm.onClick.AddListener(new UnityAction(this.OnButtonConfirm));
  }

  public static void OpenPopup()
  {
    RequestFundsPopup dialog = UIManager.instance.dialogBoxManager.GetDialog<RequestFundsPopup>();
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
    dialog.Setup();
  }

  public static void ClosePopup()
  {
    UIManager.instance.dialogBoxManager.GetDialog<RequestFundsPopup>().Hide();
  }

  public void Setup()
  {
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionScreen, 0.0f);
    Chairman chairman = Game.instance.player.team.chairman;
    this.mRequestType = chairman.CanRequestFunds();
    switch (this.mRequestType)
    {
      case Chairman.RequestFundsAnswer.Accepted:
        this.label.text = Localisation.LocaliseID("PSG_10011852", (GameObject) null);
        this.confirmLabel.text = Localisation.LocaliseID("PSG_10011856", (GameObject) null);
        this.cancelLabel.text = Localisation.LocaliseID("PSG_10011857", (GameObject) null);
        break;
      case Chairman.RequestFundsAnswer.DeclinedLowHappiness:
        this.label.text = Localisation.LocaliseID("PSG_10011885", (GameObject) null);
        this.cancelLabel.text = Localisation.LocaliseID("PSG_10004912", (GameObject) null);
        break;
      case Chairman.RequestFundsAnswer.DeclinedPreSeason:
        this.label.text = Localisation.LocaliseID("PSG_10011855", (GameObject) null);
        this.cancelLabel.text = Localisation.LocaliseID("PSG_10004912", (GameObject) null);
        break;
      case Chairman.RequestFundsAnswer.DeclinedSeasonStart:
        this.label.text = Localisation.LocaliseID("PSG_10011854", (GameObject) null);
        this.cancelLabel.text = Localisation.LocaliseID("PSG_10004912", (GameObject) null);
        break;
      case Chairman.RequestFundsAnswer.Declined:
        this.label.text = Localisation.LocaliseID("PSG_10011853", (GameObject) null);
        this.cancelLabel.text = Localisation.LocaliseID("PSG_10004912", (GameObject) null);
        break;
    }
    GameUtility.SetActive(this.buttonConfirm.gameObject, this.mRequestType == Chairman.RequestFundsAnswer.Accepted);
    GameUtility.SetActive(this.impactContainer, this.mRequestType == Chairman.RequestFundsAnswer.Accepted);
    this.portrait.SetPortrait((Person) chairman);
    this.chairmanName.text = chairman.name;
    this.flag.SetNationality(chairman.nationality);
    this.chairmanHappiness.SetValueRange(0.0f, 1f);
    this.chairmanHappiness.SetValue(chairman.happinessNormalized, UIGauge.AnimationSetting.Animate);
    if (this.impactContainer.activeSelf)
    {
      StringVariableParser.intValue1 = 50;
      this.impactLabel.text = Localisation.LocaliseID("PSG_10011858", (GameObject) null);
      this.incomeLabel.text = GameUtility.GetCurrencyString(chairman.GetRequestFundsAmmount(), 0);
      this.chairmanHappiness.ShowDriverPin(0, Mathf.Max(1f - Mathf.Clamp01((float) (((double) chairman.GetHappiness() - (double) chairman.GetHappinessModifier() - 50.0) / 100.0)), Mathf.Clamp01(chairman.happinessNormalized - 0.5f)));
      this.chairmanHappiness.SetDriverPinAlpha(0, 1f);
    }
    else
      this.chairmanHappiness.HideDriverPin(0);
    float happinessModifier = chairman.GetHappinessModifier();
    GameUtility.SetActive(this.positiveHappinessIconModifier, (double) happinessModifier > 0.0);
    GameUtility.SetActive(this.negativeHappinessIconModifier, (double) happinessModifier < 0.0);
  }

  private void OnButtonCancel()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Back, 0.0f);
    this.Hide();
  }

  private void OnButtonConfirm()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mRequestType == Chairman.RequestFundsAnswer.Accepted)
      Game.instance.player.team.chairman.RequestFunds();
    this.Hide();
  }

  private void OnMouseEnter()
  {
    Chairman chairman = Game.instance.player.team.chairman;
    if (chairman.GetPersonalityTraitHappinessModifiers().Count <= 0 && chairman.happinessModificationHistory.historyEntryCount <= 0)
      return;
    UIManager.instance.dialogBoxManager.GetDialog<DriverStatsModifiersRollover>().ShowRollover(chairman);
  }

  private void OnMouseExit()
  {
    UIManager.instance.dialogBoxManager.GetDialog<DriverStatsModifiersRollover>().Hide();
  }
}
