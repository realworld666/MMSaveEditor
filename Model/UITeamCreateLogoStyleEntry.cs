// Decompiled with JetBrains decompiler
// Type: UITeamCreateLogoStyleEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITeamCreateLogoStyleEntry : MonoBehaviour
{
  public int styleID;
  public TextMeshProUGUI mainTeamName;
  public TextMeshProUGUI subTeamName;
  public Graphic[] primaryColorGraphics;
  public Graphic[] secondaryColorGraphics;
  public Graphic[] whiteColorGraphics;
  public Graphic[] blackColorGraphics;
  private UITeamCreateLogo.ColorMode mColorMode;

  public void SetMainName(string inString)
  {
    if (!((Object) this.mainTeamName != (Object) null) || inString == null)
      return;
    this.mainTeamName.text = inString;
  }

  public void SetSubName(string inString)
  {
    if (!((Object) this.subTeamName != (Object) null) || inString == null)
      return;
    this.subTeamName.text = inString;
  }

  public void SetMode(UITeamCreateLogo.ColorMode inColorMode)
  {
    this.mColorMode = inColorMode;
  }

  public void RefreshStyle(TeamColor inTeamColor)
  {
    switch (this.mColorMode)
    {
      case UITeamCreateLogo.ColorMode.Colored:
        Color inColor1 = inTeamColor.customLogoColor != null ? inTeamColor.customLogoColor.primary : inTeamColor.staffColor.primary;
        Color inColor2 = inTeamColor.customLogoColor != null ? inTeamColor.customLogoColor.secondary : inTeamColor.staffColor.secondary;
        if (this.primaryColorGraphics != null)
        {
          for (int index = 0; index < this.primaryColorGraphics.Length; ++index)
            this.SetGraphicColor(this.primaryColorGraphics[index], inColor1);
        }
        if (this.secondaryColorGraphics == null)
          break;
        for (int index = 0; index < this.secondaryColorGraphics.Length; ++index)
          this.SetGraphicColor(this.secondaryColorGraphics[index], inColor2);
        break;
      case UITeamCreateLogo.ColorMode.BlackWhite:
        Color customLogoLight = UIConstants.customLogoLight;
        Color customLogoDark = UIConstants.customLogoDark;
        if (this.whiteColorGraphics != null)
        {
          for (int index = 0; index < this.whiteColorGraphics.Length; ++index)
            this.SetGraphicColor(this.whiteColorGraphics[index], customLogoLight);
        }
        if (this.blackColorGraphics == null)
          break;
        for (int index = 0; index < this.blackColorGraphics.Length; ++index)
          this.SetGraphicColor(this.blackColorGraphics[index], customLogoDark);
        break;
    }
  }

  public void SetGraphicColor(Graphic inGraphic, Color inColor)
  {
    if (!((Object) inGraphic != (Object) null))
      return;
    if (inGraphic is TextMeshProUGUI)
      (inGraphic as TextMeshProUGUI).color = inColor;
    else
      inGraphic.color = inColor;
  }
}
