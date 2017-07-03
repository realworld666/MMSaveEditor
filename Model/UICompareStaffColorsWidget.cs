// Decompiled with JetBrains decompiler
// Type: UICompareStaffColorsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UICompareStaffColorsWidget : MonoBehaviour
{
  public UITeamColor[] teamColors;
  public UICompareStaffProfileWidget widget;

  public void Setup()
  {
    if (this.teamColors == null || this.teamColors.Length <= 0)
      return;
    TeamColor inTeamColour = this.widget.person == null ? Game.instance.player.team.GetTeamColor() : this.widget.person.GetTeamColor();
    for (int index = 0; index < this.teamColors.Length; ++index)
      this.teamColors[index].SetTeamColor(inTeamColour);
  }
}
