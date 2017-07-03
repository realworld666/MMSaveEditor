// Decompiled with JetBrains decompiler
// Type: UIStaffBuildingWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIStaffBuildingWidget : MonoBehaviour
{
  public UIGridList staffIconBacking;
  public UIGridList staffIconFill;
  public GameObject buildingImagesRoot;
  public GameObject buildingStatusRoot;
  public Image buildingProgress;
  public Image titleBacking;
  public Image[] buildingStars;
  public Button upgradeButton;
  public Button confirmButton;
  public Button staffPlusButton;
  public Button staffMinusButton;
  public TextMeshProUGUI upgradeButtonText;
  public TextMeshProUGUI tier;
  public TextMeshProUGUI totalStaff;
  public TextMeshProUGUI cost;
  public TextMeshProUGUI costDifference;
  public TextMeshProUGUI hireFire;
  public TextMeshProUGUI buildingStatusText;
  public TextMeshProUGUI buildingTimeText;
  public int minStaff;
  public int maxStaff;
  public int staffCount;
  public int staffChange;
  public StaffScreen staffScreen;
  private int mBuildingId;
  private HQsBuilding_v1 mBuilding;
  private Headquarters mHeadquarters;
  private GameObject[] buildingImages;

  public void OnStart()
  {
    this.upgradeButton.onClick.AddListener((UnityAction) (() =>
    {
      this.OnUpgradeButton();
      this.UpdateUpgradeButtonState();
    }));
    this.confirmButton.onClick.AddListener((UnityAction) (() =>
    {
      this.OnConfirmButton();
      this.UpdateButtonsState();
    }));
    this.staffPlusButton.onClick.AddListener((UnityAction) (() =>
    {
      this.OnStaffPlusButton();
      this.UpdateButtonsState();
    }));
    this.staffMinusButton.onClick.AddListener((UnityAction) (() =>
    {
      this.OnStaffMinusButton();
      this.UpdateButtonsState();
    }));
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

  public void UpdateStaffListings(int inStaffCount, int inMaxStaff)
  {
    if (inMaxStaff == this.staffIconBacking.itemCount && inMaxStaff == this.staffIconFill.itemCount)
      return;
    this.staffIconBacking.DestroyListItems();
    this.staffIconFill.DestroyListItems();
    this.staffIconBacking.itemPrefab.SetActive(true);
    this.staffIconFill.itemPrefab.SetActive(true);
    for (int index = 0; index < inMaxStaff; ++index)
      this.staffIconBacking.CreateListItem<Transform>();
    for (int index = 0; index < inMaxStaff; ++index)
    {
      Transform listItem = this.staffIconFill.CreateListItem<Transform>();
      if (index > inStaffCount)
        listItem.gameObject.SetActive(false);
    }
    this.staffIconBacking.itemPrefab.SetActive(false);
    this.staffIconFill.itemPrefab.SetActive(false);
  }

  public void SetStaffChangeIcons(int inValue)
  {
    int num = Mathf.Max(inValue, this.staffCount);
    for (int inIndex = 0; inIndex < this.staffIconFill.itemCount; ++inIndex)
    {
      GameObject gameObject = this.staffIconFill.GetItem(inIndex);
      if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
      {
        if (inIndex + 1 <= num)
        {
          if (!gameObject.activeSelf)
            gameObject.SetActive(true);
        }
        else if (gameObject.activeSelf)
          gameObject.SetActive(false);
        gameObject.GetComponent<Image>().color = UIConstants.staffDefault;
        if (inValue < this.staffCount && inIndex + 1 > inValue)
          gameObject.GetComponent<Image>().color = UIConstants.staffFire;
        if (inValue > this.staffCount && inIndex + 1 > this.staffCount)
          gameObject.GetComponent<Image>().color = UIConstants.staffHire;
      }
    }
  }

  public void SetStaffChangeText()
  {
    this.totalStaff.text = this.staffChange.ToString() + "/" + this.maxStaff.ToString();
    int num = Mathf.Abs(this.staffChange - this.staffCount);
    if (this.staffChange < this.staffCount)
      this.hireFire.text = "Fire " + num.ToString() + " Staff";
    else if (this.staffChange == this.staffCount)
      this.hireFire.text = "Hire/Fire";
    else
      this.hireFire.text = "Hire " + num.ToString() + " Staff";
  }

  public void SetStaffCost()
  {
  }

  private void SetBuildingTitle(HQsBuilding_v1 inBuilding)
  {
  }

  private void SetBuildingImage(HQsBuilding_v1 inBuilding)
  {
    if (inBuilding.info.buildingID < this.buildingImages.Length && (UnityEngine.Object) this.buildingImages[inBuilding.info.buildingID] != (UnityEngine.Object) null)
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
    this.minStaff = this.mBuilding.staffNumber;
    this.maxStaff = this.mBuilding.staffNumber;
    this.staffCount = this.mBuilding.staffNumber;
    this.staffChange = this.staffCount;
    this.UpdateStaffListings(this.staffCount, this.maxStaff);
    this.SetStaffChangeIcons(this.staffCount);
    this.SetStaffChangeText();
    this.SetStaffCost();
    this.UpdateButtonsState();
    this.UpdateUpgradeButtonState();
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
    this.minStaff = this.mBuilding.staffNumber;
    this.maxStaff = this.mBuilding.staffNumber;
    this.staffCount = this.mBuilding.staffNumber;
    this.staffChange = Mathf.Clamp(this.staffChange, this.minStaff, this.maxStaff);
    this.UpdateStaffListings(this.staffCount, this.maxStaff);
    this.SetStaffChangeIcons(this.staffCount);
    this.SetStaffChangeText();
    this.SetStaffCost();
    this.UpdateButtonsState();
    this.UpdateUpgradeButtonState();
  }

  public void OnUpgradeButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    switch (this.mBuilding.state)
    {
      case HQsBuilding_v1.BuildingState.NotBuilt:
        UIManager.instance.ChangeScreen("HeadquartersScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
        break;
      case HQsBuilding_v1.BuildingState.Constructed:
        if (this.mBuilding.currentLevel != this.mBuilding.info.maxLevel)
          break;
        break;
    }
  }

  public void OnConfirmButton()
  {
  }

  public void OnStaffPlusButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.staffChange = Mathf.Clamp(this.staffChange + 1, this.minStaff, this.maxStaff);
    this.SetStaffChangeIcons(this.staffChange);
    this.SetStaffChangeText();
    this.SetStaffCost();
  }

  public void OnStaffMinusButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.staffChange = Mathf.Clamp(this.staffChange - 1, this.minStaff, this.maxStaff);
    this.SetStaffChangeIcons(this.staffChange);
    this.SetStaffChangeText();
    this.SetStaffCost();
  }

  public void UpdateButtonsState()
  {
    this.staffPlusButton.interactable = this.staffChange != this.maxStaff;
    this.staffMinusButton.interactable = this.staffChange != this.minStaff;
    this.confirmButton.interactable = this.staffChange != this.staffCount;
  }

  public void UpdateUpgradeButtonState()
  {
    switch (this.mBuilding.state)
    {
      case HQsBuilding_v1.BuildingState.NotBuilt:
        this.buildingStatusRoot.gameObject.SetActive(false);
        this.upgradeButton.gameObject.SetActive(true);
        this.upgradeButton.interactable = true;
        this.upgradeButtonText.text = "BUILD";
        break;
      case HQsBuilding_v1.BuildingState.BuildingInProgress:
        this.buildingStatusRoot.gameObject.SetActive(true);
        this.upgradeButton.gameObject.SetActive(false);
        this.upgradeButton.interactable = false;
        this.buildingStatusText.text = "Building Time:";
        break;
      case HQsBuilding_v1.BuildingState.Constructed:
        if (this.mBuilding.currentLevel != this.mBuilding.info.maxLevel)
        {
          this.buildingStatusRoot.gameObject.SetActive(false);
          this.upgradeButton.gameObject.SetActive(true);
          this.upgradeButton.interactable = true;
          this.upgradeButtonText.text = "UPGRADE";
          break;
        }
        break;
      case HQsBuilding_v1.BuildingState.Upgrading:
        this.buildingStatusRoot.gameObject.SetActive(true);
        this.upgradeButton.gameObject.SetActive(false);
        this.upgradeButton.interactable = false;
        this.buildingStatusText.text = "Upgrading Time:";
        break;
    }
    if (!this.buildingStatusRoot.activeSelf)
      return;
    this.UpdateBuildingProgress();
  }
}
