// Decompiled with JetBrains decompiler
// Type: UIGraphEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class UIGraphEntry
{
  public List<Vector2> values = new List<Vector2>();
  public string fieldName = string.Empty;
  public Color graphColor = Color.black;
  public int entryIndex;

  public void InsertValues(int inEntryIndex, List<Vector2> inValues, string inFieldName, Color inGraphColor)
  {
    this.entryIndex = inEntryIndex;
    this.values = inValues;
    this.fieldName = inFieldName;
    this.graphColor = inGraphColor;
  }
}
