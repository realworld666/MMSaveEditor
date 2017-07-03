using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class DisplayEffect
{
  public bool changeDisplay = true;
  public bool changeInterrupt;
  public bool changeUIState;
}
