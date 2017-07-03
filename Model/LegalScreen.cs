// Decompiled with JetBrains decompiler
// Type: LegalScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class LegalScreen : UIScreen
{
  private float mDurection = 2f;
  private float mTimer;

  public bool isFinished
  {
    get
    {
      return (double) this.mTimer >= (double) this.mDurection;
    }
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.mTimer = 0.0f;
    this.showNavigationBars = false;
  }

  public override void OnExit()
  {
    base.OnExit();
  }

  private void Update()
  {
    this.mTimer += GameTimer.deltaTime;
  }
}
