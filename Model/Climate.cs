
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class Climate
{
	public int[] minimumTemperatures = new int[12];
	public int[] maximumTemperatures = new int[12];
	public float[] clearChance = new float[12];
	public float[] cloudyChance = new float[12];
	public float[] overcastChance = new float[12];
	public float[] stormyChance = new float[12];
	public int[] rainfall = new int[12];
	public Climate.ClimateType climateType;

	public enum ClimateType
	{
		Cool,
		Moderate,
		Warm,
		Tropical,
		Dry,
		Count,
	}
}
