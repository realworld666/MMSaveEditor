
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class TyreStrategyOption
{
	private SessionStrategy mSessionStrategy;
	private SessionStrategy.TyreOption[] mTyreOptions;
	private TyreSet[] mTyreSets;
	private TyreSet mSelectedTyre;
	private float mTyreTimeCost;
	private float mPitStopTimeCost;
	private bool mIsValidOption;

}
