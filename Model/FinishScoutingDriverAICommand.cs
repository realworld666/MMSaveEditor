
using FullSerializer;

[fsObject("v1", new System.Type[] { }, MemberSerialization = fsMemberSerialization.OptOut)]
internal class FinishScoutingDriverAICommand
{
    private AIScoutingManager manager;
    private AIScoutingManager.DriverScoutingEntry entry;

}
