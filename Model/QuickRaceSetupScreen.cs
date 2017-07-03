// Decompiled with JetBrains decompiler
// Type: QuickRaceSetupScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuickRaceSetupScreen : UIScreen
{
  public List<string> lockedCircuits = new List<string>();
  private string mSelectedCircuit = string.Empty;
  private int mDriver1GridPosition = 1;
  private int mDriver2GridPosition = 2;
  public Toggle[] raceLengthToggle;
  public Toggle[] weatherToggle;
  public Button previousLayoutButton;
  public Button nextLayoutButton;
  public TextMeshProUGUI layoutLabel;
  public Toggle fullRaceWeekendToggle;
  public Toggle raceOnlyToggle;
  public CanvasGroup gridCanvasGroup;
  public Toggle randomGridToggle;
  public Toggle predefinedGridToggle;
  public CanvasGroup gridPositionCanvasGroup;
  public TextMeshProUGUI driver1GridPosLabel;
  public Button[] driver1GridPosButton;
  public TextMeshProUGUI driver2GridPosLabel;
  public Button[] driver2GridPosButton;
  public UIGridList circuitList;
  public Flag flag;
  public TextMeshProUGUI trackName;
  public UICircuitImage circuitImage;
  public UITrackLayout minimap;
  private QuickRaceSetupState.RaceWeekend mRaceWeekend;
  private QuickRaceSetupState.GridOptions mGridOptions;
  private Circuit.TrackLayout mSelectedLayout;
  private bool mAddedListener_OnRaceLengthToggleChanged;

  public override void OnStart()
  {
    scSoundManager.BlockSoundEvents = true;
    base.OnStart();
    this.driver1GridPosButton[0].onClick.AddListener(new UnityAction(this.OnMinusDriver1GridPositionButton));
    this.driver1GridPosButton[1].onClick.AddListener(new UnityAction(this.OnPlusDriver1GridPositionButton));
    this.driver2GridPosButton[0].onClick.AddListener(new UnityAction(this.OnMinusDriver2GridPositionButton));
    this.driver2GridPosButton[1].onClick.AddListener(new UnityAction(this.OnPlusDriver2GridPositionButton));
    if (!this.mAddedListener_OnRaceLengthToggleChanged)
    {
      foreach (Toggle toggle in this.raceLengthToggle)
        toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnRaceLengthToggleChanged));
      foreach (Toggle toggle in this.weatherToggle)
        toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnRaceLengthToggleChanged));
      this.mAddedListener_OnRaceLengthToggleChanged = true;
    }
    this.previousLayoutButton.onClick.AddListener(new UnityAction(this.SelectPreviousLayout));
    this.nextLayoutButton.onClick.AddListener(new UnityAction(this.SelectNextLayout));
    scSoundManager.BlockSoundEvents = false;
  }

  public void CreateEntries()
  {
    if (this.circuitList.itemCount > 0)
      return;
    List<string> stringList = new List<string>()
    {
      "Doha",
      "Milan",
      "Singapore",
      "Yokohama",
      "Munich",
      "Beijing",
      "Ardennes",
      "Cape Town",
      "Dubai",
      "Guildford",
      "Tondela",
      "Phoenix",
      "Rio de Janeiro",
      "Black Sea",
      "Sydney",
      "Vancouver"
    };
    this.lockedCircuits = new List<string>();
    Game.instance.circuitManager = new CircuitManager();
    Game.instance.circuitManager.LoadCircuitsFromDatabase(App.instance.database, Game.instance.climateManager);
    ToggleGroup component = this.circuitList.GetComponent<ToggleGroup>();
    bool flag = false;
    for (int index = 0; index < stringList.Count; ++index)
    {
      UIQuickRaceCircuitEntry listItem = this.circuitList.CreateListItem<UIQuickRaceCircuitEntry>();
      Circuit circuitByLocationName = Game.instance.circuitManager.GetCircuitByLocationName(stringList[index]);
      listItem.toggle.group = component;
      listItem.toggle.interactable = (this.lockedCircuits.Count <= 0 ? 0 : (this.lockedCircuits.Contains(stringList[index]) ? 1 : 0)) == 0;
      listItem.Setup(circuitByLocationName);
      if (!flag && listItem.toggle.interactable)
      {
        listItem.toggle.isOn = true;
        flag = true;
        this.SetSelectedCircuit(circuitByLocationName.locationName);
      }
    }
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.CreateEntries();
    this.showNavigationBars = true;
    this.SetTopBarMode(UITopBar.Mode.Core);
    this.SetBottomBarMode(UIBottomBar.Mode.Core);
    this.raceLengthToggle[0].isOn = true;
    this.weatherToggle[0].isOn = true;
    this.mRaceWeekend = QuickRaceSetupState.RaceWeekend.Full;
    this.mGridOptions = QuickRaceSetupState.GridOptions.Random;
    this.mDriver1GridPosition = 1;
    this.mDriver2GridPosition = 2;
    this.fullRaceWeekendToggle.isOn = true;
    this.raceOnlyToggle.isOn = false;
    this.randomGridToggle.isOn = true;
    this.predefinedGridToggle.isOn = false;
    this.UpdateWeekendTypeData();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionScreen, 0.0f);
  }

  public void SetSelectedCircuit(string inCircuit)
  {
    if (this.mSelectedCircuit != inCircuit && this.mSelectedCircuit != string.Empty)
      scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mSelectedCircuit = inCircuit;
    this.mSelectedLayout = Circuit.TrackLayout.TrackA;
    this.SetCircuitData(Game.instance.circuitManager.GetCircuitByLocationNameLayout(inCircuit, this.mSelectedLayout));
    this.UpdateLayoutLabel();
  }

  private void SetCircuitData(Circuit inCircuit)
  {
    this.circuitImage.SetCircuitIcon(inCircuit);
    this.minimap.SetCircuitIcon(inCircuit);
    this.trackName.text = Localisation.LocaliseID(inCircuit.locationNameID, (GameObject) null);
    this.flag.SetNationality(inCircuit.nationality);
  }

  public void SelectPreviousLayout()
  {
    int circuitLayoutCount = Game.instance.circuitManager.GetCircuitLayoutCount(this.mSelectedCircuit);
    int num = (int) (this.mSelectedLayout - 1);
    if (num < 0)
      num = circuitLayoutCount - 1;
    this.mSelectedLayout = (Circuit.TrackLayout) num;
    this.UpdateLayoutLabel();
  }

  public void SelectNextLayout()
  {
    int circuitLayoutCount = Game.instance.circuitManager.GetCircuitLayoutCount(this.mSelectedCircuit);
    int num = (int) (this.mSelectedLayout + 1);
    if (num >= circuitLayoutCount)
      num = 0;
    this.mSelectedLayout = (Circuit.TrackLayout) num;
    this.UpdateLayoutLabel();
  }

  private void UpdateLayoutLabel()
  {
    List<Circuit> circuitsByLocation = Game.instance.circuitManager.GetCircuitsByLocation(this.mSelectedCircuit);
    for (int index = 0; index < circuitsByLocation.Count; ++index)
    {
      if (circuitsByLocation[index].trackLayout == this.mSelectedLayout)
        this.minimap.SetCircuitIcon(circuitsByLocation[index]);
    }
    switch (this.mSelectedLayout)
    {
      case Circuit.TrackLayout.TrackA:
        this.layoutLabel.text = "A";
        break;
      case Circuit.TrackLayout.TrackB:
        this.layoutLabel.text = "B";
        break;
      case Circuit.TrackLayout.TrackC:
        this.layoutLabel.text = "C";
        break;
      case Circuit.TrackLayout.TrackD:
        this.layoutLabel.text = "D";
        break;
      case Circuit.TrackLayout.TrackE:
        this.layoutLabel.text = "E";
        break;
      case Circuit.TrackLayout.TrackF:
        this.layoutLabel.text = "F";
        break;
    }
    int circuitLayoutCount = Game.instance.circuitManager.GetCircuitLayoutCount(this.mSelectedCircuit);
    this.previousLayoutButton.interactable = circuitLayoutCount > 1;
    this.nextLayoutButton.interactable = circuitLayoutCount > 1;
  }

  public void OnFullRaceWeekendButton()
  {
    if (this.mRaceWeekend != QuickRaceSetupState.RaceWeekend.Full)
      scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mRaceWeekend = QuickRaceSetupState.RaceWeekend.Full;
    this.UpdateWeekendTypeData();
  }

  public void OnRaceOnlyButton()
  {
    if (this.mRaceWeekend != QuickRaceSetupState.RaceWeekend.RaceOnly)
      scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mRaceWeekend = QuickRaceSetupState.RaceWeekend.RaceOnly;
    this.UpdateWeekendTypeData();
  }

  public void OnRandomGridButton()
  {
    if (this.mGridOptions != QuickRaceSetupState.GridOptions.Random)
      scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mGridOptions = QuickRaceSetupState.GridOptions.Random;
    this.UpdateWeekendTypeData();
  }

  public void OnPredefinedGridButton()
  {
    if (this.mGridOptions != QuickRaceSetupState.GridOptions.Predefined)
      scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mGridOptions = QuickRaceSetupState.GridOptions.Predefined;
    this.UpdateWeekendTypeData();
  }

  public void OnMinusDriver1GridPositionButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mDriver1GridPosition - 1 == this.mDriver2GridPosition)
      --this.mDriver1GridPosition;
    this.mDriver1GridPosition = Mathf.Max(1, this.mDriver1GridPosition - 1);
    if (this.mDriver1GridPosition == this.mDriver2GridPosition)
      ++this.mDriver1GridPosition;
    this.UpdateWeekendTypeData();
  }

  public void OnPlusDriver1GridPositionButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mDriver1GridPosition + 1 == this.mDriver2GridPosition)
      ++this.mDriver1GridPosition;
    this.mDriver1GridPosition = Mathf.Min(20, this.mDriver1GridPosition + 1);
    if (this.mDriver1GridPosition == this.mDriver2GridPosition)
      --this.mDriver1GridPosition;
    this.UpdateWeekendTypeData();
  }

  public void OnMinusDriver2GridPositionButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mDriver2GridPosition - 1 == this.mDriver1GridPosition)
      --this.mDriver2GridPosition;
    this.mDriver2GridPosition = Mathf.Max(1, this.mDriver2GridPosition - 1);
    if (this.mDriver2GridPosition == this.mDriver1GridPosition)
      ++this.mDriver2GridPosition;
    this.UpdateWeekendTypeData();
  }

  public void OnPlusDriver2GridPositionButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mDriver2GridPosition + 1 == this.mDriver1GridPosition)
      ++this.mDriver2GridPosition;
    this.mDriver2GridPosition = Mathf.Min(20, this.mDriver2GridPosition + 1);
    if (this.mDriver2GridPosition == this.mDriver1GridPosition)
      --this.mDriver2GridPosition;
    this.UpdateWeekendTypeData();
  }

  private void UpdateWeekendTypeData()
  {
    switch (this.mRaceWeekend)
    {
      case QuickRaceSetupState.RaceWeekend.Full:
        this.randomGridToggle.interactable = false;
        this.predefinedGridToggle.interactable = false;
        this.gridCanvasGroup.alpha = 0.25f;
        this.gridCanvasGroup.interactable = false;
        break;
      case QuickRaceSetupState.RaceWeekend.RaceOnly:
        this.randomGridToggle.interactable = true;
        this.predefinedGridToggle.interactable = true;
        this.gridCanvasGroup.alpha = 1f;
        this.gridCanvasGroup.interactable = true;
        break;
    }
    this.gridPositionCanvasGroup.interactable = this.predefinedGridToggle.isOn;
    this.gridPositionCanvasGroup.alpha = !this.gridPositionCanvasGroup.interactable ? 0.25f : 1f;
    this.driver1GridPosButton[0].interactable = this.mDriver1GridPosition != 1 && (this.mDriver1GridPosition != 2 ? 0 : (this.mDriver2GridPosition == 1 ? 1 : 0)) == 0;
    this.driver1GridPosButton[1].interactable = this.mDriver1GridPosition != 20 && (this.mDriver1GridPosition != 19 ? 0 : (this.mDriver2GridPosition == 20 ? 1 : 0)) == 0;
    this.driver1GridPosLabel.text = GameUtility.FormatForPosition(this.mDriver1GridPosition, (string) null);
    this.driver2GridPosButton[0].interactable = this.mDriver2GridPosition != 1 && (this.mDriver2GridPosition != 2 ? 0 : (this.mDriver1GridPosition == 1 ? 1 : 0)) == 0;
    this.driver2GridPosButton[1].interactable = this.mDriver2GridPosition != 20 && (this.mDriver2GridPosition != 19 ? 0 : (this.mDriver1GridPosition == 20 ? 1 : 0)) == 0;
    this.driver2GridPosLabel.text = GameUtility.FormatForPosition(this.mDriver2GridPosition, (string) null);
  }

  private void OnRaceLengthToggleChanged(bool isOn)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  private void OnWeatherToggleChanged(bool isOn)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  private void OnLayoutToggleChanged(bool isOn)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    QuickRaceSetupState state = (QuickRaceSetupState) App.instance.gameStateManager.GetState(GameState.Type.QuickRaceSetup);
    state.SetCircuit(this.mSelectedCircuit);
    for (int index = 0; index < this.raceLengthToggle.Length; ++index)
    {
      if (this.raceLengthToggle[index].isOn)
      {
        state.SetRaceLength((QuickRaceSetupState.RaceLength) index);
        break;
      }
    }
    state.SetTrackLayout(this.mSelectedLayout);
    for (int index = 0; index < this.weatherToggle.Length; ++index)
    {
      if (this.weatherToggle[index].isOn)
      {
        state.SetWeatherSettings((SessionDetails.WeatherSettings) index);
        break;
      }
    }
    state.SetRaceWeekend(this.mRaceWeekend);
    state.SetGridOptions(this.mGridOptions);
    state.SetDriver1GridPosition(this.mDriver1GridPosition);
    state.SetDriver2GridPosition(this.mDriver2GridPosition);
    UIManager.instance.ChangeScreen("ChooseSeriesScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }
}
