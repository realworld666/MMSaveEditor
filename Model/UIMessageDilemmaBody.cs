// Decompiled with JetBrains decompiler
// Type: UIMessageDilemmaBody
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMessageDilemmaBody : UIMailMessageBody
{
  public List<Button> choices = new List<Button>();
  public List<UIGridList> optionsObjects = new List<UIGridList>();
  public List<UIGridList> resultsObjects = new List<UIGridList>();
  [Header("Derived Class Settings")]
  public GameObject choiceTakenContainer;
  public GameObject optionsContainer;

  protected override void SetupButtons()
  {
    this.buttonsContainer.SetActive(this.message.buttonsRule != null);
    if (!this.message.responded && Game.instance.dilemmaSystem.IsSubjectNotInPlayerTeamAnymore(this.message))
    {
      TextDynamicData textDynamicData = new TextDynamicData();
      textDynamicData.SetMessageTextFields(string.Empty);
      this.message.messageResponseData = textDynamicData;
      this.message.SetResponded();
      this.message.SetPriorityType(Message.Priority.Normal);
      this.message.hasBeenRead = true;
    }
    this.choiceTakenContainer.SetActive(this.message.buttonsRule != null && this.message.responded);
    this.optionsContainer.SetActive(this.message.buttonsRule != null && !this.message.responded);
    if (this.message.buttonsRule == null)
      return;
    this.buttonActionText.gameObject.SetActive(false);
    if (this.message.responded)
      this.SetupOptions(this.resultsObjects, this.choices, true);
    else
      this.SetupOptions(this.optionsObjects, this.buttons, false);
  }

  private void SetupOptions(List<UIGridList> inOptions, List<Button> inOptionButtons, bool inMessageResponded)
  {
    for (int index1 = 0; index1 < inOptions.Count; ++index1)
    {
      Button inOptionButton = inOptionButtons[index1];
      bool inIsActive = false;
      for (int index2 = index1; index2 < this.message.buttonsRule.userData.Count; ++index2)
      {
        DialogCriteria dialogCriteria = this.message.buttonsRule.userData[index2];
        if (this.message.responded == inMessageResponded && dialogCriteria.mType.Equals("mailbutton", StringComparison.OrdinalIgnoreCase) && this.ShowToPlayer(dialogCriteria.mCriteriaInfo))
        {
          this.AssignButton(this.message, inOptionButton, dialogCriteria.mCriteriaInfo);
          TextMeshProUGUI componentInChildren = inOptionButton.GetComponentInChildren<TextMeshProUGUI>();
          componentInChildren.text = Game.instance.dilemmaSystem.GetLocalisedLabelForButton(dialogCriteria.mCriteriaInfo, this.message);
          if (!this.message.responded)
            inOptionButton.interactable = Game.instance.dilemmaSystem.ShouldMailButtonBeInteractable(dialogCriteria.mCriteriaInfo, this.message);
          else
            inOptionButton.interactable = false;
          inIsActive = true;
          if (this.message.responded)
            inOptionButton.GetComponent<CanvasGroup>().alpha = !(this.message.messageResponseData.textTranslated == componentInChildren.text) ? 0.5f : 1f;
          this.ShowDilemmaChoiceImpacts(inOptions[index1], dialogCriteria.mCriteriaInfo);
          break;
        }
      }
      GameUtility.SetActive(inOptionButton.gameObject, inIsActive);
      GameUtility.SetActive(inOptions[index1].gameObject, inIsActive);
    }
  }

  private void ShowDilemmaChoiceImpacts(UIGridList inGridToPopulate, string inButtonCriteria)
  {
    List<string> dilemmaImpactStrings = Game.instance.dilemmaSystem.GetDilemmaImpactStrings(this.message, inButtonCriteria);
    inGridToPopulate.DestroyListItems();
    GameUtility.SetActive(inGridToPopulate.itemPrefab, true);
    for (int index = 0; index < dilemmaImpactStrings.Count; ++index)
      inGridToPopulate.CreateListItem<TextMeshProUGUI>().text = dilemmaImpactStrings[index];
    GameUtility.SetActive(inGridToPopulate.itemPrefab, false);
  }
}
