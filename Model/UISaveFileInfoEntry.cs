// Decompiled with JetBrains decompiler
// Type: UISaveFileInfoEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISaveFileInfoEntry : MonoBehaviour
{
  [SerializeField]
  private Button wholeEntryButton;
  [SerializeField]
  private Button deleteButton;
  [SerializeField]
  private TextMeshProUGUI fileNameLabel;
  [SerializeField]
  private TextMeshProUGUI fileDateTimeLabel;
  [SerializeField]
  private UITeamLogo teamLogo;
  [SerializeField]
  private GameObject workshopLogo;
  [SerializeField]
  private RectTransform autoSaveLogoTransform;
  private SaveFileInfo mSaveFileInfo;

  public SaveFileInfo saveFileInfo
  {
    get
    {
      return this.mSaveFileInfo;
    }
  }

  private void Awake()
  {
    this.wholeEntryButton.onClick.AddListener(new UnityAction(this.OnClickEntryButton));
    this.deleteButton.onClick.AddListener(new UnityAction(this.OnClickDeleteButton));
  }

  public void SetSaveFileInfo(SaveFileInfo inSaveFileInfo)
  {
    this.mSaveFileInfo = inSaveFileInfo;
    if (this.mSaveFileInfo.isBroken)
    {
      this.fileNameLabel.text = "Corrupt: " + this.mSaveFileInfo.fileInfo.Name;
      DateTime lastWriteTimeUtc = this.mSaveFileInfo.fileInfo.LastWriteTimeUtc;
      this.fileDateTimeLabel.text = GameUtility.FormatDateTimeToShortDateString(lastWriteTimeUtc) + " " + lastWriteTimeUtc.ToLongTimeString();
      this.teamLogo.SetTeam(-1);
      this.wholeEntryButton.image.color = Color.red;
    }
    else if (!this.mSaveFileInfo.IsValidDLC())
    {
      this.fileNameLabel.text = Localisation.LocaliseID("PSG_10011988", (GameObject) null);
      StringVariableParser.subject = new Person();
      StringVariableParser.subject.gender = this.mSaveFileInfo.gameInfo.playerGender;
      this.SetupGenericLabels();
      this.wholeEntryButton.image.color = Color.red;
    }
    else
    {
      StringVariableParser.subject = new Person();
      StringVariableParser.subject.gender = this.mSaveFileInfo.gameInfo.playerGender;
      this.fileNameLabel.text = this.mSaveFileInfo.GetDisplayName();
      this.SetupGenericLabels();
    }
  }

  private void SetupGenericLabels()
  {
    DateTime date = this.mSaveFileInfo.saveInfo.date;
    this.fileDateTimeLabel.text = GameUtility.FormatDateTimeToShortDateString(date) + " " + date.ToLongTimeString();
    if (this.mSaveFileInfo.gameInfo.teamLogo == null)
    {
      if (string.IsNullOrEmpty(this.mSaveFileInfo.gameInfo.teamName))
        this.teamLogo.SetTeam(-1);
      else
        this.teamLogo.SetTeam(this.mSaveFileInfo.gameInfo.teamLogoID);
    }
    else
    {
      TeamLogo teamLogo = this.mSaveFileInfo.gameInfo.teamLogo;
      this.teamLogo.SetCustomTeamLogo(teamLogo.styleID, teamLogo.teamFirstName, teamLogo.teamLasttName, this.mSaveFileInfo.gameInfo.teamColor);
    }
    GameUtility.SetActive(this.teamLogo.gameObject, !this.mSaveFileInfo.IsWorkshopSave());
    GameUtility.SetActive(this.workshopLogo, this.mSaveFileInfo.IsWorkshopSave());
    this.autoSaveLogoTransform.gameObject.SetActive(this.mSaveFileInfo.saveInfo.isAutoSave);
  }

  private void OnClickDeleteButton()
  {
    GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
    Action inCancelAction = (Action) (() => {});
    Action inConfirmAction = (Action) (() =>
    {
      App.instance.saveSystem.Delete(this.mSaveFileInfo);
      UIManager.instance.dialogBoxManager.GetDialog<SaveLoadDialog>().Refresh();
    });
    string inTitle = Localisation.LocaliseID("PSG_10009098", (GameObject) null);
    string inText = Localisation.LocaliseID("PSG_10009099", (GameObject) null);
    string inCancelString = Localisation.LocaliseID("PSG_10009077", (GameObject) null);
    string inConfirmString = Localisation.LocaliseID("PSG_10009078", (GameObject) null);
    dialog.Show(inCancelAction, inCancelString, inConfirmAction, inConfirmString, inText, inTitle);
  }

  public void SetSelected(bool selected)
  {
    this.wholeEntryButton.interactable = !selected;
  }

  private void OnClickEntryButton()
  {
    UIManager.instance.dialogBoxManager.GetDialog<SaveLoadDialog>().SetSelectedFile(this.mSaveFileInfo);
  }
}
