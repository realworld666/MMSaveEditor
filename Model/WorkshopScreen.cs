// Decompiled with JetBrains decompiler
// Type: WorkshopScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using MM2;
using ModdingSystem;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WorkshopScreen : UIScreen
{
  private const string GAME_VERSION_FLAG = "GameVersionFlag";
  public Toggle modsToggle;
  public Toggle assetsToggle;
  public Toggle saveGamesToggle;
  public Toggle myWorkshopToggle;
  public UIWorkshopModsAndAssetsWidget assetsAndMods;
  public UIWorkshopItemDetailsWidget itemDetails;
  public UIWorkshopMyWorkshopWidget myWorkshop;

  private void Start()
  {
    this.assetsAndMods.gameObject.SetActive(true);
    this.myWorkshop.gameObject.SetActive(false);
    this.modsToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnModsToggle));
    this.assetsToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnAssetsToggle));
    this.saveGamesToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnSaveGamesToggle));
    this.myWorkshopToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnMyWorkshopToggle));
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.myWorkshop.OnUploadStart += new Action(this.OnUploadStarted);
    this.myWorkshop.OnUploadFinished += new Action(this.OnUploadFinished);
    this.showNavigationBars = true;
    this.SetTopBarMode(UITopBar.Mode.Core);
    this.SetBottomBarMode(UIBottomBar.Mode.Core);
    UIManager.instance.ClearForwardStack();
    this.hideMMDropdownButtons = true;
    this.modsToggle.isOn = true;
    this.assetsAndMods.SetupCurrentView(UIWorkshopModsAndAssetsWidget.ViewType.Mods);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionFactory, 0.0f);
    App.instance.modManager.modLoader.activeModsController.RecordActiveMods();
  }

  public override void OnExit()
  {
    base.OnExit();
    this.myWorkshop.OnUploadStart -= new Action(this.OnUploadStarted);
    this.myWorkshop.OnUploadFinished -= new Action(this.OnUploadFinished);
    UIManager.instance.navigationBars.SetContinueActive(true);
    ModManager modManager = App.instance.modManager;
    modManager.SaveActiveModsFile();
    modManager.ClearQueriedItemsLists();
    SteamMod.UnloadDownloadedTextures();
  }

  public override UIScreen.NavigationButtonEvent OnBackButton()
  {
    if (!this.TryWorkshopAssetLoading())
      return base.OnBackButton();
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    if (!this.TryWorkshopAssetLoading())
      UIManager.instance.ChangeScreen("TitleScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }

  private bool TryWorkshopAssetLoading()
  {
    ModManager modManager = App.instance.modManager;
    bool flag = false;
    if (modManager.modLoader.activeModsController.ActiveModsHaveChangedSinceLastRecording())
    {
      UIManager.instance.dialogBoxManager.GetDialog<WorkshopAssetLoading>().SetLoadingCompleteAction(WorkshopAssetLoading.LoadingCompleteAction.ExitingWorkshopScreen, (SaveFileInfo) null);
      UIManager.instance.dialogBoxManager.Show("WorkshopAssetLoading");
      flag = true;
    }
    return flag;
  }

  private void OnModsToggle(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (inValue)
      this.assetsAndMods.SetupCurrentView(UIWorkshopModsAndAssetsWidget.ViewType.Mods);
    GameUtility.SetActive(this.assetsAndMods.gameObject, inValue);
    GameUtility.SetActive(this.itemDetails.gameObject, inValue);
  }

  private void OnAssetsToggle(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (inValue)
      this.assetsAndMods.SetupCurrentView(UIWorkshopModsAndAssetsWidget.ViewType.Assets);
    GameUtility.SetActive(this.assetsAndMods.gameObject, inValue);
    GameUtility.SetActive(this.itemDetails.gameObject, inValue);
  }

  private void OnSaveGamesToggle(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  private void OnMyWorkshopToggle(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (inValue)
      this.CheckNewGameVersion();
    GameUtility.SetActive(this.myWorkshop.gameObject, inValue);
  }

  private void OnUploadStarted()
  {
    this.ActivateButtons(false);
  }

  private void OnUploadFinished()
  {
    this.ActivateButtons(true);
  }

  private void ActivateButtons(bool inActivate)
  {
    this.modsToggle.interactable = inActivate;
    this.assetsToggle.interactable = inActivate;
    this.saveGamesToggle.interactable = inActivate;
    this.myWorkshopToggle.interactable = inActivate;
  }

  private void OnApplicationFocus(bool inHasFocus)
  {
    if (!inHasFocus)
      return;
    this.myWorkshop.OnRefreshModContent();
  }

  private void CheckNewGameVersion()
  {
    string fullVersionString = PlayerPrefs.GetString("GameVersionFlag", string.Empty);
    if (!string.IsNullOrEmpty(fullVersionString) && fullVersionString != GameVersionNumber.version.fullVersionString)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      WorkshopScreen.\u003CCheckNewGameVersion\u003Ec__AnonStorey70 versionCAnonStorey70 = new WorkshopScreen.\u003CCheckNewGameVersion\u003Ec__AnonStorey70();
      fullVersionString = GameVersionNumber.version.fullVersionString;
      string empty = string.Empty;
      using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
      {
        StringBuilder stringBuilder = builderSafe.stringBuilder;
        stringBuilder.AppendLine(Localisation.LocaliseID("PSG_10012150", (GameObject) null));
        stringBuilder.AppendLine();
        stringBuilder.AppendLine(Localisation.LocaliseID("PSG_10012151", (GameObject) null));
        empty = stringBuilder.ToString();
      }
      string inTitle = Localisation.LocaliseID("PSG_10012149", (GameObject) null);
      string inConfirmString = Localisation.LocaliseID("PSG_10003119", (GameObject) null);
      // ISSUE: reference to a compiler-generated field
      versionCAnonStorey70.confirmation = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      versionCAnonStorey70.confirmation.Show((Action) null, string.Empty, new Action(versionCAnonStorey70.\u003C\u003Em__ED), inConfirmString, empty, inTitle);
    }
    PlayerPrefs.SetString("GameVersionFlag", fullVersionString);
  }
}
