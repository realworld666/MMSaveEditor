using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TrophyHistory
{
    public List<Trophy> trophies = new List<Trophy>();
}
