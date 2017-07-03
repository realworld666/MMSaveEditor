// Decompiled with JetBrains decompiler
// Type: CreateTeamOptionCarLivery
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class CreateTeamOptionCarLivery : CreateTeamOption
{
  public LiveryOptionsWidget liveryWidget;

  public override bool isReady
  {
    get
    {
      return true;
    }
  }

  public override void OnStart()
  {
  }

  public override void Setup()
  {
    this.liveryWidget.Setup();
  }

  public override void OnExit()
  {
    this.liveryWidget.OnExit();
  }

  private void OnDisable()
  {
    ColorPickerDialogBox.Close();
  }
}
