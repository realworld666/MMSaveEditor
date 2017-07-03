// Decompiled with JetBrains decompiler
// Type: PrefToggle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class PrefToggle
{
  public static PrefToggle.Type GetValue(bool inValue)
  {
    return inValue ? PrefToggle.Type.On : PrefToggle.Type.Off;
  }

  public enum Type
  {
    On,
    Off,
  }
}
