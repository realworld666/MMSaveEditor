// Decompiled with JetBrains decompiler
// Type: CompareStaffScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CompareStaffScreen : UIScreen
{
  public UICompareStaffProfileWidget leftPanel;
  public UICompareStaffProfileWidget rightPanel;
  public UICompareStaffStatsWidget statsPanel;
  public UICompareStaffListWidget leftList;
  public UICompareStaffListWidget rightList;
  private CompareStaffScreen.Mode mMode;
  private Person mPerson;
  private Championship mChampionship;

  public CompareStaffScreen.Mode mode
  {
    get
    {
      return this.mMode;
    }
  }

  public override void OnStart()
  {
    base.OnStart();
    this.leftPanel.OnStart();
    this.rightPanel.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.mPerson = (Person) this.data;
    if (this.mPerson != null)
    {
      this.mChampionship = this.mPerson.IsFreeAgent() ? (Championship) null : this.mPerson.contract.GetTeam().championship;
      if (this.mPerson is Driver)
        this.mMode = CompareStaffScreen.Mode.Driver;
      else if (this.mPerson is Engineer)
        this.mMode = CompareStaffScreen.Mode.Engineer;
      else if (this.mPerson is Mechanic)
        this.mMode = CompareStaffScreen.Mode.Mechanic;
      this.leftPanel.Setup(this.mPerson);
      this.CheckPanels();
      this.leftList.Setup(this.mChampionship);
      this.rightList.Setup(this.mChampionship);
      this.Refresh();
    }
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionTeam, 0.0f);
  }

  public override void OnExit()
  {
    base.OnExit();
  }

  public void Refresh()
  {
    this.PrepareObjects();
    this.statsPanel.Setup();
    this.RefreshLists();
    this.UpdateNavigation();
  }

  public void RefreshLists()
  {
    if (this.leftList.gameObject.activeSelf)
      this.leftList.RefreshList();
    if (!this.rightList.gameObject.activeSelf)
      return;
    this.rightList.RefreshList();
  }

  public void UpdateNavigation()
  {
    this.leftPanel.UpdateNavigation();
    this.rightPanel.UpdateNavigation();
  }

  public void CheckPanels()
  {
    if (this.leftPanel.person == null || this.rightPanel.person == null || this.leftPanel.person.GetType() == this.rightPanel.person.GetType() && this.leftPanel.person != this.rightPanel.person)
      return;
    this.rightPanel.Reset();
  }

  public void PrepareObjects()
  {
    GameUtility.SetActive(this.leftPanel.gameObject, this.leftPanel.person != null);
    GameUtility.SetActive(this.rightPanel.gameObject, this.rightPanel.person != null);
    GameUtility.SetActive(this.leftList.gameObject, !this.leftPanel.gameObject.activeSelf);
    GameUtility.SetActive(this.rightList.gameObject, !this.rightPanel.gameObject.activeSelf);
  }

  public void SetPersonForComparison(Person inPerson, UICompareStaffListWidget inWidget)
  {
    if ((Object) this.leftList == (Object) inWidget)
      this.leftPanel.Setup(inPerson);
    else if ((Object) this.rightList == (Object) inWidget)
      this.rightPanel.Setup(inPerson);
    this.Refresh();
  }

  public Team GetTeamOtherPerson(UICompareStaffProfileWidget inWidget)
  {
    Person personOtherWidget = this.GetPersonOtherWidget(inWidget);
    if (personOtherWidget != null && personOtherWidget.contract.employeer != null)
      return personOtherWidget.contract.GetTeam();
    return (Team) null;
  }

  public Person GetPersonOtherWidget(UICompareStaffProfileWidget inWidget)
  {
    if ((Object) inWidget == (Object) this.leftPanel)
      return this.rightPanel.person;
    if ((Object) inWidget == (Object) this.rightPanel)
      return this.leftPanel.person;
    return (Person) null;
  }

  public void UpdateLists(Person inPerson)
  {
    if (inPerson != null)
    {
      this.leftList.SetShortListed(inPerson);
      this.rightList.SetShortListed(inPerson);
    }
    this.UpdateFavourites(inPerson);
    this.RefreshLists();
  }

  public void UpdateFavourites(Person inPerson)
  {
    if (inPerson == null)
      return;
    if (this.leftPanel.person == inPerson)
    {
      this.SetFavourite(this.leftPanel, inPerson);
    }
    else
    {
      if (this.rightPanel.person != inPerson)
        return;
      this.SetFavourite(this.rightPanel, inPerson);
    }
  }

  private void SetFavourite(UICompareStaffProfileWidget inWidget, Person inPerson)
  {
    if (!((Object) inWidget != (Object) null) || inPerson == null)
      return;
    if (inPerson is Driver)
      inWidget.overviewWidget.SetFavourite();
    else if (inPerson is Engineer)
    {
      inWidget.overviewWidget.SetFavourite();
    }
    else
    {
      if (!(inPerson is Mechanic))
        return;
      inWidget.overviewWidget.SetFavourite();
    }
  }

  public enum Mode
  {
    Driver,
    Engineer,
    Mechanic,
  }
}
