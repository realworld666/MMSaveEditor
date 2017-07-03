// Decompiled with JetBrains decompiler
// Type: UIHUBStrategy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class UIHUBStrategy : UIHUBStepOption
{
  public UIHUBStrategyEntry[] entries;
  public UIHUBSelection widget;

  public override void OnStart()
  {
    for (int index = 0; index < this.entries.Length; ++index)
      this.entries[index].OnStart();
  }

  public override void Setup()
  {
    RacingVehicle[] vehicles = this.widget.vehicles;
    int length1 = vehicles.Length;
    int length2 = this.entries.Length;
    for (int index = 0; index < length1; ++index)
    {
      if (index < length2)
        this.entries[index].Setup(vehicles[index]);
    }
  }

  public override void OnShow()
  {
    for (int index = 0; index < this.widget.vehicles.Length; ++index)
    {
      if (index < this.entries.Length)
        this.entries[index].OnShow();
    }
  }

  public override bool IsReady()
  {
    return true;
  }
}
