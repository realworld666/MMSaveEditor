// Decompiled with JetBrains decompiler
// Type: UIContractSettingsEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIContractSettingsEntry : MonoBehaviour
{
  public virtual void Setup(Contract inTargetContract, Contract inDraftContract)
  {
  }

  public virtual void PopulateWithPreviousProposal(Contract inDraftProposal)
  {
  }

  public virtual void UpdateContractInfo(Contract inContract)
  {
  }

  public virtual void Reset(ContractPerson inContract)
  {
  }

  public virtual void Revert()
  {
  }

  public virtual bool HaveTheSettingsChanged()
  {
    return false;
  }

  public virtual ContractEvaluationPerson.ReactionType GetScore(Contract inContract)
  {
    return ContractEvaluationPerson.ReactionType.Insulted;
  }

  public virtual void SetupImportanceLabel(ContractPerson inContract, TextMeshProUGUI inLabel)
  {
  }

  public virtual void SetupSubtitleImportanceLabel(ContractPerson inContract, TextMeshProUGUI inLabel)
  {
    GameUtility.SetActive(inLabel.gameObject, false);
  }

  public virtual void SetPrevious()
  {
  }

  public virtual bool AreSettingsChosen()
  {
    return true;
  }

  public virtual void SetDefaultValue(ContractPerson inContract)
  {
  }

  protected bool TrySetImportanceLabelUnknown(ContractPerson inContract, TextMeshProUGUI inLabel)
  {
    if (inContract.person.CanShowStats())
      return false;
    inLabel.text = Localisation.LocaliseID("PSG_10009284", (GameObject) null);
    inLabel.color = UIConstants.whiteColor;
    inLabel.alpha = 1f;
    return true;
  }

  protected virtual void SetupImportanceLabelForValue(UIContractSettingsEntry.ImportanceLabelType inImportanceType, TextMeshProUGUI inLabel)
  {
    if (inImportanceType == UIContractSettingsEntry.ImportanceLabelType.High)
      inLabel.alpha = 1f;
    else if (inImportanceType == UIContractSettingsEntry.ImportanceLabelType.Medium)
    {
      inLabel.alpha = 1f;
    }
    else
    {
      if (inImportanceType != UIContractSettingsEntry.ImportanceLabelType.Low)
        return;
      inLabel.alpha = 1f;
    }
  }

  public enum ImportanceLabelType
  {
    High,
    Medium,
    Low,
  }
}
