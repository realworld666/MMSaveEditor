using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SeasonDirector
{
    private CutCornerSeasonDirector mCutCornerSeasonDirector = new CutCornerSeasonDirector();
    private bool mIsSetupForSeason;

}
