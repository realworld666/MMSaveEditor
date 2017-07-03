using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Engineer : Person
{
    public EngineerStats stats = new EngineerStats();
    public EngineerStats lastAccumulatedStats = new EngineerStats();
    public float improvementRate;
    public List<CarPartComponent> availableComponents = new List<CarPartComponent>();
    private readonly float negativeImprovementHQScalar = 0.9f;
    private readonly float negativeImprovementHQOverallScalar = 0.03f;
    private readonly float negativeMaxImprovementHQ = 0.75f;
    private readonly float learnNewComponentChancePerAbilityStar = 0.8f;

}
