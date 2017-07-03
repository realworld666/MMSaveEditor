// Decompiled with JetBrains decompiler
// Type: ContractNegotiationRollover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContractNegotiationRollover : UIDialogBox
{
  [SerializeField]
  private TextMeshProUGUI personName;
  [SerializeField]
  private TextMeshProUGUI teamName;
  [SerializeField]
  private TextMeshProUGUI personAge;
  [SerializeField]
  private UIAbilityStars personAbility;
  [SerializeField]
  private GameObject termNotMet;
  [SerializeField]
  private GameObject considering;
  [SerializeField]
  private GameObject accepted;
  [SerializeField]
  private UIGridList contractOptionEntries;
  private Person mPerson;
  private RectTransform mTransform;

  public void ShowRollover(Person inPerson)
  {
    this.mTransform = this.GetComponent<RectTransform>();
    GameUtility.SetTooltipTransformInsideScreen(this.mTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
    this.Setup(inPerson);
    GameUtility.SetActive(this.gameObject, true);
  }

  public void HideRollover()
  {
    GameUtility.SetActive(this.gameObject, false);
  }

  private void Update()
  {
    GameUtility.SetTooltipTransformInsideScreen(this.mTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
  }

  private void Setup(Person inPerson)
  {
    this.mPerson = inPerson;
    this.personName.text = this.mPerson.name;
    if (!this.mPerson.IsFreeAgent())
      this.teamName.text = this.mPerson.contract.GetTeam().name;
    else
      this.teamName.text = "-";
    this.personAge.text = this.mPerson.GetAge().ToString();
    Driver mPerson = this.mPerson as Driver;
    if (mPerson != null)
      this.personAbility.SetAbilityStarsData(mPerson);
    else
      this.personAbility.SetAbilityStarsData(this.mPerson);
    GameUtility.SetActive(this.termNotMet, this.mPerson.contractManager.isProposalRejected);
    GameUtility.SetActive(this.considering, this.mPerson.contractManager.isConsideringProposal);
    GameUtility.SetActive(this.accepted, this.mPerson.contractManager.isProposalAccepted);
    this.contractOptionEntries.DestroyListItems();
    this.contractOptionEntries.itemPrefab.SetActive(true);
    List<UIContractOptionEntry.OptionType> negotiationTypes = this.mPerson.contractManager.GetOptionNegotiationTypes();
    for (int index = 0; index < negotiationTypes.Count; ++index)
    {
      UIContractNegotiationOptionEntry listItem = this.contractOptionEntries.CreateListItem<UIContractNegotiationOptionEntry>();
      listItem.Setup(negotiationTypes[index], this.mPerson.contractManager);
      listItem.SetupCurrentContract(negotiationTypes[index], this.mPerson.contract);
    }
    this.contractOptionEntries.itemPrefab.SetActive(false);
  }
}
