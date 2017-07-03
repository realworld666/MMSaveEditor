// Decompiled with JetBrains decompiler
// Type: UIHUBKnowledgePopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHUBKnowledgePopup : UIDialogBox
{
  private MechanicBonus.Trait[] mActiveBonuses = new MechanicBonus.Trait[2];
  private PracticeReportSessionData.KnowledgeType[] mActiveKnowledge = new PracticeReportSessionData.KnowledgeType[2];
  public UIGridList practiceGridBonuses;
  public UIGridList mechanicGridBonuses;
  public GameObject noPracticeBonuses;
  public GameObject noMechanicBonuses;
  public Button confirmButton;
  public Button clearButton;
  public Button exitButton;
  public UIHUBKnowledgeIcon[] knowledgeBonusIcons;
  public UIMechanicBonusIcon[] mechanicBonusIcons;
  private UIHUBKnowledgeEntry mCurrentKnowledgeEntry;
  private RacingVehicle mVehicle;

  public void ActivatePopup(UIHUBKnowledgeEntry inCurrentKnowledgeEntry, RacingVehicle inVehicle)
  {
    this.mVehicle = inVehicle;
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) this);
    this.mCurrentKnowledgeEntry = inCurrentKnowledgeEntry;
    this.mVehicle.bonuses.activeMechanicBonuses.CopyTo((Array) this.mActiveBonuses, 0);
    this.mVehicle.bonuses.activeKnowledgeBonuses.CopyTo((Array) this.mActiveKnowledge, 0);
    this.confirmButton.onClick.RemoveAllListeners();
    this.confirmButton.onClick.AddListener(new UnityAction(this.OnConfirmButton));
    this.clearButton.onClick.RemoveAllListeners();
    this.clearButton.onClick.AddListener(new UnityAction(this.OnClearButton));
    this.exitButton.onClick.RemoveAllListeners();
    this.exitButton.onClick.AddListener(new UnityAction(this.OnExitButton));
    this.PopulatePracticeBonuses();
    this.PopulateMechanicBonuses(this.mVehicle);
    for (int index = 0; index < this.knowledgeBonusIcons.Length; ++index)
    {
      GameUtility.SetActive(this.knowledgeBonusIcons[index].gameObject, false);
      GameUtility.SetActive(this.mechanicBonusIcons[index].gameObject, false);
    }
    this.UpdateSelectedIcons();
    this.UpdateToggleInteractable();
  }

  private void PopulatePracticeBonuses()
  {
    this.practiceGridBonuses.DestroyListItems();
    foreach (PracticeReportKnowledgeData availableKnowledgeBonu in this.mVehicle.bonuses.GetAvailableKnowledgeBonus(false))
    {
      UIHUBKnowledgeSelection listItem = this.practiceGridBonuses.CreateListItem<UIHUBKnowledgeSelection>();
      PracticeReportSessionData.KnowledgeType knowledgeType = availableKnowledgeBonu.knowledgeType;
      listItem.toggle.onValueChanged.RemoveAllListeners();
      listItem.toggle.interactable = this.mVehicle.bonuses.isUsefullForSession(knowledgeType);
      listItem.toggle.isOn = listItem.toggle.interactable && this.mVehicle.bonuses.IsBonusActive(knowledgeType);
      listItem.SetupForPracticeKnowledge(this, knowledgeType);
    }
    GameUtility.SetActive(this.noPracticeBonuses, this.practiceGridBonuses.itemCount == 0);
  }

  private void PopulateMechanicBonuses(RacingVehicle inVehicle)
  {
    this.mechanicGridBonuses.DestroyListItems();
    foreach (MechanicBonus availableMechanicBonu in this.mVehicle.bonuses.GetAvailableMechanicBonus(false))
    {
      UIHUBKnowledgeSelection listItem = this.mechanicGridBonuses.CreateListItem<UIHUBKnowledgeSelection>();
      MechanicBonus inMechanicBonus = availableMechanicBonu;
      listItem.toggle.onValueChanged.RemoveAllListeners();
      listItem.toggle.interactable = this.mVehicle.bonuses.isUsefullForSession(inMechanicBonus);
      listItem.toggle.isOn = listItem.toggle.interactable && this.mVehicle.bonuses.IsBonusActive(inMechanicBonus.trait);
      listItem.SetupForMechanicBonus(this, inMechanicBonus);
    }
    GameUtility.SetActive(this.noMechanicBonuses, this.mechanicGridBonuses.itemCount == 0);
  }

  public void SelectKnowledgeBonus(PracticeReportSessionData.KnowledgeType inKnowledge)
  {
    this.SetBonus(inKnowledge);
    this.UpdateSelectedIcons();
    this.UpdateToggleInteractable();
  }

  public void SelectMechanicBonus(MechanicBonus inMechanicBonus)
  {
    this.SetBonus(inMechanicBonus.trait);
    this.UpdateSelectedIcons();
    this.UpdateToggleInteractable();
  }

  public void DeselectKnowledgeBonus(PracticeReportSessionData.KnowledgeType inKnowledge)
  {
    this.RemoveBonus(inKnowledge);
    this.UpdateSelectedIcons();
    this.UpdateToggleInteractable();
  }

  public void DeselectMechanicBonus(MechanicBonus inMechanicBonus)
  {
    this.RemoveBonus(inMechanicBonus.trait);
    this.UpdateSelectedIcons();
    this.UpdateToggleInteractable();
  }

  private void UpdateSelectedIcons()
  {
    PracticeReportSessionData.KnowledgeType[] knowledgeBonuses = this.mVehicle.bonuses.activeKnowledgeBonuses;
    MechanicBonus.Trait[] activeMechanicBonuses = this.mVehicle.bonuses.activeMechanicBonuses;
    for (int index1 = 0; index1 < this.mechanicBonusIcons.Length; ++index1)
    {
      for (int index2 = 0; index2 < knowledgeBonuses.Length; ++index2)
      {
        if (knowledgeBonuses[index2] != PracticeReportSessionData.KnowledgeType.Count)
        {
          GameUtility.SetActive(this.knowledgeBonusIcons[index1].gameObject, true);
          GameUtility.SetActive(this.mechanicBonusIcons[index1].gameObject, false);
          this.knowledgeBonusIcons[index1].SetupForKnowledgeType(knowledgeBonuses[index2]);
          ++index1;
        }
        if (index1 < this.mechanicBonusIcons.Length && activeMechanicBonuses[index2] != MechanicBonus.Trait.Count)
        {
          MechanicBonus bonus = Game.instance.mechanicBonusManager.GetBonus(activeMechanicBonuses[index2]);
          if (bonus != null)
          {
            GameUtility.SetActive(this.knowledgeBonusIcons[index1].gameObject, false);
            GameUtility.SetActive(this.mechanicBonusIcons[index1].gameObject, true);
            this.mechanicBonusIcons[index1].Setup(true, bonus);
            ++index1;
          }
        }
        if (index1 < this.mechanicBonusIcons.Length)
        {
          GameUtility.SetActive(this.knowledgeBonusIcons[index1].gameObject, false);
          GameUtility.SetActive(this.mechanicBonusIcons[index1].gameObject, false);
        }
      }
    }
  }

  private void OnConfirmButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.Hide();
    this.SetBonuses();
    this.mCurrentKnowledgeEntry.UpdateKnowledgeSlotsWithSelectedBonus();
  }

  private void OnExitButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.Hide();
    this.ClearToggles();
  }

  private void OnClearButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.ClearToggles();
  }

  private void ClearToggles()
  {
    for (int inIndex = 0; inIndex < this.practiceGridBonuses.itemCount; ++inIndex)
    {
      UIHUBKnowledgeSelection knowledgeSelection = this.practiceGridBonuses.GetItem<UIHUBKnowledgeSelection>(inIndex);
      knowledgeSelection.toggle.isOn = false;
      knowledgeSelection.canvasGroup.alpha = !knowledgeSelection.toggle.interactable ? 0.5f : 1f;
    }
    for (int inIndex = 0; inIndex < this.mechanicGridBonuses.itemCount; ++inIndex)
    {
      UIHUBKnowledgeSelection knowledgeSelection = this.mechanicGridBonuses.GetItem<UIHUBKnowledgeSelection>(inIndex);
      knowledgeSelection.toggle.isOn = false;
      knowledgeSelection.canvasGroup.alpha = !knowledgeSelection.toggle.interactable ? 0.5f : 1f;
    }
    this.Reset();
  }

  private void UpdateToggleInteractable()
  {
    int selectedBonusesNumber = this.GetSelectedBonusesNumber();
    for (int inIndex = 0; inIndex < this.practiceGridBonuses.itemCount; ++inIndex)
    {
      UIHUBKnowledgeSelection knowledgeSelection = this.practiceGridBonuses.GetItem<UIHUBKnowledgeSelection>(inIndex);
      if (!knowledgeSelection.toggle.isOn)
      {
        knowledgeSelection.toggle.interactable = this.mVehicle.bonuses.isUsefullForSession(knowledgeSelection.knowledgeType) && selectedBonusesNumber < 2;
        knowledgeSelection.canvasGroup.alpha = !knowledgeSelection.toggle.interactable ? 0.5f : 1f;
      }
    }
    for (int inIndex = 0; inIndex < this.mechanicGridBonuses.itemCount; ++inIndex)
    {
      UIHUBKnowledgeSelection knowledgeSelection = this.mechanicGridBonuses.GetItem<UIHUBKnowledgeSelection>(inIndex);
      if (!knowledgeSelection.toggle.isOn)
      {
        knowledgeSelection.toggle.interactable = this.mVehicle.bonuses.isUsefullForSession(knowledgeSelection.mechanicBonus) && selectedBonusesNumber < 2;
        knowledgeSelection.canvasGroup.alpha = !knowledgeSelection.toggle.interactable ? 0.5f : 1f;
      }
    }
  }

  public void Reset()
  {
    for (int index = 0; index < 2; ++index)
    {
      this.mActiveBonuses[index] = MechanicBonus.Trait.Count;
      this.mActiveKnowledge[index] = PracticeReportSessionData.KnowledgeType.Count;
    }
  }

  public bool SetBonus(MechanicBonus.Trait inBonus)
  {
    for (int index = 0; index < this.mActiveBonuses.Length; ++index)
    {
      if (this.mActiveBonuses[index] == MechanicBonus.Trait.Count)
      {
        this.mActiveBonuses[index] = inBonus;
        return true;
      }
    }
    return false;
  }

  public bool SetBonus(PracticeReportSessionData.KnowledgeType inKnowledge)
  {
    for (int index = 0; index < this.mActiveKnowledge.Length; ++index)
    {
      if (this.mActiveKnowledge[index] == PracticeReportSessionData.KnowledgeType.Count)
      {
        this.mActiveKnowledge[index] = inKnowledge;
        return true;
      }
    }
    return false;
  }

  public bool RemoveBonus(MechanicBonus.Trait inBonus)
  {
    for (int index = 0; index < this.mActiveBonuses.Length; ++index)
    {
      if (this.mActiveBonuses[index] == inBonus)
      {
        this.mActiveBonuses[index] = MechanicBonus.Trait.Count;
        return true;
      }
    }
    return false;
  }

  public bool RemoveBonus(PracticeReportSessionData.KnowledgeType inKnowledge)
  {
    for (int index = 0; index < this.mActiveKnowledge.Length; ++index)
    {
      if (this.mActiveKnowledge[index] == inKnowledge)
      {
        this.mActiveKnowledge[index] = PracticeReportSessionData.KnowledgeType.Count;
        return true;
      }
    }
    return false;
  }

  public int GetSelectedBonusesNumber()
  {
    int num = 0;
    for (int index = 0; index < 2; ++index)
    {
      if (this.mActiveBonuses[index] != MechanicBonus.Trait.Count)
        ++num;
      if (this.mActiveKnowledge[index] != PracticeReportSessionData.KnowledgeType.Count)
        ++num;
    }
    return num;
  }

  public void SetBonuses()
  {
    this.mVehicle.bonuses.RemoveAllBonus();
    for (int index = 0; index < 2; ++index)
    {
      if (this.mActiveBonuses[index] != MechanicBonus.Trait.Count)
        this.mVehicle.bonuses.SetBonus(this.mActiveBonuses[index], true);
      if (this.mActiveKnowledge[index] != PracticeReportSessionData.KnowledgeType.Count)
        this.mVehicle.bonuses.SetBonus(this.mActiveKnowledge[index], true);
    }
    this.mVehicle.bonuses.RefreshActiveBonusesList();
    this.mVehicle.performance.bonuses.RefreshCarPartBonuses();
  }
}
