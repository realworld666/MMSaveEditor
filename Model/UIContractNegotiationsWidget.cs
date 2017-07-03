// Decompiled with JetBrains decompiler
// Type: UIContractNegotiationsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIContractNegotiationsWidget : MonoBehaviour
{
  public UIGridList contractNegotiations;
  public GameObject negotiationTable;
  public Button scoutingButton;

  public void Setup(List<Contract.Job> inJobTypes)
  {
    List<Person> inFilteredPersonDraft = this.FilterDraftContracts(Game.instance.player.team.contractManager.proposedDrafts, inJobTypes);
    if (inFilteredPersonDraft.Count > 0)
    {
      GameUtility.SetActive(this.negotiationTable, true);
      this.PopulateDraftList(inFilteredPersonDraft);
    }
    else
      GameUtility.SetActive(this.negotiationTable, false);
    this.scoutingButton.onClick.RemoveAllListeners();
    this.scoutingButton.onClick.AddListener(new UnityAction(this.OnScoutingButton));
  }

  private void PopulateDraftList(List<Person> inFilteredPersonDraft)
  {
    this.contractNegotiations.DestroyListItems();
    for (int index = 0; index < inFilteredPersonDraft.Count; ++index)
      this.contractNegotiations.CreateListItem<UIContractNegotiationEntry>().SetupEntryForDraft(inFilteredPersonDraft[index]);
  }

  private List<Person> FilterDraftContracts(List<Person> inProposedPersonDraft, List<Contract.Job> inNegotiationTypes)
  {
    List<Person> personList = new List<Person>();
    for (int index1 = 0; index1 < inProposedPersonDraft.Count; ++index1)
    {
      for (int index2 = 0; index2 < inNegotiationTypes.Count; ++index2)
      {
        ContractPerson proposalContract = inProposedPersonDraft[index1].contractManager.draftProposalContract;
        if (proposalContract != null && proposalContract.job == inNegotiationTypes[index2])
        {
          personList.Add(inProposedPersonDraft[index1]);
          break;
        }
      }
    }
    return personList;
  }

  private void OnScoutingButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (UIManager.instance.currentScreen is AllDriversScreen)
      ScoutingScreen.ChangeScreen(UIScoutingFilterJobRole.Filter.Drivers);
    else
      ScoutingScreen.ChangeScreen(UIScoutingFilterJobRole.Filter.Designers);
  }
}
