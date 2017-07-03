
using FullSerializer;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class Headquarters : Entity
{
	public string sceneName = string.Empty;
	public Nationality nationality = new Nationality();
	public List<HQsBuilding_v1> hqBuildings = new List<HQsBuilding_v1>();
	public List<HQsBuilding_v1> unlockedBuildings = new List<HQsBuilding_v1>();
	public Team team;
	public int headquartersID;
	public int size;
	public int siteSpace;


}
