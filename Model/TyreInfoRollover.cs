// Decompiled with JetBrains decompiler
// Type: TyreInfoRollover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TyreInfoRollover : UIDialogBox
{
  public TextMeshProUGUI tyreNameLabel;
  public TextMeshProUGUI estimatedLapsLabel;
  public GameObject wetConditionsBox;
  public GameObject wetPerformance;
  public GameObject interPerformane;
  public GameObject slicksPerformance;
  public Image speedPerformanceFill;
  public Image durabilityPerformanceFill;
  public GameObject bonusGameObject;
  public UIGridList bonusGridList;
  public GameObject[] tyreUnlockedGameObjects;
  public GameObject tyreLockedGameObject;
  public GameObject tyreNewlyUnlockedGameObject;
  private RectTransform mRectTransform;

  protected override void Awake()
  {
    this.mRectTransform = this.GetComponent<RectTransform>();
  }

  private void Update()
  {
    GameUtility.SetTooltipTransformInsideScreen(this.mRectTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
  }

  public void Show(bool inLocked, TyreSet inTyreSet, Circuit inCircuit, List<SessionCarBonuses.DisplayBonusInfo> inBonuses, bool inIsNewlyUnlocked = false)
  {
    scSoundManager.BlockSoundEvents = true;
    if ((Object) this.mRectTransform == (Object) null)
      this.Awake();
    GameUtility.SetTooltipTransformInsideScreen(this.mRectTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
    this.gameObject.SetActive(true);
    for (int index = 0; index < this.tyreUnlockedGameObjects.Length; ++index)
      GameUtility.SetActive(this.tyreUnlockedGameObjects[index], (inLocked ? 1 : (inIsNewlyUnlocked ? 1 : 0)) == 0);
    GameUtility.SetActive(this.tyreLockedGameObject, inLocked);
    GameUtility.SetActive(this.tyreNewlyUnlockedGameObject, inIsNewlyUnlocked);
    if (inLocked)
      this.tyreNameLabel.text = Localisation.LocaliseID("PSG_10012161", (GameObject) null);
    else if (inIsNewlyUnlocked)
    {
      this.tyreNameLabel.text = Localisation.LocaliseID("PSG_10012162", (GameObject) null);
    }
    else
    {
      this.tyreNameLabel.text = inTyreSet.GetName();
      int num1 = Mathf.FloorToInt(TyreSet.CalculateLapRangeOfTyre(inTyreSet, GameUtility.MilesToMeters(inCircuit.trackLengthMiles)) * inTyreSet.GetCondition());
      int num2 = num1 - 2;
      int num3 = num2 > 0 || num1 <= 1 ? num2 : 1;
      if (num3 > 0)
        this.estimatedLapsLabel.SetText(num3.ToString() + " - " + num1.ToString());
      else
        this.estimatedLapsLabel.SetText(num1.ToString());
      switch (inTyreSet.GetCompound())
      {
        case TyreSet.Compound.Intermediate:
          GameUtility.SetActive(this.wetConditionsBox, true);
          GameUtility.SetActive(this.wetPerformance, false);
          GameUtility.SetActive(this.interPerformane, true);
          GameUtility.SetActive(this.slicksPerformance, false);
          break;
        case TyreSet.Compound.Wet:
          GameUtility.SetActive(this.wetConditionsBox, true);
          GameUtility.SetActive(this.wetPerformance, true);
          GameUtility.SetActive(this.interPerformane, false);
          GameUtility.SetActive(this.slicksPerformance, false);
          break;
        default:
          GameUtility.SetActive(this.wetConditionsBox, false);
          GameUtility.SetActive(this.wetPerformance, false);
          GameUtility.SetActive(this.interPerformane, false);
          GameUtility.SetActive(this.slicksPerformance, true);
          GameUtility.SetImageFillAmountIfDifferent(this.speedPerformanceFill, inTyreSet.GetPerformanceForUI(TyreSet.Tread.Slick), 1f / 512f);
          GameUtility.SetImageFillAmountIfDifferent(this.durabilityPerformanceFill, inTyreSet.GetDurabilityForUI(), 1f / 512f);
          break;
      }
      bool inIsActive = false;
      this.bonusGridList.DestroyListItems();
      for (int index = 0; index < inBonuses.Count; ++index)
      {
        if (inBonuses[index].IsForTyreSet(inTyreSet.GetCompound()))
        {
          inIsActive = true;
          StringVariableParser.knowledgeLevel = inBonuses[index].bonusLevel;
          StringVariableParser.knowledgeAmount = inBonuses[index].bonusAmount;
          this.bonusGridList.CreateListItem<UIRolloverBonusEntry>().SetupBonusEntry(Localisation.LocaliseID(inBonuses[index].nameID, (GameObject) null), Localisation.LocaliseID(inBonuses[index].descriptionID, (GameObject) null), inBonuses[index].GetSprite(), inBonuses[index].bonusLevel);
        }
      }
      GameUtility.SetActive(this.bonusGameObject, inIsActive);
    }
    scSoundManager.BlockSoundEvents = false;
  }

  public override void Hide()
  {
    this.gameObject.SetActive(false);
  }
}
