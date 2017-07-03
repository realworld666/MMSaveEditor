// Decompiled with JetBrains decompiler
// Type: UITeamColorManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UITeamColorManager
{
  public TeamColor defaultColour { get; private set; }

  public TeamColor currentColour { get; private set; }

  public UITeamColorManager()
  {
    this.defaultColour = TeamColor.defaultColour;
    this.currentColour = this.defaultColour;
  }

  public void SetCurrentColour(TeamColor inTeamColour, bool inUpdateGUI = false, GameObject inParent = null)
  {
    this.currentColour = inTeamColour;
    if (!inUpdateGUI)
      return;
    foreach (UITeamColor uiTeamColor in !((Object) inParent == (Object) null) ? inParent.GetComponentsInChildren<UITeamColor>(true) : Object.FindObjectsOfType<UITeamColor>())
      uiTeamColor.UpdateColor();
  }

  public void SetToPlayerTeamColour(bool inUpdateGUI)
  {
    if (Game.IsActive())
      this.SetCurrentColour(App.instance.teamColorManager.GetColor(Game.instance.player.team.colorID), inUpdateGUI, (GameObject) null);
    else
      this.SetCurrentColour(this.defaultColour, inUpdateGUI, (GameObject) null);
  }

  public void SetToPlayerTeamColour()
  {
    this.SetToPlayerTeamColour(false);
  }
}
