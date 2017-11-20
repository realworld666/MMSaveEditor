using FullSerializer;
using System.Collections.Generic;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChampionshipStandings : Entity
{
    private List<ChampionshipEntry_v1> mDrivers = new List<ChampionshipEntry_v1>();
    private List<ChampionshipEntry_v1> mTeams = new List<ChampionshipEntry_v1>();
    private List<ChampionshipEntry_v1> mInactiveDrivers = new List<ChampionshipEntry_v1>();
    private Championship mChampionship;
    public int teamEntryCount
    {
        get
        {
            return this.mTeams.Count;
        }
    }

    public ChampionshipEntry_v1 GetEntry(Entity inEntity)
    {
        ChampionshipEntry_v1 championshipEntryV1 = (ChampionshipEntry_v1)null;
        if (inEntity is Driver)
        {
            int count = this.mDrivers.Count;
            for (int index = 0; index < count; ++index)
            {
                if (this.mDrivers[index].GetEntity<Driver>() == (Driver)inEntity)
                {
                    championshipEntryV1 = this.mDrivers[index];
                    break;
                }
            }
            if (championshipEntryV1 == null)
                championshipEntryV1 = this.GetInactiveEntry(inEntity);
        }
        else
        {
            int count = this.mTeams.Count;
            for (int index = 0; index < count; ++index)
            {
                if (this.mTeams[index].GetEntity<Team>() == (Team)inEntity)
                {
                    championshipEntryV1 = this.mTeams[index];
                    break;
                }
            }
        }
        return championshipEntryV1;
    }

    public void AddEntry(Entity inEntity, Championship inChampionship)
    {
        if (inEntity is Driver)
        {
            ChampionshipEntry_v1 inactiveEntry = this.GetInactiveEntry(inEntity);
            if (inactiveEntry != null)
            {
                this.mDrivers.Add(inactiveEntry);
                this.mInactiveDrivers.Remove(inactiveEntry);
                return;
            }
        }
        ChampionshipEntry_v1 championshipEntryV1 = new ChampionshipEntry_v1();
        championshipEntryV1.Create(inEntity, inChampionship);
        if (inEntity is Driver)
        {
            this.mDrivers.Add(championshipEntryV1);
            this.mDrivers[this.mDrivers.Count - 1].SetStartingChampionshipPosition();
        }
        else
        {
            this.mTeams.Add(championshipEntryV1);
            this.mTeams[this.mTeams.Count - 1].SetStartingChampionshipPosition();
        }
    }

    public void RemoveEntry(Entity inEntity)
    {
        if (inEntity is Driver)
        {
            for (int index = 0; index < this.mDrivers.Count; ++index)
            {
                if ((Driver)inEntity == this.mDrivers[index].GetEntity<Driver>())
                    this.mDrivers.RemoveAt(index);
            }
        }
        else
        {
            for (int index = 0; index < this.mTeams.Count; ++index)
            {
                if ((Team)inEntity == this.mTeams[index].GetEntity<Team>())
                    this.mTeams.RemoveAt(index);
            }
        }
    }

    public ChampionshipEntry_v1 GetInactiveEntry(Entity inEntity)
    {
        ChampionshipEntry_v1 championshipEntryV1 = (ChampionshipEntry_v1)null;
        if (inEntity is Driver)
        {
            int count = this.mInactiveDrivers.Count;
            for (int index = 0; index < count; ++index)
            {
                if (this.mInactiveDrivers[index].GetEntity<Driver>() == (Driver)inEntity)
                {
                    championshipEntryV1 = this.mInactiveDrivers[index];
                    break;
                }
            }
        }
        return championshipEntryV1;
    }

    public bool isEntryInactive(ChampionshipEntry_v1 inEntry)
    {
        return this.mInactiveDrivers.Contains(inEntry);
    }

    public void UpdateStandings()
    {
        this.MoveInactiveDrivers();
        this.SortEntries(this.mDrivers);
        this.SortEntries(this.mTeams);
        int count1 = this.mDrivers.Count;
        for (int index = 0; index < count1; ++index)
            this.mDrivers[index].RecordChampionshipPosition(index + 1);
        int count2 = this.mTeams.Count;
        for (int index = 0; index < count2; ++index)
            this.mTeams[index].RecordChampionshipPosition(index + 1);
    }

    private void MoveInactiveDrivers()
    {
        for (int index = this.mDrivers.Count - 1; index >= 0; --index)
        {
            ChampionshipEntry_v1 mDriver = this.mDrivers[index];
            if (mDriver.isInactiveDriver())
            {
                this.mDrivers.RemoveAt(index);
                this.mInactiveDrivers.Add(mDriver);
            }
        }
    }

    private void SortEntries(List<ChampionshipEntry_v1> inEntryList)
    {
        inEntryList.Sort();
    }

    public ChampionshipEntry_v1 GetTeamEntry(int inIndex)
    {
        return this.mTeams[inIndex];
    }
}
