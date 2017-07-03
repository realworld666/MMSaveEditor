// Decompiled with JetBrains decompiler
// Type: TravelOptionType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TravelOptionType
{
  public TravelOptionType.ClassType type = TravelOptionType.ClassType.None;
  public string name = string.Empty;
  public int cost;
  public int sliderValue;
  private float mTravelDistance;

  public string GetStaffString(int inStaff)
  {
    return inStaff.ToString() + " STAFF";
  }

  public int GetCost()
  {
    return this.GetCost(0.0f, 0);
  }

  public int GetCost(int inValue)
  {
    return this.GetCost(0.0f, inValue);
  }

  public int GetCost(float inTravelDistance)
  {
    return this.GetCost(inTravelDistance, 0);
  }

  public int GetCost(float inTravelDistance, int inValue)
  {
    int inValue1 = this.sliderValue;
    if (inValue != 0)
      inValue1 = inValue;
    if ((double) inTravelDistance != 0.0)
      this.mTravelDistance = inTravelDistance;
    if (this.type == TravelOptionType.ClassType.CarLogistics)
      return DesignConstants.carLogisticsCost;
    if (this.type == TravelOptionType.ClassType.Staff)
      return inValue1 * DesignConstants.staffCost;
    if (this.type == TravelOptionType.ClassType.Hospitality)
      return DesignConstants.GetHospitalityTravelCost(inValue1);
    if (this.type != TravelOptionType.ClassType.Flights)
      return 0;
    int num1 = 0;
    int num2 = 0;
    int num3 = Mathf.Clamp(Mathf.RoundToInt(this.mTravelDistance / 1000f), 1, 10);
    switch (inValue1)
    {
      case 1:
        num1 = 100;
        num2 = 200;
        break;
      case 2:
        num1 = 200;
        num2 = 325;
        break;
      case 3:
        num1 = 300;
        num2 = 500;
        break;
      case 4:
        num1 = 500;
        num2 = 875;
        break;
      case 5:
        num1 = 1000;
        num2 = 1500;
        break;
    }
    return num1 * num3 + num2;
  }

  public string GetHospitalityString(int inHospitality)
  {
    string[] strArray = new string[5]{ "PSG_10002243", "PSG_10002244", "PSG_10002245", "PSG_10002246", "PSG_10002247" };
    if (inHospitality >= 0 && inHospitality < strArray.Length)
      return Localisation.LocaliseID(strArray[inHospitality], (GameObject) null);
    return "No String Assigned";
  }

  public string GetFlightsString(int inFlights)
  {
    string[] strArray = new string[5]{ "PSG_10002237", "PSG_10002238", "PSG_10002239", "PSG_10002240", "PSG_10002241" };
    if (inFlights >= 0 && inFlights < strArray.Length)
      return Localisation.LocaliseID(strArray[inFlights], (GameObject) null) + " " + GameUtility.GetCurrencyString((long) this.GetCost(), 0) + " PP";
    return "No String Assigned";
  }

  public enum ClassType
  {
    Staff,
    Flights,
    Hospitality,
    CarLogistics,
    None,
  }

  public enum Hospitality
  {
    CrustySandwiches,
    BoxesOfWine,
    Standard,
    VintageChampagne,
    CelebrityChefBanquet,
    Count,
  }

  public enum Flights
  {
    RoachInfested,
    Cramped,
    Standard,
    Plush,
    FirstClass,
    Count,
  }
}
