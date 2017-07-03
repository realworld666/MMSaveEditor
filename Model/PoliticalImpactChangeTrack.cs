public class PoliticalImpactChangeTrack : PoliticalImpact
{
    public PoliticalImpactChangeTrack.ImpactType impactType = PoliticalImpactChangeTrack.ImpactType.AddTrack;
    public Circuit trackAffected;
    public Circuit newTrack;
    public Circuit trackLayout;
    public int trackAffectedWeeknumber;
    private bool mReady;

    public enum ImpactType
    {
        ChangeTrackLayout,
        TrackReplace,
        AddTrack,
        AddTrackLayout,
        RemoveTrack,
    }
}
