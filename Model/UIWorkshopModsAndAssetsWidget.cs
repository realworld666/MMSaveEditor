// Decompiled with JetBrains decompiler
// Type: UIWorkshopModsAndAssetsWidget
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

public class UIWorkshopModsAndAssetsWidget : MonoBehaviour
{
  private UIWorkshopModsAndAssetsWidget.FilterType mCurrentFilter = UIWorkshopModsAndAssetsWidget.FilterType.AllItems;
  private uint mCurrentPaging = 1;
  private List<string> mFilterTags = new List<string>();
  private List<string> mLocalisationIDs = new List<string>();
  public UIGridList modList;
  public Toggle subscribedItemsButton;
  public Toggle publishedItemsButton;
  public Toggle allItemsButton;
  public Toggle activateStagingMod;
  public Dropdown tagsFilterDropdown;
  public Dropdown reviewRatingDropdown;
  public GameObject pleaseWaitIcon;
  public GameObject modsHeader;
  public GameObject assetsHeader;
  public GameObject stagingModHeader;
  public GameObject pagingWidget;
  public TextMeshProUGUI fetchErrorText;
  public UIWorkshopItemDetailsWidget itemDetailsWidget;
  [SerializeField]
  private GameObject workshopOffline;
  [SerializeField]
  private Button leftPagingButton;
  [SerializeField]
  private Button rightPagingButton;
  [SerializeField]
  private Button workshopButton;
  [SerializeField]
  private TextMeshProUGUI pagingLabel;
  [SerializeField]
  private GameObject noModsPublishedYet;
  private UIWorkshopModsAndAssetsWidget.ViewType mCurrentView;

  private void Start()
  {
    if ((UnityEngine.Object) this.subscribedItemsButton != (UnityEngine.Object) null && (UnityEngine.Object) this.publishedItemsButton != (UnityEngine.Object) null && (UnityEngine.Object) this.allItemsButton != (UnityEngine.Object) null && (UnityEngine.Object) this.activateStagingMod != (UnityEngine.Object) null)
    {
      this.subscribedItemsButton.onValueChanged.AddListener(new UnityAction<bool>(this.OnSubscribedItemsButton));
      this.publishedItemsButton.onValueChanged.AddListener(new UnityAction<bool>(this.OnPublishedItemsButton));
      this.allItemsButton.onValueChanged.AddListener(new UnityAction<bool>(this.OnAllItemsButton));
      this.activateStagingMod.onValueChanged.AddListener(new UnityAction<bool>(this.OnActivateStagingMod));
    }
    this.leftPagingButton.onClick.AddListener(new UnityAction(this.OnLeftPagingButton));
    this.rightPagingButton.onClick.AddListener(new UnityAction(this.OnRightPagingButton));
    this.workshopButton.onClick.AddListener(new UnityAction(this.OnWorkshopButton));
    this.reviewRatingDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnReviewRatingDropdown));
    this.tagsFilterDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnTagFilterDropdown));
    if (!((UnityEngine.Object) this.itemDetailsWidget != (UnityEngine.Object) null))
      return;
    this.itemDetailsWidget.SetupOwner(this);
  }

  private void OnEnable()
  {
    if (!SteamManager.Initialized)
      return;
    SteamModFetcher modFetcher = App.instance.modManager.modFetcher;
    modFetcher.OnFetchSubscribedSuccess += new Action<List<SteamMod>, uint>(this.OnFetchSuccess);
    modFetcher.OnFetchAllWorkshopItemsSuccess += new Action<List<SteamMod>, uint>(this.OnFetchSuccess);
    modFetcher.OnFetchPublishedSuccess += new Action<List<SteamMod>, uint>(this.OnFetchSuccess);
    modFetcher.OnFetchSubscribedFailure += new Action<string>(this.OnFetchFail);
    modFetcher.OnFetchAllWorkshopItemsFailure += new Action<string>(this.OnFetchFail);
    modFetcher.OnFetchPublishedFailure += new Action<string>(this.OnFetchFail);
    App.instance.modManager.modSubscriber.OnSubscribeItemAction += new Action(this.RefreshItemGridEntriesStatus);
    App.instance.modManager.modSubscriber.OnUnsubscribeItemAction += new Action(this.RefreshItemGridEntriesStatus);
    App.instance.modManager.modSubscriber.OnInstalledItemAction += new Action(this.RefreshItemGridEntriesStatus);
    this.activateStagingMod.onValueChanged.RemoveAllListeners();
    this.activateStagingMod.isOn = App.instance.modManager.modPublisher.isStaging;
    this.activateStagingMod.onValueChanged.AddListener(new UnityAction<bool>(this.OnActivateStagingMod));
  }

  private void OnDisable()
  {
    if (!SteamManager.Initialized)
      return;
    SteamModFetcher modFetcher = App.instance.modManager.modFetcher;
    modFetcher.OnFetchSubscribedSuccess -= new Action<List<SteamMod>, uint>(this.OnFetchSuccess);
    modFetcher.OnFetchAllWorkshopItemsSuccess -= new Action<List<SteamMod>, uint>(this.OnFetchSuccess);
    modFetcher.OnFetchPublishedSuccess -= new Action<List<SteamMod>, uint>(this.OnFetchSuccess);
    modFetcher.OnFetchSubscribedFailure -= new Action<string>(this.OnFetchFail);
    modFetcher.OnFetchAllWorkshopItemsFailure -= new Action<string>(this.OnFetchFail);
    modFetcher.OnFetchPublishedFailure -= new Action<string>(this.OnFetchFail);
    App.instance.modManager.modSubscriber.OnSubscribeItemAction -= new Action(this.RefreshItemGridEntriesStatus);
    App.instance.modManager.modSubscriber.OnUnsubscribeItemAction -= new Action(this.RefreshItemGridEntriesStatus);
    App.instance.modManager.modSubscriber.OnInstalledItemAction -= new Action(this.RefreshItemGridEntriesStatus);
    this.modList.HideListItems();
  }

  public void SetFilter(UIWorkshopModsAndAssetsWidget.FilterType inFilterType)
  {
    this.mCurrentFilter = inFilterType;
    this.allItemsButton.onValueChanged.RemoveAllListeners();
    this.publishedItemsButton.onValueChanged.RemoveAllListeners();
    this.subscribedItemsButton.onValueChanged.RemoveAllListeners();
    this.allItemsButton.isOn = inFilterType == UIWorkshopModsAndAssetsWidget.FilterType.AllItems;
    this.publishedItemsButton.isOn = inFilterType == UIWorkshopModsAndAssetsWidget.FilterType.PublishedItems;
    this.subscribedItemsButton.isOn = inFilterType == UIWorkshopModsAndAssetsWidget.FilterType.SubscribedItems;
    this.subscribedItemsButton.onValueChanged.AddListener(new UnityAction<bool>(this.OnSubscribedItemsButton));
    this.publishedItemsButton.onValueChanged.AddListener(new UnityAction<bool>(this.OnPublishedItemsButton));
    this.allItemsButton.onValueChanged.AddListener(new UnityAction<bool>(this.OnAllItemsButton));
  }

  public void SetupCurrentView(UIWorkshopModsAndAssetsWidget.ViewType inNewView)
  {
    GameUtility.SetActive(this.workshopOffline, !SteamManager.IsSteamOnline());
    if (!SteamManager.Initialized)
      return;
    this.mCurrentView = inNewView;
    GameUtility.SetActive(this.stagingModHeader, App.instance.modManager.modPublisher.stagingMod.containsFiles);
    if ((UnityEngine.Object) this.itemDetailsWidget != (UnityEngine.Object) null)
      this.itemDetailsWidget.SetupHeaders(this.mCurrentView);
    GameUtility.SetActive(this.modsHeader, this.mCurrentView == UIWorkshopModsAndAssetsWidget.ViewType.Mods);
    GameUtility.SetActive(this.assetsHeader, this.mCurrentView == UIWorkshopModsAndAssetsWidget.ViewType.Assets);
    this.PopulateTagFilterList();
    this.PopulateReviewRatingDropdown();
    this.mCurrentPaging = 1U;
    StringVariableParser.ordinalNumberString = this.mCurrentPaging.ToString((IFormatProvider) Localisation.numberFormatter);
    this.pagingLabel.text = Localisation.LocaliseID("PSG_10012001", (GameObject) null);
    this.QuerySteamWorkshop();
  }

  public void RefreshItemGridEntriesStatus()
  {
    for (int inIndex = 0; inIndex < this.modList.itemCount; ++inIndex)
      this.modList.GetItem<UIWorkshopModListEntry>(inIndex).RefreshEntryStatus();
    if (!((UnityEngine.Object) this.itemDetailsWidget != (UnityEngine.Object) null))
      return;
    this.itemDetailsWidget.UpdateButtonsState();
  }

  private void OnFetchSuccess(List<SteamMod> inSteamModList, uint inTotalMatchingResult)
  {
    this.pleaseWaitIcon.SetActive(false);
    if ((UnityEngine.Object) this.itemDetailsWidget != (UnityEngine.Object) null)
    {
      this.itemDetailsWidget.MakeButtonsInteractable(true);
      List<SteamMod> modList = this.GetModList();
      if (modList.Count > 0)
        this.itemDetailsWidget.SetupWorkshopItemDetails(modList[0]);
      else
        this.itemDetailsWidget.SetupWorkshopItemDetails((SteamMod) null);
    }
    this.PopulateGridEntries();
    this.MakeButtonsInteactible(true);
    if (UIManager.instance.currentScreen is NewCareerScreen && this.mCurrentFilter == UIWorkshopModsAndAssetsWidget.FilterType.AllItems)
      this.reviewRatingDropdown.value = 4;
    else
      this.reviewRatingDropdown.value = 0;
  }

  private void OnFetchFail(string inErrorType)
  {
    this.fetchErrorText.gameObject.SetActive(true);
    StringVariableParser.workshopErrorType = inErrorType;
    this.fetchErrorText.text = Localisation.LocaliseID("PSG_10012002", (GameObject) null);
    this.pleaseWaitIcon.SetActive(false);
    this.MakeButtonsInteactible(true);
    if (!((UnityEngine.Object) this.itemDetailsWidget != (UnityEngine.Object) null))
      return;
    this.itemDetailsWidget.MakeButtonsInteractable(true);
  }

  private void PopulateGridEntries()
  {
    this.modList.HideListItems();
    uint totalResultsReturned = this.GetTotalResultsReturned();
    if (this.mCurrentFilter == UIWorkshopModsAndAssetsWidget.FilterType.PublishedItems)
      GameUtility.SetActive(this.noModsPublishedYet, (int) totalResultsReturned == 0);
    List<SteamMod> modList = this.GetModList();
    for (int inIndex = 0; inIndex < modList.Count; ++inIndex)
    {
      UIWorkshopModListEntry workshopModListEntry = this.modList.GetOrCreateItem<UIWorkshopModListEntry>(inIndex);
      GameUtility.SetActive(workshopModListEntry.gameObject, true);
      workshopModListEntry.SetupEntryToShowMod(modList[inIndex], this);
    }
    GameUtility.SetActive(this.pagingWidget, totalResultsReturned > 50U);
  }

  private void PopulateTagFilterList()
  {
    this.tagsFilterDropdown.get_options().Clear();
    this.mFilterTags.Clear();
    this.mLocalisationIDs.Clear();
    if (this.mCurrentView == UIWorkshopModsAndAssetsWidget.ViewType.Mods)
    {
      this.AddFilterTags(ref this.mFilterTags, ModDatabaseFileInfo.databaseTags);
      this.AddFilterTags(ref this.mLocalisationIDs, ModDatabaseFileInfo.databaseTagsIDs);
    }
    this.AddFilterTags(ref this.mFilterTags, ModModelFileInfo.modelTags);
    this.AddFilterTags(ref this.mFilterTags, ModLogoFileInfo.logoTags);
    this.AddFilterTags(ref this.mFilterTags, ModImageFileInfo.imageTags);
    this.AddFilterTags(ref this.mFilterTags, ModVideoFileInfo.videoTags);
    this.AddFilterTags(ref this.mLocalisationIDs, ModModelFileInfo.modelTagsIDs);
    this.AddFilterTags(ref this.mLocalisationIDs, ModLogoFileInfo.logoTagsID);
    this.AddFilterTags(ref this.mLocalisationIDs, ModImageFileInfo.imageTagsIDs);
    this.AddFilterTags(ref this.mLocalisationIDs, ModVideoFileInfo.videoTagsIDs);
    this.tagsFilterDropdown.get_options().Add(new Dropdown.OptionData(Localisation.LocaliseID("PSG_10012003", (GameObject) null)));
    for (int index = 0; index < this.mFilterTags.Count; ++index)
      this.tagsFilterDropdown.get_options().Add(new Dropdown.OptionData(Localisation.LocaliseID(this.mLocalisationIDs[index], (GameObject) null)));
    this.tagsFilterDropdown.value = 0;
    this.tagsFilterDropdown.RefreshShownValue();
  }

  private void AddFilterTags(ref List<string> inList, string[] tags)
  {
    for (int index = 0; index < tags.Length; ++index)
    {
      if (!inList.Contains(tags[index]))
        inList.Add(tags[index]);
    }
  }

  private void PopulateReviewRatingDropdown()
  {
    this.reviewRatingDropdown.get_options().Clear();
    int num = 5;
    this.reviewRatingDropdown.get_options().Add(new Dropdown.OptionData(Localisation.LocaliseID("PSG_10012003", (GameObject) null)));
    for (int index = 0; index < num; ++index)
    {
      StringVariableParser.ordinalNumberString = (index + 1).ToString((IFormatProvider) Localisation.numberFormatter);
      this.reviewRatingDropdown.get_options().Add(new Dropdown.OptionData(Localisation.LocaliseID("PSG_10012004", (GameObject) null)));
    }
    this.reviewRatingDropdown.value = 0;
    this.reviewRatingDropdown.RefreshShownValue();
  }

  private void MakeButtonsInteactible(bool inActivate)
  {
    this.leftPagingButton.interactable = inActivate && this.mCurrentPaging > 1U;
    uint num = this.mCurrentPaging * 50U;
    uint totalResultsReturned = this.GetTotalResultsReturned();
    this.rightPagingButton.interactable = inActivate && num < totalResultsReturned;
    if ((UnityEngine.Object) this.subscribedItemsButton != (UnityEngine.Object) null && (UnityEngine.Object) this.publishedItemsButton != (UnityEngine.Object) null && (UnityEngine.Object) this.allItemsButton != (UnityEngine.Object) null && (UnityEngine.Object) this.activateStagingMod != (UnityEngine.Object) null)
    {
      this.allItemsButton.interactable = inActivate;
      this.subscribedItemsButton.interactable = inActivate;
      this.publishedItemsButton.interactable = inActivate;
      this.activateStagingMod.interactable = inActivate;
    }
    this.tagsFilterDropdown.interactable = inActivate;
    this.reviewRatingDropdown.interactable = inActivate;
  }

  private void OnSubscribedItemsButton(bool inActive)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!inActive)
      return;
    this.SetupFilterType(UIWorkshopModsAndAssetsWidget.FilterType.SubscribedItems);
  }

  private void OnPublishedItemsButton(bool inActive)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!inActive)
      return;
    this.SetupFilterType(UIWorkshopModsAndAssetsWidget.FilterType.PublishedItems);
  }

  private void OnAllItemsButton(bool inActive)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!inActive)
      return;
    this.SetupFilterType(UIWorkshopModsAndAssetsWidget.FilterType.AllItems);
  }

  private void SetupFilterType(UIWorkshopModsAndAssetsWidget.FilterType inFilterType)
  {
    if (this.mCurrentFilter == inFilterType)
      return;
    this.mCurrentFilter = inFilterType;
    this.mCurrentPaging = 1U;
    StringVariableParser.ordinalNumberString = this.mCurrentPaging.ToString((IFormatProvider) Localisation.numberFormatter);
    this.pagingLabel.text = Localisation.LocaliseID("PSG_10012001", (GameObject) null);
    this.QuerySteamWorkshop();
  }

  private void OnLeftPagingButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mCurrentPaging > 0U)
    {
      --this.mCurrentPaging;
      this.QuerySteamWorkshop();
    }
    StringVariableParser.ordinalNumberString = this.mCurrentPaging.ToString((IFormatProvider) Localisation.numberFormatter);
    this.pagingLabel.text = Localisation.LocaliseID("PSG_10012001", (GameObject) null);
    this.leftPagingButton.interactable = this.mCurrentPaging > 0U;
    this.rightPagingButton.interactable = true;
  }

  private void OnRightPagingButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    uint totalResultsReturned = this.GetTotalResultsReturned();
    uint num = this.mCurrentPaging * 50U;
    if (num < totalResultsReturned)
    {
      ++this.mCurrentPaging;
      this.QuerySteamWorkshop();
    }
    StringVariableParser.ordinalNumberString = this.mCurrentPaging.ToString((IFormatProvider) Localisation.numberFormatter);
    this.pagingLabel.text = Localisation.LocaliseID("PSG_10012001", (GameObject) null);
    this.leftPagingButton.interactable = true;
    this.rightPagingButton.interactable = num < totalResultsReturned;
  }

  private void OnWorkshopButton()
  {
    App.instance.modManager.OpenWorkshopOnOverlay();
  }

  private void OnSelectingWorkshopItem(SteamMod inSteamModSelected)
  {
    for (int inIndex = 0; inIndex < this.modList.itemCount; ++inIndex)
    {
      UIWorkshopModListEntry workshopModListEntry = this.modList.GetItem<UIWorkshopModListEntry>(inIndex);
      if ((long) workshopModListEntry.steamMod.modID != (long) inSteamModSelected.modID)
        workshopModListEntry.selectItemToggle.isOn = false;
    }
  }

  private void OnActivateStagingMod(bool inActivate)
  {
    App.instance.modManager.modPublisher.stagingMod.isStaging = inActivate;
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  public void ActivateMod(bool inActivate, SteamMod inSteamMod)
  {
    SteamMod subscribedMod = App.instance.modManager.GetSubscribedMod(inSteamMod.modDetails.m_nPublishedFileId.m_PublishedFileId);
    if (subscribedMod != null)
      subscribedMod.SetActive(inActivate);
    if (!((UnityEngine.Object) this.itemDetailsWidget != (UnityEngine.Object) null) || (long) this.itemDetailsWidget.steamMod.modID != (long) inSteamMod.modID)
      return;
    this.itemDetailsWidget.UpdateButtonsState();
  }

  public void SelectMod(SteamMod inSteamMod)
  {
    if ((UnityEngine.Object) this.itemDetailsWidget != (UnityEngine.Object) null)
      this.itemDetailsWidget.SetupWorkshopItemDetails(inSteamMod);
    this.OnSelectingWorkshopItem(inSteamMod);
  }

  private void OnTagFilterDropdown(int inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mCurrentPaging = 1U;
    StringVariableParser.ordinalNumberString = this.mCurrentPaging.ToString((IFormatProvider) Localisation.numberFormatter);
    this.pagingLabel.text = Localisation.LocaliseID("PSG_10012001", (GameObject) null);
    this.QuerySteamWorkshop();
  }

  private void OnReviewRatingDropdown(int inRating)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    List<SteamMod> modList = this.GetModList();
    for (int inIndex = 0; inIndex < this.modList.itemCount; ++inIndex)
    {
      UIWorkshopModListEntry workshopModListEntry = this.modList.GetItem<UIWorkshopModListEntry>(inIndex);
      bool flag = modList.Contains(workshopModListEntry.steamMod);
      int stars = workshopModListEntry.steamMod.stars;
      workshopModListEntry.gameObject.SetActive(stars >= inRating && flag);
    }
  }

  private List<string> GetTagsFromDropdownMenu()
  {
    List<string> stringList = new List<string>();
    int index = this.tagsFilterDropdown.value - 1;
    if (index >= 0)
      stringList.Add(this.mFilterTags[index]);
    return stringList;
  }

  private void QuerySteamWorkshop()
  {
    this.modList.HideListItems();
    this.fetchErrorText.gameObject.SetActive(false);
    this.pleaseWaitIcon.SetActive(true);
    GameUtility.SetActive(this.noModsPublishedYet, false);
    this.MakeButtonsInteactible(false);
    if ((UnityEngine.Object) this.itemDetailsWidget != (UnityEngine.Object) null)
      this.itemDetailsWidget.MakeButtonsInteractable(false);
    List<string> fromDropdownMenu = this.GetTagsFromDropdownMenu();
    string[] excludeTags = (string[]) null;
    if (this.mCurrentView == UIWorkshopModsAndAssetsWidget.ViewType.Assets)
      excludeTags = new string[1]
      {
        StagingMod.newGameRequiredTag
      };
    else if (this.mCurrentView == UIWorkshopModsAndAssetsWidget.ViewType.Mods)
      fromDropdownMenu.Add(StagingMod.newGameRequiredTag);
    switch (this.mCurrentFilter)
    {
      case UIWorkshopModsAndAssetsWidget.FilterType.SubscribedItems:
        App.instance.modManager.modFetcher.QueryUserSubscribedMods(fromDropdownMenu.ToArray(), excludeTags, this.mCurrentPaging);
        break;
      case UIWorkshopModsAndAssetsWidget.FilterType.PublishedItems:
        App.instance.modManager.modFetcher.GetUserPublishedMods(fromDropdownMenu.ToArray(), excludeTags, this.mCurrentPaging);
        break;
      case UIWorkshopModsAndAssetsWidget.FilterType.AllItems:
        App.instance.modManager.modFetcher.GetAllWorkshopItems(fromDropdownMenu.ToArray(), excludeTags, this.mCurrentPaging);
        break;
    }
  }

  private uint GetTotalResultsReturned()
  {
    switch (this.mCurrentFilter)
    {
      case UIWorkshopModsAndAssetsWidget.FilterType.SubscribedItems:
        return App.instance.modManager.modFetcher.subscribedItemsCount;
      case UIWorkshopModsAndAssetsWidget.FilterType.PublishedItems:
        return App.instance.modManager.modFetcher.publishedItemsCount;
      case UIWorkshopModsAndAssetsWidget.FilterType.AllItems:
        return App.instance.modManager.modFetcher.allWorkshopItemsCount;
      default:
        return 0;
    }
  }

  private List<SteamMod> GetModList()
  {
    switch (this.mCurrentFilter)
    {
      case UIWorkshopModsAndAssetsWidget.FilterType.SubscribedItems:
        return App.instance.modManager.subscribedWorkshopItems;
      case UIWorkshopModsAndAssetsWidget.FilterType.PublishedItems:
        return App.instance.modManager.userPublishedMods;
      case UIWorkshopModsAndAssetsWidget.FilterType.AllItems:
        return App.instance.modManager.allWorkshopItems;
      default:
        return new List<SteamMod>();
    }
  }

  public enum FilterType
  {
    SubscribedItems,
    PublishedItems,
    AllItems,
  }

  public enum ViewType
  {
    Mods,
    Assets,
  }
}
