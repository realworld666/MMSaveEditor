using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SeasonDirector
{
    private CutCornerSeasonDirector mCutCornerSeasonDirector = new CutCornerSeasonDirector();
    private PitstopSeasonDirector mPitstopSeasonDirector = new PitstopSeasonDirector();
    private bool mIsSetupForSeason;

}
