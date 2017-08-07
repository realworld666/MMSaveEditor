using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ContractManagerPerson
{
    public int contractPatience;
    public int contractPatienceUsed;
    public int contractPatienceAvailable;
    public DateTime cooldownPeriod = new DateTime();
    public ContractEvaluationPerson contractEvaluation = new ContractEvaluationPerson();
    private ContractManagerPerson.ContractProposalState mContractProposalState;
    private ContractPerson mTargetContract;
    private ContractPerson mDraftProposalContract;
    private ContractNegotiationScreen.NegotatiationType mNegotiationType;
    private ContractNegotiationScreen.ContractYear mContractYear;
    private ContractManagerPerson.LastChanceProposalState mIsLastChance;
    private DateTime mFiredByPlayerDate = new DateTime();
    private DateTime mLetContractNegotiationExpireCooldownPeriod = new DateTime();
    private DateTime mCancelledContractNegotiationCooldownPeriod = new DateTime();
    private CalendarEvent_v1 mCalendarConsideredEvent;
    private DateTime mContractElapsingTime = new DateTime();
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
