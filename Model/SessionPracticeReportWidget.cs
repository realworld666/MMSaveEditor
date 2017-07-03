// Decompiled with JetBrains decompiler
// Type: SessionPracticeReportWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class SessionPracticeReportWidget : UIBaseSessionHudDropdown
{
  private Map<PracticeReportSessionData.KnowledgeType, UISessionKnowledgeProgressBar> mSliders = new Map<PracticeReportSessionData.KnowledgeType, UISessionKnowledgeProgressBar>();
  public UISessionKnowledgeProgressBar firstOptionTyresSlider;
  public UISessionKnowledgeProgressBar secondOptionTyresSlider;
  public UISessionKnowledgeProgressBar thirdOptionTyresSlider;
  public UISessionKnowledgeProgressBar intermediateTyresSlider;
  public UISessionKnowledgeProgressBar wetTyresSlider;
  public UISessionKnowledgeProgressBar qualifyingTrimSlider;
  public UISessionKnowledgeProgressBar raceTrimSlider;

  protected override void OnEnable()
  {
    base.OnEnable();
    this.Setup();
  }

  private void Setup()
  {
    this.PopulateSlidersMap();
    for (int index = 0; index < this.mSliders.keys.Count; ++index)
    {
      PracticeReportSessionData.KnowledgeType knowledgeType = (PracticeReportSessionData.KnowledgeType) index;
      this.mSliders.GetMap(knowledgeType).Setup(knowledgeType);
    }
    GameUtility.SetActive(this.qualifyingTrimSlider.gameObject, Game.instance.player.team.championship.rules.qualifyingBasedActive);
  }

  private void PopulateSlidersMap()
  {
    this.mSliders.Clear();
    this.mSliders.Add(PracticeReportSessionData.KnowledgeType.FirstOptionTyres, this.firstOptionTyresSlider);
    this.mSliders.Add(PracticeReportSessionData.KnowledgeType.SecondOptionTyres, this.secondOptionTyresSlider);
    this.mSliders.Add(PracticeReportSessionData.KnowledgeType.ThirdOptionTyres, this.thirdOptionTyresSlider);
    this.mSliders.Add(PracticeReportSessionData.KnowledgeType.IntermediateTyres, this.intermediateTyresSlider);
    this.mSliders.Add(PracticeReportSessionData.KnowledgeType.WetTyres, this.wetTyresSlider);
    this.mSliders.Add(PracticeReportSessionData.KnowledgeType.QualifyingTrim, this.qualifyingTrimSlider);
    this.mSliders.Add(PracticeReportSessionData.KnowledgeType.RaceTrim, this.raceTrimSlider);
  }
}
