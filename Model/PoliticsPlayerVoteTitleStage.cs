// Decompiled with JetBrains decompiler
// Type: PoliticsPlayerVoteTitleStage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class PoliticsPlayerVoteTitleStage : MonoBehaviour
{
  public PoliticsPlayerVotePopup.Stage stage;
  public TextMeshProUGUI numberLabel;
  public GameObject stageEnabled;

  public void SetNumberLabel(int inNumber)
  {
    this.numberLabel.text = inNumber.ToString() + ".";
  }

  public void EnableStage(bool inValue)
  {
    GameUtility.SetActive(this.stageEnabled, inValue);
  }
}
