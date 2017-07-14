
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class RadioMessageSetupFeedback : RadioMessage
{
    private float mTimeUntilNextFeedback;
    private float mDriverFeedbackRating;


}
