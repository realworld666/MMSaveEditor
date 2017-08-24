
using FullSerializer;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CareerHistoryEntry
{
    public Team team;
    public Championship championship;
    public int year;
    public int wins;
    public int podiums;
    public int races;
    public int poles;
    public int DNFs;
    public int DNFsViaError;
    public int DNS;
    public int careerPoints;
    public int championships;
    private bool mIsFinished;
    private DateTime mEndDate = new DateTime();
    private DateTime mStartDate = new DateTime();

    public bool isFinished
    {
        get
        {
            return this.mIsFinished;
        }
    }

    public void MarkEntryAsFinished(DateTime inEndDate)
    {
        if (this.mIsFinished)
            return;
        this.mIsFinished = true;
        this.mEndDate = inEndDate;
    }
}
