using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarPartCondition
{
    private static readonly Dictionary<CarPart.PartType, float> pitRepairPartConditionMin = new Dictionary<CarPart.PartType, float>() { { CarPart.PartType.Brakes, 0.3f }, { CarPart.PartType.Engine, 0.2f }, { CarPart.PartType.FrontWing, 1f }, { CarPart.PartType.Gearbox, 0.2f }, { CarPart.PartType.RearWing, 0.7f }, { CarPart.PartType.Suspension, 0.4f }, { CarPart.PartType.RearWingGT, 0.7f }, { CarPart.PartType.BrakesGT, 0.3f }, { CarPart.PartType.EngineGT, 0.2f }, { CarPart.PartType.GearboxGT, 0.2f }, { CarPart.PartType.SuspensionGT, 0.4f } };
    private static readonly Dictionary<CarPart.PartType, float> pitRepairPartConditionMax = new Dictionary<CarPart.PartType, float>() { { CarPart.PartType.Brakes, 0.5f }, { CarPart.PartType.Engine, 0.4f }, { CarPart.PartType.FrontWing, 1f }, { CarPart.PartType.Gearbox, 0.4f }, { CarPart.PartType.RearWing, 1f }, { CarPart.PartType.Suspension, 0.6f }, { CarPart.PartType.RearWingGT, 1f }, { CarPart.PartType.BrakesGT, 0.5f }, { CarPart.PartType.EngineGT, 0.4f }, { CarPart.PartType.GearboxGT, 0.4f }, { CarPart.PartType.SuspensionGT, 0.6f } };
    private static readonly Dictionary<CarPart.PartType, float> pitRepairPartTimeMin = new Dictionary<CarPart.PartType, float>() { { CarPart.PartType.Brakes, 6f }, { CarPart.PartType.Engine, 15f }, { CarPart.PartType.FrontWing, 4f }, { CarPart.PartType.Gearbox, 15f }, { CarPart.PartType.RearWing, 6f }, { CarPart.PartType.Suspension, 8f }, { CarPart.PartType.RearWingGT, 6f }, { CarPart.PartType.BrakesGT, 6f }, { CarPart.PartType.EngineGT, 15f }, { CarPart.PartType.GearboxGT, 15f }, { CarPart.PartType.SuspensionGT, 8f } };
    private static readonly Dictionary<CarPart.PartType, float> pitRepairPartTimeMax = new Dictionary<CarPart.PartType, float>() { { CarPart.PartType.Brakes, 10f }, { CarPart.PartType.Engine, 30f }, { CarPart.PartType.FrontWing, 8f }, { CarPart.PartType.Gearbox, 30f }, { CarPart.PartType.RearWing, 10f }, { CarPart.PartType.Suspension, 12f }, { CarPart.PartType.RearWingGT, 10f }, { CarPart.PartType.BrakesGT, 10f }, { CarPart.PartType.EngineGT, 30f }, { CarPart.PartType.GearboxGT, 30f }, { CarPart.PartType.SuspensionGT, 12f } };
    private float mCondition = 1f;
    private bool mRefreshPitRepairAmount = true;
    private List<CarPartComponentBonus> mBonus = new List<CarPartComponentBonus>();
    private const float mechanicRepairOffsetMin = 0.05f;
    private const float mechanicRepairOffsetMax = 0.1f;
    private CarPartCondition.PartState mState;
    private float mRedBand;
    private float mActiveSessionRedZone;
    private bool mRepairInPit;
    private float mPitRepairAmount;
    private CarPart mPart;
    public enum PartState
    {
        Optimal,
        Failure,
        CatastrophicFailure,
    }

    public float condition
    {
        get
        {
            return this.mCondition;
        }
    }

    public float redZone
    {
        get
        {
            return this.mRedBand;
        }
        set
        {
            this.mRedBand = value;
            this.mActiveSessionRedZone = value;
        }
    }

    public void SetCondition(float inCondition)
    {
        this.mCondition = inCondition;
        this.mState = CarPartCondition.PartState.Optimal;
    }

    public void Setup(CarPart inPart)
    {
        this.mPart = inPart;
    }
}
