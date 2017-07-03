// Decompiled with JetBrains decompiler
// Type: FinanceBreakdownDialogBox
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinanceBreakdownDialogBox : UIDialogBox
{
  private const int listCap = 25;
  public UIGridList transactionsList;
  public TextMeshProUGUI tooltipTitle;
  public TextMeshProUGUI tooltipHeader;
  private RectTransform mTransform;

  private void CommonSetup()
  {
    this.mTransform = this.GetComponent<RectTransform>();
    GameUtility.SetTooltipTransformInsideScreen(this.mTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
    GameUtility.SetActive(this.gameObject, true);
  }

  public void ShowRollover(List<Transaction> inList, Transaction.Group inTitleGroup, bool isPerRaceEvent)
  {
    Transaction.SetupStringVariableParserForGroup(inTitleGroup);
    this.CommonSetup();
    this.SetupTooltipList(inList);
    if (isPerRaceEvent)
    {
      StringVariableParser.financialTransaction = inTitleGroup;
      this.tooltipTitle.text = Localisation.LocaliseID("PSG_10010227", (GameObject) null);
      this.tooltipHeader.text = Localisation.LocaliseID("PSG_10010228", (GameObject) null);
    }
    else
    {
      StringVariableParser.intValue1 = Mathf.Min(inList.Count, 25);
      if (StringVariableParser.intValue1 == 1)
        this.tooltipTitle.text = Localisation.LocaliseID("PSG_10010229", (GameObject) null);
      else
        this.tooltipTitle.text = Localisation.LocaliseID("PSG_10010230", (GameObject) null);
      this.tooltipHeader.text = Localisation.LocaliseEnum((Enum) inTitleGroup);
    }
  }

  public void ShowRollover(List<Transaction> inList, string inHeaderTitle)
  {
    this.CommonSetup();
    this.SetupTooltipList(inList);
    this.tooltipTitle.text = inHeaderTitle;
    this.tooltipHeader.text = Localisation.LocaliseID("PSG_10010228", (GameObject) null);
  }

  public void HideRollover()
  {
    this.Hide();
  }

  private void Update()
  {
    GameUtility.SetTooltipTransformInsideScreen(this.mTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
  }

  private void SetupTooltipList(List<Transaction> inList)
  {
    this.transactionsList.DestroyListItems();
    GameUtility.SetActive(this.transactionsList.itemPrefab, true);
    int inIndex = 0;
    for (int index = inList.Count - 1; index >= 0; --index)
    {
      Transaction transaction = inList[index];
      this.transactionsList.GetOrCreateItem<UIFinanceDetailsEntry>(inIndex).SetLabels(transaction.name, transaction.amountWithSign);
      ++inIndex;
      if (inIndex == 24)
        break;
    }
    GameUtility.SetActive(this.transactionsList.itemPrefab, false);
  }
}
