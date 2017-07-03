// Decompiled with JetBrains decompiler
// Type: LiveryOptionsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using Steamworks;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LiveryOptionsWidget : MonoBehaviour
{
  private static uint DlcAppId = DLCManager.GetDlcByName("Livery Pack").appId;
  private static int BaseDlcId = DLCManager.GetDlcByName("Base Game").dlcId;
  private static int DlcDlcId = DLCManager.GetDlcByName("Livery Pack").dlcId;
  private List<LiveryIcon> mLiveryIcons = new List<LiveryIcon>();
  private LiveryOptionsWidget.ColorType mCurrentColorType = LiveryOptionsWidget.ColorType.Count;
  private Color[] mSavedLiveryColors = new Color[6];
  public TextMeshProUGUI yearLabel;
  public GameObject togglesContainer;
  public Toggle toggleBase;
  public Toggle toggleDlc;
  public Toggle toggleWorkshop;
  public GameObject getMoreLiveriesContainer;
  public Button getMoreLiveriesButton;
  public ToggleGroup liveryToggleGroup;
  public UIGridList liveryGrid;
  public Button leftArrow;
  public Button rightArrow;
  public TextMeshProUGUI liveryStyleLabel;
  public Button autoGenerate;
  public Button randomizeColours;
  public Toggle primaryColourToggle;
  public Toggle secondaryColourToggle;
  public Toggle tertiaryColourToggle;
  public Toggle trimColourToggle;
  public Toggle sponsorLightToggle;
  public Toggle sponsorDarkToggle;
  public Image primaryColourToggleImage;
  public Image secondaryColourToggleImage;
  public Image tertiaryColourToggleImage;
  public Image trimColourToggleImage;
  public Image sponsorLightToggleImage;
  public Image sponsorDarkToggleImage;
  public Livery2D livery2D;
  private LiveryOptionsWidget.Mode mMode;
  private int mLiveryID;
  private int mCurrentLiveryIndex;
  private int mSavedLivery;
  private TeamColor mTeamColor;
  private bool mSetFrontendCar;
  private bool mRestrictColors;
  private Team mPlayerTeam;
  private bool mAllowMenuSounds;

  private void Awake()
  {
    this.autoGenerate.onClick.AddListener(new UnityAction(this.OnAutoGenerate));
    this.randomizeColours.onClick.AddListener((UnityAction) (() => this.OnAutoGenerateColours(true)));
    this.getMoreLiveriesButton.onClick.AddListener(new UnityAction(this.OnDlcButton));
    this.leftArrow.onClick.AddListener((UnityAction) (() => this.OnArrow(LiveryOptionsWidget.Direction.left)));
    this.rightArrow.onClick.AddListener((UnityAction) (() => this.OnArrow(LiveryOptionsWidget.Direction.right)));
    this.primaryColourToggle.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnColorPickerToggle(value, LiveryOptionsWidget.ColorType.Primary)));
    this.secondaryColourToggle.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnColorPickerToggle(value, LiveryOptionsWidget.ColorType.Secondary)));
    this.tertiaryColourToggle.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnColorPickerToggle(value, LiveryOptionsWidget.ColorType.Tertiary)));
    this.trimColourToggle.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnColorPickerToggle(value, LiveryOptionsWidget.ColorType.Trim)));
    this.sponsorLightToggle.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnColorPickerToggle(value, LiveryOptionsWidget.ColorType.SponsorLight)));
    this.sponsorDarkToggle.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnColorPickerToggle(value, LiveryOptionsWidget.ColorType.SponsorDark)));
  }

  public void Setup()
  {
    this.mAllowMenuSounds = false;
    App.instance.dlcManager.OnOwnedDlcChanged += new Action(this.UpdateWidget);
    this.mPlayerTeam = !Game.IsActive() ? CreateTeamManager.newTeam : Game.instance.player.team;
    this.mRestrictColors = Game.IsActive() && !this.mPlayerTeam.isCreatedByPlayer;
    this.mTeamColor = !Game.IsActive() ? CreateTeamManager.newTeamColor : this.mPlayerTeam.GetTeamColor();
    this.mLiveryID = this.mPlayerTeam.liveryID;
    this.SetupCarPreSeason();
    this.SaveLivery();
    this.SetToggles();
    this.SetLiveriesGrid();
    this.Refresh();
    if ((UnityEngine.Object) this.yearLabel != (UnityEngine.Object) null)
      this.yearLabel.text = Game.IsActive() ? this.mPlayerTeam.championship.GetFirstEventDetails().eventDate.Year.ToString() : Game.instance.time.now.Year.ToString();
    this.UpdatePickerToggleColors();
    this.mAllowMenuSounds = true;
  }

  public void OnExit()
  {
    App.instance.dlcManager.OnOwnedDlcChanged -= new Action(this.UpdateWidget);
  }

  private void UpdateWidget()
  {
    this.SetToggles();
    this.Refresh();
  }

  private void Refresh()
  {
    this.UpdateLiveryCarVisuals();
    this.SetLiveryLabel();
    this.UpdateArrowButtonsState();
  }

  private void SetToggles()
  {
    bool inIsActive1 = this.mPlayerTeam.championship.series == Championship.Series.SingleSeaterSeries && App.instance.dlcManager.IsDlcKnown(LiveryOptionsWidget.DlcAppId) && App.instance.dlcManager.IsDlcInstalled(LiveryOptionsWidget.DlcAppId);
    bool inIsActive2 = Game.instance.liveryManager.HasCustomLiveries(this.mPlayerTeam.championship.championshipID);
    GameUtility.SetActive(this.togglesContainer, inIsActive1 || inIsActive2);
    GameUtility.SetActive(this.getMoreLiveriesContainer, !inIsActive1 && this.mPlayerTeam.championship.series == Championship.Series.SingleSeaterSeries);
    if (!this.togglesContainer.activeSelf)
      return;
    this.toggleBase.onValueChanged.RemoveAllListeners();
    this.toggleDlc.onValueChanged.RemoveAllListeners();
    this.toggleWorkshop.onValueChanged.RemoveAllListeners();
    GameUtility.SetActive(this.toggleBase.gameObject, true);
    GameUtility.SetActive(this.toggleDlc.gameObject, inIsActive1);
    GameUtility.SetActive(this.toggleWorkshop.gameObject, inIsActive2);
    this.toggleBase.isOn = this.mMode == LiveryOptionsWidget.Mode.Base;
    this.toggleDlc.isOn = this.mMode == LiveryOptionsWidget.Mode.Dlc;
    this.toggleWorkshop.isOn = this.mMode == LiveryOptionsWidget.Mode.Workshop;
    this.toggleBase.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle(this.toggleBase, LiveryOptionsWidget.Mode.Base)));
    this.toggleDlc.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle(this.toggleDlc, LiveryOptionsWidget.Mode.Dlc)));
    this.toggleWorkshop.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle(this.toggleWorkshop, LiveryOptionsWidget.Mode.Workshop)));
  }

  private void SetLiveriesGrid()
  {
    this.liveryToggleGroup.SetAllTogglesOff();
    this.liveryGrid.DestroyListItems();
    GameUtility.SetActive(this.liveryGrid.itemPrefab, true);
    this.mLiveryIcons.Clear();
    int num1 = 0;
    int num2 = 0;
    foreach (LiveryData livery in Game.instance.liveryManager.liveries)
    {
      if (livery.IsValidForChampionship(this.mPlayerTeam.championship))
      {
        bool flag = Game.instance.liveryManager.IsLiveryCustom(livery.id);
        switch (this.mMode)
        {
          case LiveryOptionsWidget.Mode.Base:
            if (livery.dlcId == LiveryOptionsWidget.DlcDlcId || flag)
              continue;
            break;
          case LiveryOptionsWidget.Mode.Dlc:
            if (livery.dlcId == LiveryOptionsWidget.BaseDlcId || flag)
              continue;
            break;
          case LiveryOptionsWidget.Mode.Workshop:
            if (flag)
              break;
            continue;
        }
        LiveryIcon liveryIcon = this.liveryGrid.GetOrCreateItem<LiveryIcon>(num1);
        if (flag)
          livery.friendlyNameInt = ++num2;
        liveryIcon.Setup(Game.instance.liveryManager.GetLiveryIndex(livery.id), num1, livery.friendlyNameInt, this.mPlayerTeam.liveryID == livery.id);
        ++num1;
        if (liveryIcon.toggle.isOn)
          this.mCurrentLiveryIndex = num1;
        this.mLiveryIcons.Add(liveryIcon);
      }
    }
    GameUtility.SetActive(this.liveryGrid.itemPrefab, false);
    if (this.mLiveryIcons.Count == 0)
      Debug.LogError((object) "Could not find ANY liveries for the player to choose from on the livery screen!", (UnityEngine.Object) null);
    if (!this.gameObject.activeSelf || this.liveryToggleGroup.AnyTogglesOn() || this.mLiveryIcons.Count <= 0)
      return;
    this.mCurrentLiveryIndex = 0;
    this.mLiveryIcons[0].toggle.isOn = true;
  }

  public void SetLivery(int inLiveryIndex, int inLiveryCurrentIndex)
  {
    this.mLiveryID = Game.instance.liveryManager.GetLiveryByIndex(inLiveryIndex).id;
    this.mCurrentLiveryIndex = inLiveryCurrentIndex;
    this.Refresh();
    if (!this.mAllowMenuSounds)
      return;
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  private void SetLiveryLabel()
  {
    StringVariableParser.intValue1 = Game.instance.liveryManager.GetFriendlyNameIntForLivery(Game.instance.liveryManager.GetLiveryIndex(this.mLiveryID));
    this.liveryStyleLabel.text = Localisation.LocaliseID("PSG_10010944", (GameObject) null);
  }

  private void UpdateLiveryCarVisuals()
  {
    this.mPlayerTeam.liveryID = this.mLiveryID;
    LiveryData livery = Game.instance.liveryManager.GetLivery(this.mLiveryID);
    FrontendCar frontendCar = this.mPlayerTeam.carManager.frontendCar;
    if (frontendCar != null)
    {
      frontendCar.SetColours(this.mTeamColor.livery);
      frontendCar.SetLiveryData(livery);
    }
    if (this.mSetFrontendCar)
    {
      FrontendCar nextFrontendCar = this.mPlayerTeam.carManager.nextFrontendCar;
      if (nextFrontendCar != null)
      {
        nextFrontendCar.SetColours(this.mTeamColor.livery);
        nextFrontendCar.SetLiveryData(livery);
      }
    }
    if ((UnityEngine.Object) this.livery2D != (UnityEngine.Object) null && this.livery2D.gameObject.activeSelf)
      this.livery2D.SetLivery(livery, this.mTeamColor, this.mPlayerTeam.championship);
    this.UpdatePickerToggleColors();
  }

  private void SetupCarPreSeason()
  {
    this.mSetFrontendCar = false;
    if (!(App.instance.gameStateManager.currentState is PreSeasonState) || ((PreSeasonState) App.instance.gameStateManager.currentState).stage > PreSeasonState.PreSeasonStage.ChoosingLivery)
      return;
    FrontendCar nextFrontendCar = this.mPlayerTeam.carManager.nextFrontendCar;
    if (nextFrontendCar == null)
      return;
    this.mSetFrontendCar = true;
    if (Game.instance.liveryManager.GetLivery(this.mLiveryID).IsValidForChampionship(this.mPlayerTeam.championship))
    {
      nextFrontendCar.SetColours(this.mTeamColor.livery);
      nextFrontendCar.SetLiveryData(Game.instance.liveryManager.GetLivery(this.mLiveryID));
    }
    else
    {
      this.mTeamColor.livery.primary = Color.white;
      this.mTeamColor.livery.secondary = Color.white;
      this.mTeamColor.livery.tertiary = Color.white;
      this.mTeamColor.livery.trim = Color.white;
      this.mTeamColor.livery.lightSponsor = Color.white;
      this.mTeamColor.livery.darkSponsor = Color.white;
      this.mCurrentLiveryIndex = 0;
    }
    this.mPlayerTeam.sponsorController.RefreshSponsorsOnFrontendCar(nextFrontendCar);
  }

  private void SaveLivery()
  {
    this.mSavedLiveryColors[0] = this.mTeamColor.livery.primary;
    this.mSavedLiveryColors[1] = this.mTeamColor.livery.secondary;
    this.mSavedLiveryColors[2] = this.mTeamColor.livery.tertiary;
    this.mSavedLiveryColors[3] = this.mTeamColor.livery.trim;
    this.mSavedLiveryColors[4] = this.mTeamColor.livery.lightSponsor;
    this.mSavedLiveryColors[5] = this.mTeamColor.livery.darkSponsor;
    this.mSavedLivery = this.mLiveryID;
  }

  public void RevertChanges()
  {
    this.mPlayerTeam.liveryID = this.mSavedLivery;
    LiveryData livery = Game.instance.liveryManager.GetLivery(this.mSavedLivery);
    this.mTeamColor.livery.primary = this.mSavedLiveryColors[0];
    this.mTeamColor.livery.secondary = this.mSavedLiveryColors[1];
    this.mTeamColor.livery.tertiary = this.mSavedLiveryColors[2];
    this.mTeamColor.livery.trim = this.mSavedLiveryColors[3];
    this.mTeamColor.livery.lightSponsor = this.mSavedLiveryColors[4];
    this.mTeamColor.livery.darkSponsor = this.mSavedLiveryColors[5];
    FrontendCar frontendCar = this.mPlayerTeam.carManager.frontendCar;
    if (frontendCar != null)
    {
      frontendCar.SetColours(this.mTeamColor.livery);
      frontendCar.SetLiveryData(livery);
    }
    if (!this.mSetFrontendCar)
      return;
    FrontendCar nextFrontendCar = this.mPlayerTeam.carManager.nextFrontendCar;
    if (nextFrontendCar == null)
      return;
    nextFrontendCar.SetColours(this.mTeamColor.livery);
    nextFrontendCar.SetLiveryData(livery);
  }

  public bool hasLiveryChanged()
  {
    return this.mPlayerTeam.liveryID != this.mSavedLivery || this.mTeamColor.livery.primary != this.mSavedLiveryColors[0] || (this.mTeamColor.livery.secondary != this.mSavedLiveryColors[1] || this.mTeamColor.livery.tertiary != this.mSavedLiveryColors[2]) || (this.mTeamColor.livery.trim != this.mSavedLiveryColors[3] || this.mTeamColor.livery.lightSponsor != this.mSavedLiveryColors[4] || this.mTeamColor.livery.darkSponsor != this.mSavedLiveryColors[5]);
  }

  private Color GetColor(LiveryOptionsWidget.ColorType inColorType)
  {
    Color color = new Color();
    switch (inColorType)
    {
      case LiveryOptionsWidget.ColorType.Primary:
        color = this.mTeamColor.livery.primary;
        break;
      case LiveryOptionsWidget.ColorType.Secondary:
        color = this.mTeamColor.livery.secondary;
        break;
      case LiveryOptionsWidget.ColorType.Tertiary:
        color = this.mTeamColor.livery.tertiary;
        break;
      case LiveryOptionsWidget.ColorType.Trim:
        color = this.mTeamColor.livery.trim;
        break;
      case LiveryOptionsWidget.ColorType.SponsorLight:
        color = this.mTeamColor.livery.lightSponsor;
        break;
      case LiveryOptionsWidget.ColorType.SponsorDark:
        color = this.mTeamColor.livery.darkSponsor;
        break;
    }
    return color;
  }

  public int FindColorID(LiveryOptionsWidget.ColorType inColorType)
  {
    Color color = new Color();
    Color[] colorArray = new Color[0];
    switch (inColorType)
    {
      case LiveryOptionsWidget.ColorType.Primary:
        color = this.mTeamColor.livery.primary;
        break;
      case LiveryOptionsWidget.ColorType.Secondary:
        color = this.mTeamColor.livery.secondary;
        break;
      case LiveryOptionsWidget.ColorType.Tertiary:
        color = this.mTeamColor.livery.tertiary;
        break;
      case LiveryOptionsWidget.ColorType.Trim:
        color = this.mTeamColor.livery.trim;
        break;
      case LiveryOptionsWidget.ColorType.SponsorLight:
        color = this.mTeamColor.livery.lightSponsor;
        break;
      case LiveryOptionsWidget.ColorType.SponsorDark:
        color = this.mTeamColor.livery.darkSponsor;
        break;
    }
    for (int index = 0; index < colorArray.Length; ++index)
    {
      if (this.mTeamColor.liveryEditorOptions[index] == color)
        return index;
    }
    return -1;
  }

  private void OnColorPickerToggle(bool inIsOn, LiveryOptionsWidget.ColorType inColorType)
  {
    if (inIsOn)
    {
      RectTransform component;
      Color inCurrentColor;
      Color[] inDefaultColors;
      switch (inColorType)
      {
        case LiveryOptionsWidget.ColorType.Secondary:
          component = this.secondaryColourToggle.GetComponent<RectTransform>();
          inCurrentColor = this.mTeamColor.livery.secondary;
          inDefaultColors = this.mTeamColor.liveryEditorOptions;
          break;
        case LiveryOptionsWidget.ColorType.Tertiary:
          component = this.tertiaryColourToggle.GetComponent<RectTransform>();
          inCurrentColor = this.mTeamColor.livery.tertiary;
          inDefaultColors = this.mTeamColor.liveryEditorOptions;
          break;
        case LiveryOptionsWidget.ColorType.Trim:
          component = this.trimColourToggle.GetComponent<RectTransform>();
          inCurrentColor = this.mTeamColor.livery.trim;
          inDefaultColors = this.mTeamColor.liveryEditorOptions;
          break;
        case LiveryOptionsWidget.ColorType.SponsorLight:
          component = this.sponsorLightToggle.GetComponent<RectTransform>();
          inCurrentColor = this.mTeamColor.livery.lightSponsor;
          inDefaultColors = this.mTeamColor.lighSponsorOptions;
          break;
        case LiveryOptionsWidget.ColorType.SponsorDark:
          component = this.sponsorDarkToggle.GetComponent<RectTransform>();
          inCurrentColor = this.mTeamColor.livery.darkSponsor;
          inDefaultColors = this.mTeamColor.darkSponsorOptions;
          break;
        default:
          component = this.primaryColourToggle.GetComponent<RectTransform>();
          inCurrentColor = this.mTeamColor.livery.primary;
          inDefaultColors = this.mTeamColor.liveryEditorOptions;
          break;
      }
      this.mCurrentColorType = inColorType;
      ColorPickerDialogBox.Open(component, inCurrentColor, inDefaultColors, this.mRestrictColors);
      ColorPickerDialogBox.OnColorPicked += new Action<Color>(this.OnColorPicked);
      ColorPickerDialogBox.OnClose += new Action(this.OnColorPickerClose);
    }
    else
      ColorPickerDialogBox.Close();
  }

  private void OnColorPicked(Color inColor)
  {
    this.OnColorToggle(true, inColor, this.mCurrentColorType);
  }

  private void UpdatePickerToggleColors()
  {
    TeamColor.LiveryColour livery = this.mTeamColor.livery;
    if (!((UnityEngine.Object) this.primaryColourToggleImage != (UnityEngine.Object) null))
      return;
    this.primaryColourToggleImage.color = livery.primary;
    this.secondaryColourToggleImage.color = livery.secondary;
    this.tertiaryColourToggleImage.color = livery.tertiary;
    this.trimColourToggleImage.color = livery.trim;
    this.sponsorLightToggleImage.color = livery.lightSponsor;
    this.sponsorDarkToggleImage.color = livery.darkSponsor;
  }

  private void OnColorPickerClose()
  {
    switch (this.mCurrentColorType)
    {
      case LiveryOptionsWidget.ColorType.Primary:
        this.primaryColourToggle.isOn = false;
        break;
      case LiveryOptionsWidget.ColorType.Secondary:
        this.secondaryColourToggle.isOn = false;
        break;
      case LiveryOptionsWidget.ColorType.Tertiary:
        this.tertiaryColourToggle.isOn = false;
        break;
      case LiveryOptionsWidget.ColorType.Trim:
        this.trimColourToggle.isOn = false;
        break;
      case LiveryOptionsWidget.ColorType.SponsorLight:
        this.sponsorLightToggle.isOn = false;
        break;
      case LiveryOptionsWidget.ColorType.SponsorDark:
        this.sponsorDarkToggle.isOn = false;
        break;
    }
  }

  private void OnColorToggle(bool inValue, Color inColor, LiveryOptionsWidget.ColorType inColorType)
  {
    if (!inValue)
      return;
    switch (inColorType)
    {
      case LiveryOptionsWidget.ColorType.Primary:
        this.mTeamColor.livery.primary = inColor;
        break;
      case LiveryOptionsWidget.ColorType.Secondary:
        this.mTeamColor.livery.secondary = inColor;
        break;
      case LiveryOptionsWidget.ColorType.Tertiary:
        this.mTeamColor.livery.tertiary = inColor;
        break;
      case LiveryOptionsWidget.ColorType.Trim:
        this.mTeamColor.livery.trim = inColor;
        break;
      case LiveryOptionsWidget.ColorType.SponsorLight:
        this.mTeamColor.livery.lightSponsor = inColor;
        break;
      case LiveryOptionsWidget.ColorType.SponsorDark:
        this.mTeamColor.livery.darkSponsor = inColor;
        break;
    }
    this.UpdateLiveryCarVisuals();
  }

  private void OnToggle(Toggle inToggle, LiveryOptionsWidget.Mode inMode)
  {
    if (!inToggle.isOn)
      return;
    this.mMode = inMode;
    this.SetToggles();
    this.SetLiveriesGrid();
    this.Refresh();
    if (!this.mAllowMenuSounds)
      return;
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  public void OnDlcButton()
  {
    if (this.mAllowMenuSounds)
      scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    SteamFriends.ActivateGameOverlayToStore(new AppId_t(DLCManager.GetDlcByName("Livery Pack").appId), EOverlayToStoreFlag.k_EOverlayToStoreFlag_None);
  }

  private void OnArrow(LiveryOptionsWidget.Direction inDirection)
  {
    this.liveryToggleGroup.SetAllTogglesOff();
    switch (inDirection)
    {
      case LiveryOptionsWidget.Direction.left:
        --this.mCurrentLiveryIndex;
        break;
      case LiveryOptionsWidget.Direction.right:
        ++this.mCurrentLiveryIndex;
        break;
    }
    this.mLiveryIcons[this.mCurrentLiveryIndex].toggle.isOn = true;
    this.Refresh();
    if (!this.mAllowMenuSounds)
      return;
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  private void UpdateArrowButtonsState()
  {
    this.leftArrow.interactable = this.mCurrentLiveryIndex - 1 >= 0;
    this.rightArrow.interactable = this.mCurrentLiveryIndex + 1 < this.mLiveryIcons.Count;
  }

  private void OnAutoGenerateColours(bool inRefresh)
  {
    int length = ColorPreferences.customColors.Length;
    if (Game.IsActive() && !this.mPlayerTeam.isCreatedByPlayer)
    {
      Color[] liveryEditorOptions = this.mTeamColor.liveryEditorOptions;
      Color[] lighSponsorOptions = this.mTeamColor.lighSponsorOptions;
      Color[] darkSponsorOptions = this.mTeamColor.darkSponsorOptions;
      this.mTeamColor.livery.primary = liveryEditorOptions[RandomUtility.GetRandom(0, liveryEditorOptions.Length)];
      this.mTeamColor.livery.secondary = liveryEditorOptions[RandomUtility.GetRandom(0, liveryEditorOptions.Length)];
      this.mTeamColor.livery.tertiary = liveryEditorOptions[RandomUtility.GetRandom(0, liveryEditorOptions.Length)];
      this.mTeamColor.livery.trim = liveryEditorOptions[RandomUtility.GetRandom(0, liveryEditorOptions.Length)];
      this.mTeamColor.livery.darkSponsor = darkSponsorOptions[RandomUtility.GetRandom(0, darkSponsorOptions.Length)];
      this.mTeamColor.livery.lightSponsor = lighSponsorOptions[RandomUtility.GetRandom(0, lighSponsorOptions.Length)];
    }
    else if ((!Game.IsActive() || this.mPlayerTeam.isCreatedByPlayer) && length >= 4)
    {
      this.mTeamColor.livery.primary = ColorPreferences.customColors[RandomUtility.GetRandom(0, length)];
      this.mTeamColor.livery.secondary = ColorPreferences.customColors[RandomUtility.GetRandom(0, length)];
      this.mTeamColor.livery.tertiary = ColorPreferences.customColors[RandomUtility.GetRandom(0, length)];
      this.mTeamColor.livery.trim = ColorPreferences.customColors[RandomUtility.GetRandom(0, length)];
      this.mTeamColor.livery.darkSponsor = ColorPreferences.customColors[RandomUtility.GetRandom(0, length)];
      this.mTeamColor.livery.lightSponsor = ColorPreferences.customColors[RandomUtility.GetRandom(0, length)];
    }
    if (!inRefresh)
      return;
    this.Refresh();
  }

  private void OnAutoGenerate()
  {
    this.OnAutoGenerateColours(false);
    this.liveryToggleGroup.SetAllTogglesOff();
    this.mCurrentLiveryIndex = RandomUtility.GetRandom(0, this.liveryGrid.itemCount);
    this.mLiveryIcons[this.mCurrentLiveryIndex].toggle.isOn = true;
    this.Refresh();
  }

  public enum ColorType
  {
    Primary,
    Secondary,
    Tertiary,
    Trim,
    SponsorLight,
    SponsorDark,
    Count,
  }

  private enum Direction
  {
    left,
    right,
  }

  private enum Mode
  {
    Base,
    Dlc,
    Workshop,
  }
}
