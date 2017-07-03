
public class CarPartUnlockRequirement
{
    public virtual bool IsLocked(Team inTeam)
    {
        return false;
    }

    public virtual string GetDescription(Team inTeam)
    {
        return string.Empty;
    }
}
