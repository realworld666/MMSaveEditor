// Decompiled with JetBrains decompiler
// Type: UICharacterToolPortrait
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class UICharacterToolPortrait : MonoBehaviour
{
  public UICharacterPortrait staffPortrait;
  public UICharacterPortrait driverPortrait;
  public UITeamLogo teamLogo;
  public Flag flag;
  public TextMeshProUGUI staffName;
  public TextMeshProUGUI age;
  public CharacterCreatorToolScreen screen;
  private Person mPerson;

  public void Setup()
  {
    this.screen.profilesWidget.OnSelection -= new Action(this.SetDetails);
    this.screen.profilesWidget.OnSelection += new Action(this.SetDetails);
    this.SetDetails();
  }

  public void Setup(Person inPerson)
  {
    if (inPerson == null)
      return;
    this.mPerson = inPerson;
    this.SetPerson();
  }

  private void SetDetails()
  {
    bool flag = this.screen.profilesWidget.selectedNum == 1;
    if (flag)
    {
      this.mPerson = this.screen.profilesWidget.selectedPerson;
      this.SetPerson();
    }
    this.gameObject.SetActive(flag);
  }

  private void SetPerson()
  {
    if (this.mPerson == null)
      return;
    this.UpdatePortrait();
    Team inTeam = (Team) null;
    if (this.mPerson.contract != null)
      inTeam = this.mPerson.contract.GetTeam();
    this.teamLogo.gameObject.SetActive(inTeam != null);
    if (this.teamLogo.gameObject.activeSelf)
      this.teamLogo.SetTeam(inTeam);
    this.flag.SetNationality(this.mPerson.nationality);
    this.staffName.text = this.mPerson.shortName;
    this.age.text = this.mPerson.gender.ToString() + " " + this.mPerson.GetAge().ToString();
  }

  public void UpdateAgeGender()
  {
    this.age.text = this.mPerson.gender.ToString() + " " + this.mPerson.GetAge().ToString();
  }

  public void UpdatePortrait()
  {
    bool flag = this.mPerson is Driver;
    this.driverPortrait.gameObject.SetActive(flag);
    this.staffPortrait.gameObject.SetActive(!flag);
    if (flag)
      this.driverPortrait.SetPortrait(this.mPerson);
    else
      this.staffPortrait.SetPortrait(this.mPerson);
  }
}
