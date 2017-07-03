// Decompiled with JetBrains decompiler
// Type: UIStaffDetailsScreenPersonStatsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStaffDetailsScreenPersonStatsWidget : MonoBehaviour
{
  public UIGridList statsList;
  public TextMeshProUGUI title;

  public void Setup(Person inPerson, Dictionary<string, float> inStats)
  {
    StringVariableParser.subject = inPerson;
    StringVariableParser.contractJob = inPerson.contract.job;
    this.title.text = Localisation.LocaliseID("PSG_10010611", (GameObject) null);
    StringVariableParser.subject = (Person) null;
    this.statsList.DestroyListItems();
    using (Dictionary<string, float>.Enumerator enumerator = inStats.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        KeyValuePair<string, float> current = enumerator.Current;
        this.statsList.CreateListItem<UIStat>().SetStat(current.Key, current.Value, inPerson);
      }
    }
  }
}
