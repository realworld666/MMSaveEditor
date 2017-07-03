// Decompiled with JetBrains decompiler
// Type: PartDesignStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartDesignStatus : MonoBehaviour
{
  public TextMeshProUGUI partNameLabel;
  public TextMeshProUGUI timeRemainingLabel;
  public Image timeRemainingBar;
  public Image engineIcon;
  public Image brakesIcon;
  public Image floorIcon;
  public Image frontWingIcon;
  public Image rearWingIcon;
  public Image suspensionIcon;
  public Image gearboxIcon;
  private CarPartDesign mDesign;

  public void SetCarPartDesign(CarPartDesign inDesign)
  {
    this.mDesign = inDesign;
  }

  public void Show()
  {
    this.gameObject.SetActive(true);
  }

  public void Hide()
  {
    this.gameObject.SetActive(false);
  }

  private void Update()
  {
    if (this.mDesign == null || this.mDesign.stage != CarPartDesign.Stage.Designing)
      return;
    CarPart part = this.mDesign.part;
    this.timeRemainingLabel.text = this.mDesign.GetTimeRemainingString();
    this.timeRemainingBar.fillAmount = this.mDesign.GetCreationTimeElapsedNormalised();
    this.partNameLabel.text = part.GetPartName();
    this.engineIcon.gameObject.SetActive(part.GetPartType() == CarPart.PartType.Engine);
    this.brakesIcon.gameObject.SetActive(part.GetPartType() == CarPart.PartType.Brakes);
    this.frontWingIcon.gameObject.SetActive(part.GetPartType() == CarPart.PartType.FrontWing);
    this.rearWingIcon.gameObject.SetActive(part.GetPartType() == CarPart.PartType.RearWing);
    this.suspensionIcon.gameObject.SetActive(part.GetPartType() == CarPart.PartType.Suspension);
    this.gearboxIcon.gameObject.SetActive(part.GetPartType() == CarPart.PartType.Gearbox);
    if (this.engineIcon.gameObject.activeSelf)
      this.engineIcon.transform.FindChild("BuildFill").GetComponent<Image>().fillAmount = this.timeRemainingBar.fillAmount;
    else if (this.brakesIcon.gameObject.activeSelf)
      this.brakesIcon.transform.FindChild("BuildFill").GetComponent<Image>().fillAmount = this.timeRemainingBar.fillAmount;
    else if (this.floorIcon.gameObject.activeSelf)
      this.floorIcon.transform.FindChild("BuildFill").GetComponent<Image>().fillAmount = this.timeRemainingBar.fillAmount;
    else if (this.frontWingIcon.gameObject.activeSelf)
      this.frontWingIcon.transform.FindChild("BuildFill").GetComponent<Image>().fillAmount = this.timeRemainingBar.fillAmount;
    else if (this.rearWingIcon.gameObject.activeSelf)
      this.rearWingIcon.transform.FindChild("BuildFill").GetComponent<Image>().fillAmount = this.timeRemainingBar.fillAmount;
    else if (this.suspensionIcon.gameObject.activeSelf)
    {
      this.suspensionIcon.transform.FindChild("BuildFill").GetComponent<Image>().fillAmount = this.timeRemainingBar.fillAmount;
    }
    else
    {
      if (!this.gearboxIcon.gameObject.activeSelf)
        return;
      this.gearboxIcon.transform.FindChild("BuildFill").GetComponent<Image>().fillAmount = this.timeRemainingBar.fillAmount;
    }
  }
}
