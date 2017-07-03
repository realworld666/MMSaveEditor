
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class PIDController
{
	private float mProportional;
	private float mIntegral;
	private float mDerivative;
	private float mPreviousError;
	private float mIntegralTerm;
	private float mIntegralTermClamp;

}
