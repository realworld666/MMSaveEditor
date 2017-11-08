using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChampionshipManager : GenericManager<Championship>
{
    public NullChampionship nullChampionship = new NullChampionship();
    private List<Championship> championshipsOrdered;
    private List<Championship> championshipsOrderedDLC;
    private List<Championship> championshipsOrderedEndurance;
    private List<Championship> mReturnChampionships = new List<Championship>();
}
