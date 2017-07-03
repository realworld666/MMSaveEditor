// Decompiled with JetBrains decompiler
// Type: UIWorkshopMyWorkshopWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using ModdingSystem;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIWorkshopMyWorkshopWidget : MonoBehaviour
{
  private Vector2 mMaxSizePreview = new Vector2(256f, 128f);
  public Action OnUploadStart;
  public Action OnUploadFinished;
  public UIGridList modFilesList;
  public Button uploadToSteamButton;
  public Button refreshModContent;
  public Button steamButton;
  public GameObject invalidPreviewImage;
  public Toggle newModToggle;
  public Toggle previousUploadedModsToggle;
  public Dropdown publishedMods;
  public RawImage previewImage;
  public TextMeshProUGUI filesAddedText;
  [SerializeField]
  private GameObject headerWorkshopOffline;
  [SerializeField]
  private GameObject detailsWorkshopOffline;
  [SerializeField]
  private TMP_InputField modTitleInputField;
  [SerializeField]
  private TMP_InputField modDescriptionInputField;
  private string mModTitle;
  private string mModDescription;

  private void Start()
  {
    this.newModToggle.isOn = true;
    this.previousUploadedModsToggle.isOn = false;
    this.uploadToSteamButton.onClick.AddListener(new UnityAction(this.OnUploadToSteamButton));
    this.refreshModContent.onClick.AddListener(new UnityAction(this.OnRefreshModContent));
    this.steamButton.onClick.AddListener(new UnityAction(this.OnSteamButtonPressed));
    this.modTitleInputField.onEndEdit.AddListener(new UnityAction<string>(this.OnTitleChanged));
    this.modDescriptionInputField.onEndEdit.AddListener(new UnityAction<string>(this.OnDescriptionChanged));
    this.newModToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnNewModToggle));
    this.previousUploadedModsToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnPreviousModsToggle));
    this.publishedMods.onValueChanged.AddListener(new UnityAction<int>(this.OnPublishedModsDropdown));
    GameUtility.SetActive(this.steamButton.gameObject, false);
    GameUtility.SetActive(this.publishedMods.gameObject, false);
    this.PopulateNecessaryWidgets();
    if (!((UnityEngine.Object) this.previewImage.GetComponent<RectTransform>() != (UnityEngine.Object) null))
      return;
    this.mMaxSizePreview = this.previewImage.GetComponent<RectTransform>().sizeDelta;
  }

  public void OnEnable()
  {
    if (!SteamManager.Initialized)
      return;
    SteamModFetcher modFetcher = App.instance.modManager.modFetcher;
    modFetcher.OnFetchPublishedSuccess += new Action<List<SteamMod>, uint>(this.PopulatePublishedModsDropdown);
    modFetcher.GetUserPublishedMods((string[]) null, (string[]) null, 1U);
    bool flag = SteamManager.IsSteamOnline();
    GameUtility.SetActive(this.headerWorkshopOffline, !flag);
    GameUtility.SetActive(this.detailsWorkshopOffline, !flag);
    this.uploadToSteamButton.interactable = flag;
    this.publishedMods.value = 0;
    this.publishedMods.RefreshShownValue();
    this.steamButton.interactable = false;
  }

  public void OnDisable()
  {
    if (!SteamManager.Initialized)
      return;
    App.instance.modManager.modFetcher.OnFetchPublishedSuccess -= new Action<List<SteamMod>, uint>(this.PopulatePublishedModsDropdown);
  }

  private void PopulateNecessaryWidgets()
  {
    this.PopulateStagingModFiles();
    this.PopulatePublishedModsDropdown((List<SteamMod>) null, 1U);
    this.PopulatePreviewImage();
    StringVariableParser.ordinalNumberString = App.instance.modManager.modPublisher.stagingMod.modFiles.Count.ToString((IFormatProvider) Localisation.numberFormatter);
    this.filesAddedText.text = Localisation.LocaliseID("PSG_10012006", (GameObject) null);
  }

  private void PopulateStagingModFiles()
  {
    this.modFilesList.DestroyListItems();
    StagingMod stagingMod = App.instance.modManager.modPublisher.stagingMod;
    this.modFilesList.itemPrefab.SetActive(true);
    for (int index = 0; index < stagingMod.modFiles.Count; ++index)
      this.modFilesList.CreateListItem<UIWorkshopStagingModFileEntry>().SetupEntryFileInfo(stagingMod.modFiles[index]);
    this.modFilesList.itemPrefab.SetActive(false);
  }

  private void PopulatePublishedModsDropdown(List<SteamMod> inSteamModList, uint inTotalMatchingResult)
  {
    List<SteamMod> userPublishedMods = App.instance.modManager.userPublishedMods;
    this.publishedMods.get_options().Clear();
    this.publishedMods.get_options().Add(new Dropdown.OptionData("-"));
    for (int index = 0; index < userPublishedMods.Count; ++index)
      this.publishedMods.get_options().Add(new Dropdown.OptionData(userPublishedMods[index].modDetails.m_rgchTitle));
    this.publishedMods.RefreshShownValue();
  }

  private void PopulatePreviewImage()
  {
    StagingMod stagingMod = App.instance.modManager.modPublisher.stagingMod;
    if (stagingMod.IsPreviewImageCorrectSize())
    {
      Texture2D previewImage = stagingMod.GetPreviewImage();
      if ((UnityEngine.Object) previewImage != (UnityEngine.Object) null)
      {
        GameUtility.SetActive(this.previewImage.gameObject, true);
        this.previewImage.texture = (Texture) previewImage;
        this.previewImage.GetComponent<RectTransform>().sizeDelta = UIWorkshopMyWorkshopWidget.GetResizedImageSize(new Vector2((float) previewImage.width, (float) previewImage.height), this.mMaxSizePreview);
        this.invalidPreviewImage.gameObject.SetActive(false);
      }
      else
      {
        GameUtility.SetActive(this.previewImage.gameObject, false);
        this.invalidPreviewImage.SetActive(true);
        Debug.Log((object) "Steam Workshop - MyWorkshop: No preview image added.", (UnityEngine.Object) null);
      }
    }
    else
    {
      this.invalidPreviewImage.SetActive(true);
      Debug.Log((object) "Steam Workshop - MyWorkshop: Preview image too big, max size is 1 Mb.", (UnityEngine.Object) null);
    }
  }

  public static Vector2 GetResizedImageSize(Vector2 imageSize, Vector2 maxSize)
  {
    float num = Mathf.Min(maxSize.x / imageSize.x, maxSize.y / imageSize.y);
    return new Vector2(imageSize.x * num, imageSize.y * num);
  }

  private void MakeButtonsInteractable(bool inActivate)
  {
    bool flag = SteamManager.IsSteamOnline();
    this.modTitleInputField.interactable = inActivate;
    this.modDescriptionInputField.interactable = inActivate;
    this.uploadToSteamButton.interactable = inActivate && flag;
    this.refreshModContent.interactable = inActivate;
    this.publishedMods.interactable = inActivate;
    this.newModToggle.interactable = inActivate;
    this.previousUploadedModsToggle.interactable = inActivate;
  }

  private void OnUploadToSteamButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!string.IsNullOrEmpty(this.mModTitle) && !string.IsNullOrEmpty(this.mModDescription) && App.instance.modManager.modPublisher.stagingMod.containsFiles)
    {
      Debug.Log((object) ("Title: " + this.mModTitle + " Description: " + this.mModDescription), (UnityEngine.Object) null);
      SteamModPublisher modPublisher = App.instance.modManager.modPublisher;
      if (this.publishedMods.value > 0)
      {
        this.ConfirmUpdateWorkshopItem();
      }
      else
      {
        modPublisher.PublishMod(this.mModTitle, this.mModDescription);
        this.StartUpdatingItem();
      }
    }
    else
    {
      GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
      string str = Localisation.LocaliseID("PSG_10012008", (GameObject) null);
      dialog.Show((Action) null, str, (Action) null, str, Localisation.LocaliseID("PSG_10012007", (GameObject) null), Localisation.LocaliseID("PSG_10012009", (GameObject) null));
      Debug.Log((object) "Trying to upload a mod with no title or description: Add both fields.", (UnityEngine.Object) null);
    }
  }

  private void ConfirmUpdateWorkshopItem()
  {
    Action inConfirmAction = (Action) (() =>
    {
      App.instance.modManager.modPublisher.UpdateExistingMod(App.instance.modManager.userPublishedMods[this.publishedMods.value - 1].modDetails.m_nPublishedFileId, this.mModTitle, this.mModDescription);
      this.StartUpdatingItem();
    });
    GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
    string inCancelString = Localisation.LocaliseID("PSG_10012015", (GameObject) null);
    string inConfirmString = Localisation.LocaliseID("PSG_10012016", (GameObject) null);
    dialog.Show((Action) null, inCancelString, inConfirmAction, inConfirmString, Localisation.LocaliseID("PSG_10012013", (GameObject) null), Localisation.LocaliseID("PSG_10012014", (GameObject) null));
  }

  private void StartUpdatingItem()
  {
    SteamModPublisher modPublisher = App.instance.modManager.modPublisher;
    this.MakeButtonsInteractable(false);
    WorkshopLoadingPopup dialog = UIManager.instance.dialogBoxManager.GetDialog<WorkshopLoadingPopup>();
    dialog.SetupPopupMode(WorkshopLoadingPopup.PopupMode.PublishingItem);
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
    modPublisher.OnUploadComplete = (Action) null;
    modPublisher.OnUploadComplete += new Action(this.OnEndUploadSuccess);
    modPublisher.OnUploadFail = (Action<string>) null;
    modPublisher.OnUploadFail += new Action<string>(this.OnEndUploadFail);
    if (this.OnUploadStart == null)
      return;
    this.OnUploadStart();
  }

  private void OnTitleChanged(string inNewTitle)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mModTitle = inNewTitle;
  }

  private void OnDescriptionChanged(string inNewDescription)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mModDescription = inNewDescription;
  }

  private void OnPublishedModsDropdown(int inNewValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.steamButton.interactable = inNewValue > 0;
    if (inNewValue > 0)
    {
      SteamMod userPublishedMod = App.instance.modManager.userPublishedMods[this.publishedMods.value - 1];
      this.modTitleInputField.text = userPublishedMod.modDetails.m_rgchTitle;
      this.modDescriptionInputField.text = userPublishedMod.modDetails.m_rgchDescription;
      this.mModTitle = userPublishedMod.modDetails.m_rgchTitle;
      this.mModDescription = userPublishedMod.modDetails.m_rgchDescription;
    }
    else
    {
      if (inNewValue != 0)
        return;
      this.modTitleInputField.text = string.Empty;
      this.modDescriptionInputField.text = string.Empty;
      this.mModTitle = string.Empty;
      this.mModDescription = string.Empty;
    }
  }

  private void OnEndUploadSuccess()
  {
    GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
    string str = Localisation.LocaliseID("PSG_10012008", (GameObject) null);
    dialog.Show((Action) null, str, (Action) null, str, Localisation.LocaliseID("PSG_10012010", (GameObject) null), Localisation.LocaliseID("PSG_10012011", (GameObject) null));
    this.MakeButtonsInteractable(true);
    UIManager.instance.dialogBoxManager.GetDialog<WorkshopLoadingPopup>().Hide();
    if (this.OnUploadFinished == null)
      return;
    this.OnUploadFinished();
  }

  private void OnEndUploadFail(string inErrorType)
  {
    GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
    string str = Localisation.LocaliseID("PSG_10012008", (GameObject) null);
    StringVariableParser.workshopErrorType = inErrorType;
    dialog.Show((Action) null, str, (Action) null, str, Localisation.LocaliseID("PSG_10012012", (GameObject) null), Localisation.LocaliseID("PSG_10012009", (GameObject) null));
    this.MakeButtonsInteractable(true);
    UIManager.instance.dialogBoxManager.GetDialog<WorkshopLoadingPopup>().Hide();
    if (this.OnUploadFinished == null)
      return;
    this.OnUploadFinished();
  }

  public void OnRefreshModContent()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    StagingMod stagingMod = App.instance.modManager.modPublisher.stagingMod;
    stagingMod.UnloadCachedAssets();
    stagingMod.UpdateModStagingStatus();
    stagingMod.LoadModFolderInfo();
    this.PopulateNecessaryWidgets();
  }

  private void OnNewModToggle(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    GameUtility.SetActive(this.publishedMods.gameObject, !inValue);
    GameUtility.SetActive(this.steamButton.gameObject, !inValue);
  }

  private void OnPreviousModsToggle(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    GameUtility.SetActive(this.publishedMods.gameObject, inValue);
    GameUtility.SetActive(this.steamButton.gameObject, inValue);
  }

  private void OnSteamButtonPressed()
  {
    if (this.publishedMods.value <= 0)
      return;
    List<SteamMod> userPublishedMods = App.instance.modManager.userPublishedMods;
    App.instance.modManager.OpenItemWebpageOnOverlay(userPublishedMods[this.publishedMods.value - 1]);
  }
}
