// Decompiled with JetBrains decompiler
// Type: ERSBatteryPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ERSBatteryPanel : MonoBehaviour
{
  public Toggle[] ERSModeButtons = new Toggle[0];
  public Image[] ERSModeCooldownImages = new Image[0];
  public CanvasGroup[] ERSModeCooldownCanvasGroups = new CanvasGroup[0];
  public float disabledCanvasGroupAlpha = 0.5f;
  public GameObject[] modeIndicators = new GameObject[0];
  public float bigBatterySize = 242f;
  private ERSController.Mode mPreviousMode = ERSController.Mode.Count;
  public CanvasGroup manualToggleContainer;
  public Toggle autoERSToggle;
  private bool mAutoMode;
  public Animator panelAnimator;
  public TextMeshProUGUI titleLabel;
  public TextMeshProUGUI sizeLabel;
  public float smallBatterySize;
  public LayoutElement batterySizeLayout;
  public Image batteryFill;
  public GameObject brokenContainer;
  private RacingVehicle mVehicle;

  private void Awake()
  {
    for (ERSController.Mode mode = ERSController.Mode.Harvest; mode < ERSController.Mode.Count; ++mode)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: reference to a compiler-generated method
      this.ERSModeButtons[(int) mode].onValueChanged.AddListener(new UnityAction<bool>(new ERSBatteryPanel.\u003CAwake\u003Ec__AnonStoreyA8()
      {
        \u003C\u003Ef__this = this,
        mode = mode
      }.\u003C\u003Em__216));
    }
    this.autoERSToggle.isOn = App.instance.preferencesManager.GetSettingBool(Preference.pName.Game_AutoERS, false);
    this.autoERSToggle.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnAutoERSChanged(value)));
    if (this.mVehicle == null)
      return;
    this.OnAutoERSChanged(this.autoERSToggle.isOn);
  }

  public void Setup(RacingVehicle inVehicle)
  {
    this.mVehicle = inVehicle;
    this.OnAutoERSChanged(this.autoERSToggle.isOn);
    this.UpdateData();
  }

  private void OnEnable()
  {
    if (this.mVehicle == null)
      return;
    this.mPreviousMode = ERSController.Mode.Count;
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.sizeLabel.text = "Size:" + this.mVehicle.car.carManager.team.championship.rules.batterySize.ToString();
    this.batterySizeLayout.minWidth = this.mVehicle.car.carManager.team.championship.rules.batterySize != ChampionshipRules.EnergySystemBattery.Large ? this.smallBatterySize : this.bigBatterySize;
    this.UpdatePipsFill(false);
    this.Update();
  }

  private void Update()
  {
    if (this.mVehicle.ERSController.state == ERSController.ERSState.NotVisible)
    {
      GameUtility.SetActive(this.gameObject, false);
    }
    else
    {
      GameUtility.SetActive(this.gameObject, true);
      GameUtility.SetActive(this.brokenContainer, this.mVehicle.ERSController.state == ERSController.ERSState.Broken);
      if (this.brokenContainer.activeSelf)
        return;
      if (this.mVehicle.behaviourManager.isOutOfRace)
      {
        this.SetTogglesInteractable(false);
        this.autoERSToggle.interactable = false;
      }
      else
      {
        for (ERSController.Mode inMode = ERSController.Mode.Harvest; inMode < ERSController.Mode.Count; ++inMode)
        {
          int index = (int) inMode;
          GameUtility.SetImageFillAmountIfDifferent(this.ERSModeCooldownImages[index], 1f - this.mVehicle.ERSController.GetNormalizedCooldown(inMode), 1f / 512f);
          if (this.mVehicle.ERSController.IsModeChangeCooldownExpired(inMode))
          {
            bool flag = this.mVehicle.ERSController.mode != inMode;
            if (inMode == ERSController.Mode.Power)
            {
              flag &= this.mVehicle.ERSController.CanChangeToSpecificMode(inMode);
              if (this.mVehicle.timer.lap <= 0)
              {
                if (this.mVehicle.timer.hasCrossedStartLine)
                  GameUtility.SetImageFillAmountIfDifferent(this.ERSModeCooldownImages[index], this.mVehicle.pathController.GetDistanceAlongPath01(PathController.PathType.Track), 1f / 512f);
                else
                  GameUtility.SetImageFillAmountIfDifferent(this.ERSModeCooldownImages[index], 0.0f, 1f / 512f);
              }
            }
            this.ERSModeButtons[index].interactable = flag;
          }
          else
            this.ERSModeButtons[index].interactable = false;
          this.ERSModeCooldownCanvasGroups[index].alpha = !this.ERSModeButtons[index].interactable ? this.disabledCanvasGroupAlpha : 1f;
          bool inIsActive = this.mVehicle.ERSController.mode == inMode && !this.mVehicle.HasStopped();
          GameUtility.SetActiveAndCheckNull(this.modeIndicators[index], inIsActive);
          this.ERSModeButtons[index].isOn = this.mVehicle.ERSController.mode == inMode;
          if (inMode == ERSController.Mode.Hybrid)
          {
            ChampionshipRules rules = this.mVehicle.driver.contract.GetTeam().championship.rules;
            GameUtility.SetActive(this.ERSModeButtons[index].gameObject, rules.isHybridModeActive);
          }
          if (this.mPreviousMode != this.mVehicle.ERSController.mode && inIsActive)
            this.panelAnimator.SetTrigger(this.mVehicle.ERSController.mode.ToString());
        }
        this.UpdateData();
        this.mPreviousMode = this.mVehicle.ERSController.mode;
      }
    }
  }

  private void UpdateData()
  {
    this.titleLabel.text = Mathf.RoundToInt(this.mVehicle.ERSController.normalizedCharge * 100f).ToString() + "%";
    this.UpdatePipsFill(true);
  }

  private void OnModeSelected(bool inValue, ERSController.Mode inMode)
  {
    if (!inValue || this.mAutoMode)
      return;
    if (this.mVehicle.ERSController.mode != inMode)
    {
      this.panelAnimator.SetTrigger(inMode.ToString());
      scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    }
    this.mVehicle.ERSController.SetMode(inMode);
  }

  private void OnAutoERSChanged(bool inValue)
  {
    this.mAutoMode = inValue;
    this.mVehicle.ERSController.autoControlERS = this.mAutoMode;
    if (Game.instance.sessionManager.isSessionActive)
      scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.SetTogglesInteractable(!this.mAutoMode);
  }

  private void SetTogglesInteractable(bool inSetActive)
  {
    for (int index = 0; index < this.ERSModeButtons.Length; ++index)
      this.ERSModeButtons[index].enabled = inSetActive;
    this.manualToggleContainer.alpha = !inSetActive ? 0.2f : 1f;
  }

  private void UpdatePipsFill(bool inAnimate)
  {
    float fillAmount = this.mVehicle.ERSController.normalizedCharge;
    if (inAnimate)
      fillAmount = Mathf.MoveTowards(this.batteryFill.fillAmount, this.mVehicle.ERSController.normalizedCharge, Time.deltaTime);
    GameUtility.SetImageFillAmountIfDifferent(this.batteryFill, fillAmount, 1f / 512f);
  }

  private void OnDisable()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Back, 0.0f);
  }
}
