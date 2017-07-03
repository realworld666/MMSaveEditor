// Decompiled with JetBrains decompiler
// Type: UICompareStaffProfileWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UICompareStaffProfileWidget : MonoBehaviour
{
  public UICompareStaffNavigationWidget navigationWidget;
  public UICompareStaffTeamWidget teamWidget;
  public UICompareStaffOverviewWidget overviewWidget;
  public UICompareStaffContractWidget contractWidget;
  public UICompareStaffDriverWidget driverWidget;
  public UICompareStaffEngineerWidget engineerWidget;
  public UICompareStaffMechanicWidget mechanicWidget;
  public UICompareStaffButtonsWidget buttonsWidget;
  public UICompareStaffColorsWidget colorsWidget;
  public CompareStaffScreen screen;
  private Person mPerson;

  public Person person
  {
    get
    {
      return this.mPerson;
    }
  }

  public void OnStart()
  {
    this.navigationWidget.OnStart();
    this.driverWidget.OnStart();
    this.engineerWidget.OnStart();
    this.mechanicWidget.OnStart();
  }

  public void Setup(Person inPerson)
  {
    if (inPerson == null)
      return;
    this.mPerson = inPerson;
    this.navigationWidget.Setup(this.mPerson, false);
    this.buttonsWidget.Setup();
    this.colorsWidget.Setup();
    this.overviewWidget.Setup(this.mPerson);
    this.contractWidget.Setup(this.mPerson);
    Team team = this.mPerson.contract.GetTeam();
    if (team != null)
    {
      GameUtility.SetActive(this.teamWidget.gameObject, true);
      this.teamWidget.Setup(team);
    }
    else
      GameUtility.SetActive(this.teamWidget.gameObject, false);
    GameUtility.SetActive(this.driverWidget.gameObject, this.screen.mode == CompareStaffScreen.Mode.Driver);
    GameUtility.SetActive(this.engineerWidget.gameObject, this.screen.mode == CompareStaffScreen.Mode.Engineer);
    GameUtility.SetActive(this.mechanicWidget.gameObject, this.screen.mode == CompareStaffScreen.Mode.Mechanic);
    if (this.screen.mode == CompareStaffScreen.Mode.Driver)
      this.driverWidget.Setup((Driver) this.mPerson);
    else if (this.screen.mode == CompareStaffScreen.Mode.Engineer)
    {
      this.engineerWidget.Setup((Engineer) this.mPerson);
    }
    else
    {
      if (this.screen.mode != CompareStaffScreen.Mode.Mechanic)
        return;
      this.mechanicWidget.Setup((Mechanic) this.mPerson);
    }
  }

  public void UpdateNavigation()
  {
    if (this.mPerson == null)
      return;
    this.navigationWidget.Setup(this.mPerson, true);
  }

  public void Reset()
  {
    this.mPerson = (Person) null;
  }
}
