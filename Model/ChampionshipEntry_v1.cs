
using System;
using FullSerializer;

[fsObject("v1", new System.Type[] { typeof(ChampionshipEntry) }, MemberSerialization = fsMemberSerialization.OptOut)]
public class ChampionshipEntry_v1 : IComparable<ChampionshipEntry_v1>
{
    public int races;
    public int podiums;
    public int wins;
    public int DNFs;
    private int mCurrentPosition;
    private int[] mQualifyingPositions;
    private int[] mRacePositions;
    private int[] mChampionshipPositions;
    private int[] mPoints;
    private int[] mExpectedRacePositions;
    private int[] mEventPositions;
    private Entity mEntity;
    private Championship mChampionship;

    public int pointsEntryCount
    {
        get
        {
            return this.mPoints.Length;
        }
    }

    public ChampionshipEntry_v1()
    {
    }

    public ChampionshipEntry_v1(ChampionshipEntry v0)
    {
        this.races = v0.races;
        this.podiums = v0.podiums;
        this.wins = v0.podiums;
        this.DNFs = v0.podiums;
        this.mCurrentPosition = v0.mCurrentPosition;
        this.mQualifyingPositions = v0.mQualifyingPositions;
        this.mRacePositions = v0.mRacePositions;
        this.mChampionshipPositions = v0.mChampionshipPositions;
        this.mPoints = v0.mPoints;
        this.mExpectedRacePositions = v0.mExpectedRacePositions;
        this.mEventPositions = new int[v0.mExpectedRacePositions.Length];
        this.mEntity = v0.mEntity;
        this.mChampionship = v0.mChampionship;
    }

    public Championship championship
    {
        get
        {
            return this.mChampionship;
        }
    }

    public T GetEntity<T>() where T : Entity
    {
        return (T)this.mEntity;
    }

    public void Create(Entity inEntity, Championship inChampionship)
    {
        this.mEntity = inEntity;
        this.mChampionship = inChampionship;
        this.Reset();
    }

    public void Reset()
    {
        int count = this.mChampionship.calendar.Count;
        this.races = 0;
        this.podiums = 0;
        this.wins = 0;
        this.DNFs = 0;
        this.mQualifyingPositions = new int[count];
        this.mRacePositions = new int[count];
        this.mChampionshipPositions = new int[count];
        this.mPoints = new int[count];
        this.mExpectedRacePositions = new int[count];
        this.mEventPositions = new int[count];
    }

    public void SetStartingChampionshipPosition()
    {
        for (int index = 0; index < this.mChampionshipPositions.Length; ++index)
            this.mChampionshipPositions[index] = -1;
    }

    public void RecordChampionshipPosition(int inPosition)
    {
        this.mCurrentPosition = inPosition;
        this.mChampionshipPositions[this.mChampionship.eventNumber] = inPosition;
    }

    public bool isInactiveDriver()
    {
        Driver entity = this.GetEntity<Driver>();
        if (entity != null)
        {
            int currentPoints = this.GetCurrentPoints();
            Team team = entity.Contract.GetTeam();
            if (currentPoints <= 0 && (entity.IsFreeAgent() || team is NullTeam || team.championship != this.mChampionship))
                return true;
        }
        return false;
    }

    public int GetCurrentPoints()
    {
        return this.GetPointsForEvent(this.mChampionship.eventNumber);
    }

    public int GetPointsForEvent(int inEventNumber)
    {
        if (inEventNumber >= 0 && inEventNumber < this.pointsEntryCount)
            return this.mPoints[inEventNumber];
        return 0;
    }

    public int CompareTo(ChampionshipEntry_v1 inEntryB)
    {
        if (inEntryB == null)
            return -1;
        int num = inEntryB.GetCurrentPoints().CompareTo(this.GetCurrentPoints());
        if (num == 0)
        {
            if (this.races > 0 && inEntryB.races > 0)
            {
                for (int inPosition = 1; inPosition < 21; ++inPosition)
                {
                    num = inEntryB.GetNumberOfPositions(inPosition).CompareTo(this.GetNumberOfPositions(inPosition));
                    if (num != 0)
                        break;
                }
            }
            else if (this.races == 0 && inEntryB.races > 0 || this.races > 0 && inEntryB.races == 0)
                num = inEntryB.races.CompareTo(this.races);
            if (num == 0)
                num = !inEntryB.isDriverEntry ? string.Compare(this.GetEntity<Team>().name, inEntryB.GetEntity<Team>().name) : string.Compare(this.GetEntity<Driver>().name, inEntryB.GetEntity<Driver>().name);
        }
        return num;
    }

    public bool isDriverEntry
    {
        get
        {
            return this.mEntity is Driver;
        }
    }

    public int GetNumberOfPositions(int inPosition)
    {
        int num = 0;
        for (int index = 0; index < this.mEventPositions.Length; ++index)
        {
            if (this.mEventPositions[index] == inPosition)
                ++num;
        }
        return num;
    }
}
