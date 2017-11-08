using System;
using System.Diagnostics;

public class PoliticalImpactChangeTrack : PoliticalImpact
{
    public PoliticalImpactChangeTrack.ImpactType impactType = PoliticalImpactChangeTrack.ImpactType.AddTrack;
    public Circuit trackAffected;
    public Circuit newTrack;
    public Circuit trackLayout;
    public int trackAffectedWeeknumber;
    private bool mReady;

    public PoliticalImpactChangeTrack(string inName, string inEffect)
    {
        switch (inName)
        {
            case "ChangeTrackLayout":
                this.impactType = PoliticalImpactChangeTrack.ImpactType.ChangeTrackLayout;
                break;
            case "ReplaceTrack":
                this.impactType = PoliticalImpactChangeTrack.ImpactType.TrackReplace;
                break;
            case "AddLayout":
                this.impactType = PoliticalImpactChangeTrack.ImpactType.AddTrackLayout;
                break;
            case "AddTrack":
                this.impactType = PoliticalImpactChangeTrack.ImpactType.AddTrack;
                break;
            case "RemoveTrack":
                this.impactType = PoliticalImpactChangeTrack.ImpactType.RemoveTrack;
                break;
        }
    }

    public override void SetImpact(ChampionshipRules inRules)
    {
        switch (this.impactType)
        {
            case PoliticalImpactChangeTrack.ImpactType.ChangeTrackLayout:
                for (int index = 0; index < inRules.championship.calendarData.Count; ++index)
                {
                    RaceEventCalendarData eventCalendarData = inRules.championship.calendarData[index];
                    if (eventCalendarData.circuit == this.trackAffected)
                    {
                        eventCalendarData.circuit = this.trackLayout;
                        break;
                    }
                    //Console.WriteLine("Rule Impact - " + inRules.championship.GetChampionshipName(false, string.Empty) + " - " + this.impactType.ToString() + " - Track - " + this.trackAffected.locationName), (UnityEngine.Object)null);
                }
                break;
            case PoliticalImpactChangeTrack.ImpactType.TrackReplace:
                Debug.Assert(this.newTrack != null, "Rule Change Track Replace - Track is Null");
                if (this.newTrack == null)
                    return;
                for (int index = 0; index < inRules.championship.calendarData.Count; ++index)
                {
                    RaceEventCalendarData eventCalendarData = inRules.championship.calendarData[index];
                    if (eventCalendarData.circuit == this.trackAffected)
                    {
                        eventCalendarData.circuit = this.newTrack;
                        break;
                    }
                }
                //Debug.Log((object)("Rule Impact - " + inRules.championship.GetChampionshipName(false, string.Empty) + " - " + this.impactType.ToString() + " - Replaced - " + this.trackAffected.locationName + " - With - " + this.newTrack.locationName), (UnityEngine.Object)null);
                break;
            case PoliticalImpactChangeTrack.ImpactType.AddTrack:
            case PoliticalImpactChangeTrack.ImpactType.AddTrackLayout:
                Debug.Assert(this.newTrack != null, "Rule Change Add Track - Track is Null");
                if (this.newTrack == null)
                    return;
                inRules.championship.calendarData.Add(new RaceEventCalendarData()
                {
                    circuit = this.newTrack
                });
                // Debug.Log((object)("Rule Impact - " + inRules.championship.GetChampionshipName(false, string.Empty) + " - " + this.impactType.ToString() + " - " + this.newTrack.locationName), (UnityEngine.Object)null);
                break;
            case PoliticalImpactChangeTrack.ImpactType.RemoveTrack:
                for (int index = 0; index < inRules.championship.calendarData.Count; ++index)
                {
                    RaceEventCalendarData eventCalendarData = inRules.championship.calendarData[index];
                    if (eventCalendarData.circuit == this.trackAffected)
                    {
                        inRules.championship.calendarData.Remove(eventCalendarData);
                        break;
                    }
                }
                //Debug.Log((object)("Rule Impact - " + inRules.championship.GetChampionshipName(false, string.Empty) + " - " + this.impactType.ToString() + " - " + this.trackAffected.locationName), (UnityEngine.Object)null);
                break;
        }
        //inRules.championship.RecalculateNextYearCalendarWeeks();
        //inRules.championship.GenerateNextYearCalendar(false);
        //inRules.championship.SetupNextYearCalendarWeather();
    }

    public override bool VoteCanBeUsed(Championship inChampionship)
    {
        return this.mReady;
    }

    public enum ImpactType
    {
        ChangeTrackLayout,
        TrackReplace,
        AddTrack,
        AddTrackLayout,
        RemoveTrack,
    }
}
