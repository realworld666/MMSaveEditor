
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class RadioMessageTeamOrders : RadioMessage
{
    private float mCooldownTimer;

}
