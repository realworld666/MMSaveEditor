// Decompiled with JetBrains decompiler
// Type: UIContractTeamStaffWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIContractTeamStaffWidget : MonoBehaviour
{
  private List<Person> mPeople = new List<Person>();
  public UIContractTeamStaffEntry[] entries;
  public TextMeshProUGUI label;

  public void Setup(Person inTargetPerson)
  {
    if (inTargetPerson == null || Game.instance.player.IsUnemployed())
      return;
    bool flag = inTargetPerson is Driver;
    this.mPeople.Clear();
    if (flag)
    {
      this.mPeople = Game.instance.player.team.contractManager.GetAllPeopleOnJob(Contract.Job.Driver);
      for (int index = 0; index < this.mPeople.Count; ++index)
        this.entries[index].Setup(this.mPeople[index], inTargetPerson);
      this.label.text = Localisation.LocaliseID("PSG_10000002", (GameObject) null);
    }
    else
    {
      this.mPeople = Game.instance.player.team.contractManager.GetAllPeopleOnJob(Contract.Job.Mechanic);
      this.mPeople.Insert(0, Game.instance.player.team.contractManager.GetPersonOnJob(Contract.Job.EngineerLead));
      for (int index = 0; index < this.mPeople.Count; ++index)
        this.entries[index].Setup(this.mPeople[index], inTargetPerson);
      this.label.text = Localisation.LocaliseID("PSG_10003781", (GameObject) null);
    }
  }
}
