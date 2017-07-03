// Decompiled with JetBrains decompiler
// Type: UITeamReportScreenDriverWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITeamReportScreenDriverWidget : MonoBehaviour
{
  public float animationDuration = 1f;
  private List<UITeamReportScreenDriverWidget.UIDriverStatChangeEntry> mStatList = new List<UITeamReportScreenDriverWidget.UIDriverStatChangeEntry>();
  public CanvasGroup canvasGroup;
  public UICharacterPortrait portrait;
  public Flag flag;
  public TextMeshProUGUI driverName;
  public TextMeshProUGUI driverAge;
  public TextMeshProUGUI driverWeight;
  public TextMeshProUGUI finishingPosition;
  public UIAbilityStars abilityStars;
  public UIDriverHelmet driverHelmet;
  public GameObject newPersonalityTrait;
  public UITeamReportScreenStatEntry[] statEntries;
  public UITeamReportScreenRelationship relationshipEntry;
  public EasingUtility.Easing animationCurve;
  private Driver mDriver;
  private Mechanic mMechanic;
  private UITeamReportScreenDriverWidget.State mState;
  private float mTimer;
  private PersonalityTrait mTraitAdded;

  public void Setup(Driver inDriver)
  {
    this.mDriver = inDriver;
    this.mMechanic = this.mDriver.contract.GetTeam().GetMechanicOfDriver(this.mDriver);
    this.flag.SetNationality(this.mDriver.nationality);
    this.portrait.SetPortrait((Person) this.mDriver);
    this.driverName.text = this.mDriver.name;
    this.driverAge.text = this.mDriver.GetAge().ToString();
    GameUtility.SetActive(this.driverWeight.gameObject, this.mDriver.hasWeigthSet);
    this.driverWeight.text = GameUtility.GetWeightText((float) this.mDriver.weight, 2);
    this.driverHelmet.SetHelmet(this.mDriver);
    this.abilityStars.SetAbilityStarsData(this.mDriver);
    if ((UnityEngine.Object) this.finishingPosition != (UnityEngine.Object) null)
    {
      ChampionshipEntry_v1 championshipEntry = this.mDriver.GetChampionshipEntry();
      this.finishingPosition.text = GameUtility.FormatForPosition(championshipEntry.GetPositionForEvent(!championshipEntry.championship.HasSeasonEnded() ? championshipEntry.championship.eventNumber - 1 : championshipEntry.championship.eventNumber), (string) null);
    }
    int count = this.mDriver.personalityTraitController.temporaryPersonalityTraits.Count;
    DateTime dateTime = Game.instance.time.now.Date.AddDays(-1.0);
    for (int index = 0; index < count; ++index)
    {
      PersonalityTrait personalityTrait = this.mDriver.personalityTraitController.temporaryPersonalityTraits[index];
      if (personalityTrait != null && personalityTrait.traitStartDate >= dateTime && personalityTrait.traitStartDate != DateTime.MaxValue)
      {
        this.mTraitAdded = personalityTrait;
        break;
      }
    }
    GameUtility.SetActive(this.newPersonalityTrait, this.mTraitAdded != null);
    this.SetupStats();
    this.canvasGroup.alpha = 0.0f;
    this.canvasGroup.interactable = false;
    this.canvasGroup.blocksRaycasts = false;
    this.mState = UITeamReportScreenDriverWidget.State.Idle;
  }

  private void SetupStats()
  {
    float num1 = this.mDriver.moraleBeforeEvent * 100f;
    float num2 = this.mDriver.GetMorale() * 100f;
    float num3 = this.mMechanic == null ? 0.0f : this.mMechanic.driverRelationshipAmountBeforeEvent;
    float num4 = this.mMechanic == null ? 0.0f : this.mMechanic.CalculateCurrentRelationshipWithDriverForUI().relationshipAmount;
    DriverStats driverStats = this.mDriver.GetDriverStats();
    DriverStats statsBeforeEvent = this.mDriver.statsBeforeEvent;
    this.mStatList.Clear();
    this.mStatList.Add(new UITeamReportScreenDriverWidget.UIDriverStatChangeEntry()
    {
      oldValue = num1,
      newValue = num2,
      statMax = 100f
    });
    this.mStatList.Add(new UITeamReportScreenDriverWidget.UIDriverStatChangeEntry()
    {
      oldValue = statsBeforeEvent.braking,
      newValue = driverStats.braking,
      statMax = 20f
    });
    this.mStatList.Add(new UITeamReportScreenDriverWidget.UIDriverStatChangeEntry()
    {
      oldValue = statsBeforeEvent.cornering,
      newValue = driverStats.cornering,
      statMax = 20f
    });
    this.mStatList.Add(new UITeamReportScreenDriverWidget.UIDriverStatChangeEntry()
    {
      oldValue = statsBeforeEvent.smoothness,
      newValue = driverStats.smoothness,
      statMax = 20f
    });
    this.mStatList.Add(new UITeamReportScreenDriverWidget.UIDriverStatChangeEntry()
    {
      oldValue = statsBeforeEvent.overtaking,
      newValue = driverStats.overtaking,
      statMax = 20f
    });
    this.mStatList.Add(new UITeamReportScreenDriverWidget.UIDriverStatChangeEntry()
    {
      oldValue = statsBeforeEvent.consistency,
      newValue = driverStats.consistency,
      statMax = 20f
    });
    this.mStatList.Add(new UITeamReportScreenDriverWidget.UIDriverStatChangeEntry()
    {
      oldValue = statsBeforeEvent.adaptability,
      newValue = driverStats.adaptability,
      statMax = 20f
    });
    this.mStatList.Add(new UITeamReportScreenDriverWidget.UIDriverStatChangeEntry()
    {
      oldValue = statsBeforeEvent.fitness,
      newValue = driverStats.fitness,
      statMax = 20f
    });
    this.mStatList.Add(new UITeamReportScreenDriverWidget.UIDriverStatChangeEntry()
    {
      oldValue = statsBeforeEvent.feedback,
      newValue = driverStats.feedback,
      statMax = 20f
    });
    this.mStatList.Add(new UITeamReportScreenDriverWidget.UIDriverStatChangeEntry()
    {
      oldValue = statsBeforeEvent.focus,
      newValue = driverStats.focus,
      statMax = 20f
    });
    this.mStatList.Add(new UITeamReportScreenDriverWidget.UIDriverStatChangeEntry()
    {
      oldValue = num3,
      newValue = num4,
      statMax = 100f
    });
    int count = this.mStatList.Count;
    for (int index = 0; index < count; ++index)
      this.mStatList[index].Calculate();
    for (int index = 0; index < this.statEntries.Length; ++index)
      this.statEntries[index].Setup(this.mStatList[index]);
    if (!((UnityEngine.Object) this.relationshipEntry != (UnityEngine.Object) null) || this.mMechanic == null)
      return;
    this.relationshipEntry.Setup(this.mStatList[this.mStatList.Count - 1], this.mMechanic);
  }

  public void StartAnimating()
  {
    this.mState = UITeamReportScreenDriverWidget.State.Animating;
    this.mTimer = 0.0f;
  }

  public void AnimateStats()
  {
    for (int index = 0; index < this.statEntries.Length; ++index)
      this.statEntries[index].Animate();
    if (!((UnityEngine.Object) this.relationshipEntry != (UnityEngine.Object) null) || this.mMechanic == null)
      return;
    this.relationshipEntry.Animate();
  }

  private void Update()
  {
    if (this.mState != UITeamReportScreenDriverWidget.State.Animating)
      return;
    this.mTimer += GameTimer.deltaTime;
    this.canvasGroup.alpha = Mathf.Clamp01(this.mTimer / this.animationDuration);
    if ((double) this.mTimer < (double) this.animationDuration)
      return;
    this.AnimateStats();
    this.canvasGroup.interactable = true;
    this.canvasGroup.blocksRaycasts = true;
    this.mState = UITeamReportScreenDriverWidget.State.Idle;
  }

  public void OpenTraitRollover()
  {
    if (this.mTraitAdded == null)
      return;
    UIManager.instance.dialogBoxManager.GetDialog<DriverTraitsRollover>().ShowRollover(this.mTraitAdded);
  }

  public void CloseTraitRollover()
  {
    if (this.mTraitAdded == null)
      return;
    UIManager.instance.dialogBoxManager.GetDialog<DriverTraitsRollover>().Hide();
  }

  public class UIDriverStatChangeEntry
  {
    public float oldValue;
    public float newValue;
    public float barOldValueNormalized;
    public float barNewValueNormalized;
    public float valueChange;
    public float statMax;

    public void Calculate()
    {
      this.barOldValueNormalized = Mathf.Clamp01(this.oldValue - (float) (int) this.oldValue);
      this.barNewValueNormalized = Mathf.Clamp01(this.newValue - (float) (int) this.newValue);
      this.valueChange = this.newValue - this.oldValue;
    }
  }

  public enum State
  {
    Idle,
    Animating,
  }
}
