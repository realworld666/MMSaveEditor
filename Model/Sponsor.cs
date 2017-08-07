
using System;
using System.Collections.Generic;
using FullSerializer;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Sponsor : Entity
{
    public Sponsor.Category category;
    private static int maxOffersPerFind = 6;
    public Sponsor.Difficulty difficulty = Sponsor.Difficulty.Medium;
    public Nationality nationality = new Nationality();
    public Person liaison = new Person();
    public int logoIndex;
    public int prestigeLevel;
    public float homeBonusMultiplier = 1f;
    public int bonusTarget;
    public int upfrontValue;
    public int totalBonusAmount;
    public int perRacePayment;
    public Color sponsorColor;
    public SponsorSlot.SlotType slotSponsoring = SponsorSlot.SlotType.AirIntake;
    public int offerTimer;
    public int offerCooldown;
    public int contractTotalRaces;
    private Dictionary<Team, DateTime> mTeamsIgnored = new Dictionary<Team, DateTime>();
    private DateTime mFindContractsDate = new DateTime();


    public enum Category
    {
        AlcoholicDrinks,
        Appliances,
        Automotive,
        Banking,
        Clothing,
        Fashion,
        Food,
        Games,
        Media,
        Oil,
        Security,
        Technology,
        Telecoms,
        Travel,
    }

    public enum Difficulty
    {
        [LocalisationID("PSG_10011056")] VeryEasy,
        [LocalisationID("PSG_10010654")] Easy,
        [LocalisationID("PSG_10010655")] Medium,
        [LocalisationID("PSG_10010656")] Hard,
        [LocalisationID("PSG_10011057")] VeryHard,
        Count,
    }
}
