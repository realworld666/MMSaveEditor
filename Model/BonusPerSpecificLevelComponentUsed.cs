
public class BonusPerSpecificLevelComponentUsed : CarPartComponentBonus
{
    public CarPartStats.CarPartStat stat = CarPartStats.CarPartStat.Performance;
    public float statBoost;
    private float mBonusApplied;
    public override void OnPartBuilt(CarPartDesign inDesign, CarPart inPart)
    {
    }
}
