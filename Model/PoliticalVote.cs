
using FullSerializer;
using System.Collections.Generic;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PoliticalVote
{
    public static List<string> groupsNotAvailableForNonMainConcurrentChampionships;

    private string mName = string.Empty;
    private string mDescription = string.Empty;
    public string group = string.Empty;
    public string effectType = string.Empty;
    public int ID;
    public List<PoliticalImpact> impacts = new List<PoliticalImpact>();
    public List<PoliticalVote.TeamCharacteristics> benificialCharacteristics = new List<PoliticalVote.TeamCharacteristics>();
    public List<PoliticalVote.TeamCharacteristics> detrimentalCharacteristics = new List<PoliticalVote.TeamCharacteristics>();
    public CarPart.PartType currentPartType = CarPart.PartType.None;
    public DialogQuery messageCriteria = new DialogQuery();
    public DilemmaSystem.BribedOption playerBribe;
    private bool mDisplayRule = true;
    private Championship mChampionship;
    private bool mLockedToPlayerVote;

    public string nameID
    {
        get
        {
            return this.mName;
        }
        set
        {
            this.mName = value;
        }
    }

    public string LocalisedName
    {
        get
        {
            return Localisation.LocaliseID(mName);
        }
    }

    public string LocalisedDescription
    {
        get
        {
            return Localisation.LocaliseID(mDescription);
        }
    }

    public string descriptionID
    {
        get
        {
            return this.mDescription;
        }
        set
        {
            this.mDescription = value;
        }
    }

    public bool displayRule
    {
        get
        {
            return this.mDisplayRule;
        }
        set
        {
            this.mDisplayRule = value;
        }
    }

    public Championship championship
    {
        get
        {
            return this.mChampionship;
        }
    }

    public bool lockedToPlayerVote
    {
        get
        {
            return this.mLockedToPlayerVote;
        }
        set
        {
            this.mLockedToPlayerVote = value;
        }
    }

    public enum TeamImpact
    {
        Beneficial,
        Neutral,
        Detrimental,
    }

    public enum TeamCharacteristics
    {
        [LocalisationID("PSG_10008463")] Traditionalist,
        [LocalisationID("PSG_10008464")] Progressive,
        [LocalisationID("PSG_10008465")] Egalitarian,
        [LocalisationID("PSG_10008466")] Gimmicky,
        [LocalisationID("PSG_10008467")] TrackStatsFavourable,
        [LocalisationID("PSG_10008468")] TrackStatsUnfavourable,
        [LocalisationID("PSG_10008467")] NewTrackStatsFavourable,
        [LocalisationID("PSG_10008526")] OldTrackStatsUnfavourable,
        [LocalisationID("PSG_10008522")] NewLayoutStatsFavourable,
        [LocalisationID("PSG_10008523")] NewLayoutStatsUnfavourable,
        [LocalisationID("PSG_10008525")] OldTrackStatsFavourable,
        [LocalisationID("PSG_10008469")] TeamQualityLow,
        [LocalisationID("PSG_10008470")] TeamQualityAverage,
        [LocalisationID("PSG_10008471")] TeamQualityHigh,
        [LocalisationID("PSG_10008472")] TeamPositionTop,
        [LocalisationID("PSG_10008473")] TeamPositionHigh,
        [LocalisationID("PSG_10008474")] TeamPositionAverage,
        [LocalisationID("PSG_10008475")] TeamPositionLow,
        [LocalisationID("PSG_10008476")] TeamBudgetLow,
        [LocalisationID("PSG_10008477")] TeamBudgetAverage,
        [LocalisationID("PSG_10008478")] TeamBudgetHigh,
        [LocalisationID("PSG_10008482")] CorneringSpeedVeryHigh,
        [LocalisationID("PSG_10008483")] CorneringSpeedHigh,
        [LocalisationID("PSG_10008484")] CorneringSpeedAverage,
        [LocalisationID("PSG_10008485")] CorneringSpeedLow,
        [LocalisationID("PSG_10008486")] CorneringSpeedVeryLow,
        [LocalisationID("PSG_10008487")] TyreWearRateBad,
        [LocalisationID("PSG_10008488")] TyreWearRateGood,
        [LocalisationID("PSG_10008489")] FuelBurnRateBad,
        [LocalisationID("PSG_10008490")] FuelBurnRateGood,
        [LocalisationID("PSG_10008492")] TeamDriverQualityLow,
        [LocalisationID("PSG_10008491")] TeamDriverQualityHigh,
    }

    public void Initialize(Championship inChampionship)
    {
        this.mChampionship = inChampionship;
        //for (int index = 0; index < this.impacts.Count; ++index)
        //  this.impacts[index].Initialize(inChampionship);
    }

    public bool HasImpactOfType<T>()
    {
        int count = this.impacts.Count;
        for (int index = 0; index < count; ++index)
        {
            if (this.impacts[index] is T)
                return true;
        }
        return false;
    }

    public bool CanBeUsed()
    {
        if (this.mChampionship == null)
        {
            Console.WriteLine("Championship reference is null, using Player Championship");
            this.mChampionship = Game.instance.player.team.championship;
        }
        for (int index = 0; index < this.impacts.Count; ++index)
        {
            if (!this.impacts[index].VoteCanBeUsed(this.mChampionship))
                return false;
        }
        return true;
    }

    public void ApplyImpacts(ChampionshipRules inRules)
    {
        for (int index = 0; index < this.impacts.Count; ++index)
            this.impacts[index].SetImpact(inRules);
    }
}
