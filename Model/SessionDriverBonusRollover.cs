// Decompiled with JetBrains decompiler
// Type: SessionDriverBonusRollover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SessionDriverBonusRollover : MonoBehaviour
{
  private string mCurrentLanguage = string.Empty;
  private List<SessionCarBonuses.DisplayBonusInfo> mBonusesCar1 = new List<SessionCarBonuses.DisplayBonusInfo>();
  private List<SessionCarBonuses.DisplayBonusInfo> mBonusesCar2 = new List<SessionCarBonuses.DisplayBonusInfo>();
  private List<PersonalityTrait> mTraitsCar1 = new List<PersonalityTrait>();
  private List<PersonalityTrait> mTraitsCar2 = new List<PersonalityTrait>();
  private bool[] mTraitsDisplayStatusCar1 = new bool[0];
  private bool[] mTraitsDisplayStatusCar2 = new bool[0];
  private StringBuilder mDescriptionBuilder = new StringBuilder();
  private Vector3[] mMouseOffset = new Vector3[CarManager.carCount];
  public UIGridList bonusGridListCar1;
  public UIGridList bonusGridListCar2;
  public GameObject noActiveBonuses;
  private bool anyActiveBonuses;
  private RectTransform mRectTransform;
  private int mCurrentCarID;

  protected void Awake()
  {
    this.mRectTransform = this.GetComponent<RectTransform>();
  }

  private void Update()
  {
    GameUtility.SetTooltipTransformInsideScreen(this.mRectTransform, (RectTransform) null, this.mMouseOffset[this.mCurrentCarID], false, (RectTransform) null);
    if (!this.CheckRegenerate())
      return;
    this.Show(this.mCurrentCarID);
  }

  private void RegenerateBonusEntries()
  {
    for (int inCarID = 0; inCarID < CarManager.carCount; ++inCarID)
    {
      this.mMouseOffset[inCarID] = new Vector3(30f, 15f, 0.0f);
      List<SessionCarBonuses.DisplayBonusInfo> bonusesForCar = this.GetBonusesForCar(inCarID);
      UIGridList gridList = this.GetGridList(inCarID);
      gridList.DestroyListItems();
      for (int index = 0; index < bonusesForCar.Count; ++index)
      {
        StringVariableParser.knowledgeLevel = bonusesForCar[index].bonusLevel;
        StringVariableParser.knowledgeAmount = bonusesForCar[index].bonusAmount;
        gridList.CreateListItem<UIRolloverBonusEntry>().SetupBonusEntry(Localisation.LocaliseID(bonusesForCar[index].nameID, (GameObject) null), Localisation.LocaliseID(bonusesForCar[index].descriptionID, (GameObject) null), bonusesForCar[index].GetSprite(), bonusesForCar[index].bonusLevel);
        this.mMouseOffset[inCarID].y += 45f;
      }
      List<PersonalityTrait> traitsForCar = this.GetTraitsForCar(inCarID);
      bool[] traitsDisplayForCar = this.GetTraitsDisplayForCar(inCarID);
      for (int index1 = 0; index1 < traitsForCar.Count; ++index1)
      {
        if (traitsForCar[index1].hasSpecialCase && !traitsForCar[index1].CanApplyTrait() || !traitsForCar[index1].IsTraitRaceTrait())
        {
          traitsDisplayForCar[index1] = false;
        }
        else
        {
          traitsDisplayForCar[index1] = true;
          this.mDescriptionBuilder.Remove(0, this.mDescriptionBuilder.Length);
          this.mDescriptionBuilder.Append(traitsForCar[index1].description);
          int num = 0;
          for (int index2 = 0; index2 < PersonalityTraitDataManager.raceStats.Count; ++index2)
          {
            if (traitsForCar[index1].DoesModifyStat(PersonalityTraitDataManager.raceStats[index2]))
            {
              this.mDescriptionBuilder.Append("  ");
              this.mDescriptionBuilder.Append(Localisation.LocaliseEnum((Enum) PersonalityTraitDataManager.raceStats[index2]));
              this.mDescriptionBuilder.Append(": ");
              this.mDescriptionBuilder.Append(traitsForCar[index1].GetSingleModifierForStatText(PersonalityTraitDataManager.raceStats[index2]));
              this.mDescriptionBuilder.Append(",");
              ++num;
            }
          }
          this.mDescriptionBuilder.Remove(this.mDescriptionBuilder.Length - 1, 1);
          if (num == PersonalityTraitDataManager.raceStats.Count)
          {
            this.mDescriptionBuilder.Remove(0, this.mDescriptionBuilder.Length);
            this.mDescriptionBuilder.Append(traitsForCar[index1].description);
            this.mDescriptionBuilder.Append("  ");
            this.mDescriptionBuilder.Append(Localisation.LocaliseID("PSG_10006830", (GameObject) null));
            this.mDescriptionBuilder.Append(": ");
            this.mDescriptionBuilder.Append(traitsForCar[index1].GetSingleModifierForStatText(PersonalityTraitDataManager.raceStats[0]));
          }
          gridList.CreateListItem<UIRolloverBonusEntry>().SetupBonusEntry(traitsForCar[index1].name, this.mDescriptionBuilder.ToString(), (Sprite) null, 0);
          this.mMouseOffset[inCarID].y += 45f;
        }
      }
    }
    this.mCurrentLanguage = Localisation.currentLanguage;
  }

  public void Setup(int inCarID, List<SessionCarBonuses.DisplayBonusInfo> inBonuses, List<PersonalityTrait> inTraits)
  {
    this.SetBonusesForCar(inBonuses, inCarID);
    if (inTraits != null)
      this.SetTraitsForCar(inTraits, inCarID);
    this.RegenerateBonusEntries();
  }

  public void Show(int inCarID)
  {
    scSoundManager.BlockSoundEvents = true;
    this.mCurrentCarID = inCarID;
    if ((UnityEngine.Object) this.mRectTransform == (UnityEngine.Object) null)
      this.Awake();
    this.CheckRegenerate();
    GameUtility.SetTooltipTransformInsideScreen(this.mRectTransform, (RectTransform) null, this.mMouseOffset[this.mCurrentCarID], false, (RectTransform) null);
    this.gameObject.SetActive(true);
    this.HideOtherLists(inCarID);
    this.anyActiveBonuses = this.GetBonusesForCar(inCarID).Count > 0 || this.AnyTraitsForCar(inCarID);
    if (this.anyActiveBonuses)
    {
      GameUtility.SetActive(this.GetGridList(inCarID).gameObject, true);
      this.GetGridList(inCarID).ShowListItems();
      GameUtility.SetActive(this.noActiveBonuses, false);
    }
    else
      GameUtility.SetActive(this.noActiveBonuses, true);
    scSoundManager.BlockSoundEvents = false;
  }

  private bool CheckRegenerate()
  {
    if (this.mCurrentLanguage != Localisation.currentLanguage)
    {
      this.RegenerateBonusEntries();
      return true;
    }
    for (int inCarID = 0; inCarID < CarManager.carCount; ++inCarID)
    {
      List<PersonalityTrait> traitsForCar = this.GetTraitsForCar(inCarID);
      bool[] traitsDisplayForCar = this.GetTraitsDisplayForCar(inCarID);
      for (int index = 0; index < traitsForCar.Count; ++index)
      {
        if (traitsForCar[index].hasSpecialCase && !traitsForCar[index].CanApplyTrait() || !traitsForCar[index].IsTraitRaceTrait())
        {
          if (traitsDisplayForCar[index])
          {
            this.RegenerateBonusEntries();
            return true;
          }
        }
        else if (!traitsDisplayForCar[index])
        {
          this.RegenerateBonusEntries();
          return true;
        }
      }
    }
    return false;
  }

  public void Hide()
  {
    this.gameObject.SetActive(false);
  }

  private List<SessionCarBonuses.DisplayBonusInfo> GetBonusesForCar(int inCarID)
  {
    switch (inCarID)
    {
      case 0:
        return this.mBonusesCar1;
      case 1:
        return this.mBonusesCar2;
      default:
        Debug.LogWarning((object) "Tried to get the session bonuses for car ID != 0 or 1", (UnityEngine.Object) null);
        return (List<SessionCarBonuses.DisplayBonusInfo>) null;
    }
  }

  private List<PersonalityTrait> GetTraitsForCar(int inCarID)
  {
    switch (inCarID)
    {
      case 0:
        return this.mTraitsCar1;
      case 1:
        return this.mTraitsCar2;
      default:
        Debug.LogWarning((object) "Tried to get the driver traits for car ID != 0 or 1", (UnityEngine.Object) null);
        return (List<PersonalityTrait>) null;
    }
  }

  private bool[] GetTraitsDisplayForCar(int inCarID)
  {
    switch (inCarID)
    {
      case 0:
        return this.mTraitsDisplayStatusCar1;
      case 1:
        return this.mTraitsDisplayStatusCar2;
      default:
        Debug.LogWarning((object) "Tried to get the driver traits for car ID != 0 or 1", (UnityEngine.Object) null);
        return (bool[]) null;
    }
  }

  private bool AnyTraitsForCar(int inCarID)
  {
    foreach (bool flag in this.GetTraitsDisplayForCar(inCarID))
    {
      if (flag)
        return true;
    }
    return false;
  }

  private void SetBonusesForCar(List<SessionCarBonuses.DisplayBonusInfo> inBonuses, int inCarID)
  {
    switch (inCarID)
    {
      case 0:
        this.mBonusesCar1 = inBonuses;
        break;
      case 1:
        this.mBonusesCar2 = inBonuses;
        break;
      default:
        Debug.LogWarning((object) "Tried to setup the session bonus rollover for car ID != 0 or 1", (UnityEngine.Object) null);
        break;
    }
  }

  private void SetTraitsForCar(List<PersonalityTrait> inTraits, int inCarID)
  {
    switch (inCarID)
    {
      case 0:
        this.mTraitsCar1 = inTraits;
        this.mTraitsDisplayStatusCar1 = new bool[this.mTraitsCar1.Count];
        break;
      case 1:
        this.mTraitsCar2 = inTraits;
        this.mTraitsDisplayStatusCar2 = new bool[this.mTraitsCar2.Count];
        break;
      default:
        Debug.LogWarning((object) "Tried to setup the session bonus rollover for car ID != 0 or 1", (UnityEngine.Object) null);
        break;
    }
  }

  private UIGridList GetGridList(int inCarID)
  {
    switch (inCarID)
    {
      case 0:
        return this.bonusGridListCar1;
      case 1:
        return this.bonusGridListCar2;
      default:
        Debug.LogWarning((object) "Tried to get the session bonus rollover for car ID != 0 or 1", (UnityEngine.Object) null);
        return (UIGridList) null;
    }
  }

  private void HideOtherLists(int inCarID)
  {
    switch (inCarID)
    {
      case 0:
        GameUtility.SetActive(this.bonusGridListCar2.gameObject, false);
        break;
      case 1:
        GameUtility.SetActive(this.bonusGridListCar1.gameObject, false);
        break;
      default:
        Debug.LogWarning((object) "Tried to preserve the bonuses for car ID != 0/1 in the session bonus rollover", (UnityEngine.Object) null);
        break;
    }
  }
}
