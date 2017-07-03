// Decompiled with JetBrains decompiler
// Type: SessionHeaderWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SessionHeaderWidget : MonoBehaviour
{
  public Color selectedColor = Color.white;
  public Color inactiveColor = Color.white;
  public Color pauseColor = Color.white;
  private Color mTimeBarStartingColor = Color.white;
  private int mLapNumber = -1;
  private float mTime = -1f;
  public GameObject buttons;
  public Button pauseButton;
  public Button playButton;
  public TextMeshProUGUI sessionTypeLabel;
  public TextMeshProUGUI grandPrixLabel;
  public Image playIcon;
  public Image pauseIcon;
  public Image timeBar;
  public Image[] speedIcons;
  public GameObject pauseEffect;
  public TextMeshProUGUI lapLabel;
  public Animator animationController;
  public TextMeshProUGUI sessionStatusLabel;
  public TextMeshProUGUI flagLabel;
  public Image flag;
  public Flag circuitFlag;

  private void Awake()
  {
  }

  private void Start()
  {
    this.mTimeBarStartingColor = this.timeBar.color;
    this.pauseButton.onClick.AddListener(new UnityAction(this.OnPauseClick));
    this.playButton.onClick.AddListener(new UnityAction(this.OnPlayClick));
  }

  private void OnEnable()
  {
    if (!Game.IsActive())
      return;
    Game.instance.sessionManager.FlagChanged += new Action(this.UpdateFlag);
    this.circuitFlag.SetNationality(Game.instance.sessionManager.eventDetails.circuit.nationality);
    switch (Game.instance.sessionManager.eventDetails.currentSession.sessionType)
    {
      case SessionDetails.SessionType.Practice:
        this.sessionTypeLabel.text = Localisation.LocaliseID("PSG_10002224", (GameObject) null);
        this.sessionStatusLabel.text = Localisation.LocaliseID("PSG_10010854", (GameObject) null);
        break;
      case SessionDetails.SessionType.Qualifying:
        this.sessionTypeLabel.text = Localisation.LocaliseID("PSG_10002225", (GameObject) null);
        this.sessionStatusLabel.text = Localisation.LocaliseID("PSG_10010853", (GameObject) null);
        break;
      case SessionDetails.SessionType.Race:
        this.sessionTypeLabel.text = Localisation.LocaliseID("PSG_10010432", (GameObject) null);
        this.sessionStatusLabel.text = Localisation.LocaliseID("PSG_10008600", (GameObject) null);
        break;
    }
    this.grandPrixLabel.text = Localisation.LocaliseID(Game.instance.sessionManager.eventDetails.circuit.locationNameID, (GameObject) null);
    this.SetAnimationTriggers();
    this.UpdateFlag();
  }

  private void OnDisable()
  {
    if (!Game.IsActive())
      return;
    Game.instance.sessionManager.FlagChanged -= new Action(this.UpdateFlag);
    this.UnSetAnimationTriggers();
  }

  private void SetAnimationTriggers()
  {
    if (Game.instance == null)
      return;
    if (Game.instance.time.isPaused)
      this.OnPause();
    else
      this.OnUnPause();
    Game.instance.time.OnPause += new Action(this.OnPause);
    Game.instance.time.OnPlay += new Action(this.OnUnPause);
  }

  private void UnSetAnimationTriggers()
  {
    if (!Game.IsActive())
      return;
    Game.instance.time.OnPause -= new Action(this.OnPause);
    Game.instance.time.OnPlay -= new Action(this.OnUnPause);
  }

  private void OnPause()
  {
    if (!((UnityEngine.Object) this.animationController != (UnityEngine.Object) null) || !this.animationController.enabled)
      return;
    this.animationController.SetTrigger(AnimationHashes.Pause);
  }

  private void OnUnPause()
  {
    if (!((UnityEngine.Object) this.animationController != (UnityEngine.Object) null) || !this.animationController.enabled)
      return;
    this.animationController.SetTrigger(AnimationHashes.Play);
  }

  private void Update()
  {
    this.UpdateHeader();
    this.UpdateTimeIcons();
  }

  private void UpdateTimeIcons()
  {
    if (Game.instance.time.isPaused)
    {
      this.pauseEffect.SetActive(true);
      this.pauseIcon.gameObject.SetActive(true);
      this.playIcon.gameObject.SetActive(false);
      this.timeBar.color = this.pauseColor;
      for (int index = 0; index < this.speedIcons.Length; ++index)
        this.speedIcons[index].color = this.inactiveColor;
    }
    else
    {
      this.pauseEffect.SetActive(false);
      this.pauseIcon.gameObject.SetActive(false);
      this.playIcon.gameObject.SetActive(true);
      this.timeBar.color = this.mTimeBarStartingColor;
      GameUtility.SetImageFillAmountIfDifferent(this.timeBar, Game.instance.sessionManager.GetNormalizedSessionTime(), 1f / 512f);
      for (int index = 0; index < this.speedIcons.Length; ++index)
      {
        if ((GameTimer.Speed) index <= Game.instance.time.speed)
          this.speedIcons[index].color = this.selectedColor;
        else
          this.speedIcons[index].color = this.inactiveColor;
      }
    }
  }

  private void UpdateHeader()
  {
    SessionHeaderWidget.GetRaceTypeString(ref this.lapLabel, ref this.mLapNumber, ref this.mTime);
  }

  public static void GetRaceTypeString(ref TextMeshProUGUI label, ref int inLapNumber, ref float inTime)
  {
    if (Game.instance.sessionManager.endCondition == SessionManager.EndCondition.LapCount)
    {
      if (Game.instance.sessionManager.lap == inLapNumber)
        return;
      inLapNumber = Game.instance.sessionManager.lap;
      label.text = Game.instance.sessionManager.GetCurrentLapAndCounter();
    }
    else
    {
      if (Game.instance.sessionManager.endCondition != SessionManager.EndCondition.Time)
        return;
      float inTime1 = Mathf.Round(Game.instance.sessionManager.time);
      if ((double) inTime == (double) inTime1)
        return;
      inTime = inTime1;
      label.text = GameUtility.GetSessionTimeText(inTime1);
    }
  }

  private void UpdateFlag()
  {
    Sprite sprite = (Sprite) null;
    this.flagLabel.text = Localisation.LocaliseEnum((Enum) Game.instance.sessionManager.flag);
    switch (Game.instance.sessionManager.flag)
    {
      case SessionManager.Flag.Green:
        sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Simulation1, "RaceHUD-Standings-lapCountFlagGreen");
        this.flagLabel.color = UIConstants.greenFlagColor;
        break;
      case SessionManager.Flag.Yellow:
        sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Simulation1, "RaceHUD-Standings-lapCountFlagYellow");
        this.flagLabel.color = UIConstants.yellowFlagColor;
        break;
      case SessionManager.Flag.Chequered:
        sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Simulation1, "RaceHUD-Standings-lapCountFlagCheck");
        this.flagLabel.color = Color.white;
        break;
      case SessionManager.Flag.SafetyCar:
        sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Simulation1, "RaceHUD-Standings-lapCountFlagYellow");
        this.flagLabel.color = UIConstants.yellowFlagColor;
        break;
      case SessionManager.Flag.VirtualSafetyCar:
        sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Simulation1, "RaceHUD-Standings-lapCountFlagYellow");
        this.flagLabel.color = UIConstants.yellowFlagColor;
        break;
    }
    this.flag.sprite = sprite;
  }

  public void OnPauseClick()
  {
    if (Game.instance.time.isPaused)
    {
      Game.instance.time.UnPause(GameTimer.PauseType.Game);
      scSoundManager.Instance.UnPause(false);
    }
    else
    {
      Game.instance.time.Pause(GameTimer.PauseType.Game);
      scSoundManager.Instance.Pause(true, false, false);
    }
  }

  public void OnPlayClick()
  {
    if (Game.instance.time.isPaused)
    {
      Game.instance.time.UnPause(GameTimer.PauseType.Game);
      scSoundManager.Instance.UnPause(false);
    }
    else if (Game.instance.time.speed != GameTimer.Speed.Fast)
    {
      int num = (int) (Game.instance.time.speed + 1);
      Game.instance.time.SetSpeed((GameTimer.Speed) num);
    }
    else
      Game.instance.time.SetSpeed(GameTimer.Speed.Slow);
  }
}
