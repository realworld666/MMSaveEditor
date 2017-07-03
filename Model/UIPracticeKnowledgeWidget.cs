// Decompiled with JetBrains decompiler
// Type: UIPracticeKnowledgeWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPracticeKnowledgeWidget : MonoBehaviour
{
  private int mKnowledgePercentage = -1;
  [SerializeField]
  private Image mKnowledgeIcon;
  [SerializeField]
  private Slider mKnowledgeFill;
  [SerializeField]
  private TextMeshProUGUI mKnowledgeAmount;
  private PracticeReportSessionData.KnowledgeType mCurrentKnowledge;
  private PracticeReportKnowledgeData mCurrentKnowledgeData;
  private Image mKnowledgeFillColor;

  public void SetupForKnowldgeType(PracticeReportSessionData.KnowledgeType inKnowlegdeType)
  {
    this.mCurrentKnowledge = inKnowlegdeType;
    this.mKnowledgeFillColor = this.mKnowledgeFill.fillRect.GetComponent<Image>();
    PracticeReportKnowledgeData knowledgeOfType = Game.instance.persistentEventData.GetPlayerPracticeReportData().GetKnowledgeOfType(this.mCurrentKnowledge);
    this.mKnowledgeFillColor.color = PracticeKnowledge.GetKnowledgeLevelColor(knowledgeOfType, false);
    this.mCurrentKnowledgeData = knowledgeOfType;
    this.mKnowledgeIcon.sprite = PracticeKnowledge.GetKnowledgeSprite(this.mCurrentKnowledge, true);
    this.UpdateWidget();
  }

  private void Update()
  {
    this.UpdateWidget();
  }

  private void UpdateWidget()
  {
    GameUtility.SetSliderAmountIfDifferent(this.mKnowledgeFill, this.mCurrentKnowledgeData.normalizedKnowledgePerLevel, 1000f);
    this.mKnowledgeFillColor.color = PracticeKnowledge.GetKnowledgeLevelColor(this.mCurrentKnowledgeData, false);
    int num = Mathf.FloorToInt(this.mCurrentKnowledgeData.normalizedKnowledgePerLevel * 100f);
    if (num == this.mKnowledgePercentage)
      return;
    this.mKnowledgePercentage = num;
    this.mKnowledgeAmount.text = this.mKnowledgePercentage.ToString() + "%";
  }
}
