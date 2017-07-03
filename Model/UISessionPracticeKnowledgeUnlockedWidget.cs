// Decompiled with JetBrains decompiler
// Type: UISessionPracticeKnowledgeUnlockedWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISessionPracticeKnowledgeUnlockedWidget : MonoBehaviour
{
  private List<PracticeReportKnowledgeData> mKnowledgeDataQueue = new List<PracticeReportKnowledgeData>();
  [SerializeField]
  private Animator mAnimator;
  [SerializeField]
  private Image mKnowledgeIcon;
  [SerializeField]
  private Image mIconBacking;
  [SerializeField]
  private TextMeshProUGUI mKnowledgeName;
  [SerializeField]
  private TextMeshProUGUI mKnowledgeLevel;
  [SerializeField]
  private Image mKnowledgeLevelBacking;

  public void Show(PracticeReportKnowledgeData inPracticeKnowledge)
  {
    this.mKnowledgeDataQueue.Add(inPracticeKnowledge);
    if (this.mKnowledgeDataQueue.Count != 1)
      return;
    this.Dispatch();
  }

  private void Dispatch()
  {
    if (this.mKnowledgeDataQueue.Count > 0)
    {
      this.SetupKnowledgeData(this.mKnowledgeDataQueue[0]);
      GameUtility.SetActive(this.gameObject, true);
    }
    else
      GameUtility.SetActive(this.gameObject, false);
  }

  private void SetupKnowledgeData(PracticeReportKnowledgeData inPracticeKnowledge)
  {
    Color knowledgeLevelColor = PracticeKnowledge.GetKnowledgeLevelColor(inPracticeKnowledge.lastUnlockedLevel);
    this.mKnowledgeLevelBacking.color = knowledgeLevelColor;
    this.mIconBacking.color = knowledgeLevelColor;
    this.mKnowledgeIcon.sprite = PracticeKnowledge.GetKnowledgeSprite(inPracticeKnowledge.knowledgeType, false);
    this.mKnowledgeName.text = PracticeKnowledge.GetKnowledgeName(inPracticeKnowledge.knowledgeType);
    StringVariableParser.intValue1 = inPracticeKnowledge.lastUnlockedLevel;
    this.mKnowledgeLevel.text = Localisation.LocaliseID("PSG_10010415", (GameObject) null);
  }

  private void Update()
  {
    if ((double) this.mAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0)
      return;
    this.mAnimator.Play(0);
    this.mKnowledgeDataQueue.RemoveAt(0);
    this.Dispatch();
  }
}
