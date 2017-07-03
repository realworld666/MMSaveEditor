// Decompiled with JetBrains decompiler
// Type: UIHQBuildingEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHQBuildingEntry : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
  public Button viewButton;
  public Button upgradeButton;
  [SerializeField]
  private GameObject maxLevel;
  [SerializeField]
  private GameObject upgrading;
  [SerializeField]
  private GameObject locked;
  [SerializeField]
  private TextMeshProUGUI nameLabel;
  [SerializeField]
  private TextMeshProUGUI lockedLabel;
  [SerializeField]
  private TextMeshProUGUI levelLabel;
  [SerializeField]
  private TextMeshProUGUI statusLabel;
  [SerializeField]
  private TextMeshProUGUI timeLabel;
  [SerializeField]
  private TextMeshProUGUI upgradeButtonLabel;
  [SerializeField]
  private TextMeshProUGUI upgradeStatusLabel;
  [SerializeField]
  private TextMeshProUGUI timeLeftLabel;
  [SerializeField]
  private Slider progressBar;
  [SerializeField]
  private UIBuilding buildingIcon;
  [SerializeField]
  private UIHQGroupIcon groupIcon;
  public HeadquartersScreen screen;
  private HQsBuilding_v1 mBuilding;

  public HQsBuilding_v1 building
  {
    get
    {
      return this.mBuilding;
    }
  }

  private void Awake()
  {
  }

  public void OnStart()
  {
    this.viewButton.onClick.RemoveAllListeners();
    this.viewButton.onClick.AddListener(new UnityAction(this.OnViewButton));
    this.upgradeButton.onClick.RemoveAllListeners();
    this.upgradeButton.onClick.AddListener(new UnityAction(this.OnUpgradeButton));
  }

  public void Setup(HQsBuilding_v1 inBuilding)
  {
    this.mBuilding = inBuilding;
    this.Refresh();
  }

  private void Update()
  {
    if (this.mBuilding == null || !this.upgrading.activeSelf)
      return;
    this.progressBar.value = this.mBuilding.normalizedProgressUI;
  }

  private void Refresh()
  {
    this.SetDetails();
    this.UpdateStatus();
  }

  private void UpdateStatus()
  {
    GameUtility.SetActive(this.viewButton.gameObject, this.screen.screenMode == UIScreen.ScreenMode.Mode3D);
    GameUtility.SetActive(this.upgradeButton.gameObject, this.mBuilding.CanUpgrade() || this.mBuilding.CanBuild());
    GameUtility.SetActive(this.maxLevel, this.mBuilding.isMaxLevel);
    GameUtility.SetActive(this.upgrading, this.mBuilding.isLeveling);
    GameUtility.SetActive(this.locked, this.mBuilding.isLocked);
    GameUtility.SetActive(this.lockedLabel.gameObject, this.mBuilding.isLocked);
    GameUtility.SetActive(this.levelLabel.gameObject, this.mBuilding.isBuilt && !this.mBuilding.isLeveling);
    GameUtility.SetActive(this.statusLabel.gameObject, !this.mBuilding.isLocked && this.mBuilding.isLeveling);
    StringVariableParser.building = this.mBuilding;
    if (this.levelLabel.gameObject.activeSelf)
      this.levelLabel.text = Localisation.LocaliseID("PSG_10010003", (GameObject) null);
    if (this.statusLabel.gameObject.activeSelf)
      this.statusLabel.text = this.mBuilding.isBuilt ? Localisation.LocaliseID("PSG_10009999", (GameObject) null) : Localisation.LocaliseID("PSG_10010000", (GameObject) null);
    if (this.upgradeButton.gameObject.activeSelf)
      this.upgradeButtonLabel.text = this.mBuilding.isBuilt ? Localisation.LocaliseID("PSG_10010002", (GameObject) null) : Localisation.LocaliseID("PSG_10010001", (GameObject) null);
    if (!this.upgrading.gameObject.activeSelf)
      return;
    this.upgradeStatusLabel.text = this.mBuilding.isBuilt ? Localisation.LocaliseID("PSG_10009997", (GameObject) null) : Localisation.LocaliseID("PSG_10010000", (GameObject) null);
    this.timeLeftLabel.text = GameUtility.FormatTimeSpanWeeks(this.mBuilding.timeRemaining);
    this.progressBar.value = this.mBuilding.normalizedProgressUI;
  }

  public void SetDetails()
  {
    this.nameLabel.text = this.mBuilding.buildingName;
    this.buildingIcon.SetBuilding(this.mBuilding);
    this.groupIcon.SetIcon(this.mBuilding);
    GameUtility.SetActive(this.timeLabel.gameObject, !this.mBuilding.isMaxLevel);
    if (!this.timeLabel.gameObject.activeSelf)
      return;
    if (this.mBuilding.isLeveling)
    {
      this.timeLabel.text = GameUtility.FormatTimeSpanWeeks(this.mBuilding.timeRemaining);
    }
    else
    {
      StringVariableParser.building = this.mBuilding;
      this.timeLabel.text = this.mBuilding.isBuilt ? Localisation.LocaliseID("PSG_10010036", (GameObject) null) : Localisation.LocaliseID("PSG_10010035", (GameObject) null);
    }
  }

  private void OnViewButton()
  {
    UIHQRollover.Close();
    this.screen.SelectBuilding(this.mBuilding);
  }

  private void OnUpgradeButton()
  {
    if (this.mBuilding.isLeveling)
      return;
    UIHQRollover.Close();
    HQUpgradePopup.Open(this.mBuilding);
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    if (this.mBuilding == null)
      return;
    UIHQRollover.Setup(this.screen.screenMode, this.mBuilding, UIHQRollover.Mode.AutoSelect, true);
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    UIHQRollover.Close();
  }
}
