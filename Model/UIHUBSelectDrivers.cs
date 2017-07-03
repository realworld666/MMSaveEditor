// Decompiled with JetBrains decompiler
// Type: UIHUBSelectDrivers
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

public class UIHUBSelectDrivers : UIHUBStepOption
{
  private List<Driver> mDrivers = new List<Driver>();
  public UIHUBSelectDriverEntry[] drivers;

  public override void OnStart()
  {
    for (int index = 0; index < this.drivers.Length; ++index)
      this.drivers[index].OnStart();
  }

  public override void Setup()
  {
    this.mDrivers = Game.instance.player.team.contractManager.GetAllPeopleOnJob<Driver>(Contract.Job.Driver);
    int count = this.mDrivers.Count;
    int length = this.drivers.Length;
    for (int index = 0; index < length; ++index)
    {
      if (index < count)
        this.drivers[index].Setup(this.mDrivers[index]);
    }
  }

  public override bool IsReady()
  {
    int num = 0;
    for (int index = 0; index < this.drivers.Length; ++index)
    {
      if (this.drivers[index].isSelected)
        ++num;
    }
    return num == 2;
  }
}
