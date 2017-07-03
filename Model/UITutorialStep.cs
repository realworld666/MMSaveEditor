// Decompiled with JetBrains decompiler
// Type: UITutorialStep
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITutorialStep : MonoBehaviour
{
  public int buttonClicksNumber = 1;
  public int dropdwonValueToEndStep = -1;
  public float sliderTopValueToEndStep = -1f;
  public float sliderBottomValueToEndStep = -1f;
  private List<Transform> mAllTransformsToHighlight = new List<Transform>();
  private readonly string mTutorialObjectTag = "Tutorial";
  private List<GameObject> mChildrenToActivate = new List<GameObject>();
  public Action onStepEnd;
  public UITutorialStep.EndStepInteraction endStepInteraction;
  public UITutorialStep.HighlightGridItemsOrder highlightGridItemsOrder;
  [SerializeField]
  private Transform[] transformsToHighlight;
  [SerializeField]
  private GameObject[] transformsToForceShow;
  public UIGridList gridListToHighlight;
  [SerializeField]
  private int[] childGridIndexesToHighlight;
  public Button endStepButton;
  public Toggle endStepToggle;
  public Dropdown endStepDropdown;
  public Slider endStepSlider;
  public EventTrigger endStepEventTrigger;
  public bool toggleValueToEndStep;
  public string buttonRuntimeName;
  public Animator animator;
  public bool hideAlphaCanvas;
  public bool pauseGameOnEnter;
  public bool unlockSimulationInput;
  public bool blockShortcutsInput;
  public bool isFinished;
  private UITutorialStep.PreviousTransformAndSiblingIndex[] mPreviousParents;
  private int mCurrentButtonClicks;
  private EventTrigger.Entry mEventTriggerEntry;
  private bool mMoveHighlightTransforms;
  private bool mRegisterEndStepInteractionInputs;
  private UITutorialSequence mTutorialSequence;
  private Coroutine mCoroutine;

  public List<Transform> allTransformsToHighlight
  {
    get
    {
      return this.mAllTransformsToHighlight;
    }
  }

  public UITutorialStep.PreviousTransformAndSiblingIndex[] previousParents
  {
    get
    {
      return this.mPreviousParents;
    }
  }

  private void Awake()
  {
  }

  [Conditional("DEVELOPMENT_BUILD")]
  [Conditional("UNITY_EDITOR")]
  public void Validate()
  {
    this.SetActiveChildren(false);
    GameUtility.Assert(this.transformsToHighlight != null, "transformsToHighlight != null", (UnityEngine.Object) this);
    for (int index = 0; index < this.transformsToHighlight.Length; ++index)
      GameUtility.Assert((UnityEngine.Object) this.transformsToHighlight[index] != (UnityEngine.Object) null, "transformsToHighlight[" + (object) index + "] != null", (UnityEngine.Object) this);
    GameUtility.Assert(this.transformsToForceShow != null, "transformsToForceShow != null", (UnityEngine.Object) this);
    for (int index = 0; index < this.transformsToForceShow.Length; ++index)
      GameUtility.Assert((UnityEngine.Object) this.transformsToForceShow[index] != (UnityEngine.Object) null, "transformsToForceShow[" + (object) index + "] != null", (UnityEngine.Object) this);
    if ((UnityEngine.Object) this.gridListToHighlight != (UnityEngine.Object) null)
      GameUtility.Assert(this.childGridIndexesToHighlight != null, "childGridIndexesToHighlight != null", (UnityEngine.Object) this);
    switch (this.endStepInteraction)
    {
      case UITutorialStep.EndStepInteraction.Button:
        GameUtility.Assert((UnityEngine.Object) this.endStepButton != (UnityEngine.Object) null, "endStepButton != null", (UnityEngine.Object) this);
        break;
      case UITutorialStep.EndStepInteraction.Toggle:
        GameUtility.Assert((UnityEngine.Object) this.endStepToggle != (UnityEngine.Object) null, "endStepToggle != null", (UnityEngine.Object) this);
        break;
      case UITutorialStep.EndStepInteraction.Dropdown:
        GameUtility.Assert((UnityEngine.Object) this.endStepDropdown != (UnityEngine.Object) null, "endStepDropdown != null", (UnityEngine.Object) this);
        break;
      case UITutorialStep.EndStepInteraction.Slider:
      case UITutorialStep.EndStepInteraction.SliderNormalized:
        GameUtility.Assert((UnityEngine.Object) this.endStepSlider != (UnityEngine.Object) null, "endStepSlider != null", (UnityEngine.Object) this);
        break;
      case UITutorialStep.EndStepInteraction.RolloverEnter:
        GameUtility.Assert((UnityEngine.Object) this.endStepEventTrigger != (UnityEngine.Object) null, "endStepEventTrigger != null", (UnityEngine.Object) this);
        break;
    }
  }

  public void ActivateChildren()
  {
    this.SetActiveChildren(true);
  }

  public void SetActiveChildren(bool inState)
  {
    if (this.mChildrenToActivate.Count == 0)
    {
      for (int index = 0; index < this.transform.childCount; ++index)
      {
        GameObject gameObject = this.transform.GetChild(index).gameObject;
        if (gameObject.activeSelf)
          this.mChildrenToActivate.Add(gameObject);
      }
    }
    for (int index = 0; index < this.mChildrenToActivate.Count; ++index)
      this.mChildrenToActivate[index].SetActive(inState);
  }

  public void Activate(UITutorialSequence inTutorialSequence)
  {
    this.mTutorialSequence = inTutorialSequence;
    this.mAllTransformsToHighlight.Clear();
    this.mAllTransformsToHighlight.AddRange((IEnumerable<Transform>) this.transformsToHighlight);
    this.AddGridlistSpawnedTransformsToHighlight();
    this.gameObject.SetActive(true);
    this.RegisterUIInteractionInputs();
    this.mMoveHighlightTransforms = true;
    RectTransform component = this.GetComponent<RectTransform>();
    if ((UnityEngine.Object) component != (UnityEngine.Object) null && this.transformsToHighlight != null)
      LayoutRebuilder.ForceRebuildLayoutImmediate(component);
    if (this.blockShortcutsInput)
      Game.instance.tutorialSystem.BlockShortcutsInput(true);
    if (this.endStepInteraction == UITutorialStep.EndStepInteraction.RolloverEnter)
    {
      this.mEventTriggerEntry = new EventTrigger.Entry();
      this.mEventTriggerEntry.eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter;
      this.mEventTriggerEntry.callback.AddListener((UnityAction<BaseEventData>) (eventData => this.EndStep()));
      this.endStepEventTrigger.get_triggers().Add(this.mEventTriggerEntry);
    }
    else
    {
      if (this.endStepInteraction != UITutorialStep.EndStepInteraction.CarPartDesignBuilt)
        return;
      Game.instance.player.team.carManager.carPartDesign.OnPartBuilt += new Action(this.EndStep);
    }
  }

  private void RegisterUIInteractionInputs()
  {
    switch (this.endStepInteraction)
    {
      case UITutorialStep.EndStepInteraction.Button:
        this.endStepButton.onClick.RemoveListener(new UnityAction(this.OnDeactivateButton));
        this.endStepButton.onClick.AddListener(new UnityAction(this.OnDeactivateButton));
        break;
      case UITutorialStep.EndStepInteraction.Toggle:
        this.endStepToggle.onValueChanged.RemoveListener(new UnityAction<bool>(this.OnDeactivateToggle));
        this.endStepToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnDeactivateToggle));
        break;
      case UITutorialStep.EndStepInteraction.Dropdown:
        this.endStepDropdown.onValueChanged.RemoveListener(new UnityAction<int>(this.OnDeactivateDropdown));
        this.endStepDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnDeactivateDropdown));
        break;
      case UITutorialStep.EndStepInteraction.ButtonRuntime:
        if ((UnityEngine.Object) this.endStepButton != (UnityEngine.Object) null)
        {
          this.endStepButton.onClick.RemoveListener(new UnityAction(this.OnDeactivateButton));
          this.endStepButton.onClick.AddListener(new UnityAction(this.OnDeactivateButton));
          break;
        }
        if (!((UnityEngine.Object) this.endStepToggle != (UnityEngine.Object) null))
          break;
        this.endStepToggle.onValueChanged.RemoveListener(new UnityAction<bool>(this.OnDeactivateToggle));
        this.endStepToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnDeactivateToggle));
        break;
    }
  }

  private void AddGridlistSpawnedTransformsToHighlight()
  {
    if (!((UnityEngine.Object) this.gridListToHighlight != (UnityEngine.Object) null))
      return;
    int inGridItemsToHighlightCount = 0;
    int inGridItemsToHighlightIndex = 0;
    if (this.highlightGridItemsOrder == UITutorialStep.HighlightGridItemsOrder.FromTop)
    {
      for (int inIndex = 0; inIndex < this.gridListToHighlight.itemCount; ++inIndex)
      {
        GameObject inGridItemToHighlight = this.gridListToHighlight.GetItem(inIndex);
        if (this.TryAddGridItemAndEnds(ref inGridItemsToHighlightCount, ref inGridItemsToHighlightIndex, inGridItemToHighlight))
          break;
      }
    }
    else
    {
      if (this.highlightGridItemsOrder != UITutorialStep.HighlightGridItemsOrder.FromBottom)
        return;
      for (int inIndex = this.gridListToHighlight.itemCount - 1; inIndex >= 0; --inIndex)
      {
        GameObject inGridItemToHighlight = this.gridListToHighlight.GetItem(inIndex);
        if (this.TryAddGridItemAndEnds(ref inGridItemsToHighlightCount, ref inGridItemsToHighlightIndex, inGridItemToHighlight))
          break;
      }
    }
  }

  private bool TryAddGridItemAndEnds(ref int inGridItemsToHighlightCount, ref int inGridItemsToHighlightIndex, GameObject inGridItemToHighlight)
  {
    if (inGridItemToHighlight.activeInHierarchy)
    {
      if (inGridItemsToHighlightCount == this.childGridIndexesToHighlight[inGridItemsToHighlightIndex])
      {
        this.mAllTransformsToHighlight.Add(inGridItemToHighlight.transform);
        inGridItemsToHighlightIndex = inGridItemsToHighlightIndex + 1;
      }
      if (inGridItemsToHighlightIndex == 1)
        this.SearchForEndStepButtonRuntimeObject(inGridItemToHighlight);
      inGridItemsToHighlightCount = inGridItemsToHighlightCount + 1;
      if (inGridItemsToHighlightIndex >= this.childGridIndexesToHighlight.Length)
        return true;
    }
    return false;
  }

  private void SearchForEndStepButtonRuntimeObject(GameObject inFirstChild)
  {
    if (!((UnityEngine.Object) this.gridListToHighlight != (UnityEngine.Object) null) || this.endStepInteraction != UITutorialStep.EndStepInteraction.ButtonRuntime)
      return;
    Transform objectByTag = inFirstChild.transform.Find(this.buttonRuntimeName);
    if ((UnityEngine.Object) objectByTag == (UnityEngine.Object) null)
      objectByTag = this.FindObjectByTag(inFirstChild);
    Button componentInChildren = objectByTag.GetComponentInChildren<Button>(true);
    if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
      this.endStepButton = componentInChildren;
    else
      this.endStepToggle = objectByTag.GetComponentInChildren<Toggle>(true);
  }

  private Transform FindObjectByTag(GameObject inFirstChild)
  {
    Transform[] componentsInChildren = inFirstChild.GetComponentsInChildren<Transform>(true);
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      if (componentsInChildren[index].CompareTag(this.mTutorialObjectTag))
        return componentsInChildren[index];
    }
    return (Transform) null;
  }

  public void CancelAndReset()
  {
    if ((UnityEngine.Object) this.endStepButton != (UnityEngine.Object) null)
      this.endStepButton.onClick.RemoveListener(new UnityAction(this.OnDeactivateButton));
    if ((UnityEngine.Object) this.endStepDropdown != (UnityEngine.Object) null)
      this.endStepDropdown.onValueChanged.RemoveListener(new UnityAction<int>(this.OnDeactivateDropdown));
    if ((UnityEngine.Object) this.endStepToggle != (UnityEngine.Object) null)
      this.endStepToggle.onValueChanged.RemoveListener(new UnityAction<bool>(this.OnDeactivateToggle));
    if (this.endStepInteraction == UITutorialStep.EndStepInteraction.CarPartDesignBuilt && Game.IsActive())
      Game.instance.player.team.carManager.carPartDesign.OnPartBuilt -= new Action(this.EndStep);
    this.PlaceBackHighlightTransform();
    this.mMoveHighlightTransforms = false;
    this.isFinished = false;
    this.mCurrentButtonClicks = 0;
    this.ShowTransformToForceShow(false);
    if (this.blockShortcutsInput)
      Game.instance.tutorialSystem.BlockShortcutsInput(false);
    if (this.endStepInteraction == UITutorialStep.EndStepInteraction.RolloverEnter)
    {
      this.endStepEventTrigger.GetComponent<EventTrigger>().get_triggers().Remove(this.mEventTriggerEntry);
      this.mEventTriggerEntry = (EventTrigger.Entry) null;
    }
    GameUtility.SetActive(this.gameObject, false);
  }

  public void PauseStep(bool inPause)
  {
    GameUtility.SetActive(this.gameObject, !inPause);
    if (inPause)
      return;
    this.mRegisterEndStepInteractionInputs = true;
  }

  private void Update()
  {
    if (this.mRegisterEndStepInteractionInputs)
    {
      this.RegisterUIInteractionInputs();
      this.mRegisterEndStepInteractionInputs = false;
    }
    if (Input.GetMouseButtonUp(0))
    {
      switch (this.endStepInteraction)
      {
        case UITutorialStep.EndStepInteraction.Slider:
          if ((double) this.endStepSlider.value <= (double) this.sliderTopValueToEndStep && (double) this.endStepSlider.value >= (double) this.sliderBottomValueToEndStep)
          {
            this.EndStep();
            break;
          }
          break;
        case UITutorialStep.EndStepInteraction.NoCarPartTokensAvailable:
          this.EndStep();
          break;
        case UITutorialStep.EndStepInteraction.SliderNormalized:
          if ((double) this.endStepSlider.normalizedValue <= (double) this.sliderTopValueToEndStep && (double) this.endStepSlider.normalizedValue >= (double) this.sliderBottomValueToEndStep)
          {
            this.EndStep();
            break;
          }
          break;
        case UITutorialStep.EndStepInteraction.CarPartDesignChosenComponent:
          CarPartDesign carPartDesign = Game.instance.player.team.carManager.carPartDesign;
          for (int index = 0; index < carPartDesign.componentSlots.Count; ++index)
          {
            if (carPartDesign.componentSlots[index] != null)
            {
              this.EndStep();
              break;
            }
          }
          break;
      }
    }
    if (this.mMoveHighlightTransforms)
    {
      this.MoveHighlightToTransform();
      this.mMoveHighlightTransforms = false;
    }
    this.ShowTransformToForceShow(true);
  }

  public void ShowTransformToForceShow(bool inShow)
  {
    if (this.transformsToForceShow == null)
      return;
    for (int index = 0; index < this.transformsToForceShow.Length; ++index)
      GameUtility.SetActive(this.transformsToForceShow[index], inShow);
  }

  private void MoveHighlightToTransform()
  {
    if (this.mAllTransformsToHighlight.Count <= 0)
      return;
    this.mPreviousParents = new UITutorialStep.PreviousTransformAndSiblingIndex[this.mAllTransformsToHighlight.Count];
    for (int index1 = 0; index1 < this.mAllTransformsToHighlight.Count; ++index1)
    {
      if (this.mPreviousParents[index1] == null)
        this.mPreviousParents[index1] = new UITutorialStep.PreviousTransformAndSiblingIndex();
      if ((UnityEngine.Object) this.mAllTransformsToHighlight[index1].parent.GetComponent<LayoutGroup>() != (UnityEngine.Object) null)
      {
        GameObject go = new GameObject("(FootPrint step: " + this.name + " - sequence: " + this.mTutorialSequence.name + ") " + this.mAllTransformsToHighlight[index1].name);
        go.transform.SetParent(this.mAllTransformsToHighlight[index1].transform.parent, true);
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = this.mAllTransformsToHighlight[index1].localPosition;
        go.transform.SetSiblingIndex(this.mAllTransformsToHighlight[index1].GetSiblingIndex());
        LayoutElement component1 = this.mAllTransformsToHighlight[index1].GetComponent<LayoutElement>();
        if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
        {
          LayoutElement layoutElement = go.AddComponent<LayoutElement>(component1);
          RectTransform component2 = this.mAllTransformsToHighlight[index1].GetComponent<RectTransform>();
          if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
          {
            if ((double) layoutElement.minHeight == -1.0)
              layoutElement.minHeight = component2.sizeDelta.y;
            if ((double) layoutElement.minWidth == -1.0)
              layoutElement.minWidth = component2.sizeDelta.x;
          }
        }
        else
          go.AddComponent<RectTransform>();
        this.mPreviousParents[index1].footprintLeftBehind = go;
      }
      UITutorialStep previousStep = this.mTutorialSequence.GetPreviousStep();
      if ((UnityEngine.Object) previousStep != (UnityEngine.Object) null && previousStep.allTransformsToHighlight.Contains(this.mAllTransformsToHighlight[index1]))
      {
        for (int index2 = 0; index2 < previousStep.allTransformsToHighlight.Count; ++index2)
        {
          if ((UnityEngine.Object) previousStep.allTransformsToHighlight[index2] == (UnityEngine.Object) this.mAllTransformsToHighlight[index1])
          {
            this.mPreviousParents[index1].previousSiblingIndex = previousStep.previousParents[index2].previousSiblingIndex;
            this.mPreviousParents[index1].previousParent = previousStep.previousParents[index2].previousParent;
            if ((UnityEngine.Object) previousStep.previousParents[index2].footprintLeftBehind != (UnityEngine.Object) null)
            {
              UnityEngine.Object.Destroy((UnityEngine.Object) previousStep.previousParents[index2].footprintLeftBehind);
              break;
            }
            break;
          }
        }
        previousStep.allTransformsToHighlight.Remove(this.mAllTransformsToHighlight[index1]);
      }
      else
      {
        this.mPreviousParents[index1].previousSiblingIndex = this.mAllTransformsToHighlight[index1].GetSiblingIndex();
        this.mPreviousParents[index1].previousParent = this.mAllTransformsToHighlight[index1].transform.parent;
      }
      this.mAllTransformsToHighlight[index1].SetParent(this.transform, true);
      this.mAllTransformsToHighlight[index1].SetSiblingIndex(0);
    }
  }

  private void PlaceBackHighlightTransform()
  {
    this.mCoroutine = App.instance.StartCoroutine(this.PlaceBackHightlightTransform_Deferred());
  }

  [DebuggerHidden]
  private IEnumerator PlaceBackHightlightTransform_Deferred()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new UITutorialStep.\u003CPlaceBackHightlightTransform_Deferred\u003Ec__Iterator1B()
    {
      \u003C\u003Ef__this = this
    };
  }

  private void OnDestroy()
  {
    if (this.mCoroutine == null)
      return;
    App.instance.StopCoroutine(this.mCoroutine);
    this.mCoroutine = (Coroutine) null;
  }

  private void OnDeactivateButton()
  {
    ++this.mCurrentButtonClicks;
    if (this.mCurrentButtonClicks < this.buttonClicksNumber)
      return;
    this.endStepButton.onClick.RemoveListener(new UnityAction(this.OnDeactivateButton));
    scSoundManager.Instance.PlaySound(SoundID.Button_TutorialClick, 0.0f);
    this.EndStep();
  }

  private void OnDeactivateToggle(bool inValue)
  {
    if (inValue != this.toggleValueToEndStep)
      return;
    this.endStepToggle.onValueChanged.RemoveListener(new UnityAction<bool>(this.OnDeactivateToggle));
    this.EndStep();
  }

  private void OnDeactivateDropdown(int inValue)
  {
    if (inValue != this.dropdwonValueToEndStep)
      return;
    this.endStepDropdown.onValueChanged.RemoveListener(new UnityAction<int>(this.OnDeactivateDropdown));
    this.EndStep();
  }

  private void EndStep()
  {
    if (this.endStepInteraction == UITutorialStep.EndStepInteraction.CarPartDesignBuilt)
    {
      Game.instance.time.Pause(GameTimer.PauseType.Game);
      Game.instance.player.team.carManager.carPartDesign.OnPartBuilt -= new Action(this.EndStep);
    }
    this.PlaceBackHighlightTransform();
    if ((UnityEngine.Object) this.animator != (UnityEngine.Object) null)
      this.animator.SetTrigger(AnimationHashes.Close);
    else
      this.OnCloseAnimationEnd();
  }

  public void OnCloseAnimationEnd()
  {
    this.isFinished = true;
    this.gameObject.SetActive(false);
    if (this.unlockSimulationInput || this.blockShortcutsInput)
      Game.instance.tutorialSystem.BlockShortcutsInput(false);
    if (this.endStepInteraction == UITutorialStep.EndStepInteraction.RolloverEnter)
    {
      this.endStepEventTrigger.GetComponent<EventTrigger>().get_triggers().Remove(this.mEventTriggerEntry);
      this.mEventTriggerEntry = (EventTrigger.Entry) null;
    }
    if (this.onStepEnd == null)
      return;
    this.onStepEnd();
    this.onStepEnd = (Action) null;
  }

  public class PreviousTransformAndSiblingIndex
  {
    public int previousSiblingIndex = -1;
    public Transform previousParent;
    public GameObject footprintLeftBehind;
  }

  public enum EndStepInteraction
  {
    Button,
    Toggle,
    Dropdown,
    Slider,
    NoCarPartTokensAvailable,
    RolloverEnter,
    ButtonRuntime,
    SliderNormalized,
    CarPartDesignChosenComponent,
    CarPartDesignBuilt,
  }

  public enum HighlightGridItemsOrder
  {
    FromTop,
    FromBottom,
  }
}
