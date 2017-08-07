
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarOpinion
{
    private CarOpinion.Happiness mDriverOverallHappiness;
    private CarOpinion.Happiness mDriverAgainstOtherCarHappiness;
    private CarOpinion.Happiness mDriverAverageHappiness;
    private DialogRule mOverallCarComment;
    private DialogRule mAgainstOtherCarComment;
    private Driver mDriver;
    private float mOverallMoraleHit;
    private float mAgainstOtherCarMoraleHit;
    private readonly float mRandomOverallMoraleHitModifier;
    private readonly float mRandomAgainstOtherCarMoraleHitModifier;

    public enum Happiness
    {
        [LocalisationID("PSG_10002061")] Angry,
        [LocalisationID("PSG_10001551")] Unhappy,
        [LocalisationID("PSG_10002044")] Content,
        [LocalisationID("PSG_10001365")] Happy,
        [LocalisationID("PSG_10010104")] Delighted,
    }

    public enum CommentType
    {
        OverallCarComment,
        AgainstOtherCarComment,
    }
}
