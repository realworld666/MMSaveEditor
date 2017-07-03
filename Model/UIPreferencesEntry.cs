// Decompiled with JetBrains decompiler
// Type: UIPreferencesEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPreferencesEntry : MonoBehaviour
{
  public Supplier.CarAspect preferenceType = Supplier.CarAspect.NoseHeight;
  private List<FrontendCarData.BlendShapeData.BlendShapes> mBlendShapes = new List<FrontendCarData.BlendShapeData.BlendShapes>();
  public Slider slider;
  public Image leftBoundary;
  public Image rightBoundary;
  public TextMeshProUGUI leftLabel;
  public TextMeshProUGUI rightLabel;
  [SerializeField]
  private GameObject hiddenState;
  private float mMinBoundary;
  private float mMaxBoundary;
  private float mMinLabelValue;
  private float mMaxLabelValue;
  private bool mMouseOver;
  private bool mCancelCameraChange;

  private void Start()
  {
    this.mBlendShapes = UIPreferencesEntry.BlendShapeFromSupplierCarAspect(this.preferenceType);
    this.slider.onValueChanged.AddListener((UnityAction<float>) (value =>
    {
      this.ClampSlider(true);
      this.UpdateStats();
      this.UpdateCarBlendShapes();
    }));
    EventTrigger eventTrigger = this.gameObject.AddComponent<EventTrigger>();
    EventTrigger.Entry entry1 = new EventTrigger.Entry();
    entry1.eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter;
    entry1.callback.AddListener((UnityAction<BaseEventData>) (eventData => this.mMouseOver = true));
    eventTrigger.get_triggers().Add(entry1);
    EventTrigger.Entry entry2 = new EventTrigger.Entry();
    entry2.eventID = UnityEngine.EventSystems.EventTriggerType.PointerExit;
    entry2.callback.AddListener((UnityAction<BaseEventData>) (eventData =>
    {
      this.mMouseOver = false;
      this.HighlightStats(false);
    }));
    eventTrigger.get_triggers().Add(entry2);
    switch (this.preferenceType)
    {
      case Supplier.CarAspect.RearPackage:
        this.mMinLabelValue = 50f;
        this.mMaxLabelValue = 70f;
        break;
      case Supplier.CarAspect.NoseHeight:
        this.mMinLabelValue = 20f;
        this.mMaxLabelValue = 30f;
        break;
    }
    this.ClampSlider(false);
  }

  private void HighlightStats(bool inValue)
  {
    CarDesignScreen screen = UIManager.instance.GetScreen<CarDesignScreen>();
    if (inValue)
    {
      this.mCancelCameraChange = false;
      List<CarChassisStats.Stats> inStats = new List<CarChassisStats.Stats>();
      switch (this.preferenceType)
      {
        case Supplier.CarAspect.RearPackage:
          inStats.Add(CarChassisStats.Stats.Improvability);
          inStats.Add(CarChassisStats.Stats.TyreHeating);
          App.instance.StartCoroutine(this.SetCamera("RearPackageCamera"));
          break;
        case Supplier.CarAspect.NoseHeight:
          inStats.Add(CarChassisStats.Stats.FuelEfficiency);
          inStats.Add(CarChassisStats.Stats.TyreWear);
          App.instance.StartCoroutine(this.SetCamera("NoseHeightCamera"));
          break;
      }
      screen.estimatedOutputWidget.HighlightBarsForStats(inStats);
    }
    else
    {
      this.mCancelCameraChange = true;
      screen.estimatedOutputWidget.ResetHighlightState();
    }
  }

  [DebuggerHidden]
  private IEnumerator SetCamera(string inString)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new UIPreferencesEntry.\u003CSetCamera\u003Ec__Iterator20()
    {
      inString = inString,
      \u003C\u0024\u003EinString = inString,
      \u003C\u003Ef__this = this
    };
  }

  public void OnEnter()
  {
    this.slider.normalizedValue = 0.5f;
    this.leftBoundary.fillAmount = 0.45f;
    this.rightBoundary.fillAmount = 0.45f;
    this.mMinBoundary = 0.45f;
    this.mMaxBoundary = 0.45f;
    this.leftLabel.text = Localisation.LocaliseEnum((Enum) this.preferenceType);
    this.ClampSlider(false);
    this.UpdateCarBlendShapes();
    switch (this.preferenceType)
    {
      default:
        this.ShowSlider(Game.instance.championshipManager.GetMainChampionship(Championship.Series.SingleSeaterSeries) == Game.instance.player.team.championship);
        break;
    }
  }

  private void LateUpdate()
  {
    if (!this.mMouseOver)
      return;
    this.HighlightStats(true);
  }

  private void ClampSlider(bool inRemoveListener = false)
  {
    if (inRemoveListener)
      this.slider.onValueChanged.RemoveAllListeners();
    if ((double) this.mMinBoundary < 1.0 - (double) this.mMaxBoundary)
    {
      this.slider.normalizedValue = Mathf.Clamp01(Mathf.Clamp(this.slider.normalizedValue, Mathf.Clamp01(this.leftBoundary.fillAmount), Mathf.Clamp01(1f - this.rightBoundary.fillAmount)));
      this.rightLabel.text = GameUtility.GetHeightText(Mathf.Round(Mathf.Lerp(this.mMinLabelValue, this.mMaxLabelValue, this.slider.normalizedValue)));
    }
    else
    {
      this.slider.normalizedValue = 0.0f;
      this.rightLabel.text = "ERROR, minimum bound bigger than maximum!";
    }
    if (!inRemoveListener)
      return;
    this.slider.onValueChanged.AddListener((UnityAction<float>) (value =>
    {
      this.ClampSlider(true);
      this.UpdateStats();
      this.UpdateCarBlendShapes();
    }));
  }

  private void UpdateStats()
  {
    CarDesignScreen screen = UIManager.instance.GetScreen<CarDesignScreen>();
    screen.preferencesWidget.UpdateSliderValues();
    screen.UpdateStats();
  }

  private void UpdateCarBlendShapes()
  {
    if (this.mBlendShapes.Count <= 0)
      return;
    for (int index = 0; index < this.mBlendShapes.Count; ++index)
    {
      if (this.mBlendShapes[index] == FrontendCarData.BlendShapeData.BlendShapes.Aero || this.mBlendShapes[index] == FrontendCarData.BlendShapeData.BlendShapes.PodWidth || this.mBlendShapes[index] == FrontendCarData.BlendShapeData.BlendShapes.Nose)
        Game.instance.player.team.carManager.nextFrontendCar.SetBlendShapeWeight(this.mBlendShapes[index], 1f - this.slider.normalizedValue);
      else
        Game.instance.player.team.carManager.nextFrontendCar.SetBlendShapeWeight(this.mBlendShapes[index], this.slider.normalizedValue);
    }
  }

  private static List<FrontendCarData.BlendShapeData.BlendShapes> BlendShapeFromSupplierCarAspect(Supplier.CarAspect preferenceType)
  {
    List<FrontendCarData.BlendShapeData.BlendShapes> blendShapesList = new List<FrontendCarData.BlendShapeData.BlendShapes>();
    switch (preferenceType)
    {
      case Supplier.CarAspect.RearPackage:
        blendShapesList.Add(FrontendCarData.BlendShapeData.BlendShapes.Engine);
        blendShapesList.Add(FrontendCarData.BlendShapeData.BlendShapes.Fin);
        blendShapesList.Add(FrontendCarData.BlendShapeData.BlendShapes.PodWidth);
        blendShapesList.Add(FrontendCarData.BlendShapeData.BlendShapes.Aero);
        break;
      case Supplier.CarAspect.NoseHeight:
        blendShapesList.Add(FrontendCarData.BlendShapeData.BlendShapes.Nose);
        break;
    }
    return blendShapesList;
  }

  public void ResetBoundaries()
  {
    this.mMinBoundary = 0.0f;
    this.mMaxBoundary = 0.0f;
  }

  public void AddToMinBoundary(float inValue)
  {
    this.mMinBoundary += inValue;
    GameUtility.SetImageFillAmountIfDifferent(this.leftBoundary, this.mMinBoundary, 1f / 512f);
    this.ClampSlider(false);
  }

  public void AddToMaxBoundary(float inValue)
  {
    this.mMaxBoundary += inValue;
    GameUtility.SetImageFillAmountIfDifferent(this.rightBoundary, this.mMaxBoundary, 1f / 512f);
    this.ClampSlider(false);
  }

  private void ShowSlider(bool inShow)
  {
    GameUtility.SetActive(this.slider.gameObject, inShow);
    GameUtility.SetActive(this.hiddenState, !inShow);
  }
}
