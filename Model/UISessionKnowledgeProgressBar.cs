// Decompiled with JetBrains decompiler
// Type: UISessionKnowledgeProgressBar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISessionKnowledgeProgressBar : MonoBehaviour
{
  private int mCurrentKnowledgeLevelValue = int.MinValue;
  private int mKnowledgePercentageValue = int.MinValue;
  [SerializeField]
  private Slider mSlider;
  [SerializeField]
  private TextMeshProUGUI mKnowledgeName;
  [SerializeField]
  private TextMeshProUGUI mKnowledgePercentage;
  [SerializeField]
  private TextMeshProUGUI mNextKnowledgeLevel;
  [SerializeField]
  private TextMeshProUGUI mCurrentKnowledgeLevel;
  [SerializeField]
  private Image mKnowledgeIcon;
  [SerializeField]
  private GameObject mPadLock;
  [SerializeField]
  private Image mNextLevelKnowledgeBacking;
  [SerializeField]
  private Image mIconBacking;
  [SerializeField]
  private CanvasGroup mCanvasGroup;
  private Image mProgressBarColor;
  private PracticeReportSessionData.KnowledgeType mCurrentKnowledge;
  private PracticeReportKnowledgeData mCurrentKnowledgeData;

  public void Setup(PracticeReportSessionData.KnowledgeType inKnowlegdeType)
  {
    int compoundsAvailable = Game.instance.sessionManager.championship.rules.compoundsAvailable;
    if (inKnowlegdeType == PracticeReportSessionData.KnowledgeType.ThirdOptionTyres && compoundsAvailable < 3 || inKnowlegdeType == PracticeReportSessionData.KnowledgeType.SecondOptionTyres && compoundsAvailable < 2)
    {
      GameUtility.SetActive(this.gameObject, false);
    }
    else
    {
      GameUtility.SetActive(this.gameObject, true);
      this.mSlider.interactable = false;
      this.mCurrentKnowledge = inKnowlegdeType;
      this.mCurrentKnowledgeData = Game.instance.persistentEventData.GetPlayerPracticeReportData().GetKnowledgeOfType(this.mCurrentKnowledge);
      this.mProgressBarColor = this.mSlider.fillRect.GetComponent<Image>();
      this.mKnowledgeIcon.sprite = PracticeKnowledge.GetKnowledgeSprite(this.mCurrentKnowledge, false);
      this.mKnowledgeName.text = PracticeKnowledge.GetKnowledgeName(this.mCurrentKnowledge);
      this.UpdateSliderElements();
      this.SetCanvasAlpha();
    }
  }

  public void SetCanvasAlpha()
  {
    Team team = Game.instance.player.team;
    Driver selectedDriver1 = team.GetSelectedDriver(0);
    Driver selectedDriver2 = team.GetSelectedDriver(1);
    RacingVehicle vehicle1 = Game.instance.vehicleManager.GetVehicle(selectedDriver1);
    RacingVehicle vehicle2 = Game.instance.vehicleManager.GetVehicle(selectedDriver2);
    this.mCanvasGroup.alpha = !(vehicle1.practiceKnowledge.GetCurrentTyreKnowledge() == this.mCurrentKnowledge | vehicle2.practiceKnowledge.GetCurrentTyreKnowledge() == this.mCurrentKnowledge | vehicle1.practiceKnowledge.knowledgeType == this.mCurrentKnowledge | vehicle2.practiceKnowledge.knowledgeType == this.mCurrentKnowledge) ? 0.5f : 1f;
  }

  private void Update()
  {
    this.UpdateSliderElements();
  }

  public void UpdateSliderElements()
  {
    this.mIconBacking.color = PracticeKnowledge.GetKnowledgeLevelColor(this.mCurrentKnowledgeData, false);
    this.mProgressBarColor.color = PracticeKnowledge.GetKnowledgeLevelColor(this.mCurrentKnowledgeData, false);
    GameUtility.SetSliderAmountIfDifferent(this.mSlider, this.mCurrentKnowledgeData.normalizedKnowledgePerLevel, 1000f);
    this.mNextLevelKnowledgeBacking.color = PracticeKnowledge.GetKnowledgeLevelColor(this.mCurrentKnowledgeData, true);
    int currentKnoledgeLevel = this.mCurrentKnowledgeData.currentKnoledgeLevel;
    if (!this.mCurrentKnowledgeData.IsBonusUnlocked(PracticeReportKnowledgeData.mNumberOfBonuses - 1))
      --currentKnoledgeLevel;
    if (currentKnoledgeLevel != this.mCurrentKnowledgeLevelValue)
    {
      this.mCurrentKnowledgeLevelValue = currentKnoledgeLevel;
      bool flag = this.mCurrentKnowledgeData.IsBonusUnlocked(PracticeReportKnowledgeData.mNumberOfBonuses - 1);
      StringVariableParser.intValue1 = currentKnoledgeLevel;
      if (flag)
        this.mNextKnowledgeLevel.text = currentKnoledgeLevel.ToString();
      else
        this.mNextKnowledgeLevel.text = (currentKnoledgeLevel + 1).ToString();
      this.mCurrentKnowledgeLevel.text = Localisation.LocaliseID("PSG_10010415", (GameObject) null);
      GameUtility.SetActive(this.mPadLock, !flag);
    }
    int num = Mathf.FloorToInt(this.mCurrentKnowledgeData.normalizedKnowledgePerLevel * 100f);
    if (num == this.mKnowledgePercentageValue)
      return;
    this.mKnowledgePercentageValue = num;
    this.mKnowledgePercentage.text = num.ToString() + "%";
  }
}
