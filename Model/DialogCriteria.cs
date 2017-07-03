
using FullSerializer;
using System;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class DialogCriteria
{
	public string mType = string.Empty;
	public string mCriteriaInfo = string.Empty;
	public DialogCriteria.CriteriaOperator criteriaOperator;
	private float mParsedData;

	public enum CriteriaOperator
	{
		Equals,
		Greater,
		GreaterOrEquals,
		Smaller,
		SmallerOrEquals,
		Different,
	}
}
