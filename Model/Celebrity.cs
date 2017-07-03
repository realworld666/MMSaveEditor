using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Celebrity : Person
{
  public string job = string.Empty;
}
