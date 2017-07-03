// Decompiled with JetBrains decompiler
// Type: UITutorialStrategyPanelShow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class UITutorialStrategyPanelShow : UITutorialBespokeScript
{
  public UITutorialStrategyPanelShow.ActionType actionType = UITutorialStrategyPanelShow.ActionType.None;

  protected override void Activate()
  {
    SessionHUD screen = UIManager.instance.GetScreen<SessionHUD>();
    if (this.actionType == UITutorialStrategyPanelShow.ActionType.Hide)
    {
      for (int index = 0; index < screen.actionButtons.Length; ++index)
        screen.actionButtons[index].strategyPanel.Hide();
    }
    else
    {
      if (this.actionType != UITutorialStrategyPanelShow.ActionType.Show)
        return;
      for (int index = 0; index < screen.actionButtons.Length; ++index)
        screen.actionButtons[index].strategyPanel.Show();
    }
  }

  public enum ActionType
  {
    Hide,
    Show,
    None,
  }
}
