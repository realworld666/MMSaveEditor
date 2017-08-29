using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Investor : Entity
{
    public int investorID;
    public int pressure;
    public int logoID;
    public string bonusDescriptionID = string.Empty;
    public long startingMoney;
    public Nationality nationality = new Nationality();
    public Chairman chairman;
    public int fuelEfficiency;
    public int improveability;
    public int tyreWear;
    public int tyreHeating;
    public float bonusMarketability;
    public HQsBuildingInfo.Type[] startBuilding;
    public int partRiskBonus;
    public int driverMinAge;
    public float driverImprovementRateMultiplier;
    public int rewardID;
}
