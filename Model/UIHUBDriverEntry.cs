// Decompiled with JetBrains decompiler
// Type: UIHUBDriverEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHUBDriverEntry : MonoBehaviour
{
  public GameObject[] objectsToHideWhenDriverSelected = new GameObject[0];
  public CanvasGroup canvasGroup;
  public UICharacterPortrait driverPortrait;
  public TextMeshProUGUI driverName;
  public TextMeshProUGUI carLabel;
  public TextMeshProUGUI gridPosition;
  public Flag driverFlag;
  public UITyreWearIcon tyreWear;
  public Button addDriverButton;
  public UIAbilityStars stars;
  private RacingVehicle mVehicle;
  private Driver mDriver;

  public Driver driver
  {
    get
    {
      return this.mDriver;
    }
  }

  private void Start()
  {
    this.addDriverButton.onClick.AddListener(new UnityAction(this.OpenHudPanel));
  }

  private void OpenHudPanel()
  {
    UIManager.instance.GetScreen<SessionHUBScreen>().selectionWidget.GoToStep(UIHUBStep.Step.Drivers);
  }

  public void Setup(Driver inDriver)
  {
    if (inDriver == null)
      return;
    this.mDriver = inDriver;
    this.stars.SetAbilityStarsData(this.mDriver);
    this.mVehicle = Game.instance.vehicleManager.GetVehicle(this.mDriver);
    this.tyreWear.SetTyreSet(this.mVehicle.setup.tyreSet, this.mVehicle.bonuses.bonusDisplayInfo);
    this.tyreWear.UpdateTyreLocking(this.mVehicle, true);
    this.SetDetails();
    this.canvasGroup.alpha = 1f;
    this.canvasGroup.interactable = true;
    this.EnableObjects(false);
    GameUtility.SetActive(this.gridPosition.gameObject, Game.instance.sessionManager.eventDetails.currentSession.sessionType == SessionDetails.SessionType.Race);
    GameUtility.SetActive(this.carLabel.gameObject, !this.gridPosition.gameObject.activeSelf);
    if (!this.gridPosition.gameObject.activeSelf)
      return;
    RaceEventResults.ResultData driverQualifyingData = Game.instance.sessionManager.eventDetails.results.GetDriverQualifyingData(this.mDriver);
    StringVariableParser.stringValue1 = GameUtility.FormatForPosition(driverQualifyingData != null ? driverQualifyingData.gridPosition : this.mVehicle.stats.qualifyingPosition, (string) null);
    this.gridPosition.text = Localisation.LocaliseID("PSG_10011516", (GameObject) null);
  }

  private void EnableObjects(bool inValue)
  {
    for (int index = 0; index < this.objectsToHideWhenDriverSelected.Length; ++index)
      this.objectsToHideWhenDriverSelected[index].SetActive(inValue);
  }

  public void Refresh()
  {
    if (this.mDriver == null)
      return;
    this.tyreWear.SetTyreSet(this.mVehicle.setup.tyreSet, this.mVehicle.bonuses.bonusDisplayInfo);
  }

  public void Clear()
  {
    this.mDriver = (Driver) null;
    this.canvasGroup.alpha = 0.0f;
    this.canvasGroup.interactable = false;
    this.EnableObjects(true);
  }

  private void SetDetails()
  {
    this.driverPortrait.SetPortrait((Person) this.mDriver);
    this.driverName.text = this.mDriver.name;
    this.driverFlag.SetNationality(this.mDriver.nationality);
  }
}
