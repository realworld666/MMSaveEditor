// Decompiled with JetBrains decompiler
// Type: PoliticsStringsParser
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public static class PoliticsStringsParser
{
  public static string GetData(string inString, Dictionary<string, string> inSpecialStrings)
  {
    string str = inString;
    if (inString.Contains("PSG_"))
      str = Localisation.LocaliseID(inString, (GameObject) null);
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    bool flag = false;
    for (int index = 0; index < str.Length; ++index)
    {
      if (str[index].ToString() == "{")
        flag = true;
      else if (flag && str[index].ToString() != "}")
        empty1 += (string) (object) str[index];
      else if (str[index].ToString() == "}")
      {
        if (!inSpecialStrings.ContainsKey(empty1))
        {
          Debug.LogError((object) ("PoliticalVote special string dictionary does not have a definition for " + empty1), (Object) null);
          empty2 += empty1;
        }
        else
          empty2 += inSpecialStrings[empty1];
        empty1 = string.Empty;
        flag = false;
      }
      else
        empty2 += str[index].ToString();
    }
    return empty2;
  }
}
