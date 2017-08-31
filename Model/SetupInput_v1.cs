using System;
using System.Collections.Generic;
using FullSerializer;

[fsObject("v1", new System.Type[] { typeof(SessionSetup.SetupInput) }, MemberSerialization = fsMemberSerialization.OptOut)]
public class SetupInput_v1
{
    private static readonly float minSetupInput = -1f;
    private static readonly float maxSetupInput = 1f;
    private Championship.Series mSeries = Championship.Series.Count;
    private Dictionary<SetupInput_v1.SetupInputOptions, float> mSetupComponents;

    public SetupInput_v1(SessionSetup.SetupInput v0)
    {
        this.mSeries = Championship.Series.SingleSeaterSeries;
    }

    public SetupInput_v1()
    {

    }

    public enum SetupInputOptions
    {
        tyrePressureOption,
        tyreCamberOption,
        suspensionStiffnessOption,
        gearRatioOption,
        frontWingAngleOption,
        rearWingAngleOption,
        ballastDistributionOption,
    }
}
