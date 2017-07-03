// Decompiled with JetBrains decompiler
// Type: PoliticsPlayerVoteStageChooseDetails
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class PoliticsPlayerVoteStageChooseDetails : PoliticsPlayerVoteStage
{
  private List<string> mBannedLocations = new List<string>();
  private List<Circuit> mLeftSelectorCircuits = new List<Circuit>();
  private List<Circuit> mRightSelectorCircuits = new List<Circuit>();
  private List<Circuit> mChampionshipCircuits = new List<Circuit>();
  private List<string> mChampionshipLocations = new List<string>();
  private string mLeftTitle = string.Empty;
  private string mRightTitle = string.Empty;
  public PoliticsPlayerVoteTrackEntry trackSelectorLeft;
  public PoliticsPlayerVoteTrackEntry trackSelectorRight;
  private PoliticalImpactChangeTrack mImpact;

  public override void OnStart()
  {
    this.trackSelectorLeft.OnStart();
    this.trackSelectorRight.OnStart();
  }

  public override void Setup()
  {
    this.mImpact = this.widget.selectedVote.GetImpactOfType<PoliticalImpactChangeTrack>();
    this.mImpact.SetReady(true);
    this.UpdateSelectors();
    this.UpdateCircuitsList();
    this.trackSelectorLeft.Setup(this.mLeftTitle, this.mLeftSelectorCircuits);
    if (!this.UpdateCircuitsLayoutReplace())
      this.trackSelectorRight.Setup(this.mRightTitle, this.mRightSelectorCircuits);
    this.RefreshRuleTracksSelected();
    base.Setup();
  }

  public override void Reset()
  {
    this.trackSelectorLeft.ResetCircuit();
    this.trackSelectorRight.ResetCircuit();
  }

  public override void Hide()
  {
    base.Hide();
  }

  public void OnCircuitSelected()
  {
    this.UpdateCircuitsLayoutReplace();
    this.RefreshRuleTracksSelected();
  }

  private void RefreshRuleTracksSelected()
  {
    switch (this.mImpact.impactType)
    {
      case PoliticalImpactChangeTrack.ImpactType.ChangeTrackLayout:
        this.mImpact.trackAffected = this.trackSelectorLeft.selectedCircuit;
        this.mImpact.trackLayout = this.trackSelectorRight.selectedCircuit;
        break;
      case PoliticalImpactChangeTrack.ImpactType.TrackReplace:
        this.mImpact.newTrack = this.trackSelectorRight.selectedCircuit;
        this.mImpact.trackAffected = this.trackSelectorLeft.selectedCircuit;
        break;
      case PoliticalImpactChangeTrack.ImpactType.AddTrack:
      case PoliticalImpactChangeTrack.ImpactType.AddTrackLayout:
        this.mImpact.newTrack = this.trackSelectorLeft.selectedCircuit;
        break;
      case PoliticalImpactChangeTrack.ImpactType.RemoveTrack:
        this.mImpact.trackAffected = this.trackSelectorLeft.selectedCircuit;
        this.mImpact.trackAffectedWeeknumber = this.widget.championship.calendarData[this.trackSelectorLeft.selectedCircuitIndex].week;
        break;
    }
  }

  private void UpdateSelectors()
  {
    switch (this.mImpact.impactType)
    {
      case PoliticalImpactChangeTrack.ImpactType.ChangeTrackLayout:
        this.mLeftTitle = Localisation.LocaliseID("PSG_10011867", (GameObject) null);
        this.mRightTitle = Localisation.LocaliseID("PSG_10011865", (GameObject) null);
        break;
      case PoliticalImpactChangeTrack.ImpactType.TrackReplace:
        this.mLeftTitle = Localisation.LocaliseID("PSG_10011866", (GameObject) null);
        this.mRightTitle = Localisation.LocaliseID("PSG_10011864", (GameObject) null);
        break;
      case PoliticalImpactChangeTrack.ImpactType.AddTrack:
        this.mLeftTitle = Localisation.LocaliseID("PSG_10011864", (GameObject) null);
        this.mRightTitle = string.Empty;
        break;
      case PoliticalImpactChangeTrack.ImpactType.AddTrackLayout:
        this.mLeftTitle = Localisation.LocaliseID("PSG_10011865", (GameObject) null);
        this.mRightTitle = string.Empty;
        break;
      case PoliticalImpactChangeTrack.ImpactType.RemoveTrack:
        this.mLeftTitle = Localisation.LocaliseID("PSG_10011866", (GameObject) null);
        this.mRightTitle = string.Empty;
        break;
    }
  }

  private void UpdateCircuitsList()
  {
    this.UpdateBannedCircuits();
    this.mLeftSelectorCircuits.Clear();
    this.mRightSelectorCircuits.Clear();
    this.mChampionshipCircuits.Clear();
    this.mChampionshipLocations.Clear();
    for (int index = 0; index < this.widget.championship.calendarData.Count; ++index)
    {
      Circuit circuit = this.widget.championship.calendarData[index].circuit;
      if (!this.mChampionshipCircuits.Contains(circuit) && !this.isCircuitBanned(circuit))
        this.mChampionshipCircuits.Add(circuit);
      if (!this.mChampionshipLocations.Contains(circuit.locationName) && !this.isCircuitBanned(circuit))
        this.mChampionshipLocations.Add(circuit.locationName);
    }
    switch (this.mImpact.impactType)
    {
      case PoliticalImpactChangeTrack.ImpactType.ChangeTrackLayout:
        int count1 = this.mChampionshipCircuits.Count;
        for (int index = 0; index < count1; ++index)
        {
          Circuit championshipCircuit = this.mChampionshipCircuits[index];
          if (this.GetNumberOfLayouts(championshipCircuit) > 0)
            this.mLeftSelectorCircuits.Add(championshipCircuit);
        }
        break;
      case PoliticalImpactChangeTrack.ImpactType.TrackReplace:
        int count2 = this.mChampionshipCircuits.Count;
        for (int index = 0; index < count2; ++index)
          this.mLeftSelectorCircuits.Add(this.mChampionshipCircuits[index]);
        int circuitCount1 = Game.instance.circuitManager.circuitCount;
        for (int index = 0; index < circuitCount1; ++index)
        {
          Circuit circuit = Game.instance.circuitManager.circuits[index];
          if (!this.mChampionshipCircuits.Contains(circuit) && !this.isCircuitBanned(circuit))
            this.mRightSelectorCircuits.Add(circuit);
        }
        break;
      case PoliticalImpactChangeTrack.ImpactType.AddTrack:
        int circuitCount2 = Game.instance.circuitManager.circuitCount;
        for (int index = 0; index < circuitCount2; ++index)
        {
          Circuit circuit = Game.instance.circuitManager.circuits[index];
          if (!this.mChampionshipLocations.Contains(circuit.locationName) && !this.isCircuitBanned(circuit))
            this.mLeftSelectorCircuits.Add(circuit);
        }
        break;
      case PoliticalImpactChangeTrack.ImpactType.AddTrackLayout:
        int circuitCount3 = Game.instance.circuitManager.circuitCount;
        for (int index = 0; index < circuitCount3; ++index)
        {
          Circuit circuit = Game.instance.circuitManager.circuits[index];
          if (!this.mChampionshipCircuits.Contains(circuit) && !this.isCircuitBanned(circuit))
            this.mLeftSelectorCircuits.Add(circuit);
        }
        break;
      case PoliticalImpactChangeTrack.ImpactType.RemoveTrack:
        int count3 = this.mChampionshipCircuits.Count;
        for (int index = 0; index < count3; ++index)
          this.mLeftSelectorCircuits.Add(this.mChampionshipCircuits[index]);
        break;
    }
  }

  private bool UpdateCircuitsLayoutReplace()
  {
    if (this.mImpact.impactType != PoliticalImpactChangeTrack.ImpactType.ChangeTrackLayout)
      return false;
    this.mRightSelectorCircuits.Clear();
    Circuit selectedCircuit = this.trackSelectorLeft.selectedCircuit;
    int circuitCount = Game.instance.circuitManager.circuitCount;
    for (int index = 0; index < circuitCount; ++index)
    {
      Circuit circuit = Game.instance.circuitManager.circuits[index];
      if (circuit != selectedCircuit && circuit.locationName == selectedCircuit.locationName)
        this.mRightSelectorCircuits.Add(circuit);
    }
    this.trackSelectorRight.Setup(this.mRightTitle, this.mRightSelectorCircuits);
    return true;
  }

  private int GetNumberOfLayouts(Circuit inCircuit)
  {
    int num = 0;
    int circuitCount = Game.instance.circuitManager.circuitCount;
    for (int index = 0; index < circuitCount; ++index)
    {
      Circuit circuit = Game.instance.circuitManager.circuits[index];
      if (circuit.locationName == inCircuit.locationName && circuit.trackLayout != inCircuit.trackLayout)
        ++num;
    }
    return num;
  }

  private void UpdateBannedCircuits()
  {
    this.mBannedLocations.Clear();
    int count = this.widget.politicalSystem.votesForSeason.Count;
    for (int index = 0; index < count; ++index)
    {
      PoliticalVote politicalVote = this.widget.politicalSystem.votesForSeason[index];
      if (politicalVote.HasImpactOfType<PoliticalImpactChangeTrack>())
      {
        PoliticalImpactChangeTrack impactOfType = politicalVote.GetImpactOfType<PoliticalImpactChangeTrack>();
        if (impactOfType.newTrack != null && !this.mBannedLocations.Contains(impactOfType.newTrack.locationName))
          this.mBannedLocations.Add(impactOfType.newTrack.locationName);
        if (impactOfType.trackAffected != null && !this.mBannedLocations.Contains(impactOfType.trackAffected.locationName))
          this.mBannedLocations.Add(impactOfType.trackAffected.locationName);
        if (impactOfType.trackLayout != null && !this.mBannedLocations.Contains(impactOfType.trackLayout.locationName))
          this.mBannedLocations.Add(impactOfType.trackLayout.locationName);
      }
    }
  }

  private bool isCircuitBanned(string inLocationName)
  {
    return this.mBannedLocations.Contains(inLocationName);
  }

  private bool isCircuitBanned(Circuit inCircuit)
  {
    return this.mBannedLocations.Contains(inCircuit.locationName);
  }
}
