
using FullSerializer;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class DialogCriteria
{
    public DialogCriteria.CriteriaOperator criteriaOperator;
    public string mType = string.Empty;
    public string mCriteriaInfo = string.Empty;

    private float mParsedData;

    public DialogCriteria()
    {
    }

    public DialogCriteria(string inType, string inInfo)
    {
        this.mType = inType;
        this.mCriteriaInfo = inInfo;
        this.SetParsedData();
    }

    public void SetParsedData()
    {
        float.TryParse(this.mCriteriaInfo, out this.mParsedData);
    }

    public bool CriteriaMatch(DialogCriteria inCriteria)
    {
        if (this.mType.Equals(inCriteria.mType, StringComparison.OrdinalIgnoreCase))
        {
            switch (this.criteriaOperator)
            {
                case DialogCriteria.CriteriaOperator.Equals:
                    return this.mCriteriaInfo.Equals(inCriteria.mCriteriaInfo, StringComparison.OrdinalIgnoreCase);
                case DialogCriteria.CriteriaOperator.Greater:
                    float result1 = 0.0f;
                    this.CanParseToFloat(inCriteria, out result1);
                    return (double)result1 > (double)this.mParsedData;
                case DialogCriteria.CriteriaOperator.GreaterOrEquals:
                    float result2 = 0.0f;
                    this.CanParseToFloat(inCriteria, out result2);
                    return (double)result2 >= (double)this.mParsedData;
                case DialogCriteria.CriteriaOperator.Smaller:
                    float result3 = 0.0f;
                    this.CanParseToFloat(inCriteria, out result3);
                    return (double)result3 < (double)this.mParsedData;
                case DialogCriteria.CriteriaOperator.SmallerOrEquals:
                    float result4 = 0.0f;
                    this.CanParseToFloat(inCriteria, out result4);
                    return (double)result4 <= (double)this.mParsedData;
                case DialogCriteria.CriteriaOperator.Different:
                    return !this.mCriteriaInfo.Equals(inCriteria.mCriteriaInfo, StringComparison.OrdinalIgnoreCase);
            }
        }
        return false;
    }

    private void CanParseToFloat(DialogCriteria inCriteria, out float result)
    {
        float result1 = 0.0f;
        if (!float.TryParse(inCriteria.mCriteriaInfo, out result1))
            throw new Exception(string.Format("{0} has {1} as criteria info, should be float.", (object)inCriteria.mType, (object)inCriteria.mCriteriaInfo), null);
        result = result1;
    }

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
