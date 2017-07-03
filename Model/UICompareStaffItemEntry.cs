// Decompiled with JetBrains decompiler
// Type: UICompareStaffItemEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICompareStaffItemEntry : MonoBehaviour
{
  public Button button;
  public Button compareButton;
  public Button scoutButton;
  public Button checkQueueButton;
  public Toggle toggle;
  public UICharacterPortrait driverPortrait;
  public UICharacterPortrait staffPortrait;
  public TextMeshProUGUI personName;
  public TextMeshProUGUI teamName;
  public TextMeshProUGUI compareButtonLabel;
  public UIAbilityStars abilityStars;
  public UICompareStaffListWidget widget;
  private UICompareStaffItemEntry.Type mType;
  private Person mPerson;

  public UICompareStaffItemEntry.Type type
  {
    get
    {
      return this.mType;
    }
  }

  public Person person
  {
    get
    {
      return this.mPerson;
    }
  }

  public void Setup(Person inPerson)
  {
    if (inPerson == null)
      return;
    this.mPerson = inPerson;
    this.SetButtonListeners();
    if (inPerson is Driver)
      this.SetupDriver((Driver) inPerson);
    else if (inPerson is Engineer)
    {
      this.SetupEngineer((Engineer) inPerson);
    }
    else
    {
      if (!(inPerson is Mechanic))
        return;
      this.SetupMechanic((Mechanic) inPerson);
    }
  }

  private void SetButtonListeners()
  {
    this.toggle.onValueChanged.RemoveAllListeners();
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle()));
    this.button.onClick.RemoveAllListeners();
    this.button.onClick.AddListener(new UnityAction(this.OnButton));
    this.compareButton.onClick.RemoveAllListeners();
    this.compareButton.onClick.AddListener(new UnityAction(this.OnButton));
    this.scoutButton.onClick.RemoveAllListeners();
    this.scoutButton.onClick.AddListener(new UnityAction(this.OnButton));
    this.checkQueueButton.onClick.RemoveAllListeners();
    this.checkQueueButton.onClick.AddListener(new UnityAction(this.OnButton));
  }

  public void SetButtonInteratable(bool inValue)
  {
    this.button.interactable = inValue;
    this.compareButton.interactable = inValue;
    this.scoutButton.interactable = inValue;
    this.checkQueueButton.interactable = inValue;
  }

  public void SetupDriver(Driver inDriver)
  {
    if (inDriver == null)
      return;
    this.mType = UICompareStaffItemEntry.Type.Driver;
    this.toggle.isOn = inDriver.isShortlisted;
    GameUtility.SetActive(this.staffPortrait.gameObject, false);
    GameUtility.SetActive(this.driverPortrait.gameObject, true);
    GameUtility.SetActive(this.abilityStars.gameObject, inDriver.CanShowStats());
    this.driverPortrait.SetPortrait((Person) inDriver);
    this.SetDetails((Person) inDriver);
    ScoutingManager scoutingManager = Game.instance.scoutingManager;
    bool flag = !inDriver.hasBeenScouted && scoutingManager.IsScouting() && (scoutingManager.IsDriverCurrentlyScouted(inDriver) || scoutingManager.IsDriverInScoutQueue(inDriver));
    this.compareButtonLabel.text = Localisation.LocaliseID("PSG_10001416", (GameObject) null);
    GameUtility.SetActive(this.compareButton.gameObject, inDriver.CanShowStats());
    GameUtility.SetActive(this.scoutButton.gameObject, !inDriver.CanShowStats() && !flag);
    GameUtility.SetActive(this.checkQueueButton.gameObject, !inDriver.CanShowStats() && flag);
    if (!this.abilityStars.gameObject.activeSelf)
      return;
    this.abilityStars.SetAbilityStarsData(inDriver);
  }

  public void SetupEngineer(Engineer inEngineer)
  {
    if (inEngineer == null)
      return;
    this.mType = UICompareStaffItemEntry.Type.Engineer;
    this.toggle.isOn = inEngineer.isShortlisted;
    GameUtility.SetActive(this.driverPortrait.gameObject, false);
    GameUtility.SetActive(this.staffPortrait.gameObject, true);
    GameUtility.SetActive(this.abilityStars.gameObject, true);
    this.staffPortrait.SetPortrait((Person) inEngineer);
    this.SetDetails((Person) inEngineer);
    this.abilityStars.SetAbilityStarsData((Person) inEngineer);
    GameUtility.SetActive(this.compareButton.gameObject, true);
    GameUtility.SetActive(this.scoutButton.gameObject, false);
    GameUtility.SetActive(this.checkQueueButton.gameObject, false);
    this.compareButtonLabel.text = Localisation.LocaliseID("PSG_10007757", (GameObject) null);
  }

  public void SetupMechanic(Mechanic inMechanic)
  {
    if (inMechanic == null)
      return;
    this.mType = UICompareStaffItemEntry.Type.Mechanic;
    this.toggle.isOn = inMechanic.isShortlisted;
    GameUtility.SetActive(this.driverPortrait.gameObject, false);
    GameUtility.SetActive(this.staffPortrait.gameObject, true);
    GameUtility.SetActive(this.abilityStars.gameObject, true);
    this.staffPortrait.SetPortrait((Person) inMechanic);
    this.SetDetails((Person) inMechanic);
    this.abilityStars.SetAbilityStarsData((Person) inMechanic);
    GameUtility.SetActive(this.compareButton.gameObject, true);
    GameUtility.SetActive(this.scoutButton.gameObject, false);
    GameUtility.SetActive(this.checkQueueButton.gameObject, false);
    this.compareButtonLabel.text = Localisation.LocaliseID("PSG_10007756", (GameObject) null);
  }

  public void SetDetails(Person inPerson)
  {
    if (inPerson == null)
      return;
    this.personName.text = inPerson.name;
    this.teamName.text = inPerson.contract.GetTeam().name;
  }

  private void OnToggle()
  {
    if (this.mPerson == null || this.mPerson.isShortlisted == this.toggle.isOn)
      return;
    this.mPerson.ToggleShortlisted(this.toggle.isOn);
    this.widget.screen.UpdateLists(this.mPerson);
  }

  private void OnButton()
  {
    if (this.mPerson == null)
      return;
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mPerson is Driver)
    {
      Driver mPerson = (Driver) this.mPerson;
      ScoutingManager scoutingManager = Game.instance.scoutingManager;
      bool flag = !mPerson.hasBeenScouted && scoutingManager.IsScouting() && (scoutingManager.IsDriverCurrentlyScouted(mPerson) || scoutingManager.IsDriverInScoutQueue(mPerson));
      if (mPerson.CanShowStats())
        this.widget.screen.SetPersonForComparison(this.mPerson, this.widget);
      else if (flag)
      {
        UIManager.instance.ChangeScreen("ScoutingScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
      }
      else
      {
        scoutingManager.AddScoutingAssignment(mPerson);
        GameUtility.SetActive(this.scoutButton.gameObject, false);
        GameUtility.SetActive(this.checkQueueButton.gameObject, true);
      }
    }
    else
      this.widget.screen.SetPersonForComparison(this.mPerson, this.widget);
  }

  public enum Type
  {
    Driver,
    Engineer,
    Mechanic,
  }
}
