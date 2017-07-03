// Decompiled with JetBrains decompiler
// Type: UITeamReportScreenChairmanWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITeamReportScreenChairmanWidget : MonoBehaviour
{
  public float animationDuration = 1f;
  private List<UITeamReportScreenDriverWidget.UIDriverStatChangeEntry> mStatList = new List<UITeamReportScreenDriverWidget.UIDriverStatChangeEntry>();
  public CanvasGroup canvasGroup;
  public UICharacterPortrait portrait;
  public Flag flag;
  public TextMeshProUGUI chairmanName;
  public TextMeshProUGUI chairmanAge;
  public TextMeshProUGUI teamExpectedResult;
  public TextMeshProUGUI teamAchievedResult;
  public UITeamReportScreenStatEntry[] statEntries;
  public EasingUtility.Easing animationCurve;
  private Chairman mChairman;
  private UITeamReportScreenChairmanWidget.State mState;
  private float mTimer;

  public void Setup(Chairman inChairman)
  {
    this.mChairman = inChairman;
    this.flag.SetNationality(this.mChairman.nationality);
    this.portrait.SetPortrait((Person) this.mChairman);
    this.chairmanName.text = this.mChairman.name;
    this.chairmanAge.text = this.mChairman.GetAge().ToString();
    this.SetupStats();
    this.canvasGroup.alpha = 0.0f;
    this.canvasGroup.interactable = false;
    this.canvasGroup.blocksRaycasts = false;
    this.mState = UITeamReportScreenChairmanWidget.State.Idle;
  }

  private void SetupStats()
  {
    Team team = this.mChairman.contract.GetTeam();
    ChampionshipEntry_v1 championshipEntry = team.GetChampionshipEntry();
    if (championshipEntry != null)
    {
      int positionForEvent = championshipEntry.GetPositionForEvent(!team.championship.HasSeasonEnded() ? team.championship.eventNumber - 1 : team.championship.eventNumber);
      this.teamExpectedResult.text = GameUtility.FormatForPosition(team.chairman.expectedTeamChampionshipResult, (string) null);
      this.teamAchievedResult.text = GameUtility.FormatForPosition(positionForEvent, (string) null);
      this.teamAchievedResult.color = positionForEvent > team.chairman.expectedTeamChampionshipResult ? UIConstants.negativeColor : UIConstants.positiveColor;
    }
    this.mStatList.Clear();
    this.mStatList.Add(new UITeamReportScreenDriverWidget.UIDriverStatChangeEntry()
    {
      oldValue = (float) this.mChairman.happinessBeforeEvent,
      newValue = (float) this.mChairman.GetHappiness(),
      statMax = 100f
    });
    this.mStatList.Add(new UITeamReportScreenDriverWidget.UIDriverStatChangeEntry()
    {
      oldValue = team.marketabilityBeforeEvent * 100f,
      newValue = team.GetTeamTotalMarketability() * 100f,
      statMax = 100f
    });
    int count = this.mStatList.Count;
    for (int index = 0; index < count; ++index)
      this.mStatList[index].Calculate();
    for (int index = 0; index < this.statEntries.Length; ++index)
      this.statEntries[index].Setup(this.mStatList[index]);
  }

  public void StartAnimating()
  {
    this.mState = UITeamReportScreenChairmanWidget.State.Animating;
    this.mTimer = 0.0f;
  }

  public void AnimateStats()
  {
    for (int index = 0; index < this.statEntries.Length; ++index)
      this.statEntries[index].Animate();
  }

  private void Update()
  {
    if (this.mState != UITeamReportScreenChairmanWidget.State.Animating)
      return;
    this.mTimer += GameTimer.deltaTime;
    this.canvasGroup.alpha = Mathf.Clamp01(this.mTimer / this.animationDuration);
    if ((double) this.mTimer < (double) this.animationDuration)
      return;
    this.AnimateStats();
    this.canvasGroup.interactable = true;
    this.canvasGroup.blocksRaycasts = true;
    this.mState = UITeamReportScreenChairmanWidget.State.Idle;
  }

  public enum State
  {
    Idle,
    Animating,
  }
}
