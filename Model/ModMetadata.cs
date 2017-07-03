// Decompiled with JetBrains decompiler
// Type: ModdingSystem.ModMetadata
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.IO;

namespace ModdingSystem
{
  public class ModMetadata
  {
    private Map<string, string> mMetadata = new Map<string, string>();

    public void Clear()
    {
      this.mMetadata.Clear();
    }

    public void LoadMetadata(string inStringMetadata)
    {
      StringReader stringReader = new StringReader(inStringMetadata);
      while (true)
      {
        string str = stringReader.ReadLine();
        if (!string.IsNullOrEmpty(str))
        {
          string[] strArray = str.Split(',');
          this.mMetadata.Add(strArray[0], strArray[1]);
        }
        else
          break;
      }
    }

    public string GetMetadataForFile(string inFileName)
    {
      if (this.mMetadata.keys.Contains(inFileName))
        return this.mMetadata.GetMap(inFileName);
      return (string) null;
    }

    public static string CreateMetadata(BasicMod inMod)
    {
      return string.Empty;
    }
  }
}
