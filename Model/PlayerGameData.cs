
using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PlayerGameData
{
    private List<int> mRulesExposedToThePlayer = new List<int>();
    private bool mHasBeenExposedToWeightStripping;
    private bool mHasBeenExposedToEnduranceMail;
    private bool mHasBeenExposedToPitCrewMail;
}
