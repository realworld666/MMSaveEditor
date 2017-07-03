// Decompiled with JetBrains decompiler
// Type: UIHUBKnowledgeEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHUBKnowledgeEntry : MonoBehaviour
{
  public UIHUBKnowledgeSlot[] knowledgeSlots;
  public Button autoPickButton;
  public Button clearBonuses;
  public UIHUBStrategy strategyWidget;
  private RacingVehicle mRacingVehicle;
  private bool mIsReady;

  public bool isReady
  {
    get
    {
      return this.mIsReady;
    }
  }

  public void OnStart()
  {
    this.autoPickButton.onClick.AddListener(new UnityAction(this.OnAutoPickButton));
    this.clearBonuses.onClick.AddListener(new UnityAction(this.OnClearBonusesButton));
  }

  public void Setup(RacingVehicle inVehicle)
  {
    this.mRacingVehicle = inVehicle;
    if (this.mRacingVehicle != null)
    {
      for (int index = 0; index < this.knowledgeSlots.Length; ++index)
        this.knowledgeSlots[index].Setup(this, this.mRacingVehicle, true);
    }
    this.UpdateKnowledgeSlotsWithSelectedBonus();
  }

  public void UpdateKnowledgeSlotsWithSelectedBonus()
  {
    PracticeReportSessionData.KnowledgeType[] knowledgeBonuses = this.mRacingVehicle.bonuses.activeKnowledgeBonuses;
    MechanicBonus.Trait[] activeMechanicBonuses = this.mRacingVehicle.bonuses.activeMechanicBonuses;
    int index1 = 0;
    for (int index2 = 0; index2 < knowledgeBonuses.Length; ++index2)
    {
      if (knowledgeBonuses[index2] != PracticeReportSessionData.KnowledgeType.Count)
      {
        this.knowledgeSlots[index1].SetEmpty(false);
        this.knowledgeSlots[index1].SetupPracticeKnowledgeInfo(knowledgeBonuses[index2]);
        ++index1;
      }
    }
    for (int index2 = 0; index2 < knowledgeBonuses.Length; ++index2)
    {
      if (activeMechanicBonuses[index2] != MechanicBonus.Trait.Count)
      {
        MechanicBonus bonus = Game.instance.mechanicBonusManager.GetBonus(activeMechanicBonuses[index2]);
        if (bonus != null)
        {
          this.knowledgeSlots[index1].SetEmpty(false);
          this.knowledgeSlots[index1].SetupMechanicBonusInfo(bonus);
          ++index1;
        }
      }
    }
    for (int index2 = index1; index2 < this.knowledgeSlots.Length; ++index2)
      this.knowledgeSlots[index2].SetEmpty(true);
    this.mIsReady = this.mRacingVehicle.bonuses.hasCompletedSelection();
    this.strategyWidget.OnShow();
  }

  private void OnClearBonusesButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mRacingVehicle.bonuses.Reset();
    this.UpdateKnowledgeSlotsWithSelectedBonus();
  }

  public void OnAutoPickButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mRacingVehicle.bonuses.SelectRandomBonuses();
    this.UpdateKnowledgeSlotsWithSelectedBonus();
  }
}
