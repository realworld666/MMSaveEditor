using System.Collections.Generic;
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class PromisesController
{
    private readonly int promiseFulfilledID = 217;
    private readonly int promiseBrokenID = 218;
    private List<Promise> mPromisesMade = new List<Promise>();
    private HQsBuilding_v1 mNextBuildingToBuildPromised;
    private CarPart.PartType mNextCarPartImprovementPromised = CarPart.PartType.None;
    private Mechanic mNextMechanicToFirePromised;
    private int mChampionshipPositionPromised = -1;
}
