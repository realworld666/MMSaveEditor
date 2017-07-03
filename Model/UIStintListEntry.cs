// Decompiled with JetBrains decompiler
// Type: UIStintListEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIStintListEntry : MonoBehaviour
{
  public Button button;
  public TextMeshProUGUI lapsLabel;
  public TextMeshProUGUI averageTimeLabel;
  public UICarSetupTyreIcon tyreIcon;
  public TextMeshProUGUI overallBalanceLabel;
  public Button useSetupButton;
  public GameObject stintObject;
  public TextMeshProUGUI headerText;
  public GameObject headerObject;
  private RacingVehicle mVehicle;
  private SetupStintData mSetupStintData;

  private void Awake()
  {
    if ((Object) this.button == (Object) null)
      return;
    this.button.onClick.AddListener(new UnityAction(this.OnUseSetupButton));
    EventTrigger eventTrigger = this.button.gameObject.AddComponent<EventTrigger>();
    EventTrigger.Entry entry1 = new EventTrigger.Entry();
    entry1.eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter;
    entry1.callback.AddListener((UnityAction<BaseEventData>) (eventData => this.OnMouseRolloverEnter()));
    eventTrigger.get_triggers().Add(entry1);
    EventTrigger.Entry entry2 = new EventTrigger.Entry();
    entry2.eventID = UnityEngine.EventSystems.EventTriggerType.PointerExit;
    entry2.callback.AddListener((UnityAction<BaseEventData>) (eventData => this.OnMouseRolloverExit()));
    eventTrigger.get_triggers().Add(entry2);
  }

  public void SetSetupStintData(RacingVehicle inVehicle, SetupStintData inStintData)
  {
    if ((Object) this.headerObject != (Object) null)
      GameUtility.SetActive(this.headerObject, false);
    if ((Object) this.stintObject != (Object) null)
      GameUtility.SetActive(this.stintObject, true);
    this.mVehicle = inVehicle;
    this.mSetupStintData = inStintData;
    this.lapsLabel.text = this.mSetupStintData.lapCount.ToString();
    this.averageTimeLabel.text = GameUtility.GetLapTimeText(this.mSetupStintData.averageLapTime, false);
    this.tyreIcon.SetTyre(this.mSetupStintData.tyreCompound, 1f);
    bool inIsActive = App.instance.gameStateManager.currentState is SessionState && Game.instance.sessionManager.sessionType != SessionDetails.SessionType.Race;
    if (inIsActive)
      GameUtility.SetActive(this.button.gameObject, inIsActive);
    this.overallBalanceLabel.text = GameUtility.GetPercentageText(inStintData.GetOverallSetupPercentage(), 0.0f, false, false);
  }

  public void SetSetupStintData(SessionDetails.SessionType inSessionType)
  {
    GameUtility.SetActive(this.headerObject, true);
    GameUtility.SetActive(this.stintObject, false);
    if (!((Object) this.headerText != (Object) null))
      return;
    switch (inSessionType)
    {
      case SessionDetails.SessionType.Practice:
        this.headerText.text = Localisation.LocaliseID("PSG_10006938", (GameObject) null);
        break;
      case SessionDetails.SessionType.Qualifying:
        this.headerText.text = Localisation.LocaliseID("PSG_10006675", (GameObject) null);
        break;
      case SessionDetails.SessionType.Race:
        this.headerText.text = Localisation.LocaliseID("PSG_10002251", (GameObject) null);
        break;
    }
  }

  public void OnUseSetupButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mVehicle.setup.SetTargetSetupInput(this.mSetupStintData.setupInput);
    PitScreen screen = UIManager.instance.GetScreen<PitScreen>();
    screen.setupBalanceWidget.SetFeedbackFromStint(this.mSetupStintData);
    screen.optionsSelectionWidget.setupInputWidget.SetToSetupInput(this.mSetupStintData.setupInput);
    UIManager.instance.dialogBoxManager.GetDialog<SetupInfoRollover>().Show(this.mVehicle, screen.setupBalanceWidget.targetSetupOutput, this.mSetupStintData);
  }

  public void OnMouseRolloverEnter()
  {
    PitScreen screen = UIManager.instance.GetScreen<PitScreen>();
    if (!((Object) screen != (Object) null) || !screen.hasFocus)
      return;
    screen.setupBalanceWidget.PreviewSetupFromRollover(true, this.mSetupStintData.setupOutput);
    UIManager.instance.dialogBoxManager.GetDialog<SetupInfoRollover>().Show(this.mVehicle, screen.setupBalanceWidget.targetSetupOutput, this.mSetupStintData);
  }

  public void OnMouseRolloverExit()
  {
    PitScreen screen = UIManager.instance.GetScreen<PitScreen>();
    if (!((Object) screen != (Object) null) || !screen.hasFocus)
      return;
    screen.setupBalanceWidget.PreviewSetupFromRollover(false, (SessionSetup.SetupOutput) null);
    UIManager.instance.dialogBoxManager.GetDialog<SetupInfoRollover>().Hide();
  }
}
