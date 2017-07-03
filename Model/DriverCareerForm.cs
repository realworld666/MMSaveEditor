using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class DriverCareerForm
{
	private static int maxFormCount = 5;
	public static float maxFormRating = 10f;
	public static float minFormRating = 0.0f;
	private static EasingUtility.Easing[] availableFormCurves = new EasingUtility.Easing[12] { EasingUtility.Easing.InQuad, EasingUtility.Easing.OutQuad, EasingUtility.Easing.InOutQuad, EasingUtility.Easing.OutInQuad, EasingUtility.Easing.InCubic, EasingUtility.Easing.OutCubic, EasingUtility.Easing.InOutCubic, EasingUtility.Easing.OutInCubic, EasingUtility.Easing.InSin, EasingUtility.Easing.OutSin, EasingUtility.Easing.InOutSin, EasingUtility.Easing.OutInSin };
	public float[] formValues = new float[DriverCareerForm.maxFormCount];
	private float mFormCurveProgress = 1f;
	private DriverCareerForm.Form mForm = DriverCareerForm.Form.Medium;
	private float mTopFormRating;
	private float mBottomFormRating;
	private float mFormCurveUnit;
	private EasingUtility.Easing mFormCurve;
	public enum Form
	{
		High,
		Medium,
		Low,
		Count,
	}
}
