// Decompiled with JetBrains decompiler
// Type: CarHappinessOverviewRollover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarHappinessOverviewRollover : UIDialogBox
{
  [SerializeField]
  private TextMeshProUGUI mDriverHappiness;
  [SerializeField]
  private TextMeshProUGUI mExpectedPosition;
  [SerializeField]
  private TextMeshProUGUI mCarOverallComment;
  [SerializeField]
  private TextMeshProUGUI mCarCommentAgainstOtherDriver;
  [SerializeField]
  private TextMeshProUGUI mCarOverallMoraleImpact;
  [SerializeField]
  private TextMeshProUGUI mCarAgainstOtherDriverMoraleImpact;
  [SerializeField]
  private Image mCarOverallReaction;
  [SerializeField]
  private Image mCarReactionAgainstOtherDriver;
  [SerializeField]
  private GameObject mNoOpinionContainer;
  [SerializeField]
  private GameObject mHasOpinionContainer;
  private RectTransform mRectTransform;

  public void ShowRollover(Driver inDriver)
  {
    this.mRectTransform = this.GetComponent<RectTransform>();
    GameUtility.SetTooltipTransformInsideScreen(this.mRectTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
    GameUtility.SetActive(this.gameObject, true);
    this.SetupHappinessData(inDriver);
  }

  private void Update()
  {
    GameUtility.SetTooltipTransformInsideScreen(this.mRectTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
  }

  private void SetupHappinessData(Driver inDriver)
  {
    StringVariableParser.SetStaticData((Person) inDriver);
    StringVariableParser.ordinalNumberString = GameUtility.FormatForPosition(inDriver.expectedRacePosition, (string) null);
    this.mExpectedPosition.text = Localisation.LocaliseID("PSG_10010860", (GameObject) null);
    if (!inDriver.carOpinion.HasOpinion())
      inDriver.carOpinion.CalculateDriverOpinions(inDriver);
    if (!inDriver.carOpinion.HasOpinion())
    {
      this.mDriverHappiness.text = Localisation.LocaliseID("PSG_10010528", (GameObject) null);
      this.mDriverHappiness.color = Color.grey;
      GameUtility.SetActive(this.mNoOpinionContainer, true);
      GameUtility.SetActive(this.mHasOpinionContainer, false);
    }
    else
    {
      GameUtility.SetActive(this.mNoOpinionContainer, false);
      GameUtility.SetActive(this.mHasOpinionContainer, true);
      CarOpinion.Happiness averageHappiness = inDriver.carOpinion.GetDriverAverageHappiness();
      StringVariableParser.subject = (Person) inDriver;
      this.mDriverHappiness.text = Localisation.LocaliseEnum((Enum) averageHappiness);
      this.mDriverHappiness.color = CarOpinion.GetColor(averageHappiness);
      DialogRule driverComment1 = inDriver.carOpinion.GetDriverComment(CarOpinion.CommentType.OverallCarComment);
      DialogRule driverComment2 = inDriver.carOpinion.GetDriverComment(CarOpinion.CommentType.AgainstOtherCarComment);
      if (driverComment1 != null)
      {
        this.mCarOverallComment.text = Localisation.LocaliseID(driverComment1.localisationID, (GameObject) null);
        if (driverComment2 != null)
          this.mCarCommentAgainstOtherDriver.text = Localisation.LocaliseID(driverComment2.localisationID, (GameObject) null);
      }
      else
      {
        this.mCarOverallComment.text = string.Empty;
        this.mCarCommentAgainstOtherDriver.text = string.Empty;
      }
      CarOpinion.Happiness overallHappiness = inDriver.carOpinion.GetDriverOverallHappiness();
      CarOpinion.Happiness otherCarHappiness = inDriver.carOpinion.GetDriverAgainstOtherCarHappiness();
      this.AssignReaction(this.mCarOverallReaction, overallHappiness);
      this.AssignReaction(this.mCarReactionAgainstOtherDriver, otherCarHappiness);
      this.mCarOverallMoraleImpact.text = Mathf.RoundToInt(inDriver.carOpinion.overallMoraleHit * 100f).ToString() + "%";
      this.mCarAgainstOtherDriverMoraleImpact.text = Mathf.RoundToInt(inDriver.carOpinion.againstOtherCarMoraleHit * 100f).ToString() + "%";
      StringVariableParser.ResetAllStaticReferences();
    }
  }

  private void AssignReaction(Image inImage, CarOpinion.Happiness inHappiness)
  {
    switch (inHappiness)
    {
      case CarOpinion.Happiness.Angry:
        inImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-AngrySmileyLarge");
        break;
      case CarOpinion.Happiness.Unhappy:
        inImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-UnhappySmileyLarge");
        break;
      case CarOpinion.Happiness.Content:
        inImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-AverageSmileyLarge");
        break;
      case CarOpinion.Happiness.Happy:
      case CarOpinion.Happiness.Delighted:
        inImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "Smileys-HappySmileyLarge");
        break;
    }
  }
}
