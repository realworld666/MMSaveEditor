// Decompiled with JetBrains decompiler
// Type: UIDriversSummary
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDriversSummary : MonoBehaviour
{
  public List<Image> stars = new List<Image>();
  public TextMeshProUGUI totalDriverCostLabel;
  public TextMeshProUGUI chairMansRecomendation;

  public void Setup()
  {
    this.totalDriverCostLabel.text = GameUtility.GetCurrencyString((long) Game.instance.player.team.contractManager.EndOfMonthCost(), 0);
    float num = 0.0f;
    for (int inIndex = 0; inIndex < Team.driverCount; ++inIndex)
    {
      Driver driver = Game.instance.player.team.GetDriver(inIndex);
      if (driver != null)
        num += driver.GetDriverStats().marketability;
    }
    this.SetStars(num / 3f);
  }

  private void SetStars(float inStat)
  {
    float num = 5f;
    for (int index = 0; index < this.stars.Count; ++index)
    {
      if ((double) index <= (double) inStat * (double) num)
        this.stars[index].gameObject.SetActive(true);
      else
        this.stars[index].gameObject.SetActive(false);
    }
  }
}
