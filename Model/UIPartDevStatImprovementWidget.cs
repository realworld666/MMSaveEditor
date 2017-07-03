// Decompiled with JetBrains decompiler
// Type: UIPartDevStatImprovementWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPartDevStatImprovementWidget : MonoBehaviour
{
  public CarPartStats.CarPartStat stat = CarPartStats.CarPartStat.Count;
  public GameObject fillBarContainer;
  public GameObject queueTextContainer;
  public TextMeshProUGUI finishDateDescription;
  public TextMeshProUGUI finishDateTrack;
  public Button clearPartsStack;
  public Button autoFillPartsStack;
  public Slider workCompleteSlider;
  public UIGridList itemList;
  public TextMeshProUGUI queueStateText;
  public TextMeshProUGUI queueStateDescription;
  private PartImprovement mPartImprovement;

  private void Start()
  {
    this.clearPartsStack.onClick.AddListener(new UnityAction(this.ClearPartStack));
    this.autoFillPartsStack.onClick.AddListener(new UnityAction(this.AutoFillPartStack));
  }

  private void AutoFillPartStack()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mPartImprovement.AutoFill(this.stat);
  }

  private void ClearPartStack()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mPartImprovement.RemoveAllPartImprove(this.stat);
  }

  public void Setup()
  {
    this.mPartImprovement = Game.instance.player.team.carManager.partImprovement;
    this.RefreshTimeLabels();
    this.RecreateItemsOnStacks();
  }

  public void UpdateWidget()
  {
    this.mPartImprovement = Game.instance.player.team.carManager.partImprovement;
    this.RefreshTimeLabels();
    this.UpdateWorkDone();
  }

  private void UpdateWorkDone()
  {
    for (int inIndex = 0; inIndex < this.itemList.itemCount; ++inIndex)
    {
      UIPartDevStatImprovementPartEntry improvementPartEntry = this.itemList.GetItem<UIPartDevStatImprovementPartEntry>(inIndex);
      if (improvementPartEntry.part != null)
        improvementPartEntry.UpdateStats();
    }
  }

  private void RefreshTimeLabels()
  {
    this.finishDateDescription.color = Color.white;
    if (this.mPartImprovement.WorkOnStatActive(this.stat))
    {
      DateTime inEndDate = this.mPartImprovement.partWorkEndDate[(int) this.stat];
      this.finishDateDescription.text = UIPartDevStatImprovementWidget.GetDateText(inEndDate);
      this.finishDateTrack.text = UIPartDevStatImprovementWidget.GetEventText(inEndDate);
      GameUtility.SetActive(this.fillBarContainer, true);
      GameUtility.SetActive(this.queueTextContainer, false);
    }
    else
    {
      GameUtility.SetActive(this.fillBarContainer, false);
      GameUtility.SetActive(this.queueTextContainer, true);
    }
    this.workCompleteSlider.value = 0.0f;
    if (this.mPartImprovement.partsToImprove[(int) this.stat].Count == 0)
    {
      this.queueStateText.transform.parent.gameObject.SetActive(true);
      this.queueStateText.text = Localisation.LocaliseID("PSG_10010381", (GameObject) null);
      StringVariableParser.partStat = this.stat;
      this.queueStateDescription.text = Localisation.LocaliseID("PSG_10010384", (GameObject) null);
    }
    else
    {
      this.workCompleteSlider.value = this.mPartImprovement.GetNormalizedTimeToFinishWork(this.stat);
      if (this.mPartImprovement.HasAvailableSlot(this.stat))
      {
        GameUtility.SetActive(this.queueStateText.transform.parent.gameObject, false);
      }
      else
      {
        GameUtility.SetActive(this.queueStateText.transform.parent.gameObject, true);
        this.queueStateText.text = Localisation.LocaliseID("PSG_10010382", (GameObject) null);
        this.queueStateDescription.text = Localisation.LocaliseID("PSG_10010383", (GameObject) null);
      }
    }
  }

  public void RefreshItemsData()
  {
    for (int inIndex = 0; inIndex < this.mPartImprovement.partsToImprove[(int) this.stat].Count; ++inIndex)
    {
      CarPart inPart = this.mPartImprovement.partsToImprove[(int) this.stat][inIndex];
      if (inPart != null)
        this.itemList.GetItem<UIPartDevStatImprovementPartEntry>(inIndex).Setup(inPart, UIPartDevStatImprovementPartEntry.State.Used, this.stat);
    }
    for (int count = this.mPartImprovement.partsToImprove[(int) this.stat].Count; count < this.mPartImprovement.GetPartSlotsMaximumCount(); ++count)
    {
      UIPartDevStatImprovementPartEntry improvementPartEntry = this.itemList.GetItem<UIPartDevStatImprovementPartEntry>(count);
      if (count < this.mPartImprovement.GetPartSlotsCount())
        improvementPartEntry.Setup((CarPart) null, UIPartDevStatImprovementPartEntry.State.Free, this.stat);
      else
        improvementPartEntry.Setup((CarPart) null, UIPartDevStatImprovementPartEntry.State.Locked, this.stat);
    }
  }

  private void RecreateItemsOnStacks()
  {
    this.itemList.DestroyListItems();
    for (int index = 0; index < this.mPartImprovement.GetPartSlotsMaximumCount(); ++index)
      this.itemList.CreateListItem<UIPartDevStatImprovementPartEntry>();
    this.RefreshItemsData();
  }

  public static int GetHoursToCompletion(DateTime inEndDate)
  {
    return Mathf.CeilToInt(Mathf.Abs((float) (Game.instance.time.now - inEndDate).TotalHours));
  }

  public static string GetHoursToCompletionString(DateTime inEndDate)
  {
    StringVariableParser.intValue1 = UIPartDevStatImprovementWidget.GetHoursToCompletion(inEndDate);
    return Localisation.LocaliseID("PSG_10010385", (GameObject) null);
  }

  public static string GetDateText(DateTime inEndDate)
  {
    string empty = string.Empty;
    TimeSpan timeSpan = Game.instance.player.team.championship.GetCurrentEventDetails().eventDate - inEndDate;
    int num1 = Mathf.RoundToInt((float) timeSpan.TotalDays);
    string str;
    if (num1 == 0)
    {
      int hours = timeSpan.Hours;
      if (hours == 0)
      {
        int minutes = timeSpan.Minutes;
        if (minutes > 0)
        {
          StringVariableParser.intValue1 = minutes;
          str = GameUtility.ColorToRichTextHex(UIConstants.positiveColor) + (minutes <= 1 ? Localisation.LocaliseID("PSG_10010387", (GameObject) null) : Localisation.LocaliseID("PSG_10010386", (GameObject) null)) + "</color>";
        }
        else
        {
          int num2 = Mathf.Abs(minutes);
          StringVariableParser.intValue1 = num2;
          str = GameUtility.ColorToRichTextHex(UIConstants.negativeColor) + (num2 <= 1 ? Localisation.LocaliseID("PSG_10010387", (GameObject) null) : Localisation.LocaliseID("PSG_10010386", (GameObject) null)) + "</color>";
        }
      }
      else if (hours > 0)
      {
        StringVariableParser.intValue1 = hours;
        str = GameUtility.ColorToRichTextHex(UIConstants.positiveColor) + (hours <= 1 ? Localisation.LocaliseID("PSG_10010440", (GameObject) null) : Localisation.LocaliseID("PSG_10010439", (GameObject) null)) + "</color>";
      }
      else
      {
        int num2 = Mathf.Abs(hours);
        StringVariableParser.intValue1 = num2;
        str = GameUtility.ColorToRichTextHex(UIConstants.negativeColor) + (num2 <= 1 ? Localisation.LocaliseID("PSG_10010440", (GameObject) null) : Localisation.LocaliseID("PSG_10010439", (GameObject) null)) + "</color>";
      }
    }
    else if (num1 > 0)
    {
      StringVariableParser.intValue1 = num1;
      str = GameUtility.ColorToRichTextHex(UIConstants.positiveColor) + (num1 <= 1 ? Localisation.LocaliseID("PSG_10010442", (GameObject) null) : Localisation.LocaliseID("PSG_10010441", (GameObject) null)) + "</color>";
    }
    else
    {
      int num2 = Mathf.Abs(num1);
      StringVariableParser.intValue1 = num2;
      str = GameUtility.ColorToRichTextHex(UIConstants.negativeColor) + (num2 <= 1 ? Localisation.LocaliseID("PSG_10010442", (GameObject) null) : Localisation.LocaliseID("PSG_10010441", (GameObject) null)) + "</color>";
    }
    return str;
  }

  public static string GetEventText(DateTime inEndDate)
  {
    TimeSpan timeSpan = Game.instance.player.team.championship.GetCurrentEventDetails().eventDate - inEndDate;
    int days = timeSpan.Days;
    if (days == 0)
    {
      int hours = timeSpan.Hours;
      if (hours == 0)
      {
        if (timeSpan.Minutes > 0)
          return Localisation.LocaliseID("PSG_10010437", (GameObject) null);
        return Localisation.LocaliseID("PSG_10010438", (GameObject) null);
      }
      if (hours > 0)
        return Localisation.LocaliseID("PSG_10010437", (GameObject) null);
      return Localisation.LocaliseID("PSG_10010438", (GameObject) null);
    }
    if (days > 0)
      return Localisation.LocaliseID("PSG_10010437", (GameObject) null);
    return Localisation.LocaliseID("PSG_10010438", (GameObject) null);
  }
}
