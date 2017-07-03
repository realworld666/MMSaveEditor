// Decompiled with JetBrains decompiler
// Type: UIStaffOtherBuildingWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStaffOtherBuildingWidget : MonoBehaviour
{
  public UIGridList staffIcons;
  public GameObject buildingImagesRoot;
  public GameObject buildingStatusRoot;
  public GameObject staffIconsTimesRoot;
  public GameObject costPerMonthRoot;
  public GameObject incomeRoot;
  public Image buildingProgress;
  public Image titleBacking;
  public Image[] buildingStars;
  public TextMeshProUGUI buildingName;
  public TextMeshProUGUI buildingStatusText;
  public TextMeshProUGUI buildingTimeText;
  public TextMeshProUGUI tier;
  public TextMeshProUGUI costPerMonth;
  public TextMeshProUGUI income;
  public TextMeshProUGUI staffCountTimes;
  public int minStaff;
  public int maxStaff;
  public int staffCount;
  private int mBuildingId;
  private HQsBuilding_v1 mBuilding;
  private Headquarters mHeadquarters;
  private GameObject[] buildingImages;

  public void OnStart()
  {
    this.buildingImages = new GameObject[this.buildingImagesRoot.transform.childCount];
    for (int index = 0; index < this.buildingImagesRoot.transform.childCount; ++index)
    {
      this.buildingImages[index] = this.buildingImagesRoot.transform.GetChild(index).gameObject;
      this.buildingImages[index].SetActive(false);
    }
  }

  private void Update()
  {
    if (!this.buildingStatusRoot.activeSelf || this.mBuilding == null || this.mBuilding.state != HQsBuilding_v1.BuildingState.BuildingInProgress && this.mBuilding.state != HQsBuilding_v1.BuildingState.Upgrading)
      return;
    this.UpdateBuildingProgress();
  }

  public void UpdateBuildingProgress()
  {
    this.buildingProgress.fillAmount = this.mBuilding.normalizedProgress;
    int num = (int) this.mBuilding.timeRemaining.TotalDays / 7;
    if (num == 0)
      this.buildingTimeText.text = "> 1 week remaining";
    else
      this.buildingTimeText.text = num <= 1 ? num.ToString() + " week remaining" : num.ToString() + " weeks remaining";
  }

  public void UpdateStaffListings(int inStaffCount)
  {
    if (inStaffCount == this.staffIcons.itemCount)
      return;
    if (inStaffCount <= 12)
    {
      if (this.staffIconsTimesRoot.activeSelf)
        this.staffIconsTimesRoot.SetActive(false);
      if (!this.staffIcons.gameObject.activeSelf)
        this.staffIcons.gameObject.SetActive(true);
      this.staffIcons.DestroyListItems();
      this.staffIcons.itemPrefab.SetActive(true);
      for (int index = 0; index < inStaffCount; ++index)
        this.staffIcons.CreateListItem<Transform>();
      this.staffIcons.itemPrefab.SetActive(false);
    }
    else
    {
      if (!this.staffIconsTimesRoot.activeSelf)
        this.staffIconsTimesRoot.SetActive(true);
      if (this.staffIcons.gameObject.activeSelf)
        this.staffIcons.gameObject.SetActive(false);
      this.staffCountTimes.text = "x " + this.staffCount.ToString();
    }
  }

  public void SetBuildingStatus()
  {
    bool flag = true;
    switch (this.mBuilding.state)
    {
      case HQsBuilding_v1.BuildingState.NotBuilt:
        flag = true;
        break;
      case HQsBuilding_v1.BuildingState.BuildingInProgress:
        flag = false;
        break;
      case HQsBuilding_v1.BuildingState.Constructed:
        flag = true;
        break;
      case HQsBuilding_v1.BuildingState.Upgrading:
        flag = false;
        break;
    }
    if (flag)
    {
      if (this.buildingStatusRoot.activeSelf)
        this.buildingStatusRoot.SetActive(false);
      if (!this.costPerMonthRoot.activeSelf)
        this.costPerMonthRoot.SetActive(true);
      if (this.incomeRoot.activeSelf)
        return;
      this.incomeRoot.SetActive(true);
    }
    else
    {
      if (!this.buildingStatusRoot.activeSelf)
        this.buildingStatusRoot.SetActive(true);
      if (this.costPerMonthRoot.activeSelf)
        this.costPerMonthRoot.SetActive(false);
      if (!this.incomeRoot.activeSelf)
        return;
      this.incomeRoot.SetActive(false);
    }
  }

  public void CheckSelfActive(bool inValue)
  {
    if (this.gameObject.activeSelf == inValue)
      return;
    this.gameObject.SetActive(inValue);
  }

  public void SetStaffCostIncome()
  {
  }

  private void SetBuildingTitle(HQsBuilding_v1 inBuilding)
  {
  }

  private void SetBuildingImage(HQsBuilding_v1 inBuilding)
  {
    if (inBuilding.info.buildingID < this.buildingImages.Length && (Object) this.buildingImages[inBuilding.info.buildingID] != (Object) null)
      this.buildingImages[inBuilding.info.buildingID].SetActive(true);
    else
      this.buildingImages[0].SetActive(true);
  }

  private void SetBuildingStars(HQsBuilding_v1 inBuilding)
  {
    int num = 0;
    while (num <= inBuilding.currentLevel)
      ++num;
  }

  private void ResetBuildingStars()
  {
    for (int index = 0; index < this.buildingStars.Length; ++index)
    {
      this.buildingStars[index].color = UIConstants.starDefault;
      if (index <= this.mBuilding.info.maxLevel)
        this.buildingStars[index].gameObject.SetActive(true);
      else
        this.buildingStars[index].gameObject.SetActive(false);
    }
  }

  public void Setup(int inBuildingIndex)
  {
    this.mBuildingId = inBuildingIndex;
    this.mHeadquarters = Game.instance.player.team.headquarters;
    this.mBuilding = this.mHeadquarters.GetBuilding(this.mBuildingId);
    if (this.mBuilding == null)
      return;
    this.ResetBuildingStars();
    this.SetBuildingTitle(this.mBuilding);
    this.SetBuildingImage(this.mBuilding);
    this.SetBuildingStars(this.mBuilding);
    this.SetBuildingStatus();
    this.minStaff = this.mBuilding.staffNumber;
    this.maxStaff = this.mBuilding.staffNumber;
    this.staffCount = this.mBuilding.staffNumber;
    this.UpdateStaffListings(this.staffCount);
    this.SetStaffCostIncome();
  }

  public void Refresh(int inBuildingIndex)
  {
    this.mBuildingId = inBuildingIndex;
    this.mHeadquarters = Game.instance.player.team.headquarters;
    this.mBuilding = this.mHeadquarters.GetBuilding(this.mBuildingId);
    if (this.mBuilding == null)
      return;
    this.ResetBuildingStars();
    this.SetBuildingTitle(this.mBuilding);
    this.SetBuildingImage(this.mBuilding);
    this.SetBuildingStars(this.mBuilding);
    this.SetBuildingStatus();
    this.minStaff = this.mBuilding.staffNumber;
    this.maxStaff = this.mBuilding.staffNumber;
    this.staffCount = this.mBuilding.staffNumber;
    this.UpdateStaffListings(this.staffCount);
    this.SetStaffCostIncome();
  }
}
