// Decompiled with JetBrains decompiler
// Type: UITutorial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Diagnostics;
using UnityEngine;

public class UITutorial : MonoBehaviour
{
  private GameState.Type mPreviousGameStateType = GameState.Type.NewGameSetup;
  [SerializeField]
  private GameObject overlay;
  [SerializeField]
  private bool screenSpaceCamera;
  [SerializeField]
  private UITutorialSequence[] tutorialSequences;
  [SerializeField]
  [HideInInspector]
  private string mTutorialName;
  private UIScreen mCurrentScreen;

  public string tutorialName
  {
    get
    {
      return this.mTutorialName;
    }
  }

  public void SetTutorialName(string inTutorialName)
  {
    this.mTutorialName = inTutorialName;
  }

  private void Awake()
  {
    Animator component1 = this.overlay.GetComponent<Animator>();
    for (int inID = 0; inID < this.tutorialSequences.Length; ++inID)
      this.tutorialSequences[inID].Initialise(this.mTutorialName, inID, component1);
    Canvas component2 = this.transform.parent.gameObject.GetComponent<Canvas>();
    if ((UnityEngine.Object) component2 != (UnityEngine.Object) null && this.screenSpaceCamera)
      UIManager.instance.SetupCanvasCamera(component2);
    this.ShowOverlay(false);
    for (int index = 0; index < this.tutorialSequences.Length; ++index)
      this.tutorialSequences[index].HideTutorialSteps();
  }

  [Conditional("DEVELOPMENT_BUILD")]
  [Conditional("UNITY_EDITOR")]
  public void Validate()
  {
    GameUtility.Assert((UnityEngine.Object) this.overlay != (UnityEngine.Object) null, "overlay != null", (UnityEngine.Object) this);
    for (int index = 0; index < this.tutorialSequences.Length; ++index)
      GameUtility.Assert((UnityEngine.Object) this.tutorialSequences[index] != (UnityEngine.Object) null, "tutorialSequences[ " + (object) index + " ]!= null", (UnityEngine.Object) this);
  }

  private void OnEnable()
  {
    if (!this.IsTutorialActive())
      return;
    for (int index = 0; index < this.tutorialSequences.Length; ++index)
    {
      this.tutorialSequences[index].OnEnableLoadSequenceState();
      if (this.tutorialSequences[index].CanStartSequence())
      {
        this.ShowOverlay(true);
        break;
      }
    }
  }

  private void Update()
  {
    if (!this.IsTutorialActive() || App.instance.gameStateManager.currentState.type == GameState.Type.NewGameSetup || App.instance.gameStateManager.currentState.type == GameState.Type.ChallengeSetup)
      return;
    if (App.instance.gameStateManager.currentState.type != this.mPreviousGameStateType)
    {
      this.CheckIfAnySequenceIsNowIrrelevant();
      this.mPreviousGameStateType = App.instance.gameStateManager.currentState.type;
    }
    this.CheckIfAnySequenceCanStart();
  }

  private void CheckIfAnySequenceIsNowIrrelevant()
  {
    for (int index = 0; index < this.tutorialSequences.Length; ++index)
      this.tutorialSequences[index].UpdateSequenceState();
  }

  private void CheckIfAnySequenceCanStart()
  {
    for (int inSequenceIndex = 0; inSequenceIndex < this.tutorialSequences.Length; ++inSequenceIndex)
    {
      this.tutorialSequences[inSequenceIndex].LazyLoadSequenceState();
      if (this.tutorialSequences[inSequenceIndex].isInitialised && this.tutorialSequences[inSequenceIndex].CanStartSequence())
        this.StartTutorialSequence(inSequenceIndex);
      else if (this.tutorialSequences[inSequenceIndex].notStartedYet)
        this.tutorialSequences[inSequenceIndex].HideGameObjectsPreSequence();
    }
  }

  private void StartTutorialSequence(int inSequenceIndex)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UITutorial.\u003CStartTutorialSequence\u003Ec__AnonStorey75 sequenceCAnonStorey75 = new UITutorial.\u003CStartTutorialSequence\u003Ec__AnonStorey75();
    // ISSUE: reference to a compiler-generated field
    sequenceCAnonStorey75.inSequenceIndex = inSequenceIndex;
    // ISSUE: reference to a compiler-generated field
    sequenceCAnonStorey75.\u003C\u003Ef__this = this;
    if (!this.IsTutorialActive())
      return;
    // ISSUE: reference to a compiler-generated field
    Game.instance.tutorialSystem.SetCurrentTutorialSequenceData(this.mTutorialName, sequenceCAnonStorey75.inSequenceIndex);
    Animator[] componentsInChildren = this.GetComponentsInChildren<Animator>(true);
    if (componentsInChildren != null)
    {
      for (int index = 0; index < componentsInChildren.Length; ++index)
        componentsInChildren[index].updateMode = AnimatorUpdateMode.UnscaledTime;
    }
    // ISSUE: reference to a compiler-generated field
    if (this.tutorialSequences[sequenceCAnonStorey75.inSequenceIndex].isOnGoing)
      return;
    // ISSUE: reference to a compiler-generated field
    this.tutorialSequences[sequenceCAnonStorey75.inSequenceIndex].StartSequence();
    this.ShowOverlay(true);
    this.mCurrentScreen = UIManager.instance.currentScreen;
    this.mCurrentScreen.canEnterPreferencesScreen = false;
    // ISSUE: reference to a compiler-generated field
    this.tutorialSequences[sequenceCAnonStorey75.inSequenceIndex].onSequenceEnd = (Action) null;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated method
    this.tutorialSequences[sequenceCAnonStorey75.inSequenceIndex].onSequenceEnd += new Action(sequenceCAnonStorey75.\u003C\u003Em__FF);
  }

  public void QueueUpNewStartingSequence(int inSequenceIndex)
  {
    if (this.tutorialSequences[inSequenceIndex].isOnGoing)
      return;
    this.tutorialSequences[inSequenceIndex].SetSequenceToStart();
    this.CheckIfAnySequenceCanStart();
  }

  private bool IsTutorialActive()
  {
    return Game.instance != null && Game.instance.tutorialSystem != null && Game.instance.tutorialSystem.isTutorialActive;
  }

  private void OnSequenceEnd(int inSequenceIndex)
  {
    if (inSequenceIndex == this.tutorialSequences.Length - 1)
    {
      this.EndSequenceAction(this.tutorialSequences[inSequenceIndex]);
    }
    else
    {
      if (this.tutorialSequences[inSequenceIndex + 1].CanStartSequence())
        return;
      this.EndSequenceAction(this.tutorialSequences[inSequenceIndex]);
    }
  }

  private void EndSequenceAction(UITutorialSequence inEndingTutorialSequence)
  {
    this.ShowOverlay(false);
    if ((UnityEngine.Object) inEndingTutorialSequence == (UnityEngine.Object) null)
      Game.instance.tutorialSystem.currentTutorialSequence = (UITutorialSequence) null;
    else if ((UnityEngine.Object) Game.instance.tutorialSystem.currentTutorialSequence == (UnityEngine.Object) inEndingTutorialSequence)
    {
      Game.instance.tutorialSystem.currentTutorialSequence = (UITutorialSequence) null;
      Game.instance.time.UnPause(GameTimer.PauseType.Tutorial);
    }
    if (!((UnityEngine.Object) this.mCurrentScreen != (UnityEngine.Object) null))
      return;
    this.mCurrentScreen.canEnterPreferencesScreen = true;
  }

  public bool IsTutorialOnRaceGrid()
  {
    if (!this.IsTutorialActive())
      return false;
    for (int index = 0; index < this.tutorialSequences.Length; ++index)
    {
      if (this.tutorialSequences[index].isSequenceOnRaceGrid && !this.tutorialSequences[index].isFinished)
        return true;
    }
    return false;
  }

  public bool IsTutorialHidingGameObject(GameObject inGameObject)
  {
    for (int index1 = 0; index1 < this.tutorialSequences.Length; ++index1)
    {
      this.tutorialSequences[index1].LazyLoadSequenceState();
      if (this.tutorialSequences[index1].notStartedYet)
      {
        for (int index2 = 0; index2 < this.tutorialSequences[index1].gameObjectsToHide.Length; ++index2)
        {
          if (this.tutorialSequences[index1].gameObjectsToHide[index2].Equals((object) inGameObject))
            return true;
        }
      }
    }
    return false;
  }

  public void CancelAndResetTutorial()
  {
    this.EndSequenceAction((UITutorialSequence) null);
    for (int index = 0; index < this.tutorialSequences.Length; ++index)
      this.tutorialSequences[index].CancelAndReset();
  }

  public void PauseTutorial(bool inPause)
  {
    for (int index = 0; index < this.tutorialSequences.Length; ++index)
    {
      if (this.tutorialSequences[index].isOnGoing)
      {
        this.ShowOverlay(!inPause);
        this.tutorialSequences[index].PauseSequence(inPause);
        break;
      }
    }
  }

  private void ShowOverlay(bool inShow)
  {
    GameUtility.SetActive(this.overlay, inShow);
  }
}
