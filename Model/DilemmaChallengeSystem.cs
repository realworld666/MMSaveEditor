using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class DilemmaChallengeSystem
{
    private DilemmaSystem mOwner;
    private List<string> mNextChainedDilemmaPSGs;

}
