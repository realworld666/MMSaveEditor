// Decompiled with JetBrains decompiler
// Type: CreatePlayerScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CreatePlayerScreen : UIScreen
{
  private Portrait mPortrait = new Portrait();
  private List<GameObject> mTabSteps = new List<GameObject>();
  public UICreatePlayerDetailsWidget detailsWidget;
  public UICreatePlayerProfileWidget profileWidget;
  public UICreatePlayerAppearanceWidget appearanceWidget;
  public TMP_InputField firstNameInputField;
  public TMP_InputField lastNameInputField;
  public TextMeshProUGUI playerNameLabel;
  public GameObject playerNameErrorMessage;
  public GameObject backstoryContainer;
  private Person.Gender mGender;
  private bool mShouldCapitaliseFirstName;
  private bool mShouldCapitaliseLastName;

  public Portrait playerPortrait
  {
    get
    {
      return this.mPortrait;
    }
  }

  public Person.Gender playerGender
  {
    get
    {
      return this.mGender;
    }
    set
    {
      this.mGender = value;
    }
  }

  public override void OnStart()
  {
    base.OnStart();
    this.profileWidget.OnStart();
    this.appearanceWidget.OnStart();
    this.AddInputListeners();
    this.CreateTabStepsList();
  }

  public override void OnEnter()
  {
    if (!Game.IsActive())
    {
      this.screenName = "PSG_10007817";
      this.screenSubtitle = "PSG_10004547";
    }
    else
    {
      this.screenName = "PSG_10007974";
      this.screenSubtitle = "PSG_10004547";
    }
    base.OnEnter();
    this.showNavigationBars = true;
    this.mPortrait.Copy(Game.instance.player.portrait);
    if (!Game.IsActive() && !Game.instance.player.hasSetPortrait || Game.IsActive())
      this.mGender = Game.instance.player.gender;
    this.mShouldCapitaliseFirstName = this.firstNameInputField.text.Length == 0 || this.firstNameInputField.text.Length > 1;
    this.mShouldCapitaliseLastName = this.lastNameInputField.text.Length == 0 || this.lastNameInputField.text.Length > 1;
    if (!Game.IsActive())
    {
      this.SetTopBarMode(UITopBar.Mode.Core);
      this.SetBottomBarMode(UIBottomBar.Mode.Core);
      GameUtility.SetActive(this.detailsWidget.gameObject, true);
      GameUtility.SetActive(this.playerNameErrorMessage, true);
      GameUtility.SetActive(this.backstoryContainer, true);
      this.RemoveInputListeners();
      this.firstNameInputField.text = Game.instance.player.firstName;
      this.lastNameInputField.text = Game.instance.player.lastName;
      this.AddInputListeners();
      if (!Game.instance.player.hasSetPortrait)
      {
        this.detailsWidget.Setup();
        this.appearanceWidget.Setup();
        this.profileWidget.Setup();
        this.RefreshPortrait();
      }
      this.OnPlayerNameEdited();
    }
    else
    {
      this.SetTopBarMode(UITopBar.Mode.Core);
      this.SetBottomBarMode(UIBottomBar.Mode.PlayerAction);
      UIManager.instance.navigationBars.bottomBar.MarkContinueButtonForUpdate();
      this.continueButtonLabel = Localisation.LocaliseID("PSG_10011044", (GameObject) null);
      GameUtility.SetActive(this.detailsWidget.gameObject, false);
      GameUtility.SetActive(this.playerNameErrorMessage, false);
      GameUtility.SetActive(this.backstoryContainer, false);
      this.appearanceWidget.Setup();
      this.profileWidget.Setup();
      this.RefreshPortrait();
    }
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionDrivers, 0.0f);
  }

  public void Update()
  {
    if (!Input.GetKeyDown(KeyCode.Tab))
      return;
    this.GoToNextTabStep();
  }

  public override void OnExit()
  {
    base.OnExit();
    Game.instance.player.hasSetPortrait = true;
  }

  private void AddInputListeners()
  {
    this.firstNameInputField.onValueChanged.AddListener((UnityAction<string>) (param0 => this.OnPlayerNameEdited()));
    this.lastNameInputField.onValueChanged.AddListener((UnityAction<string>) (param0 => this.OnPlayerNameEdited()));
  }

  private void RemoveInputListeners()
  {
    this.firstNameInputField.onValueChanged.RemoveAllListeners();
    this.lastNameInputField.onValueChanged.RemoveAllListeners();
  }

  private void OnPlayerNameEdited()
  {
    string str1 = this.PrepareNameInputString(this.firstNameInputField.text, Game.instance.player.firstName, ref this.mShouldCapitaliseFirstName);
    string str2 = this.PrepareNameInputString(this.lastNameInputField.text, Game.instance.player.lastName, ref this.mShouldCapitaliseLastName);
    Game.instance.player.SetName(str1.Trim(), str2.Trim());
    this.playerNameLabel.text = Game.instance.player.name;
    bool flag = this.IsPlayerNameValid();
    this.continueButtonInteractable = flag;
    GameUtility.SetActive(this.playerNameErrorMessage, !flag);
    if (str1 != this.firstNameInputField.text)
      this.firstNameInputField.text = str1;
    if (!(str2 != this.lastNameInputField.text))
      return;
    this.lastNameInputField.text = str2;
  }

  private string PrepareNameInputString(string inName, string inPreviousName, ref bool inShouldCapitalise)
  {
    if (!string.IsNullOrEmpty(inName) && inName.Length == 1)
      inName = inName.Trim();
    if (string.IsNullOrEmpty(inName))
      return string.Empty;
    if (inPreviousName.Length != 0 || inName.Length != 1 || !inShouldCapitalise)
      return inName;
    inShouldCapitalise = false;
    return char.ToUpper(inName[0]).ToString();
  }

  private void ClosePopup()
  {
    UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>().Hide();
  }

  public void AutoGeneratePortrait()
  {
    this.mPortrait.GenerateRandomPortrait(this.mGender, false);
    this.RefreshPortrait();
  }

  private void RefreshPortrait()
  {
    this.profileWidget.RefreshPortrait();
    this.appearanceWidget.SetSkinTones(false);
    this.appearanceWidget.SetHairStyles();
    this.appearanceWidget.SetHairColours(false);
    this.appearanceWidget.SetFacialHair();
    this.appearanceWidget.SetGlasses();
    this.appearanceWidget.SetAccessories();
  }

  public bool IsPlayerNameValid()
  {
    return !string.IsNullOrEmpty(this.firstNameInputField.text.Replace(" ", string.Empty)) && !string.IsNullOrEmpty(this.lastNameInputField.text.Replace(" ", string.Empty));
  }

  public void SavePlayer()
  {
    Game.instance.player.portrait.Copy(this.mPortrait);
    Game.instance.player.gender = this.mGender;
  }

  private void CreateTabStepsList()
  {
    this.mTabSteps.Clear();
    this.mTabSteps.Add(this.firstNameInputField.gameObject);
    this.mTabSteps.Add(this.lastNameInputField.gameObject);
  }

  private void GoToNextTabStep()
  {
    for (int index = 0; index < this.mTabSteps.Count; ++index)
    {
      if ((UnityEngine.Object) EventSystem.current.currentSelectedGameObject == (UnityEngine.Object) this.mTabSteps[index])
      {
        if (index < this.mTabSteps.Count - 1)
        {
          int num;
          EventSystem.current.SetSelectedGameObject(this.mTabSteps[num = index + 1]);
          break;
        }
        EventSystem.current.SetSelectedGameObject(this.mTabSteps[0]);
        break;
      }
    }
  }

  public override UIScreen.NavigationButtonEvent OnCancelButton()
  {
    UIManager.instance.ChangeScreen("PlayerScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }

  public override UIScreen.NavigationButtonEvent OnBackButton()
  {
    if (!Game.IsActive())
      this.SavePlayer();
    return UIScreen.NavigationButtonEvent.LetGameStateHandle;
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    this.SavePlayer();
    if (!Game.IsActive())
    {
      if (!Game.instance.challengeManager.IsAttemptingChallenge())
        UIManager.instance.ChangeScreen("ChooseOrCreateTeamScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
      else
        UIManager.instance.ChangeScreen("ChooseSeriesScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    }
    else
      UIManager.instance.ChangeScreen("PlayerScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }
}
