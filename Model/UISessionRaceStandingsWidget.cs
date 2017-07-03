// Decompiled with JetBrains decompiler
// Type: UISessionRaceStandingsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class UISessionRaceStandingsWidget : UIBaseSessionTopBarWidget
{
  private void Awake()
  {
    this.keybinding = KeyBinding.Name.SessionStandings;
    this.useKeyBinding = true;
  }

  public override void OnEnter()
  {
  }

  public override bool ShouldBeEnabled()
  {
    return App.instance.gameStateManager.currentState is SessionState;
  }

  private void Update()
  {
    this.CheckKeyBinding();
  }
}
