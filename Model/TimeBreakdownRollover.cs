// Decompiled with JetBrains decompiler
// Type: TimeBreakdownRollover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Text;
using TMPro;
using UnityEngine;

public class TimeBreakdownRollover : UIDialogBox
{
  public UIGridList grid;
  public GameObject headerTime;
  public GameObject headerChance;
  public TextMeshProUGUI pitscrewSizeText;
  private RectTransform mRectTransform;
  private SessionSetup mSetup;

  protected override void Awake()
  {
    base.Awake();
    this.mRectTransform = this.GetComponent<RectTransform>();
  }

  private void Update()
  {
    GameUtility.SetTooltipTransformInsideScreen(this.mRectTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
  }

  public void Show(RectTransform inTransform, RacingVehicle inVehicle)
  {
    if ((UnityEngine.Object) this.mRectTransform == (UnityEngine.Object) null)
      this.Awake();
    GameUtility.SetTooltipTransformInsideScreen(this.mRectTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
    this.mSetup = inVehicle.setup;
    GameUtility.SetActive(this.headerTime, true);
    GameUtility.SetActive(this.headerChance, false);
    this.grid.DestroyListItems();
    GameUtility.SetActive(this.grid.itemPrefab, true);
    switch (inVehicle.driver.contract.GetTeam().championship.rules.pitCrewSize)
    {
      case ChampionshipRules.PitStopCrewSize.Large:
        this.pitscrewSizeText.text = Localisation.LocaliseID("PSG_10011241", (GameObject) null);
        break;
      default:
        this.pitscrewSizeText.text = Localisation.LocaliseID("PSG_10011242", (GameObject) null);
        break;
    }
    this.pitscrewSizeText.color = UIConstants.pitStopChangedOrange;
    SessionSetup.PitCrewSizeDependentSteps timeLimitingPitStep = this.mSetup.CalculateTimeLimitingPitStep();
    if ((double) this.mSetup.GetSetupTimeImpact() != 0.0)
      this.CreateEntry(Localisation.LocaliseID("PSG_10010352", (GameObject) null), this.mSetup.GetSetupTimeImpact(), timeLimitingPitStep == SessionSetup.PitCrewSizeDependentSteps.All || timeLimitingPitStep == SessionSetup.PitCrewSizeDependentSteps.Setup);
    if (this.mSetup.IsChangingTrim())
      this.CreateEntry(Localisation.LocaliseID("PSG_10010353", (GameObject) null), this.mSetup.GetTrimTimeImpact(), timeLimitingPitStep == SessionSetup.PitCrewSizeDependentSteps.All || timeLimitingPitStep == SessionSetup.PitCrewSizeDependentSteps.Trim);
    if ((double) this.mSetup.GetFuelTimeImpact() != 0.0)
    {
      bool hasTimeImpact = timeLimitingPitStep == SessionSetup.PitCrewSizeDependentSteps.All || timeLimitingPitStep == SessionSetup.PitCrewSizeDependentSteps.Fuel;
      bool flag = inVehicle.bonuses.IsBonusActive(MechanicBonus.Trait.PitStopGuruRefuelling);
      float inTimeImpact = this.mSetup.GetFuelTimeImpact() * (!flag ? 1f : 2f);
      int inLaps = this.mSetup.targetFuelLaps - this.mSetup.startFuelLaps;
      StringVariableParser.intValue1 = inLaps;
      using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
      {
        StringBuilder stringBuilder = builderSafe.stringBuilder;
        stringBuilder.Append(Localisation.LocaliseID("PSG_10010731", (GameObject) null));
        stringBuilder.Append(": ");
        stringBuilder.Append(GameUtility.FormatForLaps(inLaps));
        this.CreateEntry(stringBuilder.ToString(), inTimeImpact, hasTimeImpact);
      }
      if (flag && hasTimeImpact)
        this.CreateEntry(Localisation.LocaliseEnum((Enum) MechanicBonus.Trait.PitStopGuruRefuelling), (float) (-(double) inTimeImpact / 2.0), true);
    }
    if (this.mSetup.IsChangingTyres())
    {
      bool hasTimeImpact = timeLimitingPitStep == SessionSetup.PitCrewSizeDependentSteps.All || timeLimitingPitStep == SessionSetup.PitCrewSizeDependentSteps.Tyres;
      bool flag = inVehicle.bonuses.IsBonusActive(MechanicBonus.Trait.PitStopGuruTyreChanges);
      float inTimeImpact = this.mSetup.GetTyreTimeImpact() * (!flag ? 1f : 2f);
      StringVariableParser.stringValue1 = Localisation.LocaliseEnum((Enum) this.mSetup.targetSetup.tyreSet.GetCompound());
      this.CreateEntry(Localisation.LocaliseID("PSG_10010355", (GameObject) null), inTimeImpact, hasTimeImpact);
      if (flag && hasTimeImpact)
        this.CreateEntry(Localisation.LocaliseEnum((Enum) MechanicBonus.Trait.PitStopGuruTyreChanges), (float) (-(double) inTimeImpact / 2.0), true);
    }
    if (this.mSetup.isRepairingParts())
    {
      bool hasTimeImpact = timeLimitingPitStep == SessionSetup.PitCrewSizeDependentSteps.All || timeLimitingPitStep == SessionSetup.PitCrewSizeDependentSteps.Condition;
      bool flag = inVehicle.bonuses.IsBonusActive(MechanicBonus.Trait.QuickFixes);
      float inTimeImpact = this.mSetup.GetConditionReplairTimeImpact() * (!flag ? 1f : 2f);
      this.CreateEntry(Localisation.LocaliseID("PSG_10010356", (GameObject) null), inTimeImpact, hasTimeImpact);
      if (flag)
        this.CreateEntry(Localisation.LocaliseEnum((Enum) MechanicBonus.Trait.QuickFixes), (float) (-(double) inTimeImpact / 2.0), true);
    }
    if (Game.instance.sessionManager.sessionType == SessionDetails.SessionType.Race)
    {
      StringVariableParser.stringValue1 = Localisation.LocaliseEnum((Enum) inVehicle.strategy.pitStrategy);
      this.CreateEntry(Localisation.LocaliseID("PSG_10010351", (GameObject) null), this.mSetup.GetPitStrategyTimeImpact(), true);
    }
    if (this.mSetup.isChanging())
      this.CreateEntry(Localisation.LocaliseID("PSG_10010357", (GameObject) null), this.mSetup.GetMechanicTimeImpact(), true);
    if (inVehicle.bonuses.IsBonusActive(MechanicBonus.Trait.PitStopLegend))
      this.CreateEntry(Localisation.LocaliseEnum((Enum) MechanicBonus.Trait.PitStopLegend), inVehicle.bonuses.GetBonus(MechanicBonus.Trait.PitStopLegend), true);
    GameUtility.SetActive(this.grid.itemPrefab, false);
    GameUtility.SetTooltipTransformInsideScreen(this.mRectTransform, inTransform, new Vector3(), false, (RectTransform) null);
    GameUtility.SetActive(this.gameObject, true);
  }

  public void ShowErrorChance(RectTransform inTransform, RacingVehicle inVehicle, int inPitStrategy)
  {
    if ((UnityEngine.Object) this.mRectTransform == (UnityEngine.Object) null)
      this.Awake();
    SessionStrategy.PitStrategy inStrategy = (SessionStrategy.PitStrategy) inPitStrategy;
    GameUtility.SetTooltipTransformInsideScreen(this.mRectTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
    this.mSetup = inVehicle.setup;
    GameUtility.SetActive(this.headerTime, false);
    GameUtility.SetActive(this.headerChance, true);
    this.grid.DestroyListItems();
    GameUtility.SetActive(this.grid.itemPrefab, true);
    string percentageText = GameUtility.GetPercentageText(this.mSetup.changes.GetStrategyErrorPercentForDisplay(inStrategy), 0.0f, true, false);
    if (!this.mSetup.changes.isRiskTakerBonusActive(inStrategy))
    {
      StringVariableParser.stringValue1 = Localisation.LocaliseEnum((Enum) inStrategy);
      this.CreateEntry(Localisation.LocaliseID("PSG_10010351", (GameObject) null), percentageText);
    }
    else
      this.CreateEntry(Localisation.LocaliseID("PSG_10010358", (GameObject) null), percentageText);
    this.CreateEntry(Localisation.LocaliseID("PSG_10010359", (GameObject) null), GameUtility.GetPercentageText(this.mSetup.changes.GetMechanicErrorPercentForDisplay(), 0.0f, true, false), UIConstants.positiveColor);
    GameUtility.SetActive(this.grid.itemPrefab, false);
    GameUtility.SetTooltipTransformInsideScreen(this.mRectTransform, inTransform, new Vector3(), false, (RectTransform) null);
    GameUtility.SetActive(this.gameObject, true);
  }

  private void CreateEntry(string InTitle, float inTimeImpact, bool hasTimeImpact = true)
  {
    Color inColor = !hasTimeImpact ? UIConstants.pitStopChangedOrange : ((double) inTimeImpact < 0.0 ? UIConstants.positiveColor : UIConstants.negativeColor);
    this.grid.CreateListItem<UIFinanceDetailsEntry>().SetLabels(InTitle, inTimeImpact, inColor, hasTimeImpact);
  }

  private void CreateEntry(string InTitle, string inValue)
  {
    this.grid.CreateListItem<UIFinanceDetailsEntry>().SetLabels(InTitle, inValue);
  }

  private void CreateEntry(string InTitle, string inValue, Color inColor)
  {
    this.grid.CreateListItem<UIFinanceDetailsEntry>().SetLabels(InTitle, inValue, inColor);
  }

  public override void Hide()
  {
    GameUtility.SetActive(this.gameObject, false);
  }
}
