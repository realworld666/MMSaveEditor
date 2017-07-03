// Decompiled with JetBrains decompiler
// Type: UIpartDevImprovementWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIpartDevImprovementWidget : MonoBehaviour
{
  public CarPartStats.CarPartStat stat = CarPartStats.CarPartStat.Count;
  public TextMeshProUGUI staffWorkRate;
  public TextMeshProUGUI mechanicRate;
  public UICharacterPortrait portrait;
  public Flag flag;
  public TextMeshProUGUI mechanicName;
  public TextMeshProUGUI mechanicJobTitle;
  public TextMeshProUGUI chiefMechanicImpactLabel;
  public Button swapMechanic;
  public UIPartDevMechanicAllocationWidget allocationWidget;
  public UIAbilityStars stars;
  private PartImprovement mPartImprovement;

  private void Start()
  {
    this.swapMechanic.onClick.AddListener(new UnityAction(this.SwapMechanics));
    this.allocationWidget.mechanicSlider.onValueChanged.AddListener((UnityAction<float>) (value => this.RefreshLabels()));
  }

  private void SwapMechanics()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mPartImprovement.SwapChiefMechanics();
    UIManager.instance.GetScreen<FactoryPartDevelopmentScreen>().partImprovementWidgets[0].Setup();
    UIManager.instance.GetScreen<FactoryPartDevelopmentScreen>().partImprovementWidgets[1].Setup();
  }

  public void Setup()
  {
    this.mPartImprovement = Game.instance.player.team.carManager.partImprovement;
    Person chiefMechanic = this.mPartImprovement.GetChiefMechanic(this.stat);
    this.stars.SetAbilityStarsData(chiefMechanic);
    this.portrait.SetPortrait(chiefMechanic);
    this.flag.SetNationality(chiefMechanic.nationality);
    this.mechanicName.text = chiefMechanic.name;
    StringVariableParser.subject = chiefMechanic;
    this.mechanicJobTitle.text = Localisation.LocaliseEnum((Enum) chiefMechanic.contract.job);
    this.chiefMechanicImpactLabel.text = Localisation.LocaliseID("PSG_10009301", (GameObject) null);
    StringVariableParser.subject = (Person) null;
    this.RefreshLabels();
  }

  public void RefreshLabels()
  {
    if (this.stat == CarPartStats.CarPartStat.Performance)
    {
      this.staffWorkRate.text = "+" + (this.mPartImprovement.GetWorkRate(this.stat) * 32400f).ToString("0.0", (IFormatProvider) Localisation.numberFormatter);
      this.mechanicRate.text = "+" + (this.mPartImprovement.GetChiefMechanicWorkRate(this.stat) * 32400f).ToString("0.0", (IFormatProvider) Localisation.numberFormatter);
    }
    else
    {
      this.staffWorkRate.text = ((float) ((double) this.mPartImprovement.GetWorkRate(this.stat) * 32400.0 * 100.0)).ToString("0.0", (IFormatProvider) Localisation.numberFormatter) + "%";
      this.mechanicRate.text = ((float) ((double) this.mPartImprovement.GetChiefMechanicWorkRate(this.stat) * 32400.0 * 100.0)).ToString("0.0", (IFormatProvider) Localisation.numberFormatter) + "%";
    }
  }
}
