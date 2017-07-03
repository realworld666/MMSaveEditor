
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class FrontendCarData
{
	public FrontendCarData.ModelData modelData = new FrontendCarData.ModelData();
	public FrontendCarData.BlendShapeData blendShapeData = new FrontendCarData.BlendShapeData();
	public TeamColor.LiveryColour colourData = new TeamColor.LiveryColour();
	public LiveryData liveryData = new LiveryData();
	public FrontendCarData.SponsorData sponsorData = new FrontendCarData.SponsorData();

	[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
	public class ModelData
	{
		public static readonly FrontendCarData.ModelData defaultModelData = new FrontendCarData.ModelData() { chassisModelPath = "WGP1/Chassis/Chassis_01/Chassis_01_c001", brakeModelPath = "WGP1/Brakes/Brake_01/Brake_01_c001", wheelModelPath = "WGP1/Wheels/Wheel_01/Wheel_01_c001", frontWingModelPath = "WGP1/Front_Wings/Front_Wing_01/Front_Wing_01_c001", rearWingModelPath = "WGP1/Rear_Wings/Rear_Wing_01/Rear_Wing_01_c001", seatModelPath = "WGP1/Seats/Seat_01/Seat_01_c001", steeringWheelModelPath = "WGP1/Steering_Wheels/Steering_Wheel_01/Steering_Wheel_01_c001" };
		public string chassisModelPath;
		public string brakeModelPath;
		public string wheelModelPath;
		public string frontWingModelPath;
		public string rearWingModelPath;
		public string seatModelPath;
		public string steeringWheelModelPath;

	}

	[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
	public class BlendShapeData
	{
		public static readonly FrontendCarData.BlendShapeData defaultBlendShapeData = new FrontendCarData.BlendShapeData() { blendShapeValues = new float[5] { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f } };
		public float[] blendShapeValues = new float[5];

		public enum BlendShapes
		{
			Nose,
			Engine,
			Fin,
			Aero,
			PodWidth,
			Count,
			Invalid,
		}
	}

	[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
	public class SponsorData
	{
		public static readonly FrontendCarData.SponsorData defaultSponsorData = new FrontendCarData.SponsorData() { sponsorLogoIndex = new int[6] { 1, 2, 3, 4, 5, 6 } };
		public int[] sponsorLogoIndex = new int[6];
	}
}
