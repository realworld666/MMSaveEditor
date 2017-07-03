// Decompiled with JetBrains decompiler
// Type: NewGameSetupState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class NewGameSetupState : GameState
{
  public override GameState.Type type
  {
    get
    {
      return GameState.Type.NewGameSetup;
    }
  }

  public override UIManager.ScreenSet screenSet
  {
    get
    {
      return UIManager.ScreenSet.Title;
    }
  }

  public override GameState.Group group
  {
    get
    {
      return GameState.Group.Frontend;
    }
  }

  public override void OnEnter(bool fromSave = false)
  {
    base.OnEnter(fromSave);
  }

  public override void GetFirstScreenForState(out string screenName, out UIManager.ScreenTransition screenTransition, out float transitionDuration, bool fromSave = false)
  {
    screenName = "NewCareerScreen";
    screenTransition = UIManager.ScreenTransition.Fade;
    transitionDuration = 1.5f;
  }

  public override void Update()
  {
    base.Update();
  }
}
