// Decompiled with JetBrains decompiler
// Type: MM2.SaveException
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;

namespace MM2
{
  internal class SaveException : Exception
  {
    public SaveException()
    {
    }

    public SaveException(string message)
      : base(message)
    {
    }

    public SaveException(string message, Exception inner)
      : base(message, inner)
    {
    }
  }
}
