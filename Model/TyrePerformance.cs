
using FullSerializer;
using System;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class TyrePerformance : PerformanceImpact
{
	private static TyreSet.Tread[] treadsToValidate = new TyreSet.Tread[3] { TyreSet.Tread.Slick, TyreSet.Tread.LightTread, TyreSet.Tread.HeavyTread };
	private const float lapDistanceMultiplier = 0.6666667f;
	private SessionWeatherDetails mWeatherDetails;
	private TyreDesignData mTyreDesignData;

}
