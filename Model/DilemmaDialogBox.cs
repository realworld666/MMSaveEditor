// Decompiled with JetBrains decompiler
// Type: DilemmaDialogBox
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class DilemmaDialogBox : UIDialogBox
{
  public UICharacterPortrait driverPortrait;
  public UICharacterPortrait staffPortrait;
  public Flag characterFlag;
  public TextMeshProUGUI header;
  public TextMeshProUGUI characterNameLabel;
  public TextMeshProUGUI characterJobTitle;
  public TextMeshProUGUI dilemmaTextLabel;
  public TextMeshProUGUI okayButtonLabel;
  public TextMeshProUGUI cancelButtonLabel;
  public Animator animator;
  private RadioMessage mDilemmaRadioMessage;
  private DilemmaDialogBox.Mode mMode;
  private DilemmaDialogBox.UltimatumType mUltimatumType;

  protected override void Awake()
  {
    base.Awake();
  }

  protected override void OnEnable()
  {
    base.OnEnable();
    this.OnOKButton += new Action(this.OnActionButton);
    this.OnCancelButton += new Action(this.OnCloseButton);
  }

  public void SetMode(DilemmaDialogBox.Mode inMode)
  {
    this.mMode = inMode;
  }

  public static void ShowDilemma(RadioMessage inRadioMessage)
  {
    DilemmaDialogBox dialog = UIManager.instance.dialogBoxManager.GetDialog<DilemmaDialogBox>();
    dialog.SetupPersonPortrait(inRadioMessage.personWhoSpeaks);
    dialog.characterNameLabel.text = inRadioMessage.personWhoSpeaks.shortName;
    dialog.characterFlag.SetNationality(inRadioMessage.personWhoSpeaks.nationality);
    dialog.header.text = "Dilemma";
    dialog.dilemmaTextLabel.text = inRadioMessage.text.GetText();
    dialog.mDilemmaRadioMessage = inRadioMessage;
    dialog.SetMode(DilemmaDialogBox.Mode.Dilemma);
    GameUtility.SetActive(dialog.cancelButton.gameObject, true);
    dialog.okayButtonLabel.text = Localisation.LocaliseID("PSG_10009078", (GameObject) null);
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
  }

  public static void ShowUltimatum(DilemmaDialogBox.UltimatumType inType)
  {
    DilemmaDialogBox dialog = UIManager.instance.dialogBoxManager.GetDialog<DilemmaDialogBox>();
    dialog.mUltimatumType = inType;
    Chairman inChairman = (Chairman) null;
    switch (dialog.mUltimatumType)
    {
      case DilemmaDialogBox.UltimatumType.Warning:
        inChairman = Game.instance.player.team.chairman;
        dialog.header.text = Localisation.LocaliseID("PSG_10004895", (GameObject) null);
        dialog.dilemmaTextLabel.text = Localisation.LocaliseID("PSG_10004896", (GameObject) null);
        dialog.okayButtonLabel.text = Localisation.LocaliseID("PSG_10003119", (GameObject) null);
        break;
      case DilemmaDialogBox.UltimatumType.Safe:
        inChairman = Game.instance.player.team.chairman;
        dialog.header.text = Localisation.LocaliseID("PSG_10004899", (GameObject) null);
        dialog.dilemmaTextLabel.text = Localisation.LocaliseID("PSG_10004900", (GameObject) null);
        dialog.okayButtonLabel.text = Localisation.LocaliseID("PSG_10003119", (GameObject) null);
        break;
      case DilemmaDialogBox.UltimatumType.Fired:
        inChairman = Game.instance.player.team.chairman;
        dialog.header.text = Localisation.LocaliseID("PSG_10004897", (GameObject) null);
        dialog.dilemmaTextLabel.text = Localisation.LocaliseID("PSG_10004898", (GameObject) null);
        dialog.okayButtonLabel.text = Localisation.LocaliseID("PSG_10004901", (GameObject) null);
        break;
    }
    inChairman.ultimatum.shown = true;
    dialog.SetupChairman(inChairman);
    dialog.SetMode(DilemmaDialogBox.Mode.Ultimatum);
    GameUtility.SetActive(dialog.cancelButton.gameObject, false);
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
  }

  public void SetupChairman(Chairman inChairman)
  {
    this.SetupPersonPortrait((Person) inChairman);
    this.characterNameLabel.text = inChairman.shortName;
    this.characterJobTitle.text = Localisation.LocaliseEnum((Enum) inChairman.contract.job);
    this.characterFlag.SetNationality(inChairman.nationality);
  }

  private void SetupPersonPortrait(Person inPerson)
  {
    Driver driver = inPerson as Driver;
    if (driver != null)
    {
      GameUtility.SetActive(this.driverPortrait.gameObject, true);
      GameUtility.SetActive(this.staffPortrait.gameObject, false);
      this.driverPortrait.SetPortrait((Person) driver);
    }
    else
    {
      GameUtility.SetActive(this.driverPortrait.gameObject, false);
      GameUtility.SetActive(this.staffPortrait.gameObject, true);
      this.staffPortrait.SetPortrait(inPerson);
    }
  }

  public void OnActionButton()
  {
    if (this.mMode == DilemmaDialogBox.Mode.Dilemma)
      this.mDilemmaRadioMessage.DoDilemmaAction();
    this.Hide();
  }

  public void OnCloseButton()
  {
    this.Hide();
  }

  public enum Mode
  {
    Dilemma,
    Ultimatum,
  }

  public enum UltimatumType
  {
    Warning,
    Safe,
    Fired,
  }
}
