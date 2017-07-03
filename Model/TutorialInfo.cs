using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TutorialInfo
{
    private List<string> mRulesViewed = new List<string>();
    private List<string> mTutorialsViewed = new List<string>();
    private List<string> mRulesSimulationViewed = new List<string>();
    private Dictionary<TutorialInfo.RuleStatus, List<TutorialInfo.TutorialRule>> mTutorialsSimulationViewed;
    public enum RuleStatus
    {
        Triggered,
        Viewed,
    }

    public class TutorialRule
    {
        public DialogRule rule;
        public string gameArea;



        public TutorialRule(DialogRule inRule, string inRuleGameArea)
        {
            this.rule = inRule;
            this.gameArea = inRuleGameArea;
        }
    }
}
