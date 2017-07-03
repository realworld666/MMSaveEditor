// Decompiled with JetBrains decompiler
// Type: UIStandingsChampionshipWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIStandingsChampionshipWidget : MonoBehaviour
{
  public TextMeshProUGUI championshipName;
  public TextMeshProUGUI championshipYear;
  public TextMeshProUGUI championshipPrizeFund;
  public TextMeshProUGUI championshipAudience;
  public UIChampionshipLogo championshipLogo;

  public void Setup(Championship inChampionship)
  {
    this.championshipName.text = inChampionship.GetChampionshipName(false);
    this.championshipYear.text = Game.instance.time.now.Year.ToString();
    this.championshipPrizeFund.text = GameUtility.GetCurrencyString((long) inChampionship.prizeFund, 0);
    this.championshipAudience.text = GameUtility.FormatNumberString(inChampionship.tvAudience);
    this.championshipLogo.SetChampionship(inChampionship);
  }
}
