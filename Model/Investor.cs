using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Investor : Entity
{
  public string bonusDescriptionID = string.Empty;
  public Nationality nationality = new Nationality();
  public int investorID;
  public int pressure;
  public int logoID;
  public long startingMoney;
  public Chairman chairman;
  public int fuelEfficiency;
  public int improveability;
  public int tyreWear;
  public int tyreHeating;
  public float bonusMarketability;
  public HQsBuildingInfo.Type[] startBuilding;
}
