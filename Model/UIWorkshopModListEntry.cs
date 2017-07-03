// Decompiled with JetBrains decompiler
// Type: UIWorkshopModListEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using ModdingSystem;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIWorkshopModListEntry : MonoBehaviour
{
  private Vector2 mMaxSizePreview = new Vector2();
  public TextMeshProUGUI modTitle;
  public TextMeshProUGUI modDownloadProgress;
  public Toggle activateToggle;
  public Toggle selectItemToggle;
  public Slider downloadProgress;
  public RawImage modPreview;
  public Button modDetailsButton;
  public Button subscribeButton;
  public Button unsubscribeButton;
  public UIWorkshopReviewWidget reviewWidget;
  public GameObject isInstalledIcon;
  public GameObject isInstallingIcon;
  [SerializeField]
  private GameObject workshopOffline;
  [SerializeField]
  private GameObject warningFor2dModeAssets;
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
    this.modDetailsButton.onClick.AddListener(new UnityAction(this.OnModDetailsButton));
    this.selectItemToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnClickThis));
    this.subscribeButton.onClick.AddListener(new UnityAction(this.OnSubscribeButton));
    this.unsubscribeButton.onClick.AddListener(new UnityAction(this.OnUnsubscribeButton));
    this.activateToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleActivate));
    if (!((UnityEngine.Object) this.modPreview.GetComponent<RectTransform>() != (UnityEngine.Object) null))
      return;
    this.mMaxSizePreview = this.modPreview.GetComponent<RectTransform>().sizeDelta;
  }

  public void SetupEntryToShowMod(SteamMod inSteamMod, UIWorkshopModsAndAssetsWidget inOwner)
  {
    GameUtility.SetActive(this.workshopOffline, !SteamManager.IsSteamOnline());
    this.mOwner = inOwner;
    this.mSteamMod = inSteamMod;
    this.modTitle.text = this.mSteamMod.modDetails.m_rgchTitle;
    this.RefreshEntryStatus();
    this.reviewWidget.SetupReviewForMod(this.mSteamMod);
    if ((UnityEngine.Object) this.mSteamMod.previewTexture == (UnityEngine.Object) null)
    {
      this.mSteamMod.onFetchPreview -= new Action(this.OnFetchPreviewImage);
      this.mSteamMod.onFetchPreview += new Action(this.OnFetchPreviewImage);
      this.StartCoroutine(this.mSteamMod.DownloadPreviewTexture());
    }
    else
      this.OnFetchPreviewImage();
    GameUtility.SetActive(this.modPreview.gameObject, false);
    bool flag1 = !App.instance.preferencesManager.GetSettingBool(Preference.pName.Video_3D, true);
    bool flag2 = false;
    for (int index = 0; index < ModModelFileInfo.modelTags.Length; ++index)
    {
      if (this.mSteamMod.HasTag(ModModelFileInfo.modelTags[index]))
      {
        flag2 = true;
        break;
      }
    }
    GameUtility.SetActive(this.warningFor2dModeAssets, flag2 && flag1);
  }

  public void RefreshEntryStatus()
  {
    this.SetInstalledIcon();
    this.isInstallingIcon.gameObject.SetActive(this.mSteamMod.isDownloading);
    this.downloadProgress.gameObject.SetActive(this.mSteamMod.isDownloading);
    this.subscribeButton.gameObject.SetActive(!this.mSteamMod.isSubscribed);
    this.unsubscribeButton.gameObject.SetActive(this.mSteamMod.isSubscribed);
    this.activateToggle.gameObject.SetActive(this.mSteamMod.isSubscribed);
    bool flag = SteamManager.IsSteamOnline();
    this.subscribeButton.interactable = flag;
    this.unsubscribeButton.interactable = flag;
    SteamMod subscribedMod = App.instance.modManager.GetSubscribedMod(this.mSteamMod.modDetails.m_nPublishedFileId.m_PublishedFileId);
    if (this.mSteamMod.isSubscribed && subscribedMod == null && !flag)
    {
      this.activateToggle.isOn = false;
      this.activateToggle.interactable = false;
    }
    else if (subscribedMod != null)
    {
      this.activateToggle.interactable = true;
      this.activateToggle.isOn = subscribedMod.isActive;
    }
    else
    {
      this.activateToggle.interactable = true;
      this.activateToggle.isOn = this.mSteamMod.isActive;
    }
  }

  private void Update()
  {
    if (this.mSteamMod == null || !this.mSteamMod.isDownloading)
      return;
    float downloadProgress = App.instance.modManager.modFetcher.GetDownloadProgress(this.mSteamMod);
    this.downloadProgress.value = downloadProgress;
    StringVariableParser.ordinalNumberString = (downloadProgress * 100f).ToString((IFormatProvider) Localisation.numberFormatter);
    this.modDownloadProgress.text = Localisation.LocaliseID("PSG_10012005", (GameObject) null);
  }

  public void SetInstalledIcon()
  {
    if (this.mSteamMod.isSubscribed)
      this.isInstalledIcon.SetActive(this.mSteamMod.isInstalled);
    else
      this.isInstalledIcon.SetActive(false);
  }

  private void OnToggleActivate(bool inActivate)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mSteamMod.SetActive(inActivate);
    this.mOwner.ActivateMod(inActivate, this.mSteamMod);
  }

  private void OnSubscribeButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    App.instance.modManager.modSubscriber.SubscribeItem(this.mSteamMod);
  }

  private void OnUnsubscribeButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    App.instance.modManager.modSubscriber.UnsubscribeItem(this.mSteamMod);
  }

  private void OnModDetailsButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mSteamMod == null)
      return;
    App.instance.modManager.OpenItemWebpageOnOverlay(this.mSteamMod);
  }

  private void OnClickThis(bool inSelected)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!inSelected)
      return;
    this.mOwner.SelectMod(this.mSteamMod);
  }

  private void OnFetchPreviewImage()
  {
    if (!((UnityEngine.Object) this.mSteamMod.previewTexture != (UnityEngine.Object) null) || !((UnityEngine.Object) this.modPreview != (UnityEngine.Object) null))
      return;
    GameUtility.SetActive(this.modPreview.gameObject, true);
    this.modPreview.texture = (Texture) this.mSteamMod.previewTexture;
    this.modPreview.GetComponent<RectTransform>().sizeDelta = UIWorkshopMyWorkshopWidget.GetResizedImageSize(new Vector2((float) this.mSteamMod.previewTexture.width, (float) this.mSteamMod.previewTexture.height), this.mMaxSizePreview);
  }

  private void OnDisable()
  {
    if (this.mSteamMod == null)
      return;
    this.StopCoroutine(this.mSteamMod.DownloadPreviewTexture());
    this.mSteamMod.previewTexture = (Texture2D) null;
    this.modPreview.texture = (Texture) null;
  }
}
