
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TeamPrincipalManager : PersonManager<TeamPrincipal>
{
}
