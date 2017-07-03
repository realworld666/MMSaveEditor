using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class LiveryManager
{
    private LiveryData[] _liveries;
    private LiveryData[] _dlcLiveries;
    private LiveryData[] _customLiveries;
    private LiveryData[] _currentLiveriesArr;

    private List<LiveryData> _currentLiveries = new List<LiveryData>();
}
