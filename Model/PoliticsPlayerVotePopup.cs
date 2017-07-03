// Decompiled with JetBrains decompiler
// Type: PoliticsPlayerVotePopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PoliticsPlayerVotePopup : UIDialogBox
{
  public PoliticsPlayerVoteTitleStage[] stageTitles;
  public PoliticsPlayerVoteStage[] stages;
  public Button buttonCurrentRules;
  public Button buttonUpcomingVotes;
  public Button buttonCancel;
  public Button buttonBack;
  public Button buttonContinue;
  public Button buttonConfirm;
  private Championship mChampionship;
  private PoliticalSystem mPoliticalSystem;
  private PoliticalVote mSelectedVote;
  private Action mReturnToPopupAction;
  private Action mCompleteProposalAction;
  private PoliticsPlayerVoteStage mActiveStage;
  private PoliticsPlayerVotePopup.Stage mStage;
  private bool mDetailsStageActive;

  public PoliticalSystem politicalSystem
  {
    get
    {
      return this.mPoliticalSystem;
    }
  }

  public Championship championship
  {
    get
    {
      return this.mChampionship;
    }
  }

  public PoliticalVote selectedVote
  {
    get
    {
      return this.mSelectedVote;
    }
  }

  public static void Open(Action inCompleteAction)
  {
    PoliticsPlayerVotePopup dialog = UIManager.instance.dialogBoxManager.GetDialog<PoliticsPlayerVotePopup>();
    dialog.Setup(inCompleteAction);
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
  }

  public static void Close()
  {
    UIManager.instance.dialogBoxManager.GetDialog<PoliticsPlayerVotePopup>().ClosePopup();
  }

  protected override void Awake()
  {
    base.Awake();
    this.buttonCurrentRules.onClick.AddListener(new UnityAction(this.OnButtonCurrentRules));
    this.buttonUpcomingVotes.onClick.AddListener(new UnityAction(this.OnButtonUpcomingVotes));
    this.buttonCancel.onClick.AddListener(new UnityAction(this.OnButtonCancel));
    this.buttonBack.onClick.AddListener(new UnityAction(this.OnButtonBack));
    this.buttonContinue.onClick.AddListener(new UnityAction(this.OnButtonContinue));
    this.buttonConfirm.onClick.AddListener(new UnityAction(this.OnButtonConfirm));
    for (int index = 0; index < this.stages.Length; ++index)
      this.stages[index].OnStart();
    this.mReturnToPopupAction = (Action) (() => this.ShowPopup());
  }

  public void Setup(Action inCompleteAction)
  {
    this.mStage = PoliticsPlayerVotePopup.Stage.Choose;
    this.mChampionship = Game.instance.player.team.championship;
    this.mPoliticalSystem = this.mChampionship.politicalSystem;
    this.mSelectedVote = (PoliticalVote) null;
    this.mCompleteProposalAction = inCompleteAction;
    for (int index = 0; index < this.stages.Length; ++index)
      this.stages[index].Reset();
    this.ActivateStage(this.mStage);
  }

  public void ShowPopup()
  {
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) this);
  }

  public void ClosePopup()
  {
    this.Hide();
  }

  private void ActivateStage(PoliticsPlayerVotePopup.Stage inStage)
  {
    for (int index = 0; index < this.stages.Length; ++index)
    {
      if (this.stages[index].stage == inStage)
      {
        this.mActiveStage = this.stages[index];
        this.stages[index].Setup();
      }
      else
        this.stages[index].Hide();
    }
    this.RefreshStageTitles();
    this.UpdateButtonsState();
  }

  private void GoToNextStage()
  {
    this.mStage = this.mStage != PoliticsPlayerVotePopup.Stage.Choose ? this.mStage + 1 : (!this.mDetailsStageActive ? PoliticsPlayerVotePopup.Stage.Propose : PoliticsPlayerVotePopup.Stage.TrackDetails);
    this.ActivateStage(this.mStage);
  }

  private void GoToPreviousStage()
  {
    this.mStage = this.mStage != PoliticsPlayerVotePopup.Stage.Propose ? this.mStage - 1 : (!this.mDetailsStageActive ? PoliticsPlayerVotePopup.Stage.Choose : PoliticsPlayerVotePopup.Stage.TrackDetails);
    this.ActivateStage(this.mStage);
  }

  private void UpdateButtonsState()
  {
    GameUtility.SetActive(this.buttonBack.gameObject, this.mStage > PoliticsPlayerVotePopup.Stage.Choose);
    GameUtility.SetActive(this.buttonContinue.gameObject, this.mStage < PoliticsPlayerVotePopup.Stage.Propose);
    GameUtility.SetActive(this.buttonConfirm.gameObject, this.mStage == PoliticsPlayerVotePopup.Stage.Propose);
    this.buttonContinue.interactable = this.mActiveStage.isReady();
  }

  private void RefreshStageTitles()
  {
    this.mDetailsStageActive = this.mSelectedVote != null && this.mSelectedVote.HasImpactOfType<PoliticalImpactChangeTrack>();
    GameUtility.SetActive(this.stageTitles[1].gameObject, this.mDetailsStageActive);
    this.stageTitles[2].SetNumberLabel(!this.mDetailsStageActive ? 2 : 3);
    for (int index = 0; index < this.stageTitles.Length; ++index)
      this.stageTitles[index].EnableStage(this.stageTitles[index].stage == this.mStage);
  }

  public void SelectVote(PoliticalVote inVote)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mSelectedVote = inVote;
    this.RefreshStageTitles();
    this.UpdateButtonsState();
  }

  private void OnButtonCancel()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Back, 0.0f);
    this.ClosePopup();
  }

  private void OnButtonBack()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Back, 0.0f);
    this.GoToPreviousStage();
  }

  private void OnButtonContinue()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.GoToNextStage();
  }

  private void OnButtonConfirm()
  {
    if (this.mSelectedVote == null)
      return;
    Action inOnTransactionFail = (Action) (() => this.ShowPopup());
    Action inOnTransactionSucess = (Action) (() =>
    {
      if (this.mCompleteProposalAction != null)
        this.mCompleteProposalAction();
      Game.instance.player.team.championship.politicalSystem.GeneratePlayerVote(this.mSelectedVote);
    });
    this.ClosePopup();
    if (Game.instance.player.playerBackStoryType == PlayerBackStory.PlayerBackStoryType.Politico)
    {
      scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
      inOnTransactionSucess();
    }
    else
    {
      Transaction transaction = new Transaction(Transaction.Group.GlobalMotorsport, Transaction.Type.Debit, 1000000, Localisation.LocaliseID("PSG_10011859", (GameObject) null));
      Game.instance.player.team.financeController.finance.ProcessTransactions(inOnTransactionSucess, inOnTransactionFail, true, transaction);
    }
  }

  private void OnButtonCurrentRules()
  {
    this.ClosePopup();
    UIChampionshipRulesDialog.ShowRollover(Game.instance.player.team.championship, UIChampionshipRulesDialog.Mode.CurrentRules, false, this.mReturnToPopupAction);
  }

  private void OnButtonUpcomingVotes()
  {
    this.ClosePopup();
    UIChampionshipRulesDialog.ShowRollover(Game.instance.player.team.championship, UIChampionshipRulesDialog.Mode.UpcomingVotes, true, this.mReturnToPopupAction);
  }

  public enum Stage
  {
    Choose,
    TrackDetails,
    Propose,
  }
}
