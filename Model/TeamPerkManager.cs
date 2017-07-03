
using FullSerializer;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class TeamPerkManager
{
	public Dictionary<TeamPerk.Type, bool> perks = new Dictionary<TeamPerk.Type, bool>( (IEqualityComparer<TeamPerk.Type>)new TeamPerk.TypeComparer() );
	private Team mTeam;


}
