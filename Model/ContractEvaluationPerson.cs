
using FullSerializer;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class ContractEvaluationPerson
{
	public ContractDesiredValuesHelper desiredContractValues = new ContractDesiredValuesHelper();
	private readonly float[] basicMaxReactionThresholds = new float[4] { 25f, 50f, 75f, 100f };
	private float[] weightedMaxReactionThresholds = new float[4];
	private ContractNegotiationScreen.NegotatiationType mNegotiationType;
	private ContractPerson mDraftProposalContract;
	private Person mPerson;


	public enum ReactionType
	{
		[LocalisationID( "PSG_10010103" )] Insulted,
		[LocalisationID( "PSG_10001551" )] UnHappy,
		[LocalisationID( "PSG_10010146" )] Neutral,
		[LocalisationID( "PSG_10010104" )] Delighted,
	}
}
