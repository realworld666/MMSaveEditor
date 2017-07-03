// Decompiled with JetBrains decompiler
// Type: UIEliminatedDriverIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEliminatedDriverIcon : MonoBehaviour
{
  public UICharacterPortrait driverPortrait;
  public UITyreWearIcon tyre;
  public Flag flag;
  public GameObject playerDriverHighlight;
  public TextMeshProUGUI positionText;
  public TextMeshProUGUI driverNameText;
  public TextMeshProUGUI teamText;
  public TextMeshProUGUI championshipPositionText;
  public TextMeshProUGUI timeDeltaText;

  public void Setup(Driver inDriver, int inPosition, TyreSet inTyreSet, float inTimeDelta)
  {
    this.positionText.text = GameUtility.FormatForPosition(inPosition, (string) null);
    this.driverNameText.text = inDriver.name;
    this.teamText.text = inDriver.contract.GetTeam().name;
    this.flag.SetNationality(inDriver.nationality);
    this.driverPortrait.SetPortrait((Person) inDriver);
    if ((double) inTimeDelta < 0.0)
      this.timeDeltaText.text = Localisation.LocaliseID("PSG_10011998", (GameObject) null);
    else
      this.timeDeltaText.text = "+" + GameUtility.GetLapTimeText(inTimeDelta, false);
    this.championshipPositionText.text = Localisation.LocaliseID("PSG_10007716", (GameObject) null) + ": " + GameUtility.FormatForPosition(inDriver.GetChampionshipEntry().GetCurrentChampionshipPosition(), (string) null);
    GameUtility.SetActive(this.playerDriverHighlight, inDriver.IsPlayersDriver());
    this.tyre.SetTyreSet(inTyreSet, (List<SessionCarBonuses.DisplayBonusInfo>) null);
  }
}
