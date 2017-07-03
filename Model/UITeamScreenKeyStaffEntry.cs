// Decompiled with JetBrains decompiler
// Type: UITeamScreenKeyStaffEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITeamScreenKeyStaffEntry : MonoBehaviour
{
  public UICharacterPortrait portrait;
  public Flag nationalityFlag;
  public UIJobSecurity jobSecurity;
  public TextMeshProUGUI name;
  public TextMeshProUGUI age;
  public GameObject vacant;
  public GameObject jobSecurityContainer;
  public Button personsPageButton;
  public UIScoutDriverWidget abilityUnknown;
  public UIAbilityStars stars;
  private Person mPerson;

  private void Awake()
  {
    if (!((Object) this.personsPageButton != (Object) null))
      return;
    this.personsPageButton.onClick.AddListener(new UnityAction(this.GoToPersonsPage));
  }

  private void GoToPersonsPage()
  {
    if (this.mPerson == null)
      return;
    if (this.mPerson.contract.job == Contract.Job.EngineerLead || this.mPerson.contract.job == Contract.Job.Engineer || this.mPerson.contract.job == Contract.Job.Mechanic)
      UIManager.instance.ChangeScreen("StaffDetailsScreen", (Entity) this.mPerson, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
    else if (this.mPerson.contract.job == Contract.Job.TeamPrincipal)
    {
      if (this.mPerson != Game.instance.player)
        return;
      UIManager.instance.ChangeScreen("PlayerScreen", (Entity) this.mPerson, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
    }
    else
    {
      if (this.mPerson.contract.job != Contract.Job.Driver)
        return;
      UIManager.instance.ChangeScreen("DriverScreen", (Entity) this.mPerson, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
    }
  }

  public void Setup(Person inPerson)
  {
    this.mPerson = inPerson;
    GameUtility.SetActive(this.gameObject, this.mPerson != null);
    if (this.mPerson == null)
      return;
    if ((Object) this.personsPageButton != (Object) null)
    {
      bool flag = !(App.instance.gameStateManager.currentState is FrontendState);
      if (this.mPerson.contract.job == Contract.Job.TeamPrincipal && this.mPerson == Game.instance.player)
        this.personsPageButton.interactable = !flag;
      else
        this.personsPageButton.interactable = true;
    }
    if (inPerson is Driver)
    {
      Driver inDriver = (Driver) inPerson;
      if (inDriver.CanShowStats())
      {
        GameUtility.SetActive(this.abilityUnknown.gameObject, false);
        GameUtility.SetActive(this.stars.gameObject, true);
        this.stars.SetAbilityStarsData(inDriver);
      }
      else
      {
        GameUtility.SetActive(this.abilityUnknown.gameObject, true);
        GameUtility.SetActive(this.stars.gameObject, false);
        this.abilityUnknown.SetupScoutDriverWidget(inDriver);
      }
    }
    else
      this.stars.SetAbilityStarsData(inPerson);
    if ((Object) this.jobSecurityContainer != (Object) null && this.mPerson is TeamPrincipal)
    {
      bool inIsActive = this.mPerson.IsReplacementPerson();
      GameUtility.SetActive(this.jobSecurityContainer, !inIsActive);
      GameUtility.SetActive(this.vacant, inIsActive);
      if (this.jobSecurityContainer.activeSelf)
        this.jobSecurity.SetJobSecurity(((TeamPrincipal) this.mPerson).jobSecurity);
    }
    this.portrait.SetPortrait(this.mPerson);
    this.nationalityFlag.SetNationality(this.mPerson.nationality);
    this.name.text = this.mPerson.shortName;
    this.age.text = this.mPerson.GetAge().ToString();
  }

  public void UpdateAbility()
  {
    if (!((Object) this.abilityUnknown != (Object) null) || !(this.mPerson is Driver))
      return;
    this.abilityUnknown.SetupScoutDriverWidget(this.mPerson as Driver);
  }
}
