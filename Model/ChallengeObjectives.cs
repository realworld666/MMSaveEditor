using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChallengeObjectives
{
    private List<ChallengeObjective> mObjectives = new List<ChallengeObjective>();
}
