// Decompiled with JetBrains decompiler
// Type: UIResultsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class UIResultsWidget : MonoBehaviour
{
  public GameObject header;
  public GameObject noData;
  public GameObject results;
  public UIGridList resultsList;

  public virtual void PopulateList(List<RaceEventResults.SessonResultData> inList)
  {
  }

  public void Activate(bool inValue)
  {
    GameUtility.SetActive(this.header, inValue);
    GameUtility.SetActive(this.results, inValue);
    GameUtility.SetActive(this.noData, inValue);
  }
}
