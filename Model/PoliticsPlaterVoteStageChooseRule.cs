// Decompiled with JetBrains decompiler
// Type: PoliticsPlaterVoteStageChooseRule
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PoliticsPlaterVoteStageChooseRule : PoliticsPlayerVoteStage
{
  private List<PoliticalVote> mVotes = new List<PoliticalVote>();
  public UIGridList grid;
  public ToggleGroup toggleGroup;
  public ScrollRect scrollRect;

  public override void OnStart()
  {
  }

  public override void Setup()
  {
    this.UpdateAvailableVotesList();
    this.SetGrid();
    base.Setup();
  }

  public override void Hide()
  {
    base.Hide();
  }

  public override bool isReady()
  {
    return this.widget.selectedVote != null;
  }

  private void LateUpdate()
  {
    if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
      return;
    GameObject selectedGameObject = EventSystem.current.currentSelectedGameObject;
    for (int inIndex = 0; inIndex < this.grid.itemCount; ++inIndex)
    {
      GameObject gameObject = this.grid.GetItem(inIndex);
      if (gameObject.activeSelf && (Object) selectedGameObject == (Object) gameObject)
      {
        GameUtility.SnapScrollRectTo(gameObject.GetComponent<RectTransform>(), this.scrollRect, GameUtility.AnchorType.Y, GameUtility.AnchorLocation.Center);
        break;
      }
    }
  }

  private void SetGrid()
  {
    this.toggleGroup.SetAllTogglesOff();
    this.grid.DestroyListItems();
    GameUtility.SetActive(this.grid.itemPrefab, true);
    int count = this.mVotes.Count;
    for (int index = 0; index < count; ++index)
    {
      PoliticsPlayerVoteEntry listItem = this.grid.CreateListItem<PoliticsPlayerVoteEntry>();
      PoliticalVote mVote = this.mVotes[index];
      listItem.Setup(mVote, this.widget.selectedVote != null && this.widget.selectedVote.ID == mVote.ID);
    }
    GameUtility.SetActive(this.grid.itemPrefab, false);
  }

  private void UpdateAvailableVotesList()
  {
    this.mVotes.Clear();
    List<int> intList = new List<int>((IEnumerable<int>) App.instance.votesManager.voteIDs);
    int count = intList.Count;
    for (int index = 0; index < count; ++index)
    {
      PoliticalVote inVote = App.instance.votesManager.votes[intList[index]].Clone();
      inVote.Initialize(this.widget.championship);
      if (inVote.HasImpactOfType<PoliticalImpactChangeTrack>())
      {
        PoliticalImpactChangeTrack impactOfType = inVote.GetImpactOfType<PoliticalImpactChangeTrack>();
        if (impactOfType.CanTrackImpactBeApplied(this.widget.championship) && this.CanAddTrackImpact(impactOfType, this.widget.championship))
          this.mVotes.Add(inVote);
      }
      else if (!this.widget.politicalSystem.HasVote(inVote) && this.widget.politicalSystem.CanVoteBeUsed(inVote, this.widget.politicalSystem.votesForSeason) && inVote.CanBeUsed())
        this.mVotes.Add(inVote);
    }
  }

  private bool CanAddTrackImpact(PoliticalImpactChangeTrack inImpact, Championship inChampionship)
  {
    int num = inChampionship.eventCount + this.GetImpactEventCount(inImpact);
    int count = inChampionship.politicalSystem.votesForSeason.Count;
    for (int index = count - inChampionship.politicalSystem.votesRemaining; index < count; ++index)
    {
      PoliticalVote politicalVote = inChampionship.politicalSystem.votesForSeason[index];
      if (politicalVote.HasImpactOfType<PoliticalImpactChangeTrack>())
        num += this.GetImpactEventCount(politicalVote.GetImpactOfType<PoliticalImpactChangeTrack>());
    }
    if (num >= 10)
      return num <= 16;
    return false;
  }

  private int GetImpactEventCount(PoliticalImpactChangeTrack inImpact)
  {
    if (inImpact.impactType == PoliticalImpactChangeTrack.ImpactType.AddTrack || inImpact.impactType == PoliticalImpactChangeTrack.ImpactType.AddTrackLayout)
      return 1;
    return inImpact.impactType == PoliticalImpactChangeTrack.ImpactType.RemoveTrack ? -1 : 0;
  }
}
