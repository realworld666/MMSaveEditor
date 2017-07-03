
using FullSerializer;

[fsObject("v1", new System.Type[] { }, MemberSerialization = fsMemberSerialization.OptOut)]
internal class FinishScoutingDriverCommand
{
    private ScoutingManager scoutingManager;
    private ScoutingManager.ScoutingEntry entry;


}
