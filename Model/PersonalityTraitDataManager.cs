
using System.Collections.Generic;
using FullSerializer;
using MM2;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PersonalityTraitDataManager
{
    [fsIgnore]
    public static List<PersonalityTrait.StatModified> raceStats = new List<PersonalityTrait.StatModified>() { PersonalityTrait.StatModified.Adaptability, PersonalityTrait.StatModified.Braking, PersonalityTrait.StatModified.Consistency, PersonalityTrait.StatModified.Cornering, PersonalityTrait.StatModified.Feedback, PersonalityTrait.StatModified.Fitness, PersonalityTrait.StatModified.Focus, PersonalityTrait.StatModified.Overtakng, PersonalityTrait.StatModified.Smoothness };
    public Dictionary<int, PersonalityTraitData> personalityTraits = new Dictionary<int, PersonalityTraitData>();
    private VersionNumber mVersion;

}
