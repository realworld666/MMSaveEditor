
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RaceEventCalendarData
{
  public Circuit circuit;
  public int week;
}
