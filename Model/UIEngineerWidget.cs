// Decompiled with JetBrains decompiler
// Type: UIEngineerWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEngineerWidget : MonoBehaviour
{
  public List<UIAbilityStar> stars = new List<UIAbilityStar>();
  private CarStats.StatType mStat = CarStats.StatType.Acceleration;
  public UICharacterPortrait portrait;
  public TextMeshProUGUI engineerName;
  public Flag engineerFlag;
  public TextMeshProUGUI engineerComment;

  public CarStats.StatType stat
  {
    get
    {
      return this.mStat;
    }
  }

  public void OnEnter()
  {
    Person personOnJob = Game.instance.player.team.contractManager.GetPersonOnJob(Contract.Job.EngineerLead);
    this.portrait.SetPortrait(personOnJob);
    this.engineerName.text = personOnJob.name;
    this.engineerFlag.SetNationality(personOnJob.nationality);
    this.engineerComment.text = "Choose a part type to create.";
  }
}
