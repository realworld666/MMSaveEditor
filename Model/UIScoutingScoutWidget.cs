// Decompiled with JetBrains decompiler
// Type: UIScoutingScoutWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIScoutingScoutWidget : MonoBehaviour
{
  public UIGridList grid;
  public Button cancelJobsButton;
  public UICharacterPortrait scoutPortrait;
  public TextMeshProUGUI scoutName;
  public GameObject headerScouting;
  public GameObject headerInQueue;
  public GameObject headerComplete;
  public GameObject entryPrefab;
  public ScoutingScreen screen;
  private ScoutingManager mScoutingManager;

  public ScoutingManager scoutingManager
  {
    get
    {
      return this.mScoutingManager;
    }
  }

  public void OnStart()
  {
    this.cancelJobsButton.onClick.AddListener(new UnityAction(this.OnCancelJobs));
    GameUtility.SetActive(this.cancelJobsButton.gameObject, false);
  }

  public void Setup()
  {
    this.mScoutingManager = Game.instance.scoutingManager;
    this.SetDetails();
    this.Refresh();
  }

  public void Refresh()
  {
    this.SetGrid();
  }

  private void SetDetails()
  {
    Person personOnJob = Game.instance.player.team.contractManager.GetPersonOnJob(Contract.Job.Scout);
    this.scoutPortrait.SetPortrait(personOnJob);
    this.scoutName.text = personOnJob.name;
  }

  private void SetGrid()
  {
    GameUtility.SetActive(this.cancelJobsButton.gameObject, this.mScoutingManager.IsScouting());
    this.grid.DestroyListItems();
    bool inValue1 = this.mScoutingManager.scoutingAssigmentsCompleteCount >= 1;
    this.CreateHeader(this.headerComplete, inValue1);
    if (inValue1)
      this.CreateEntries(UIScoutingScoutWidget.Type.Completed);
    this.CreateHeader(this.headerScouting, true);
    this.CreateEntries(UIScoutingScoutWidget.Type.OnGoing);
    bool inValue2 = this.mScoutingManager.scoutingAssignmentsCount >= 1;
    this.CreateHeader(this.headerInQueue, inValue2);
    if (inValue2)
      this.CreateEntries(UIScoutingScoutWidget.Type.InQueue);
    GameUtility.SetActive(this.entryPrefab, false);
  }

  private void CreateHeader(GameObject inHeader, bool inValue)
  {
    GameUtility.SetActive(inHeader, inValue);
    if (!inValue)
      return;
    inHeader.transform.SetAsLastSibling();
  }

  private void CreateEntries(UIScoutingScoutWidget.Type inType)
  {
    this.grid.itemPrefab = this.entryPrefab;
    GameUtility.SetActive(this.entryPrefab, true);
    switch (inType)
    {
      case UIScoutingScoutWidget.Type.OnGoing:
        int totalScoutingSlots = this.mScoutingManager.totalScoutingSlots;
        for (int index = 0; index < totalScoutingSlots; ++index)
        {
          UIScoutingEntry listItem = this.grid.CreateListItem<UIScoutingEntry>();
          if (this.mScoutingManager.IsSlotLocked(index))
          {
            int inLevelRequired = index - this.mScoutingManager.baseScoutingSlotsCount;
            listItem.SetupLocked(inLevelRequired);
          }
          else if (this.mScoutingManager.IsSlotEmpty(index))
            listItem.SetupEmpty();
          else
            listItem.Setup(this.mScoutingManager.GetCurrentScoutingEntry(index).driver, inType);
        }
        break;
      case UIScoutingScoutWidget.Type.Completed:
        int assigmentsCompleteCount = this.mScoutingManager.scoutingAssigmentsCompleteCount;
        for (int index = 0; index < assigmentsCompleteCount; ++index)
          this.grid.CreateListItem<UIScoutingEntry>().Setup(this.mScoutingManager.GetCompletedDriver(index).driver, inType);
        break;
      case UIScoutingScoutWidget.Type.InQueue:
        int assignmentsCount = this.mScoutingManager.scoutingAssignmentsCount;
        for (int index = 0; index < assignmentsCount; ++index)
          this.grid.CreateListItem<UIScoutingEntry>().Setup(this.mScoutingManager.GetDriverInQueue(index).driver, inType);
        break;
    }
  }

  private void OnCancelJobs()
  {
    GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
    string inCancelString = Localisation.LocaliseID("PSG_10009077", (GameObject) null);
    string inConfirmString = Localisation.LocaliseID("PSG_10009078", (GameObject) null);
    string inText = Localisation.LocaliseID("PSG_10010497", (GameObject) null);
    string inTitle = Localisation.LocaliseID("PSG_10010498", (GameObject) null);
    Action inConfirmAction = (Action) (() =>
    {
      this.mScoutingManager.StopAllScoutingJobs();
      this.screen.Refresh();
    });
    dialog.Show((Action) null, inCancelString, inConfirmAction, inConfirmString, inText, inTitle);
  }

  public enum Type
  {
    OnGoing,
    Completed,
    InQueue,
  }
}
