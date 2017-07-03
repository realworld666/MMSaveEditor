// Decompiled with JetBrains decompiler
// Type: UIHQInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHQInfo : MonoBehaviour
{
  private Vector2 mViewPortPoint = Vector2.zero;
  public Toggle toggle;
  public RectTransform rectTransform;
  public TextMeshProUGUI buildingName;
  public TextMeshProUGUI buildingLevel;
  public TextMeshProUGUI buildingStatus;
  public GameObject progressBarParent;
  public Image progressBar;
  public UIHQGroupIcon groupIcon;
  public GameObject upgradeIcon;
  public UIHQBuildingStatusTest testWidget;
  public HeadquartersScreen screen;
  private HQsStaticBuilding mBuilding;
  private Camera mCamera;
  private int mSiblingIndex;
  private bool mPreview;

  public HQsStaticBuilding building
  {
    get
    {
      return this.mBuilding;
    }
  }

  public bool isPreview
  {
    get
    {
      return this.mPreview;
    }
  }

  public void Setup(HQsStaticBuilding inBuilding)
  {
    if (!((UnityEngine.Object) inBuilding != (UnityEngine.Object) null))
      return;
    this.mBuilding = inBuilding;
    this.toggle.onValueChanged.RemoveAllListeners();
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle()));
    this.mBuilding.sBuilding.OnNotification -= new Action<HQsBuilding_v1.NotificationState>(this.OnBuildingNotification);
    this.mBuilding.sBuilding.OnNotification += new Action<HQsBuilding_v1.NotificationState>(this.OnBuildingNotification);
    this.buildingName.text = this.mBuilding.sBuilding.buildingName;
    this.mSiblingIndex = this.rectTransform.GetSiblingIndex();
    this.Refresh();
    this.UpdateState();
  }

  private void Refresh()
  {
    this.UpdateLevel();
    this.UpdateStatusIcon();
    this.UpdatePosition();
    this.CheckTestMode();
  }

  private void LateUpdate()
  {
    if (!((UnityEngine.Object) this.mBuilding != (UnityEngine.Object) null))
      return;
    this.UpdatePosition();
  }

  public void UpdatePosition()
  {
    this.mCamera = this.screen.headquartersScene.cameraController.GetCamera();
    this.mViewPortPoint = (Vector2) this.mCamera.WorldToViewportPoint(this.mBuilding.center.transform.position);
    this.rectTransform.anchorMin = this.mViewPortPoint;
    this.rectTransform.anchorMax = this.mViewPortPoint;
  }

  public void CheckTestMode()
  {
    GameUtility.SetActive(this.testWidget.gameObject, this.screen.isInTestMode);
    if (!this.testWidget.gameObject.activeSelf)
      return;
    this.testWidget.OnStart();
    this.testWidget.Setup(this.mBuilding.sBuilding);
  }

  public void UpdateProgressBar()
  {
  }

  public void UpdateLevel()
  {
    GameUtility.SetActive(this.buildingLevel.gameObject, !this.mBuilding.sBuilding.isLocked && !this.mBuilding.sBuilding.isLeveling && !this.mBuilding.sBuilding.isMaxLevel);
    GameUtility.SetActive(this.buildingStatus.gameObject, this.mBuilding.sBuilding.isLocked || this.mBuilding.sBuilding.isLeveling || this.mBuilding.sBuilding.isMaxLevel);
    StringVariableParser.building = this.mBuilding.sBuilding;
    if (this.buildingLevel.gameObject.activeSelf)
      this.buildingLevel.text = this.mBuilding.sBuilding.isBuilt ? Localisation.LocaliseID("PSG_10010003", (GameObject) null) : Localisation.LocaliseID("PSG_10009991", (GameObject) null);
    if (!this.buildingStatus.gameObject.activeSelf)
      return;
    this.buildingStatus.text = !this.mBuilding.sBuilding.isLocked ? (!this.mBuilding.sBuilding.isMaxLevel ? (this.mBuilding.sBuilding.isBuilt ? Localisation.LocaliseID("PSG_10009999", (GameObject) null) : Localisation.LocaliseID("PSG_10010000", (GameObject) null)) : Localisation.LocaliseID("PSG_10009993", (GameObject) null)) : Localisation.LocaliseID("PSG_10009992", (GameObject) null);
  }

  public void UpdateStatusIcon()
  {
    if (!((UnityEngine.Object) this.mBuilding != (UnityEngine.Object) null) || !this.gameObject.activeSelf)
      return;
    GameUtility.SetActive(this.groupIcon.gameObject, !this.mBuilding.sBuilding.isLeveling);
    GameUtility.SetActive(this.upgradeIcon, this.mBuilding.sBuilding.isLeveling);
    if (!this.groupIcon.gameObject.activeSelf)
      return;
    this.groupIcon.SetIcon(this.mBuilding.sBuilding);
  }

  private void UpdateState()
  {
    if (this.screen.isInTestMode)
    {
      this.Show();
    }
    else
    {
      switch (this.mBuilding.sBuilding.state)
      {
        case HQsBuilding_v1.BuildingState.NotBuilt:
          this.Close();
          break;
        case HQsBuilding_v1.BuildingState.BuildingInProgress:
        case HQsBuilding_v1.BuildingState.Constructed:
        case HQsBuilding_v1.BuildingState.Upgrading:
          this.Show();
          break;
      }
    }
  }

  public void OnBuildingNotification(HQsBuilding_v1.NotificationState inState)
  {
    this.UpdateState();
    if (!((UnityEngine.Object) this.mBuilding != (UnityEngine.Object) null))
      return;
    this.Refresh();
  }

  public void Highlight(bool inHighlight)
  {
    if (inHighlight)
      this.mBuilding.PreviewBuilding(true, false);
    else
      this.mBuilding.PreviewBuilding(false, false);
  }

  public void Show()
  {
    GameUtility.SetActive(this.gameObject, true);
  }

  public void Close()
  {
    GameUtility.SetActive(this.gameObject, false);
  }

  public void Preview(bool inPreview)
  {
    this.mPreview = inPreview;
    if (this.mPreview)
    {
      this.toggle.isOn = true;
      this.Refresh();
      this.Show();
    }
    else
    {
      this.toggle.isOn = false;
      if (this.mBuilding.sBuilding.state != HQsBuilding_v1.BuildingState.NotBuilt)
        return;
      this.Close();
    }
  }

  private void OnToggle()
  {
    if (this.toggle.isOn && (UnityEngine.Object) this.mBuilding != (UnityEngine.Object) null && (UnityEngine.Object) this.mBuilding != (UnityEngine.Object) this.screen.selectedStaticBuilding)
    {
      this.Highlight(false);
      this.screen.SelectBuilding(this.mBuilding.sBuilding);
      this.rectTransform.SetAsLastSibling();
    }
    else
    {
      if (this.toggle.isOn)
        return;
      this.rectTransform.SetSiblingIndex(this.mSiblingIndex);
    }
  }

  public void Select()
  {
    this.toggle.isOn = true;
    this.Highlight(false);
  }

  public void ClearNotification()
  {
    if (!((UnityEngine.Object) this.mBuilding != (UnityEngine.Object) null))
      return;
    this.toggle.isOn = false;
    this.mBuilding.sBuilding.OnNotification -= new Action<HQsBuilding_v1.NotificationState>(this.OnBuildingNotification);
  }
}
