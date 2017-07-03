// Decompiled with JetBrains decompiler
// Type: UIRacePodiumRepresentative
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class UIRacePodiumRepresentative : MonoBehaviour
{
  public UICharacterPortrait portrait;
  public TextMeshProUGUI fullName;
  public TextMeshProUGUI teamName;
  public TextMeshProUGUI job;
  public Flag flag;
  public UITeamLogo teamLogo;
  private Person mPerson;

  public void Setup(Person inPerson)
  {
    if (inPerson == null)
      return;
    this.mPerson = inPerson;
    this.portrait.SetPortrait(this.mPerson);
    this.fullName.text = this.mPerson.name;
    StringVariableParser.subject = this.mPerson;
    this.job.text = Localisation.LocaliseEnum((Enum) this.mPerson.contract.job);
    StringVariableParser.subject = (Person) null;
    this.teamName.text = this.mPerson.contract.GetTeam().name;
    this.flag.SetNationality(this.mPerson.nationality);
    this.teamLogo.SetTeam(this.mPerson.contract.GetTeam());
  }
}
