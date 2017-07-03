// Decompiled with JetBrains decompiler
// Type: CreateTeamOptionNameOrigin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CreateTeamOptionNameOrigin : CreateTeamOption
{
  public TMP_InputField mainTextInput;
  public TMP_InputField subTextInput;
  public Dropdown continentDropdown;
  public UIGridList nationalityGrid;
  public ScrollRect nationalityScrollRect;
  public ToggleGroup nationalityToggleGroup;
  public CreateTeamScreen screen;
  private bool mAllowMenuSounds;

  public override bool isReady
  {
    get
    {
      return this.IsNameValid();
    }
  }

  public override void OnStart()
  {
  }

  private void OnEnable()
  {
    this.SetCameraPanControl(false, false);
  }

  private void OnDisable()
  {
    this.SetCameraPanControl(true, false);
  }

  private void Update()
  {
    this.SetCameraPanControl(false, false);
  }

  public override void Setup()
  {
    this.mAllowMenuSounds = false;
    this.continentDropdown.onValueChanged.RemoveAllListeners();
    this.mainTextInput.onValueChanged.RemoveAllListeners();
    this.subTextInput.onValueChanged.RemoveAllListeners();
    this.LoadTeamName();
    this.SetContinents();
    this.SetNationalityGrid();
    this.continentDropdown.onValueChanged.AddListener((UnityAction<int>) (param0 => this.SetNationalityGrid()));
    this.mainTextInput.onValueChanged.AddListener(new UnityAction<string>(this.ValidateInputText));
    this.subTextInput.onValueChanged.AddListener(new UnityAction<string>(this.ValidateInputText));
    this.mAllowMenuSounds = true;
  }

  private void SetCameraPanControl(bool inForce = false, bool inValue = false)
  {
    if (this.screen.screenMode != UIScreen.ScreenMode.Mode3D || !((UnityEngine.Object) this.screen.studioScene != (UnityEngine.Object) null) || !((UnityEngine.Object) this.screen.freeRoamCamera != (UnityEngine.Object) null))
      return;
    this.screen.freeRoamCamera.SetDisablePanControls(!inForce ? this.mainTextInput.isFocused || this.subTextInput.isFocused : inValue);
  }

  private void LoadTeamName()
  {
    this.mainTextInput.text = CreateTeamManager.teamFirstName;
    this.subTextInput.text = CreateTeamManager.teamLastName;
    this.screen.overviewWidget.RefreshLogo();
    this.screen.overviewWidget.RefreshNationality();
  }

  private void SetContinents()
  {
    this.continentDropdown.ClearOptions();
    int length = Enum.GetValues(typeof (Nationality.Continent)).Length;
    Nationality.Continent continent = CreateTeamManager.newTeam.nationality.continent;
    for (int index = 0; index < length; ++index)
    {
      Nationality.Continent inContinent = (Nationality.Continent) index;
      this.continentDropdown.get_options().Add(new Dropdown.OptionData()
      {
        text = Localisation.LocaliseID(Nationality.GetContinentStringID(inContinent), (GameObject) null).ToUpper()
      });
      if (inContinent == continent)
        this.continentDropdown.value = index;
    }
    this.continentDropdown.RefreshShownValue();
  }

  private void SetNationalityGrid()
  {
    Nationality.Continent inContinent = (Nationality.Continent) this.continentDropdown.value;
    List<Nationality> nationalitiesForContinent = App.instance.nationalityManager.GetNationalitiesForContinent(inContinent);
    Nationality nationality = CreateTeamManager.newTeam.nationality;
    RectTransform inRectTransform = (RectTransform) null;
    nationalitiesForContinent.Sort();
    this.nationalityToggleGroup.SetAllTogglesOff();
    this.nationalityGrid.DestroyListItems();
    int count = nationalitiesForContinent.Count;
    for (int inIndex = 0; inIndex < count; ++inIndex)
    {
      Nationality inNationality = nationalitiesForContinent[inIndex];
      UICreatePlayerNationalityEntry nationalityEntry = this.nationalityGrid.GetOrCreateItem<UICreatePlayerNationalityEntry>(inIndex);
      nationalityEntry.Setup(inNationality);
      nationalityEntry.toggle.isOn = nationality.continent == inContinent ? nationality.localisedCountry == inNationality.localisedCountry : inIndex == 0;
      if (nationalityEntry.toggle.isOn)
        inRectTransform = nationalityEntry.GetComponent<RectTransform>();
    }
    GameUtility.SetActive(this.nationalityGrid.itemPrefab, false);
    App.instance.StartCoroutine(this.SnapGrid(inRectTransform));
  }

  [DebuggerHidden]
  private IEnumerator SnapGrid(RectTransform inRectTransform)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new CreateTeamOptionNameOrigin.\u003CSnapGrid\u003Ec__Iterator21()
    {
      inRectTransform = inRectTransform,
      \u003C\u0024\u003EinRectTransform = inRectTransform,
      \u003C\u003Ef__this = this
    };
  }

  public void SelectCountry(Nationality inNationality)
  {
    if (!(inNationality != (Nationality) null))
      return;
    this.PlayMenuSound();
    CreateTeamManager.SetTeamNationality(inNationality);
    this.screen.overviewWidget.RefreshNationality();
  }

  public void PlayMenuSound()
  {
    if (!this.mAllowMenuSounds)
      return;
    scSoundManager.Instance.PlaySound(SoundID.Button_AppearanceChange, 0.0f);
  }

  private void ValidateInputText(string inString)
  {
    this.screen.DisplayErrorMessage(this.IsNameValid());
    CreateTeamManager.SetTeamName(this.mainTextInput.text, this.subTextInput.text);
    this.screen.overviewWidget.RefreshLogo();
    this.screen.overviewWidget.RefreshNationality();
  }

  public bool IsNameValid()
  {
    return !string.IsNullOrEmpty(this.mainTextInput.text.Replace(" ", string.Empty)) && !string.IsNullOrEmpty(this.subTextInput.text.Replace(" ", string.Empty));
  }
}
