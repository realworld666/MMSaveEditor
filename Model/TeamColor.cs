
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class TeamColor
{

	public Color carColor;
	public TeamColor.LiveryColour livery = new TeamColor.LiveryColour();
	public Color primaryLiveryOption = new Color();
	public Color secondaryLiveryOption = new Color();
	public Color tertiaryLiveryOption = new Color();
	public Color trimLiveryOption = new Color();
	public Color[] liveryEditorOptions;
	public Color[] lighSponsorOptions;
	public Color[] darkSponsorOptions;
	private int mColorID;

	[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
	public class LiveryColour
	{
		public Color primary;
		public Color secondary;
		public Color tertiary;
		public Color trim;
		public Color lightSponsor;
		public Color darkSponsor;


	}
}
