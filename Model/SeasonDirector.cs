using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SeasonDirector
{
    private CutCornerSeasonDirector mCutCornerSeasonDirector = new CutCornerSeasonDirector();
    private PitstopSeasonDirector mPitstopSeasonDirector;
    private bool mIsSetupForSeason;

}
