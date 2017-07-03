using FullSerializer;

[fsObject("v1", new System.Type[] { }, MemberSerialization = fsMemberSerialization.OptOut)]
internal class ChangeScreenCommand
{
    private string screen;
    private Entity focusEntity;

}
