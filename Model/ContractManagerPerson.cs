using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class ContractManagerPerson
{
	public DateTime cooldownPeriod = new DateTime();
	public ContractEvaluationPerson contractEvaluation = new ContractEvaluationPerson();
	private DateTime mFiredByPlayerDate = new DateTime();
	private DateTime mLetContractNegotiationExpireCooldownPeriod = new DateTime();
	private DateTime mCancelledContractNegotiationCooldownPeriod = new DateTime();
	private DateTime mContractElapsingTime = new DateTime();
	public int contractPatience;
	public int contractPatienceUsed;
	public int contractPatienceAvailable;
	private ContractManagerPerson.ContractProposalState mContractProposalState;
	private ContractPerson mTargetContract;
	private ContractPerson mDraftProposalContract;
	private ContractNegotiationScreen.NegotatiationType mNegotiationType;
	private ContractNegotiationScreen.ContractYear mContractYear;
	private ContractManagerPerson.LastChanceProposalState mIsLastChance;
	private CalendarEvent_v1 mCalendarConsideredEvent;
	private CalendarEvent_v1 mContractNegotiationExpiringEvent;
	private CalendarEvent_v1 mContractElapsedEvent;
	private Person mPerson;


	public enum ContractProposalState
	{
		NoContractProposed,
		ConsideringProposal,
		ProposalRejected,
		ProposalAccepted,
	}

	public enum LastChanceProposalState
	{
		NotLastChanceYet,
		IsLastChance,
		LastChanceUsed,
	}
}
