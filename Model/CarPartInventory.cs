using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarPartInventory
{
    public List<CarPart> brakesInventory = new List<CarPart>();
    public List<CarPart> engineInventory = new List<CarPart>();
    public List<CarPart> frontWingInventory = new List<CarPart>();
    public List<CarPart> gearboxInventory = new List<CarPart>();
    public List<CarPart> rearWingInventory = new List<CarPart>();
    public List<CarPart> suspensionInventory = new List<CarPart>();
    public List<CarPart> rearWingGTInventory = new List<CarPart>();
    public List<CarPart> brakesGTInventory = new List<CarPart>();
    public List<CarPart> engineGTInventory = new List<CarPart>();
    public List<CarPart> gearboxGTInventory = new List<CarPart>();
    public List<CarPart> suspensionGTInventory = new List<CarPart>();
    private List<CarPart> brakesGETInventory = new List<CarPart>();
    private List<CarPart> engineGETInventory = new List<CarPart>();
    private List<CarPart> frontWingGETInventory = new List<CarPart>();
    private List<CarPart> gearboxGETInventory = new List<CarPart>();
    private List<CarPart> rearWingGETInventory = new List<CarPart>();
    private List<CarPart> suspensionGETInventory = new List<CarPart>();
    public List<List<CarPart>> inventories = new List<List<CarPart>>();

    public List<CarPart> GetPartInventory(CarPart.PartType inPartType)
    {
        switch (inPartType)
        {
            case CarPart.PartType.Brakes:
                return this.brakesInventory;
            case CarPart.PartType.Engine:
                return this.engineInventory;
            case CarPart.PartType.FrontWing:
                return this.frontWingInventory;
            case CarPart.PartType.Gearbox:
                return this.gearboxInventory;
            case CarPart.PartType.RearWing:
                return this.rearWingInventory;
            case CarPart.PartType.Suspension:
                return this.suspensionInventory;
            case CarPart.PartType.RearWingGT:
                return this.rearWingGTInventory;
            case CarPart.PartType.BrakesGT:
                return this.brakesGTInventory;
            case CarPart.PartType.EngineGT:
                return this.engineGTInventory;
            case CarPart.PartType.GearboxGT:
                return this.gearboxGTInventory;
            case CarPart.PartType.SuspensionGT:
                return this.suspensionGTInventory;
            default:
                return (List<CarPart>)null;
        }
    }

    public float GetHighestStatOfType(CarPart.PartType inType, CarPartStats.CarPartStat inStat)
    {
        CarPart partWithHighestStat = this.GetPartWithHighestStat(inType, inStat);
        if (partWithHighestStat == null)
            return 0.0f;
        return partWithHighestStat.stats.statWithPerformance;
    }

    public CarPart GetPartWithHighestStat(CarPart.PartType inType, CarPartStats.CarPartStat inStat)
    {
        CarPart carPart = (CarPart)null;
        List<CarPart> partInventory = this.GetPartInventory(inType);
        for (int index = 0; index < partInventory.Count; ++index)
        {
            switch (inStat)
            {
                case CarPartStats.CarPartStat.MainStat:
                    if (!partInventory[index].IsBanned && (carPart == null || (double)partInventory[index].stats.statWithPerformance > (double)carPart.stats.statWithPerformance))
                    {
                        carPart = partInventory[index];
                        break;
                    }
                    break;
                case CarPartStats.CarPartStat.Reliability:
                    if (!partInventory[index].IsBanned && (carPart == null || (double)partInventory[index].stats.reliability > (double)carPart.stats.reliability))
                    {
                        carPart = partInventory[index];
                        break;
                    }
                    break;
            }
        }
        return carPart;
    }

    public CarPart GetHighestStatPartOfType(CarPart.PartType inType)
    {
        CarPart carPart = (CarPart)null;
        List<CarPart> partInventory = this.GetPartInventory(inType);
        for (int index = 0; index < partInventory.Count; ++index)
        {
            if (!partInventory[index].IsBanned && (carPart == null || (double)partInventory[index].stats.statWithPerformance > (double)carPart.stats.statWithPerformance))
                carPart = partInventory[index];
        }
        return carPart;
    }

    public void AddPart(CarPart inPart)
    {
        switch (inPart.GetPartType())
        {
            case CarPart.PartType.Brakes:
                this.brakesInventory.Add(inPart);
                break;
            case CarPart.PartType.Engine:
                this.engineInventory.Add(inPart);
                break;
            case CarPart.PartType.FrontWing:
                this.frontWingInventory.Add(inPart);
                break;
            case CarPart.PartType.Gearbox:
                this.gearboxInventory.Add(inPart);
                break;
            case CarPart.PartType.RearWing:
                this.rearWingInventory.Add(inPart);
                break;
            case CarPart.PartType.Suspension:
                this.suspensionInventory.Add(inPart);
                break;
            case CarPart.PartType.RearWingGT:
                this.rearWingGTInventory.Add(inPart);
                break;
            case CarPart.PartType.BrakesGT:
                this.brakesGTInventory.Add(inPart);
                break;
            case CarPart.PartType.EngineGT:
                this.engineGTInventory.Add(inPart);
                break;
            case CarPart.PartType.GearboxGT:
                this.gearboxGTInventory.Add(inPart);
                break;
            case CarPart.PartType.SuspensionGT:
                this.suspensionGTInventory.Add(inPart);
                break;
        }
    }

    public void DestroyParts(CarPart.PartType inType)
    {
        List<CarPart> partInventory = this.GetPartInventory(inType);
        for (int index = 0; index < partInventory.Count; ++index)
        {
            CarPart carPart = partInventory[index];
            partInventory[index] = (CarPart)null;
            carPart.DestroyCarPart();
        }
        partInventory.Clear();
    }
}
