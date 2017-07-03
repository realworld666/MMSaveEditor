// Decompiled with JetBrains decompiler
// Type: CharacterCreatorToolScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class CharacterCreatorToolScreen : UIScreen
{
  public UICharacterToolProfiles profilesWidget;
  public UICharacterToolMenu menuWidget;
  public UICharacterToolCustomize customizeWidget;
  public UICharacterToolPopup savePopup;
  public UICharacterToolEdit editPopup;

  public override void OnStart()
  {
    base.OnStart();
    this.profilesWidget.OnStart();
    this.menuWidget.OnStart();
    this.customizeWidget.OnStart();
    this.savePopup.OnStart();
    this.editPopup.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.profilesWidget.Setup();
    this.menuWidget.Setup();
    this.customizeWidget.Setup();
  }

  private void Update()
  {
  }
}
