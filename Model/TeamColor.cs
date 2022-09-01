
using FullSerializer;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TeamColor
{
    public static TeamColor defaultColour = new TeamColor();
    public UIColour primaryUIColour = new UIColour();
    public UIColour secondaryUIColour = new UIColour();
    public StaffColour staffColor = new StaffColour();
    public StaffColour customLogoColor = new StaffColour();
    public HelmetColour helmetColor = new HelmetColour();
    public Color carColor = Color.white;
    public LiveryColour livery = new LiveryColour();
    public Color primaryLiveryOption = new Color();
    public Color secondaryLiveryOption = new Color();
    public Color tertiaryLiveryOption = new Color();
    public Color trimLiveryOption = new Color();
    public Color[] liveryEditorOptions;
    public Color[] lighSponsorOptions;
    public Color[] darkSponsorOptions;
    private int mColorID;

    public int colorID
    {
        get
        {
            return mColorID;
        }
        set
        {
            mColorID = value;
        }
    }

    public class UIColour
    {
        public Color normal = Color.white;
        public Color highlighted = Color.white;
        public Color pressed = Color.white;
        public Color disabled = Color.white;
    }

    public class StaffColour
    {
        public Color primary = Color.white;
        public Color secondary = Color.white;
    }

    public class HelmetColour
    {
        public Color primary = Color.white;
        public Color secondary = Color.white;
        public Color tertiary = Color.white;
    }

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class LiveryColour
    {
        public Color primary = Color.white;
        public Color secondary = Color.white;
        public Color tertiary = Color.white;
        public Color trim = Color.white;
        public Color lightSponsor = Color.white;
        public Color darkSponsor = Color.white;


    }
}
