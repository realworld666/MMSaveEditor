// Decompiled with JetBrains decompiler
// Type: DriverTraitsRollover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class DriverTraitsRollover : UIDialogBox
{
  private bool mIsDriverScreen = true;
  public TextMeshProUGUI traitName;
  public TextMeshProUGUI traitDescription;
  public UIGridList impactsList;
  [SerializeField]
  private GameObject impactsObject;
  [SerializeField]
  private GameObject timeRemaining;
  [SerializeField]
  private TextMeshProUGUI timeRemaningText;
  [SerializeField]
  private GameObject specialConditionObject;
  [SerializeField]
  private TextMeshProUGUI specialCaseDescription;
  private RectTransform mTransform;
  private PersonalityTrait mPersonalityTrait;

  public PersonalityTrait currentPersonalityTraitDisplay
  {
    get
    {
      return this.mPersonalityTrait;
    }
  }

  public void ShowRollover(PersonalityTrait inPersonalityTrait)
  {
    scSoundManager.BlockSoundEvents = true;
    this.mIsDriverScreen = UIManager.instance.IsScreenOpen("DriverScreen");
    this.mTransform = this.GetComponent<RectTransform>();
    GameUtility.SetTooltipTransformInsideScreen(this.mTransform, (RectTransform) null, new Vector3(), this.mIsDriverScreen, (RectTransform) null);
    GameUtility.SetActive(this.gameObject, true);
    this.mPersonalityTrait = inPersonalityTrait;
    this.traitName.text = inPersonalityTrait.name;
    this.traitDescription.text = inPersonalityTrait.description;
    this.impactsList.HideListItems();
    GameUtility.SetActive(this.impactsList.itemPrefab, false);
    int inIndex = 0;
    for (int index = 0; index < 18; ++index)
    {
      PersonalityTrait.StatModified inStatModified = (PersonalityTrait.StatModified) index;
      if (inStatModified != PersonalityTrait.StatModified.DesiredWins && inPersonalityTrait.DoesModifyStat(inStatModified))
      {
        GameUtility.SetActive(this.impactsObject, true);
        UIDriverStatsModifierEntry statsModifierEntry = this.impactsList.GetOrCreateItem<UIDriverStatsModifierEntry>(inIndex);
        GameUtility.SetActive(statsModifierEntry.gameObject, true);
        statsModifierEntry.Setup(Localisation.LocaliseEnum((Enum) inStatModified), inPersonalityTrait, inStatModified);
        ++inIndex;
      }
    }
    if (inIndex <= 0)
      GameUtility.SetActive(this.impactsObject, false);
    bool specialCaseDescription = inPersonalityTrait.hasSpecialCaseDescription;
    GameUtility.SetActive(this.specialConditionObject, specialCaseDescription);
    if (specialCaseDescription)
      this.specialCaseDescription.text = inPersonalityTrait.GetSpecialCaseDescriptionText();
    bool inIsActive = inPersonalityTrait.data.type == PersonalityTraitData.TraitType.Temporary;
    GameUtility.SetActive(this.timeRemaining, inIsActive);
    if (inIsActive)
      this.timeRemaningText.text = GameUtility.FormatTimeSpanWeeks(inPersonalityTrait.timeRemaning);
    scSoundManager.BlockSoundEvents = false;
  }

  public override void Hide()
  {
    base.Hide();
    this.mPersonalityTrait = (PersonalityTrait) null;
  }

  private void Update()
  {
    GameUtility.SetTooltipTransformInsideScreen(this.mTransform, (RectTransform) null, new Vector3(), this.mIsDriverScreen, (RectTransform) null);
  }
}
