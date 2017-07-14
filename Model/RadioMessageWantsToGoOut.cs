
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class RadioMessageWantsToGoOut : RadioMessage
{
    private float mTimeUntilNextMessage;
    private float mDriverFeedbackRating;


}
