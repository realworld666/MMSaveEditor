using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CelebrityManager : PersonManager<Celebrity>
{
}
