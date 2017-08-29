
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class AIDefendingBehaviour : AIRacingBehaviour
{
    private RacingVehicle mTarget;
}
