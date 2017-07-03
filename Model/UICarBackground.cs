// Decompiled with JetBrains decompiler
// Type: UICarBackground
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UICarBackground : MonoBehaviour
{
  private Championship.Series mSeries = Championship.Series.Count;
  public Image carImage;
  private int mTeamID;
  private int mChampionshipID;

  public void SetCar(Team inTeam)
  {
    if (inTeam is NullTeam)
      return;
    this.mTeamID = inTeam.teamID;
    this.mChampionshipID = inTeam.championship.championshipID;
    this.mSeries = inTeam.championship.series;
    this.SetCarGraphics();
  }

  public void SetCar(int inTeamID, int inChampionshipID, Championship.Series inSeries)
  {
    this.mTeamID = inTeamID;
    this.mChampionshipID = inChampionshipID;
    this.mSeries = inSeries;
    this.SetCarGraphics();
  }

  private void SetCarGraphics()
  {
    switch (this.mSeries)
    {
      case Championship.Series.SingleSeaterSeries:
        this.carImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.CarSelectImages1, "GP" + (this.mChampionshipID + 1).ToString() + "-Car" + (this.mTeamID + 1).ToString());
        break;
      case Championship.Series.GTSeries:
        this.carImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.GTCarSelectImages1, "GT" + (this.mChampionshipID - 2).ToString() + "-Car" + (this.mTeamID + 1).ToString());
        break;
    }
  }
}
