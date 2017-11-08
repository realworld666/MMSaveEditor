using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class DriverStatsProgression
{
    public string progressionType;
    public float braking;
    public float cornering;
    public float smoothness;
    public float overtaking;
    public float consistency;
    public float adaptability;
    public float fitness;
    public float feedback;
    public float focus;


    public DriverStatsProgression()
    {
    }

    public DriverStatsProgression(float inBraking, float inCornering, float inSmoothness, float inOvertaking, float inConsistency, float inAdapatability, float inFitness, float inFeedback, float inFocus)
    {
        this.braking = inBraking;
        this.cornering = inCornering;
        this.smoothness = inSmoothness;
        this.overtaking = inOvertaking;
        this.consistency = inConsistency;
        this.adaptability = inAdapatability;
        this.fitness = inFitness;
        this.feedback = inFeedback;
        this.focus = inFocus;
    }

    public DriverStatsProgression(DriverStatsProgression inProgression, float inAmount)
    {
        this.braking = inProgression.braking * inAmount;
        this.cornering = inProgression.cornering * inAmount;
        this.smoothness = inProgression.smoothness * inAmount;
        this.overtaking = inProgression.overtaking * inAmount;
        this.consistency = inProgression.consistency * inAmount;
        this.adaptability = inProgression.adaptability * inAmount;
        this.fitness = inProgression.fitness * inAmount;
        this.feedback = inProgression.feedback * inAmount;
        this.focus = inProgression.focus * inAmount;
    }

    public DriverStatsProgression(DatabaseEntry inEntry, string inType)
    {
        if (inEntry == null)
            return;
        this.progressionType = inType;
        this.braking = (float)inEntry.GetIntValue("Braking") / 100f;
        this.cornering = (float)inEntry.GetIntValue("Cornering") / 100f;
        this.smoothness = (float)inEntry.GetIntValue("Smoothness") / 100f;
        this.overtaking = (float)inEntry.GetIntValue("Overtaking") / 100f;
        this.consistency = (float)inEntry.GetIntValue("Consistency") / 100f;
        this.adaptability = (float)inEntry.GetIntValue("Adaptability") / 100f;
        this.fitness = (float)inEntry.GetIntValue("Fitness") / 100f;
        this.feedback = (float)inEntry.GetIntValue("Feedback") / 100f;
        this.focus = (float)inEntry.GetIntValue("Focus") / 100f;
    }

    public DriverStatsProgression(DatabaseEntry inEntry, int inIndex, string inType)
    {
        if (inEntry == null)
            return;
        string stringValue1 = inEntry.GetStringValue("Braking");
        string stringValue2 = inEntry.GetStringValue("Cornering");
        string stringValue3 = inEntry.GetStringValue("Smoothness");
        string stringValue4 = inEntry.GetStringValue("Overtaking");
        string stringValue5 = inEntry.GetStringValue("Consistency");
        string stringValue6 = inEntry.GetStringValue("Adaptability");
        string stringValue7 = inEntry.GetStringValue("Fitness");
        string stringValue8 = inEntry.GetStringValue("Feedback");
        string stringValue9 = inEntry.GetStringValue("Focus");
        string[] strArray1 = stringValue1.Split(';');
        string[] strArray2 = stringValue2.Split(';');
        string[] strArray3 = stringValue3.Split(';');
        string[] strArray4 = stringValue4.Split(';');
        string[] strArray5 = stringValue5.Split(';');
        string[] strArray6 = stringValue6.Split(';');
        string[] strArray7 = stringValue7.Split(';');
        string[] strArray8 = stringValue8.Split(';');
        string[] strArray9 = stringValue9.Split(';');
        this.progressionType = inType + (object)inIndex;
        if (strArray1.Length > inIndex)
            this.braking = Convert.ToSingle(strArray1[inIndex]) / 100f;
        if (strArray2.Length > inIndex)
            this.cornering = Convert.ToSingle(strArray2[inIndex]) / 100f;
        if (strArray3.Length > inIndex)
            this.smoothness = Convert.ToSingle(strArray3[inIndex]) / 100f;
        if (strArray4.Length > inIndex)
            this.overtaking = Convert.ToSingle(strArray4[inIndex]) / 100f;
        if (strArray5.Length > inIndex)
            this.consistency = Convert.ToSingle(strArray5[inIndex]) / 100f;
        if (strArray6.Length > inIndex)
            this.adaptability = Convert.ToSingle(strArray6[inIndex]) / 100f;
        if (strArray7.Length > inIndex)
            this.fitness = Convert.ToSingle(strArray7[inIndex]) / 100f;
        if (strArray8.Length > inIndex)
            this.feedback = Convert.ToSingle(strArray8[inIndex]) / 100f;
        if (strArray9.Length <= inIndex)
            return;
        this.focus = Convert.ToSingle(strArray9[inIndex]) / 100f;
    }

}
