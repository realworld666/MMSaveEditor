
using FullSerializer;

[fsObject("v1", new System.Type[] { }, MemberSerialization = fsMemberSerialization.OptOut)]
internal class FinishScoutingEngineerAICommand
{
    private AIScoutingManager manager;
    private AIScoutingManager.EngineerScoutingEntry entry;

}
