// Decompiled with JetBrains decompiler
// Type: PreSeasonTestingScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PreSeasonTestingScreen : UIScreen
{
  private float[] mInitialDriverMorale = new float[2];
  private float[] mFinalDriverMorale = new float[2];
  private float mUpdateMoraleTimer = 1.5f;
  private float mCurrentUpdateTimer = 1.5f;
  public UIGridList grid;
  public GameObject notStartedState;
  public GameObject runningState;
  public GameObject[] chequeredFlags;
  public Button startSessionButton;
  public TextMeshProUGUI sessionTimeLabel;
  public UICharacterPortrait[] driverPortrait;
  public UIAbilityStars[] driverStars;
  public Flag[] driverFlag;
  public UIGauge[] driverConfidenceWithCar;
  public TextMeshProUGUI[] driverNameLabel;
  public TextMeshProUGUI[] driverAgeLabel;
  public GameObject[] driverMoraleChangeContainers;
  public TextMeshProUGUI[] driverMoraleChangeLabel;
  public Slider carSlider;
  public UIGauge staffMorale;
  private PreSeasonTesting mPreSeasonTesting;

  public override void OnStart()
  {
    base.OnStart();
    this.startSessionButton.onClick.AddListener(new UnityAction(this.OnStartSessionButton));
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.SetBottomBarMode(UIBottomBar.Mode.Core);
    UIManager.instance.navigationBars.bottomBar.MarkContinueButtonForUpdate();
    this.continueButtonInteractable = false;
    UIManager.instance.ClearNavigationStacks();
    this.canEnterPreferencesScreen = false;
    this.dontAddToBackStack = true;
    Team team = Game.instance.player.team;
    this.mPreSeasonTesting = team.championship.preSeasonTesting;
    for (int inIndex = 0; inIndex < Team.mainDriverCount; ++inIndex)
    {
      Driver driver = team.GetDriver(inIndex);
      this.driverNameLabel[inIndex].text = driver.shortName;
      StringVariableParser.intValue1 = driver.GetAge();
      this.driverAgeLabel[inIndex].text = Localisation.LocaliseID("PSG_10010748", (GameObject) null);
      this.driverPortrait[inIndex].SetPortrait((Person) driver);
      this.driverStars[inIndex].SetAbilityStarsData(driver);
      this.driverFlag[inIndex].SetNationality(driver.nationality);
      this.mInitialDriverMorale[inIndex] = driver.GetMorale();
      this.driverConfidenceWithCar[inIndex].SetValue(driver.GetMorale(), UIGauge.AnimationSetting.DontAnimate);
    }
    this.staffMorale.SetValue(this.mPreSeasonTesting.staffMorale, UIGauge.AnimationSetting.DontAnimate);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionScreen, 0.0f);
  }

  public override void OnExit()
  {
    base.OnExit();
    UIManager.instance.ClearNavigationStacks();
  }

  public override void OpenConfirmDialogBox(Action inAction)
  {
    GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
    Action inCancelAction = (Action) (() => {});
    string inTitle = Localisation.LocaliseID("PSG_10009109", (GameObject) null);
    string inText = Localisation.LocaliseID("PSG_10009110", (GameObject) null);
    string inCancelString = Localisation.LocaliseID("PSG_10009081", (GameObject) null);
    UIManager.instance.dialogBoxManager.Show("GenericConfirmation");
    dialog.Show(inCancelAction, inCancelString, (Action) null, string.Empty, inText, inTitle);
  }

  private void Update()
  {
    StringVariableParser.sessionTime = GameUtility.GetSessionTimeText(this.mPreSeasonTesting.sessionTime);
    this.sessionTimeLabel.text = Localisation.LocaliseID("PSG_10010747", (GameObject) null);
    this.carSlider.value = 1f - this.mPreSeasonTesting.sessionTimeNormalized;
    this.needsPlayerConfirmation = true;
    switch (this.mPreSeasonTesting.state)
    {
      case PreSeasonTesting.State.NotStarted:
        this.UpdateNotStarted();
        break;
      case PreSeasonTesting.State.Running:
        this.UpdateRunning();
        break;
      case PreSeasonTesting.State.Finished:
        this.UpdateFinished();
        break;
    }
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    if (this.mPreSeasonTesting.state == PreSeasonTesting.State.Finished)
      this.needsPlayerConfirmation = false;
    return UIScreen.NavigationButtonEvent.LetGameStateHandle;
  }

  private void UpdateNotStarted()
  {
    GameUtility.SetActive(this.notStartedState, true);
    GameUtility.SetActive(this.runningState, false);
    for (int index = 0; index < this.chequeredFlags.Length; ++index)
      GameUtility.SetActive(this.chequeredFlags[index], false);
    for (int index = 0; index < this.driverMoraleChangeContainers.Length; ++index)
      GameUtility.SetActive(this.driverMoraleChangeContainers[index], false);
  }

  private void UpdateRunning()
  {
    GameUtility.SetActive(this.notStartedState, false);
    GameUtility.SetActive(this.runningState, true);
    for (int index = 0; index < this.chequeredFlags.Length; ++index)
      GameUtility.SetActive(this.chequeredFlags[index], false);
    for (int index = 0; index < this.driverMoraleChangeContainers.Length; ++index)
      GameUtility.SetActive(this.driverMoraleChangeContainers[index], false);
    this.UpdateStandings();
    this.mCurrentUpdateTimer -= GameTimer.deltaTime;
    if ((double) this.mCurrentUpdateTimer > 0.0)
      return;
    Team team = Game.instance.player.team;
    for (int inIndex = 0; inIndex < Team.mainDriverCount; ++inIndex)
    {
      Driver driver = team.GetDriver(inIndex);
      this.driverConfidenceWithCar[inIndex].SetValue(driver.GetMorale() + RandomUtility.GetRandom(-0.15f, 0.15f), UIGauge.AnimationSetting.Animate);
    }
    this.mCurrentUpdateTimer = this.mUpdateMoraleTimer;
  }

  private void UpdateStandings()
  {
    bool flag = false;
    for (int inIndex = 0; inIndex < this.grid.itemCount; ++inIndex)
    {
      UIPreSeasonTestingData seasonTestingData = this.grid.GetItem<UIPreSeasonTestingData>(inIndex);
      GameUtility.SetActive(seasonTestingData.gameObject, seasonTestingData.data.laps > 0);
      if (this.mPreSeasonTesting.GetTestingData(inIndex) != seasonTestingData.data)
      {
        flag = true;
        for (int index = 0; index < this.mPreSeasonTesting.testingDataCount; ++index)
        {
          if (this.mPreSeasonTesting.GetTestingData(index) == seasonTestingData.data)
          {
            seasonTestingData.GetComponent<RectTransform>().SetSiblingIndex(index);
            break;
          }
        }
      }
    }
    if (!flag)
      return;
    this.grid.grid.GetComponent<GridLayoutGroup>().enabled = false;
    this.grid.grid.GetComponent<GridLayoutGroup>().enabled = true;
  }

  private void UpdateFinished()
  {
    GameUtility.SetActive(this.notStartedState, false);
    GameUtility.SetActive(this.runningState, true);
    for (int index = 0; index < this.driverMoraleChangeContainers.Length; ++index)
      GameUtility.SetActive(this.driverMoraleChangeContainers[index], true);
    for (int index = 0; index < this.chequeredFlags.Length; ++index)
      GameUtility.SetActive(this.chequeredFlags[index], true);
    Team team = Game.instance.player.team;
    for (int index = 0; index < Team.mainDriverCount; ++index)
    {
      Driver driver = team.GetDriver(index);
      this.mFinalDriverMorale[index] = driver.GetMorale();
      this.SetMoraleImpactLabel(index);
      this.driverConfidenceWithCar[index].SetValue(driver.GetMorale(), UIGauge.AnimationSetting.Animate);
    }
    this.staffMorale.SetValue(this.mPreSeasonTesting.staffMorale, UIGauge.AnimationSetting.Animate);
    this.continueButtonInteractable = true;
    ((PreSeasonState) App.instance.gameStateManager.currentState).SetStage(PreSeasonState.PreSeasonStage.Finished);
  }

  private void SetMoraleImpactLabel(int inDriverIndex)
  {
    float num = this.mFinalDriverMorale[inDriverIndex] - this.mInitialDriverMorale[inDriverIndex];
    using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
    {
      StringBuilder stringBuilder = builderSafe.stringBuilder;
      if ((double) num > 0.0)
      {
        stringBuilder.Append("+");
        this.driverMoraleChangeLabel[inDriverIndex].color = UIConstants.colorBandGreen;
      }
      else
        this.driverMoraleChangeLabel[inDriverIndex].color = UIConstants.colorBandRed;
      stringBuilder.Append(Mathf.RoundToInt(num * 100f).ToString((IFormatProvider) Localisation.numberFormatter));
      this.driverMoraleChangeLabel[inDriverIndex].text = stringBuilder.ToString();
    }
  }

  public void OnStartSessionButton()
  {
    this.mPreSeasonTesting.StartSession();
    this.grid.DestroyListItems();
    for (int inIndex = 0; inIndex < this.mPreSeasonTesting.testingDataCount; ++inIndex)
    {
      UIPreSeasonTestingData listItem = this.grid.CreateListItem<UIPreSeasonTestingData>();
      listItem.SetData(this.mPreSeasonTesting.GetTestingData(inIndex));
      GameUtility.SetActive(listItem.gameObject, false);
    }
  }
}
