
using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TeamPerk
{
    public enum Type
    {
        SponsorsLevel5,
        ForecastWeather1,
        ForecastWeather2,
        ForecastWeather3,
        Count,
    }

    public class TypeComparer : IEqualityComparer<TeamPerk.Type>
    {
        public bool Equals(TeamPerk.Type x, TeamPerk.Type y)
        {
            return x == y;
        }

        public int GetHashCode(TeamPerk.Type codeh)
        {
            return (int)codeh;
        }
    }
}
