// Decompiled with JetBrains decompiler
// Type: UIGenericCarBackground
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UIGenericCarBackground : MonoBehaviour
{
  private Championship.Series mSeries = Championship.Series.Count;
  public Image teamBackingGradient;
  public Image teamBacking;
  public ActivateForSeries.GameObjectData[] background;
  private TeamColor mTeamColour;

  public void SetCar(Team inTeam)
  {
    this.mSeries = inTeam.championship.series;
    this.mTeamColour = inTeam.GetTeamColor();
    this.SetGraphics();
  }

  public void SetCarBackground(Championship.Series inSeries)
  {
    this.mSeries = inSeries;
    this.mTeamColour = (TeamColor) null;
    this.SetGraphics();
  }

  private void SetGraphics()
  {
    GameUtility.SetActiveForSeries(this.mSeries, this.background);
    if (this.mTeamColour == null)
      return;
    if ((Object) this.teamBackingGradient != (Object) null)
      this.teamBackingGradient.color = this.mTeamColour.primaryUIColour.normal;
    if (!((Object) this.teamBacking != (Object) null))
      return;
    this.teamBacking.color = this.mTeamColour.primaryUIColour.normal;
  }
}
