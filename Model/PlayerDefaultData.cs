
using System;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PlayerDefaultData
{
    public string defaultNationality = string.Empty;
    public DateTime defaultDateOfBirth = new DateTime();
    public TeamPrincipalStats defaultStats = new TeamPrincipalStats();
    public Portrait defaultPortrait = new Portrait();
    public Person.Gender defaultGender;
}
