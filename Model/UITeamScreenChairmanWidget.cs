// Decompiled with JetBrains decompiler
// Type: UITeamScreenChairmanWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITeamScreenChairmanWidget : MonoBehaviour
{
  public UICharacterPortrait portrait;
  public TextMeshProUGUI nameLabel;
  public TextMeshProUGUI age;
  public TextMeshProUGUI enquireButtonLabel;
  public UIGauge happiness;
  public Flag nationalityFlag;
  public Button enquireAboutJob;
  public GameObject positiveHappinessIconModifier;
  public GameObject negativeHappinessIconModifier;
  private Chairman mChairman;
  private Team mTeam;

  private void Awake()
  {
    this.enquireAboutJob.onClick.AddListener(new UnityAction(this.OpenEnquireAboutJobWindow));
  }

  private void OpenEnquireAboutJobWindow()
  {
    if (this.mChairman == null)
      return;
    UIManager.instance.dialogBoxManager.GetDialog<ApproachDialogBox>().Show((Person) this.mChairman, ApproachDialogBox.ApproachType.SignNewContract);
  }

  public void Setup(Team inTeam)
  {
    this.mTeam = inTeam;
    this.mChairman = (Chairman) this.mTeam.contractManager.GetPersonOnJob(Contract.Job.Chairman);
    this.portrait.SetPortrait((Person) this.mChairman);
    this.nameLabel.text = this.mChairman.shortName;
    this.age.text = this.mChairman.GetAge().ToString();
    this.nationalityFlag.SetNationality(this.mChairman.nationality);
    bool flag1 = !(App.instance.gameStateManager.currentState is FrontendState);
    GameUtility.SetActive(this.enquireAboutJob.gameObject, !this.mTeam.IsPlayersTeam());
    if (this.enquireAboutJob.gameObject.activeSelf)
    {
      bool flag2 = Game.instance.player.HasAppliedForTeam(this.mTeam);
      this.enquireAboutJob.interactable = !flag1 && !flag2 && App.instance.dlcManager.IsSeriesAvailable(this.mTeam.championship.series);
      this.enquireButtonLabel.text = flag2 ? Localisation.LocaliseEnum((Enum) Game.instance.player.GetApplication(this.mTeam).status) : Localisation.LocaliseID("PSG_10004598", (GameObject) null);
    }
    this.happiness.SetValueRange(0.0f, 1f);
    this.happiness.SetValue(this.mChairman.happinessNormalized, UIGauge.AnimationSetting.Animate);
    float happinessModifier = this.mChairman.GetHappinessModifier();
    GameUtility.SetActive(this.positiveHappinessIconModifier, this.mTeam.IsPlayersTeam() && (double) happinessModifier > 0.0);
    GameUtility.SetActive(this.negativeHappinessIconModifier, this.mTeam.IsPlayersTeam() && (double) happinessModifier < 0.0);
  }

  private void OnMouseEnter()
  {
    if (!this.mTeam.IsPlayersTeam() || this.mChairman.GetPersonalityTraitHappinessModifiers().Count <= 0 && this.mChairman.happinessModificationHistory.historyEntryCount <= 0)
      return;
    UIManager.instance.dialogBoxManager.GetDialog<DriverStatsModifiersRollover>().ShowRollover(this.mChairman);
  }

  private void OnMouseExit()
  {
    UIManager.instance.dialogBoxManager.GetDialog<DriverStatsModifiersRollover>().Hide();
  }
}
