
using FullSerializer;
using System;
using System.Collections.Generic;
using System.Text;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class ContractDesiredValuesHelper
{
	private static readonly ContractEvaluationWeightings evaluationWeightings = new ContractEvaluationWeightings();
	private static readonly ContractVariablesContainer variablesContainer = new ContractVariablesContainer();
	private readonly float mScalarForValues = 1000000f;
	private float[] mDesiredRaceBonus = new float[2];
	private float[] mDesiredQualifyingBonus = new float[2];
	private ContractPerson.Status mDesiredStatus = ContractPerson.Status.Reserve;
	private ContractPerson.BuyoutClauseSplit mDesiredBuyoutSplit = ContractPerson.BuyoutClauseSplit.PersonPaysAll;
	private ContractVariablesData.RangeType mWageRangeType = ContractVariablesData.RangeType.RangeD;
	private ContractVariablesData.RangeType mSignOnFeeRangeType = ContractVariablesData.RangeType.RangeD;
	private readonly int mRandomChangeNegotiationWeight;
	private float mBaseDesiredWages;
	private float mDesiredWages;
	private float mDesiredSignOnFee;
	private ContractPerson.ContractLength mDesiredContractLength;
	private bool mWantSignOnFee;
	private bool mWantsQualifyingBonus;
	private bool mWantsRaceBonus;
	private bool mIsInterestedToTalk;
	private float mNegotiationWeight;
	private float mMinimumContractValueAcceptance;
	private Person mTargetPerson;
	private ContractNegotiationScreen.NegotatiationType mNegotiationType;
	private int mDesiredChampionship;

}
