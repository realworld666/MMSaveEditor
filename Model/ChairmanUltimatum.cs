using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChairmanUltimatum
{
  public int positionExpected;
  public int positionAccomplished;
  public bool complete;
  public bool onGoing;
  public bool shown;
}
