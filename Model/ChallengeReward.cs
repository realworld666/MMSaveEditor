using FullSerializer;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChallengeReward
{
    public ChallengeReward.Type type = ChallengeReward.Type.InvestorNipponMotors;
    public string titleID = string.Empty;
    public string rewardTextID = string.Empty;
    public string imageDisplay = string.Empty;

    public enum Type
    {
        UnderdogAchievement,
        TopManagerAchievement,
        RyoHiraoka,
        PlayerAccessory1,
        PlayerAccessory2,
        MargateAndPereya,
        PlayerAccessory3,
        InvestorNipponMotors,
        GTLivery1,
        InvestorJandemole,
        OpenWheelLivery1,
        PlayerBackstoryMotorsportLegend,
        GTLivery2,
        OpenWheelLivery2,
        Count,
    }
}
