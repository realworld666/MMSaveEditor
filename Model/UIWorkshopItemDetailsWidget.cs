// Decompiled with JetBrains decompiler
// Type: UIWorkshopItemDetailsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using ModdingSystem;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIWorkshopItemDetailsWidget : MonoBehaviour
{
  private Vector2 mMaxSizePreview = new Vector2(256f, 128f);
  public TextMeshProUGUI itemTitle;
  public TextMeshProUGUI itemDescription;
  public TextMeshProUGUI authorName;
  public TextMeshProUGUI itemSizeText;
  public TextMeshProUGUI itemPostedText;
  public TextMeshProUGUI itemLastUpdatedText;
  public TextMeshProUGUI downloadProgressText;
  public RawImage previewItemImage;
  public Button steamWorkshopDetailsButton;
  public Button subscribeButton;
  public Button unsubscribeButton;
  public Toggle activateItemToggle;
  public Slider downloadProgress;
  public GameObject noImageObject;
  public GameObject installedIcon;
  public GameObject installingIcon;
  public GameObject modsHeader;
  public GameObject assetsHeader;
  public UIWorkshopReviewWidget reviewWidget;
  private SteamMod mSteamMod;
  private UIWorkshopModsAndAssetsWidget mOwner;

  public SteamMod steamMod
  {
    get
    {
      return this.mSteamMod;
    }
  }

  private void Start()
  {
    if (!((UnityEngine.Object) this.previewItemImage.GetComponent<RectTransform>() != (UnityEngine.Object) null))
      return;
    this.mMaxSizePreview = this.previewItemImage.GetComponent<RectTransform>().sizeDelta;
  }

  private void OnEnable()
  {
    App.instance.modManager.modFetcher.OnFetchSteamModAuthor += new Action<ulong>(this.OnFetchAuthorName);
  }

  private void OnDisable()
  {
    App.instance.modManager.modFetcher.OnFetchSteamModAuthor -= new Action<ulong>(this.OnFetchAuthorName);
  }

  private void OnFetchAuthorName(ulong inSteamID)
  {
    if (this.mSteamMod == null || (long) this.mSteamMod.modDetails.m_ulSteamIDOwner != (long) inSteamID)
      return;
    StringVariableParser.workshopCreatorName = this.mSteamMod.authorName;
    this.authorName.text = Localisation.LocaliseID("PSG_10012082", (GameObject) null);
  }

  public void SetupHeaders(UIWorkshopModsAndAssetsWidget.ViewType inViewType)
  {
    GameUtility.SetActive(this.modsHeader, inViewType == UIWorkshopModsAndAssetsWidget.ViewType.Mods);
    GameUtility.SetActive(this.assetsHeader, inViewType == UIWorkshopModsAndAssetsWidget.ViewType.Assets);
  }

  public void SetupOwner(UIWorkshopModsAndAssetsWidget inOwner)
  {
    this.mOwner = inOwner;
  }

  public void SetupWorkshopItemDetails(SteamMod inSteamMod)
  {
    this.mSteamMod = inSteamMod;
    if (this.mSteamMod != null)
    {
      this.steamWorkshopDetailsButton.onClick.RemoveAllListeners();
      this.steamWorkshopDetailsButton.onClick.AddListener(new UnityAction(this.OnWorkshopDetailsButton));
      this.subscribeButton.onClick.RemoveAllListeners();
      this.subscribeButton.onClick.AddListener((UnityAction) (() => App.instance.modManager.modSubscriber.SubscribeItem(this.mSteamMod)));
      this.unsubscribeButton.onClick.RemoveAllListeners();
      this.unsubscribeButton.onClick.AddListener((UnityAction) (() => App.instance.modManager.modSubscriber.UnsubscribeItem(this.mSteamMod)));
      this.activateItemToggle.onValueChanged.RemoveAllListeners();
      this.activateItemToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnActivateToggle));
      this.UpdateButtonsState();
      this.itemTitle.text = this.mSteamMod.modDetails.m_rgchTitle;
      this.itemDescription.text = this.mSteamMod.modDetails.m_rgchDescription;
      if (App.instance.modManager.modFetcher.FetchAuthorName(this.mSteamMod, out StringVariableParser.workshopCreatorName))
        this.authorName.text = Localisation.LocaliseID("PSG_10012082", (GameObject) null);
      else
        this.authorName.text = string.Empty;
      this.itemSizeText.text = ((float) this.mSteamMod.modDetails.m_nFileSize / 1000000f).ToString("0.00", (IFormatProvider) Localisation.numberFormatter) + " Mb";
      this.itemPostedText.text = GameUtility.FormatDateTimeToLongDateString(GameUtility.FromUnixTime(this.mSteamMod.modDetails.m_rtimeCreated), string.Empty);
      this.itemLastUpdatedText.text = GameUtility.FormatDateTimeToLongDateString(GameUtility.FromUnixTime(this.mSteamMod.modDetails.m_rtimeUpdated), string.Empty);
      if ((UnityEngine.Object) this.mSteamMod.previewTexture == (UnityEngine.Object) null)
      {
        this.mSteamMod.onFetchPreview -= new Action(this.SetupPreviewImage);
        this.mSteamMod.onFetchPreview += new Action(this.SetupPreviewImage);
        this.StartCoroutine(this.mSteamMod.DownloadPreviewTexture());
      }
      this.SetupPreviewImage();
      this.reviewWidget.SetupReviewForMod(this.mSteamMod);
    }
    else
    {
      this.steamWorkshopDetailsButton.onClick.RemoveAllListeners();
      this.subscribeButton.onClick.RemoveAllListeners();
      this.unsubscribeButton.onClick.RemoveAllListeners();
      this.activateItemToggle.onValueChanged.RemoveAllListeners();
      this.steamWorkshopDetailsButton.interactable = false;
      this.subscribeButton.interactable = false;
      this.unsubscribeButton.interactable = false;
      this.activateItemToggle.interactable = false;
    }
  }

  private void Update()
  {
    if (this.mSteamMod == null)
    {
      this.itemTitle.text = "-";
      this.itemDescription.text = "-";
      this.authorName.text = "-";
      this.itemSizeText.text = "-";
      this.itemPostedText.text = "-";
      this.itemLastUpdatedText.text = "-";
      this.reviewWidget.SetupDefaults();
      GameUtility.SetActive(this.installingIcon, false);
      GameUtility.SetActive(this.installedIcon, false);
      GameUtility.SetActive(this.noImageObject, true);
      GameUtility.SetActive(this.previewItemImage.gameObject, false);
    }
    else if (this.mSteamMod.isDownloading)
    {
      GameUtility.SetActive(this.installingIcon, true);
      float downloadProgress = App.instance.modManager.modFetcher.GetDownloadProgress(this.mSteamMod);
      this.downloadProgress.value = downloadProgress;
      StringVariableParser.ordinalNumberString = (downloadProgress * 100f).ToString((IFormatProvider) Localisation.numberFormatter);
      this.downloadProgressText.text = Localisation.LocaliseID("PSG_10012005", (GameObject) null);
      GameUtility.SetActive(this.installedIcon, false);
    }
    else if (this.mSteamMod.isSubscribed && this.mSteamMod.isInstalled)
    {
      GameUtility.SetActive(this.installingIcon, false);
      GameUtility.SetActive(this.installedIcon, true);
    }
    else
    {
      GameUtility.SetActive(this.installingIcon, false);
      GameUtility.SetActive(this.installedIcon, false);
    }
  }

  private void SetupPreviewImage()
  {
    if (this.mSteamMod != null && (UnityEngine.Object) this.mSteamMod.previewTexture != (UnityEngine.Object) null)
    {
      this.noImageObject.SetActive(false);
      this.previewItemImage.gameObject.SetActive(true);
      this.previewItemImage.texture = (Texture) this.mSteamMod.previewTexture;
      this.previewItemImage.GetComponent<RectTransform>().sizeDelta = UIWorkshopMyWorkshopWidget.GetResizedImageSize(new Vector2((float) this.mSteamMod.previewTexture.width, (float) this.mSteamMod.previewTexture.height), this.mMaxSizePreview);
    }
    else
    {
      GameUtility.SetActive(this.noImageObject, true);
      GameUtility.SetActive(this.previewItemImage.gameObject, false);
    }
  }

  public void UpdateButtonsState()
  {
    this.subscribeButton.gameObject.SetActive(!this.mSteamMod.isSubscribed);
    this.unsubscribeButton.gameObject.SetActive(this.mSteamMod.isSubscribed);
    this.activateItemToggle.gameObject.SetActive(this.mSteamMod.isSubscribed);
    this.activateItemToggle.onValueChanged.RemoveAllListeners();
    SteamMod subscribedMod = App.instance.modManager.GetSubscribedMod(this.mSteamMod.modDetails.m_nPublishedFileId.m_PublishedFileId);
    bool flag = SteamManager.IsSteamOnline();
    if (this.mSteamMod.isSubscribed && subscribedMod == null && !flag)
    {
      this.activateItemToggle.isOn = false;
      this.activateItemToggle.interactable = false;
    }
    else if (subscribedMod != null)
    {
      this.activateItemToggle.interactable = true;
      this.activateItemToggle.isOn = subscribedMod.isActive;
    }
    else
    {
      this.activateItemToggle.interactable = true;
      this.activateItemToggle.isOn = this.mSteamMod.isActive;
    }
    this.activateItemToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnActivateToggle));
  }

  public void MakeButtonsInteractable(bool inActivate)
  {
    bool flag = SteamManager.IsSteamOnline();
    this.steamWorkshopDetailsButton.interactable = inActivate;
    this.subscribeButton.interactable = inActivate && flag;
    this.unsubscribeButton.interactable = inActivate && flag;
    this.activateItemToggle.interactable = inActivate;
    this.reviewWidget.MakeButtonsInteractable(inActivate);
  }

  private void OnWorkshopDetailsButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    App.instance.modManager.OpenItemWebpageOnOverlay(this.mSteamMod);
  }

  private void OnActivateToggle(bool inActivate)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mSteamMod.SetActive(inActivate);
    this.mOwner.ActivateMod(inActivate, this.mSteamMod);
    this.mOwner.RefreshItemGridEntriesStatus();
  }
}
