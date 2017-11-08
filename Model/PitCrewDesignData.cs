
using FullSerializer;
using System.Xml.Serialization;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PitCrewDesignData
{
    [XmlElement("FixingPartsTaskTimes")]
    public FixingPartsTaskTimes fixingPartsTaskTimes = new FixingPartsTaskTimes();
    [XmlElement("PitCrewFundings")]
    public PitCrewFundings pitCrewFundings = new PitCrewFundings();
    [XmlElement("PitCrewContractPerRaceCosts")]
    public PitCrewContractPerRaceCosts pitCrewContractPerRaceCosts = new PitCrewContractPerRaceCosts();
    [XmlElement("MinimumFrontJackTaskTime")]
    public float minFrontJackTime;
    [XmlElement("MedianFrontJackTaskTime")]
    public float medFrontJackTime;
    [XmlElement("MaximumFrontJackTaskTime")]
    public float maxFrontJackTime;
    [XmlElement("MinimumRearJackTaskTime")]
    public float minRearJackTime;
    [XmlElement("MedianRearJackTaskTime")]
    public float medRearJackTime;
    [XmlElement("MaximumRearJackTaskTime")]
    public float maxRearJackTime;
    [XmlElement("MinimumTyresTaskTime")]
    public float minTyresTime;
    [XmlElement("MedianTyresTaskTime")]
    public float medTyresTime;
    [XmlElement("MaximumTyresTaskTime")]
    public float maxTyresTime;
    [XmlElement("MinimumRefuellingTaskTime")]
    public float minRefuellingTime;
    [XmlElement("MedianRefuellingTaskTime")]
    public float medRefuellingTime;
    [XmlElement("MaximumRefuellingTaskTime")]
    public float maxRefuellingTime;
    [XmlElement("MinimumBatteryTaskTime")]
    public float minBatteryChargeTime;
    [XmlElement("MedianBatteryTaskTime")]
    public float medBatteryChargeTime;
    [XmlElement("MaximumBatteryTaskTime")]
    public float maxBatteryChargeTime;
    [XmlElement("SafeStrategyErrorPercent")]
    public float safeStrategyErrorPercent;
    [XmlElement("BalancedStrategyErrorPercent")]
    public float balancedStrategyErrorPercent;
    [XmlElement("FastStrategyErrorPercent")]
    public float fastStrategyErrorPercent;
    [XmlElement("FrontJackConfidenceDecrease")]
    public float frontJackConfidenceDecrease;
    [XmlElement("RearJackConfidenceDecrease")]
    public float rearJackConfidenceDecrease;
    [XmlElement("TyresConfidenceDecrease")]
    public float tyresConfidenceDecrease;
    [XmlElement("FixingPartsConfidenceDecrease")]
    public float fixingConfidenceDecrease;
    [XmlElement("RefuellingConfidenceDecrease")]
    public float refuellingConfidenceDecrease;
    [XmlElement("FrontJackConfidenceDecreaseMistake")]
    public float frontJackConfidenceDecreaseMistake;
    [XmlElement("RearJackConfidenceDecreaseMistake")]
    public float rearJackConfidenceDecreaseMistake;
    [XmlElement("TyresConfidenceDecreaseMistake")]
    public float tyresConfidenceDecreaseMistake;
    [XmlElement("FixingPartsConfidenceDecreaseMistake")]
    public float fixingConfidenceDecreaseMistake;
    [XmlElement("RefuellingConfidenceDecreaseMistake")]
    public float refuellingConfidenceDecreaseMistake;
    [XmlElement("LowPitCrewFundingConfidenceIncrease")]
    public float lowFundingIncreaseRate;
    [XmlElement("MediumPitCrewFundingConfidenceIncrease")]
    public float mediumFundingIncreaseRate;
    [XmlElement("HighPitCrewFundingConfidenceIncrease")]
    public float highFundingIncreaseRate;
    [XmlElement("ContractSignOnFee")]
    public long contractSignOnFee;
    [XmlElement("MaximumPitCrewSize")]
    public int maximumPitCrewSize;
    [XmlElement("MaximumPitCrewApplicants")]
    public int maximumPitCrewApplicantsNumber;
    [XmlElement("NumberOfRacesToRemoveApplicant")]
    public int numberOfRacesToRemoveApplicant;
    [XmlElement("MinMonthsToPeakAge")]
    public int minNumberOfMonthBeforePeakAge;
    [XmlElement("MaxMonthsToPeakAge")]
    public int maxNumberOfMonthBeforePeakAge;


}
