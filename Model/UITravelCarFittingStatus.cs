// Decompiled with JetBrains decompiler
// Type: UITravelCarFittingStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UITravelCarFittingStatus : MonoBehaviour
{
  public GameObject completeFittingStatus;
  public GameObject errorFittingStatus;
  public TextMeshProUGUI errorFittingText;
  public GameObject frontwingHighlight;
  public GameObject brakesHighlight;
  public GameObject suspensionHighlight;
  public GameObject engineHighlight;
  public GameObject gearboxHighlight;
  public GameObject rearwingHighlight;

  public void SetupCarPartStatus(Car inCar)
  {
    bool inIsActive = inCar.HasAllPartSlotsFitted();
    GameUtility.SetActive(this.completeFittingStatus, inIsActive);
    GameUtility.SetActive(this.errorFittingStatus, !inIsActive);
    if (!inIsActive)
    {
      if (inCar.seriesCurrentParts.Length - inCar.GetPartFittedCount() == 1)
      {
        CarPart.PartType[] partType = CarPart.GetPartType(inCar.carManager.team.championship.series, false);
        for (int index = 0; index < partType.Length; ++index)
        {
          if (inCar.seriesCurrentParts[index] == null)
          {
            StringVariableParser.partFrontendUI = partType[index];
            this.errorFittingText.text = Localisation.LocaliseID("PSG_10010618", (GameObject) null);
            break;
          }
        }
      }
      else
        this.errorFittingText.text = Localisation.LocaliseID("PSG_10010617", (GameObject) null);
    }
    switch (inCar.carManager.team.championship.series)
    {
      case Championship.Series.SingleSeaterSeries:
        GameUtility.SetActive(this.frontwingHighlight, inCar.HasPartFitted(CarPart.PartType.FrontWing));
        GameUtility.SetActive(this.gearboxHighlight, inCar.HasPartFitted(CarPart.PartType.Gearbox));
        GameUtility.SetActive(this.brakesHighlight, inCar.HasPartFitted(CarPart.PartType.Brakes));
        GameUtility.SetActive(this.suspensionHighlight, inCar.HasPartFitted(CarPart.PartType.Suspension));
        GameUtility.SetActive(this.engineHighlight, inCar.HasPartFitted(CarPart.PartType.Engine));
        GameUtility.SetActive(this.rearwingHighlight, inCar.HasPartFitted(CarPart.PartType.RearWing));
        break;
      case Championship.Series.GTSeries:
        GameUtility.SetActive(this.gearboxHighlight, inCar.HasPartFitted(CarPart.PartType.GearboxGT));
        GameUtility.SetActive(this.brakesHighlight, inCar.HasPartFitted(CarPart.PartType.BrakesGT));
        GameUtility.SetActive(this.suspensionHighlight, inCar.HasPartFitted(CarPart.PartType.SuspensionGT));
        GameUtility.SetActive(this.engineHighlight, inCar.HasPartFitted(CarPart.PartType.EngineGT));
        GameUtility.SetActive(this.rearwingHighlight, inCar.HasPartFitted(CarPart.PartType.RearWingGT));
        break;
    }
  }
}
