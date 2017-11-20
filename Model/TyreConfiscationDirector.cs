
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TyreConfiscationDirector
{
    public TyreConfiscationDirector.ConfiscatedTyre[] confiscatedTyres = new TyreConfiscationDirector.ConfiscatedTyre[GameStatsConstants.qualifyingThresholdForQ3];
    private bool mQualifyingTyresLocked;
    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class ConfiscatedTyre
    {
        private int mTyreIndex = -1;
        private Driver mDriver;
        private SessionStrategy.TyreOption mTyreOption;


    }
}
