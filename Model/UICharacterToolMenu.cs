// Decompiled with JetBrains decompiler
// Type: UICharacterToolMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICharacterToolMenu : MonoBehaviour
{
  private Contract.Job mLoadJob = Contract.Job.Actor;
  public Toggle chairmansButton;
  public Toggle principalsButton;
  public Toggle driversButton;
  public Toggle mechanicsButton;
  public Toggle engineersButton;
  public Toggle celebritiesButton;
  public Toggle assistantsButton;
  public Toggle scoutsButton;
  public Toggle journalistsButton;
  public Toggle sponsorsButton;
  public Toggle createNewToggle;
  public Toggle loadDatabaseToggle;
  public Toggle randomRegenToggle;
  public Button clearProfilesButton;
  public Button loadButton;
  public Button saveButton;
  public CharacterCreatorToolScreen screen;
  private Person mLoadPerson;

  public bool useRegenRandom
  {
    get
    {
      return this.randomRegenToggle.isOn;
    }
  }

  public void OnStart()
  {
    this.chairmansButton.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SelectMode(this.chairmansButton, (Person) new Chairman())));
    this.principalsButton.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SelectMode(this.principalsButton, (Person) new TeamPrincipal())));
    this.driversButton.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SelectMode(this.driversButton, (Person) new Driver())));
    this.mechanicsButton.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SelectMode(this.mechanicsButton, (Person) new Mechanic())));
    this.engineersButton.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SelectMode(this.engineersButton, (Person) new Engineer())));
    this.celebritiesButton.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SelectMode(this.celebritiesButton, (Person) new Celebrity())));
    this.assistantsButton.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SelectMode(this.assistantsButton, (Person) new Assistant())));
    this.scoutsButton.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SelectMode(this.scoutsButton, (Person) new Scout())));
    this.journalistsButton.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnLoadDB(this.journalistsButton, Contract.Job.Journalist)));
    this.sponsorsButton.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnLoadDB(this.sponsorsButton, Contract.Job.SponsorLiasion)));
    this.clearProfilesButton.onClick.AddListener((UnityAction) (() => this.screen.profilesWidget.ClearGrid()));
    this.loadButton.onClick.AddListener(new UnityAction(this.OnLoadButton));
    this.saveButton.onClick.AddListener(new UnityAction(this.OnSaveButton));
  }

  public void Setup()
  {
    this.screen.profilesWidget.OnSelection -= new Action(this.UpdateButtonsState);
    this.screen.profilesWidget.OnSelection += new Action(this.UpdateButtonsState);
    this.UpdateButtonsState();
  }

  public void SelectMode(Toggle inToggle, Person inPerson)
  {
    if (!inToggle.isOn)
      return;
    this.mLoadPerson = inPerson;
  }

  public void OnLoadDB(Toggle inToggle, Contract.Job inJob)
  {
    if (!inToggle.isOn)
      return;
    this.mLoadJob = inJob;
    this.mLoadPerson = (Person) null;
  }

  public void OnLoadButton()
  {
    if (this.loadDatabaseToggle.isOn)
    {
      if (this.mLoadPerson != null)
        this.screen.profilesWidget.LoadDB(this.mLoadPerson);
      else
        this.screen.profilesWidget.LoadDB(this.mLoadJob);
    }
    else if (this.mLoadPerson != null)
      this.screen.profilesWidget.LoadPerson(this.mLoadPerson);
    else
      this.screen.profilesWidget.LoadPerson(this.mLoadJob);
  }

  public void OnSaveButton()
  {
    if (this.screen.profilesWidget.selectedNum <= 0)
      return;
    this.screen.savePopup.Setup(this.screen.profilesWidget.selectedPerson);
  }

  private void UpdateButtonsState()
  {
    this.saveButton.interactable = this.screen.profilesWidget.selectedNum > 0;
  }
}
