// Decompiled with JetBrains decompiler
// Type: UITeamCreateLogo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UITeamCreateLogo : MonoBehaviour
{
  public static readonly string[] logos = new string[14]
  {
    "Style 1",
    "Style 2",
    "Style 3",
    "Style 4",
    "Style 5",
    "Style 6",
    "Style 7",
    "Style 8",
    "Style 9",
    "Style 10",
    "Style 11",
    "Style 12",
    "Style 13",
    "Style 14"
  };
  private string mMainText = string.Empty;
  private string mSubText = string.Empty;
  public UITeamCreateLogoStyleEntry[] styles;
  public UITeamCreateLogo.ColorMode colorMode;
  private UITeamCreateLogoStyleEntry mSelectedStyle;

  public void SetStyle()
  {
    int styleId = CreateTeamManager.newTeam.customLogo.styleID;
    for (int index = 0; index < this.styles.Length; ++index)
    {
      GameUtility.SetActive(this.styles[index].gameObject, this.styles[index].styleID == styleId);
      if (this.styles[index].gameObject.activeSelf)
      {
        this.mSelectedStyle = this.styles[index];
        this.UpdateStyle();
      }
    }
  }

  public void Refresh()
  {
    this.mMainText = CreateTeamManager.teamFirstName;
    this.mSubText = CreateTeamManager.teamLastName;
    this.SetStyle();
  }

  public void SetName(string inMainString, string inSubString)
  {
    if (inMainString != null)
      this.mMainText = inMainString;
    if (inSubString != null)
      this.mSubText = inSubString;
    this.UpdateStyle();
  }

  public void UpdateStyle()
  {
    if (!((Object) this.mSelectedStyle != (Object) null))
      return;
    this.mSelectedStyle.SetMode(this.colorMode);
    this.mSelectedStyle.RefreshStyle(CreateTeamManager.newTeamColor);
    this.mSelectedStyle.SetMainName(this.mMainText);
    this.mSelectedStyle.SetSubName(this.mSubText);
  }

  public void RefreshColors()
  {
    if (!((Object) this.mSelectedStyle != (Object) null))
      return;
    this.mSelectedStyle.RefreshStyle(CreateTeamManager.newTeamColor);
  }

  public UITeamCreateLogoStyleEntry GetStyle(int inStyleId)
  {
    for (int index = 0; index < this.styles.Length; ++index)
    {
      if (this.styles[index].styleID == inStyleId)
        return this.styles[index];
    }
    return (UITeamCreateLogoStyleEntry) null;
  }

  public enum ColorMode
  {
    Colored,
    BlackWhite,
  }
}
