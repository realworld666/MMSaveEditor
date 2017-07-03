// Decompiled with JetBrains decompiler
// Type: UISessionHUBStandingsDriverEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UISessionHUBStandingsDriverEntry : UISessionHUBStandingsEntry
{
  public Flag driverFlag;
  public TextMeshProUGUI drivername;
  public GameObject bonusFastestLap;
  public GameObject bonusPolePosition;
  private Driver mDriver;

  public override void OnStart()
  {
    base.OnStart();
  }

  public override void Setup(SessionHUBStandingsScreen.HUBStanding inStanding)
  {
    if (inStanding.driver == null)
      return;
    if (this.mDriver != inStanding.driver)
    {
      this.mDriver = inStanding.driver;
      this.driverFlag.SetNationality(inStanding.driver.nationality);
      this.drivername.text = inStanding.driver.shortName;
    }
    if (this.position != inStanding.predictedPosition)
    {
      this.position = inStanding.predictedPosition;
      this.racePosition.text = inStanding.predictedPosition.ToString();
    }
    GameUtility.SetActive(this.bonusFastestLap, inStanding.hasFastestLap);
    GameUtility.SetActive(this.bonusPolePosition, inStanding.hasPolePosition);
    base.Setup(inStanding);
  }

  public void ShowToolTip(int inType)
  {
    switch (inType)
    {
      case 1:
        UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Open(Localisation.LocaliseID("PSG_10007150", (GameObject) null), Localisation.LocaliseID("PSG_10007151", (GameObject) null));
        break;
      case 2:
        UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Open(Localisation.LocaliseID("PSG_10007152", (GameObject) null), Localisation.LocaliseID("PSG_10007153", (GameObject) null));
        break;
    }
  }

  public void HideTooltip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Hide();
  }
}
