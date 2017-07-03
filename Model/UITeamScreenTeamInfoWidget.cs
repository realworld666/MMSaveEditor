// Decompiled with JetBrains decompiler
// Type: UITeamScreenTeamInfoWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITeamScreenTeamInfoWidget : MonoBehaviour
{
  public Toggle[] championshipToggles = new Toggle[2];
  public Image carImage;
  public TextMeshProUGUI nameLabel;
  public TextMeshProUGUI teamWorth;
  public TextMeshProUGUI racingSeries;
  public TextMeshProUGUI fanBase;
  public TextMeshProUGUI championshipName;
  public TextMeshProUGUI championshipPosition;
  public UITeamLogo logo;
  public Button leftArrowButton;
  public Button rightArrowButton;
  public Image[] stars;
  public GameObject championshipToggleContainter;
  private Championship.Series mSeries;
  private Championship mChampionship;
  private int mCurrentTeamIndex;
  private Team mTeam;

  private void Awake()
  {
    this.leftArrowButton.onClick.AddListener((UnityAction) (() => this.ChangeTeam(-1)));
    this.rightArrowButton.onClick.AddListener((UnityAction) (() => this.ChangeTeam(1)));
  }

  private void ChangeTeam(int inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mCurrentTeamIndex += inValue;
    if (this.mCurrentTeamIndex < 0)
    {
      if (this.mChampionship.championshipAboveID != -1)
        this.mChampionship = Game.instance.championshipManager.GetChampionshipByID(this.mChampionship.championshipAboveID);
      this.mCurrentTeamIndex = this.mChampionship.standings.teamEntryCount - 1;
    }
    else if (this.mCurrentTeamIndex > this.mChampionship.standings.teamEntryCount - 1)
    {
      if (this.mChampionship.championshipBelowID != -1)
        this.mChampionship = Game.instance.championshipManager.GetChampionshipByID(this.mChampionship.championshipBelowID);
      this.mCurrentTeamIndex = 0;
    }
    UIManager.instance.currentScreen.data = (Entity) this.mChampionship.standings.GetTeamEntry(this.mCurrentTeamIndex).GetEntity<Team>();
    scSoundManager.BlockSoundEvents = true;
    UIManager.instance.currentScreen.OnEnter();
    scSoundManager.BlockSoundEvents = false;
  }

  private void RefreshSeriesToggles()
  {
    for (int index = 0; index < this.championshipToggles.Length; ++index)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UITeamScreenTeamInfoWidget.\u003CRefreshSeriesToggles\u003Ec__AnonStoreyA0 togglesCAnonStoreyA0 = new UITeamScreenTeamInfoWidget.\u003CRefreshSeriesToggles\u003Ec__AnonStoreyA0();
      // ISSUE: reference to a compiler-generated field
      togglesCAnonStoreyA0.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      togglesCAnonStoreyA0.series = (Championship.Series) index;
      this.championshipToggles[index].onValueChanged.RemoveAllListeners();
      // ISSUE: reference to a compiler-generated field
      this.championshipToggles[index].isOn = togglesCAnonStoreyA0.series == this.mSeries;
      // ISSUE: reference to a compiler-generated method
      this.championshipToggles[index].onValueChanged.AddListener(new UnityAction<bool>(togglesCAnonStoreyA0.\u003C\u003Em__1E2));
    }
  }

  private void SetChampionshipCategory(bool inValue, Championship.Series inSeries)
  {
    if (!inValue)
      return;
    if (Game.instance.player.IsUnemployed() || inSeries != this.mChampionship.series)
    {
      this.mChampionship = Game.instance.championshipManager.GetMainChampionship(inSeries);
      this.mCurrentTeamIndex = 0;
      this.ChangeTeam(0);
    }
    this.mSeries = inSeries;
  }

  public void Setup(Team inTeam)
  {
    GameUtility.SetActive(this.championshipToggleContainter, Game.instance.championshipManager.isGTSeriesActive);
    this.mTeam = inTeam;
    this.mChampionship = this.mTeam.championship;
    this.mSeries = this.mChampionship.series;
    this.mCurrentTeamIndex = this.mChampionship.standings.GetEntry((Entity) this.mTeam).GetCurrentChampionshipPosition() - 1;
    this.leftArrowButton.interactable = this.mChampionship.championshipAboveID != -1 || this.mCurrentTeamIndex > 0;
    this.rightArrowButton.interactable = this.mChampionship.championshipBelowID != -1 || this.mCurrentTeamIndex < this.mChampionship.standings.teamEntryCount - 1;
    this.nameLabel.text = inTeam.name;
    this.teamWorth.text = GameUtility.GetCurrencyString(inTeam.financeController.worth, 0);
    this.racingSeries.text = this.mTeam.championship.GetChampionshipName(false);
    StringVariableParser.floatValue1 = MathsUtility.RoundToDecimal(inTeam.fanBase);
    this.fanBase.text = Localisation.LocaliseID("PSG_10010815", (GameObject) null);
    this.championshipName.text = this.mTeam.championship.GetChampionshipName(false);
    this.championshipPosition.text = GameUtility.FormatForPosition(this.mTeam.GetChampionshipEntry().GetCurrentChampionshipPosition(), (string) null);
    this.logo.SetTeam(this.mTeam);
    if ((Object) this.carImage != (Object) null)
      this.carImage.gameObject.SetActive(App.instance.gameStateManager.currentState is FrontendState);
    this.SetStars();
    this.RefreshSeriesToggles();
  }

  public void SetStars()
  {
    float scale = MathsUtility.RoundToScale(this.mTeam.GetStarsStat(), 4f);
    int index = 0;
    while (index < this.stars.Length)
    {
      GameUtility.SetActive(this.stars[index].gameObject, true);
      GameUtility.SetImageFillAmountIfDifferent(this.stars[index], Mathf.Clamp01(scale), 1f / 512f);
      ++index;
      --scale;
    }
  }
}
