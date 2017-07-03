// Decompiled with JetBrains decompiler
// Type: UIPartDevMechanicAllocationWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPartDevMechanicAllocationWidget : MonoBehaviour
{
  public TextMeshProUGUI mechanicsPerformance;
  public TextMeshProUGUI mechanicsReliability;
  public TextMeshProUGUI totalMechanicsLabel;
  public Slider mechanicSlider;
  public Button hire;
  private PartImprovement mPartImprovement;

  private void Awake()
  {
    this.hire.onClick.AddListener(new UnityAction(this.OnHireButton));
    this.mechanicSlider.onValueChanged.AddListener((UnityAction<float>) (value => this.OnMechanicSliderChange(value)));
  }

  private void OnMechanicSliderChange(float inValue)
  {
    scSoundManager.Instance.PlaySquareSlider();
    if ((double) this.mechanicSlider.normalizedValue != (double) this.mPartImprovement.normalizedMechanicDistribution)
    {
      this.mPartImprovement.playerMechanicsPreference = this.mechanicSlider.normalizedValue;
      this.mPartImprovement.SplitMechanics(this.mechanicSlider.normalizedValue);
    }
    this.UpdateMechanicLabels();
  }

  private void OnHireButton()
  {
    UIManager.instance.ChangeScreen("HeadquartersScreen", (Entity) Game.instance.player.team.headquarters.GetBuilding(HQsBuildingInfo.Type.Factory), UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }

  public void Setup()
  {
    this.mPartImprovement = Game.instance.player.team.carManager.partImprovement;
    float mechanicDistribution = this.mPartImprovement.normalizedMechanicDistribution;
    this.mechanicSlider.maxValue = (float) Game.instance.player.team.headquarters.GetBuilding(HQsBuildingInfo.Type.Factory).staffNumber;
    this.mPartImprovement.SplitMechanics(mechanicDistribution);
    this.UpdateMechanicLabels();
  }

  public void UpdateMechanicLabels()
  {
    this.totalMechanicsLabel.text = "Total Mechanics: " + this.mPartImprovement.GetTotalMechanics().ToString();
    this.mechanicsPerformance.text = this.mPartImprovement.mechanics[3].ToString();
    this.mechanicsReliability.text = this.mPartImprovement.mechanics[1].ToString();
    this.mechanicSlider.normalizedValue = this.mPartImprovement.normalizedMechanicDistribution;
  }
}
