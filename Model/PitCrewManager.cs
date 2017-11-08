
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PitCrewManager : PersonManager<PitCrewMember>
{
    private PitCrewRegen mPitCrewRegen = new PitCrewRegen();
    private readonly int startingPitCrewTempPool = 10;


}
