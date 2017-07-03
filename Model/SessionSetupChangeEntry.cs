
using System.Collections.Generic;

public class SessionSetupChangeEntry
{
    public SessionSetupChangeEntry.TyreSlot tyreSlot = SessionSetupChangeEntry.TyreSlot.BackLeft;
    public SessionSetupChangeEntry.Target target;
    public CarPart.PartType partSlot;
    public SessionSetupRepairPart repairPart;
    public float rechargeAmount;
    public bool brokeERS;
    public float expectedTime;
    public float time;
    public float mistakeTime;
    public float pitStopStart;

    public bool isSet
    {
        get
        {
            return (double)this.time > 0.0;
        }
    }

    public float expectedEnd
    {
        get
        {
            return this.pitStopStart + this.expectedTime + this.mistakeTime;
        }
    }

    public float mistakeStart
    {
        get
        {
            return this.pitStopStart + this.time;
        }
    }

    public float mistakeEnd
    {
        get
        {
            return this.pitStopStart + this.time + this.mistakeTime;
        }
    }

    public void Reset()
    {
        this.time = 0.0f;
        this.mistakeTime = 0.0f;
        this.pitStopStart = 0.0f;
        this.rechargeAmount = 0.0f;
        this.brokeERS = false;
    }

    public enum Target
    {
        Fuel,
        Tyres,
        PartsRepair,
        BatteryRecharge,
        Count,
    }

    public class TargetComparer : IEqualityComparer<SessionSetupChangeEntry.Target>
    {
        public bool Equals(SessionSetupChangeEntry.Target x, SessionSetupChangeEntry.Target y)
        {
            return x == y;
        }

        public int GetHashCode(SessionSetupChangeEntry.Target codeh)
        {
            return (int)codeh;
        }
    }

    public enum TyreSlot
    {
        FrontLeft,
        FrontRight,
        BackLeft,
        BackRight,
    }
}
