
using System.Collections.Generic;

public class PartTypeSlotSettings
{
    public CarPart.PartType partType;
    public int baseMinStat;
    public int baseMaxStat;
    public int championshipMinStat;
    public int championshipMaxStat;
    public float buildTimeDays;
    public float materialsCost;
    public float[] timePerLevel = new float[GameStatsConstants.slotCount];
    public float[] costPerLevel = new float[GameStatsConstants.slotCount];
    public CarPartUnlockRequirement[] unlockRequirements = new CarPartUnlockRequirement[GameStatsConstants.slotCount];

}
