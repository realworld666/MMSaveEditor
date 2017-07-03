// Decompiled with JetBrains decompiler
// Type: UIDriverHelmet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UIDriverHelmet : MonoBehaviour
{
  public Image helmetBase;
  public Image helmetSecondary;
  public Image helmetTertiary;
  private TeamColor.HelmetColour mHelmetColour;
  private UIDriverHelmet.HelmetType mHelmetType;
  private Team mTeam;

  public void SetHelmet(Driver inDriver)
  {
    if (!inDriver.IsFreeAgent())
    {
      this.mTeam = inDriver.contract.GetTeam();
      switch (this.mTeam.GetDriverIndex(inDriver) + 1)
      {
        case 0:
          this.mHelmetType = UIDriverHelmet.HelmetType.ReserveDriver;
          break;
        case 1:
          this.mHelmetType = UIDriverHelmet.HelmetType.FirstDriver;
          break;
        case 2:
          this.mHelmetType = UIDriverHelmet.HelmetType.SecondDriver;
          break;
      }
      this.mHelmetColour = this.mTeam.GetTeamColor().helmetColor;
    }
    else
    {
      this.mHelmetType = UIDriverHelmet.HelmetType.FirstDriver;
      this.mHelmetColour = App.instance.teamColorManager.GetColor(0).helmetColor;
    }
    this.SetColors();
  }

  public void SetHelmet(Team inTeam, UIDriverHelmet.HelmetType inHelmetType)
  {
    this.mHelmetType = inHelmetType;
    if (!(inTeam is NullTeam))
    {
      this.mHelmetColour = inTeam.GetTeamColor().helmetColor;
    }
    else
    {
      this.mHelmetType = UIDriverHelmet.HelmetType.FirstDriver;
      this.mHelmetColour = App.instance.teamColorManager.GetColor(0).helmetColor;
    }
    this.SetColors();
  }

  public void SetHelmet(TeamColor.HelmetColour inHelmetColor, UIDriverHelmet.HelmetType inHelmetType)
  {
    this.mHelmetType = inHelmetType;
    this.mHelmetColour = inHelmetColor;
    this.SetColors();
  }

  private void SetColors()
  {
    switch (this.mHelmetType)
    {
      case UIDriverHelmet.HelmetType.FirstDriver:
        this.helmetBase.color = this.mHelmetColour.primary;
        this.helmetSecondary.color = this.mHelmetColour.secondary;
        this.helmetTertiary.color = this.mHelmetColour.tertiary;
        break;
      case UIDriverHelmet.HelmetType.SecondDriver:
        this.helmetBase.color = this.mHelmetColour.secondary;
        this.helmetSecondary.color = this.mHelmetColour.tertiary;
        this.helmetTertiary.color = this.mHelmetColour.primary;
        break;
      case UIDriverHelmet.HelmetType.ReserveDriver:
        this.helmetBase.color = this.mHelmetColour.tertiary;
        this.helmetSecondary.color = this.mHelmetColour.primary;
        this.helmetTertiary.color = this.mHelmetColour.secondary;
        break;
    }
  }

  public enum HelmetType
  {
    FirstDriver,
    SecondDriver,
    ReserveDriver,
  }
}
