// Decompiled with JetBrains decompiler
// Type: UIQualifyEliminationEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIQualifyEliminationEntry : MonoBehaviour
{
  public TextMeshProUGUI positionText;
  public TextMeshProUGUI driverNameText;
  public TextMeshProUGUI teamText;
  public GameObject hightlight;
  public Image teamColourSlash;
  public Flag flag;

  public void Setup(Driver inDriver, int inPosition)
  {
    GameUtility.SetActive(this.hightlight, inDriver.IsPlayersDriver());
    this.positionText.text = GameUtility.FormatForPosition(inPosition, (string) null);
    this.driverNameText.text = inDriver.name;
    this.teamText.text = inDriver.contract.GetTeam().name;
    this.flag.SetNationality(inDriver.nationality);
    this.teamColourSlash.color = inDriver.contract.GetTeam().GetTeamColor().primaryUIColour.normal;
  }
}
