
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class HQsDependency
{
	public HQsBuildingInfo.Type buildingType;
	public int requiredLevel;

}
