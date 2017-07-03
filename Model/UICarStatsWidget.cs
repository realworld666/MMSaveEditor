// Decompiled with JetBrains decompiler
// Type: UICarStatsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICarStatsWidget : MonoBehaviour
{
  public UISimulationStatEntry[] stats = new UISimulationStatEntry[0];
  public UISimulationStatEntry[] horizontalStatsLeft = new UISimulationStatEntry[0];
  public UISimulationStatEntry[] horizontalStatsRight = new UISimulationStatEntry[0];
  public TextMeshProUGUI[] relevantForCircuit = new TextMeshProUGUI[0];
  public TextMeshProUGUI[] relevantForCircuitHorizontal = new TextMeshProUGUI[0];
  private List<UISimulationStatEntry> allStatBars = new List<UISimulationStatEntry>();
  private CarStats.StatType mStat = CarStats.StatType.Count;
  public TextMeshProUGUI car1Name;
  public TextMeshProUGUI car2Name;
  public GameObject horizontalBarsHolder;
  public GameObject verticalBarsHolder;
  public Dropdown circuitDropdown;
  public Dropdown circuitDropdownHorizontal;
  public Toggle showAverageToggle;
  public Toggle showBarsVerticly;
  public RectTransform comparisonBox;
  public TextMeshProUGUI playersCarStatOnGrid;
  public TextMeshProUGUI statName;
  public UITeamLogo bestOnGridLogo;
  public UITeamLogo worstOnGridLogo;
  public UICarStatIcon statIcon;
  private Circuit mSelectedCircuit;

  private void Start()
  {
    this.allStatBars.AddRange((IEnumerable<UISimulationStatEntry>) this.stats);
    this.allStatBars.AddRange((IEnumerable<UISimulationStatEntry>) this.horizontalStatsLeft);
    this.allStatBars.AddRange((IEnumerable<UISimulationStatEntry>) this.horizontalStatsRight);
    this.circuitDropdownHorizontal.onValueChanged.AddListener((UnityAction<int>) (value => this.OnDropdownValueChanged(value)));
    this.circuitDropdown.onValueChanged.AddListener((UnityAction<int>) (value => this.OnDropdownValueChanged(value)));
    this.showAverageToggle.onValueChanged.AddListener((UnityAction<bool>) (value => this.HideAverageLine(value)));
    this.showBarsVerticly.onValueChanged.AddListener((UnityAction<bool>) (value => this.ShowVerticalBars(value)));
  }

  private void ShowVerticalBars(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.horizontalBarsHolder.SetActive(!inValue);
    this.verticalBarsHolder.SetActive(inValue);
  }

  private void HideAverageLine(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    for (int index = 0; index < this.allStatBars.Count; ++index)
      this.allStatBars[index].HideAverageLine(inValue);
  }

  public void ShowStatComparisonCar1(int inStat)
  {
    this.ShowStatComparison((CarStats.StatType) inStat, 0);
  }

  public void ShowStatComparisonCar2(int inStat)
  {
    this.ShowStatComparison((CarStats.StatType) inStat, 1);
  }

  public void ShowStatComparison(CarStats.StatType inStat, int inCar)
  {
    Team team = Game.instance.player.team;
    this.mStat = inStat;
    this.statIcon.SetIcon(this.mStat, team.championship.series);
    this.statName.text = Localisation.LocaliseEnum((Enum) this.mStat);
    Car car = team.carManager.GetCar(inCar);
    List<Car> carStandingsOnStat = CarManager.GetCarStandingsOnStat(this.mStat, car.carManager.team.championship);
    int num = carStandingsOnStat.IndexOf(car);
    if (num != 0)
      this.playersCarStatOnGrid.text = team.GetDriver(inCar).lastName + "'s car: <color=white>" + GameUtility.FormatForPosition(num + 1, (string) null) + "</color> on grid";
    else
      this.playersCarStatOnGrid.text = team.GetDriver(inCar).lastName + "'s car: " + GameUtility.ColorToRichTextHex(UIConstants.sectorSessionFastestColor) + GameUtility.FormatForPosition(num + 1, (string) null) + "</color> on grid";
    this.bestOnGridLogo.SetTeam(carStandingsOnStat[0].carManager.team);
    this.worstOnGridLogo.SetTeam(carStandingsOnStat[carStandingsOnStat.Count - 1].carManager.team);
    this.comparisonBox.gameObject.SetActive(true);
  }

  public void HideStatComparison()
  {
    this.comparisonBox.gameObject.SetActive(false);
  }

  private void OnDropdownValueChanged(int inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    Championship championship = Game.instance.player.team.championship;
    this.mSelectedCircuit = championship.calendar[inValue + championship.eventNumber].circuit;
    this.UpdateCircuitOptions();
  }

  public void HighlightForStats(CarPart.PartType inType)
  {
    List<CarStats.StatType> statTypeList = new List<CarStats.StatType>();
    float num = 1f;
    if (inType != CarPart.PartType.None)
    {
      statTypeList.Add(CarPart.GetStatForPartType(inType));
      num = 0.3f;
    }
    for (int index = 0; index < this.allStatBars.Count; ++index)
      this.allStatBars[index].canvasGroup.alpha = !statTypeList.Contains(this.allStatBars[index].stat) ? num : 1f;
  }

  private void Update()
  {
    if (!this.comparisonBox.gameObject.activeSelf)
      return;
    Vector2 mousePosition = (Vector2) Input.mousePosition;
    mousePosition.x += this.comparisonBox.sizeDelta.x / 2f;
    mousePosition.y -= this.comparisonBox.sizeDelta.y / 2f;
    this.comparisonBox.transform.position = (Vector3) mousePosition;
  }

  public void OnEnter()
  {
    this.circuitDropdown.ClearOptions();
    this.circuitDropdownHorizontal.ClearOptions();
    Championship championship = Game.instance.player.team.championship;
    for (int eventNumber = championship.eventNumber; eventNumber < championship.calendar.Count; ++eventNumber)
    {
      string text = Localisation.LocaliseID(championship.calendar[eventNumber].circuit.locationNameID, (GameObject) null);
      this.circuitDropdown.get_options().Add(new Dropdown.OptionData(text));
      this.circuitDropdownHorizontal.get_options().Add(new Dropdown.OptionData(text));
      if (eventNumber == championship.eventNumber)
      {
        this.mSelectedCircuit = championship.calendar[eventNumber].circuit;
        this.circuitDropdown.captionText.text = text;
        this.circuitDropdownHorizontal.captionText.text = text;
      }
    }
    this.SetupBars();
    this.UpdateCircuitOptions();
    this.HideStatComparison();
    this.ShowVerticalBars(this.showBarsVerticly.isOn);
    this.HideAverageLine(this.showAverageToggle.isOn);
    this.car1Name.text = Game.instance.player.team.GetDriver(0).lastName + "'s Car";
    this.car2Name.text = Game.instance.player.team.GetDriver(1).lastName + "'s Car";
  }

  public void SetupBars()
  {
    for (int index = 0; index < this.stats.Length; ++index)
    {
      this.stats[index].Setup((CarStats.StatType) index);
      this.horizontalStatsLeft[index].Setup((CarStats.StatType) index);
      this.horizontalStatsRight[index].Setup((CarStats.StatType) index);
    }
  }

  private void UpdateCircuitOptions()
  {
    this.SetCircuitOptions(this.mSelectedCircuit);
  }

  public void SetCircuitOptions(Circuit inCircuit)
  {
    for (int index = 0; index < this.relevantForCircuit.Length; ++index)
    {
      if (inCircuit == null)
      {
        this.relevantForCircuitHorizontal[index].text = "-";
        this.relevantForCircuitHorizontal[index].color = CarStats.GetColorForCircuitRelevancy(CarStats.RelevantToCircuit.No);
        this.relevantForCircuit[index].text = "-";
        this.relevantForCircuit[index].color = CarStats.GetColorForCircuitRelevancy(CarStats.RelevantToCircuit.No);
      }
      else
      {
        CarStats.RelevantToCircuit relevancy = CarStats.GetRelevancy(Mathf.RoundToInt(inCircuit.trackStatsCharacteristics.GetStat(this.stats[index].stat)));
        this.relevantForCircuitHorizontal[index].text = Localisation.LocaliseEnum((Enum) relevancy);
        this.relevantForCircuitHorizontal[index].color = CarStats.GetColorForCircuitRelevancy(relevancy);
        this.relevantForCircuit[index].text = Localisation.LocaliseEnum((Enum) relevancy);
        this.relevantForCircuit[index].color = CarStats.GetColorForCircuitRelevancy(relevancy);
      }
    }
  }
}
