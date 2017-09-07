public class PoliticalImpactChangeTrack : PoliticalImpact
{
    public PoliticalImpactChangeTrack.ImpactType impactType = PoliticalImpactChangeTrack.ImpactType.AddTrack;
    public Circuit trackAffected;
    public Circuit newTrack;
    public Circuit trackLayout;
    public int trackAffectedWeeknumber;
    private bool mReady;
    private string inName;
    private string inEffect;

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

    public enum ImpactType
    {
        ChangeTrackLayout,
        TrackReplace,
        AddTrack,
        AddTrackLayout,
        RemoveTrack,
    }
}
