using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public abstract class PersonManager<T> : GenericManager<T> where T : Person
{
    protected List<T> mReplacementPeople = new List<T>();

    public bool IsReplacementPerson(T inPerson)
    {
        return this.mReplacementPeople.Contains(inPerson);
    }

    public List<T> GetReplacementPeople()
    {
        return this.mReplacementPeople;
    }
}
