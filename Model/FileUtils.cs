// Decompiled with JetBrains decompiler
// Type: FileUtils
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;

public static class FileUtils
{
  public static bool TryDeleteFileIfExists(string path)
  {
    if (!File.Exists(path))
      return true;
    try
    {
      File.Delete(path);
      return true;
    }
    catch (Exception ex)
    {
      return false;
    }
  }

  public static bool TryMoveFileIfExists(string fromPath, string toPath)
  {
    if (!File.Exists(fromPath))
      return true;
    FileUtils.TryDeleteFileIfExists(toPath);
    try
    {
      File.Move(fromPath, toPath);
      return true;
    }
    catch (Exception ex)
    {
      return false;
    }
  }
}
