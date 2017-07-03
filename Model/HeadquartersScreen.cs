// Decompiled with JetBrains decompiler
// Type: HeadquartersScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeadquartersScreen : UIScreen
{
  private List<HQsStaticBuilding> buildings = new List<HQsStaticBuilding>();
  public UIHQSelectedBuilding selectionWidget;
  public UIHQBuildingsBarWidget barWidget;
  public UIHQOverviewWidget overviewWidget;
  public UIHQInfoBars infoWidget;
  public UIHQLevelling levellingWidget;
  private HQsStaticBuilding mStaticBuilding;
  private HQsBuilding_v1 mBuilding;
  private Headquarters mHeadquarters;
  private HeadquartersScene mHeadquartersScene;
  private bool mTestMode;
  private scSoundContainer mSoundContainer;

  public HeadquartersScene headquartersScene
  {
    get
    {
      return this.mHeadquartersScene;
    }
  }

  public Headquarters headquarters
  {
    get
    {
      return this.mHeadquarters;
    }
  }

  public List<HQsStaticBuilding> staticBuildings
  {
    get
    {
      return this.buildings;
    }
  }

  public HQsStaticBuilding selectedStaticBuilding
  {
    get
    {
      return this.mStaticBuilding;
    }
  }

  public bool isMouseHoverUI
  {
    get
    {
      return EventSystem.current.IsPointerOverGameObject();
    }
  }

  public bool isInTestMode
  {
    get
    {
      return this.mTestMode;
    }
  }

  public override void OnStart()
  {
    base.OnStart();
    this.barWidget.OnStart();
    this.overviewWidget.OnStart();
    this.selectionWidget.OnStart();
    this.PreloadScene();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.mHeadquarters = Game.instance.player.team.headquarters;
    this.mBuilding = this.data == null ? (HQsBuilding_v1) null : (HQsBuilding_v1) this.data;
    this.LoadScene();
    this.LoadUI();
    this.RemoveNotifications();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
    scSoundManager.CheckPlaySound(SoundID.Ambience_Factory, ref this.mSoundContainer, 0.0f);
  }

  public override void OnExit()
  {
    base.OnExit();
    this.DeselectBuilding();
    HQUpgradePopup.Close();
    this.selectionWidget.ClearNotification();
    this.levellingWidget.ClearNotifications();
    this.infoWidget.ClearNotifications();
    this.barWidget.ClearNotifications();
    this.overviewWidget.ClearNotifications();
    if ((UnityEngine.Object) this.headquartersScene != (UnityEngine.Object) null)
      this.headquartersScene.OnExit();
    this.LeaveScene();
    scSoundManager.CheckStopSound(ref this.mSoundContainer);
  }

  private void Update()
  {
    if (this.screenMode != UIScreen.ScreenMode.Mode3D || this.mHeadquartersScene.isDragging || (!InputManager.instance.GetKeyUp(KeyBinding.Name.MouseLeft) || !this.selectionWidget.gameObject.activeSelf) || (this.isMouseHoverUI || !this.mHeadquartersScene.isHoverClickableBuilding && !this.mHeadquartersScene.isHoverEmptyArea))
      return;
    this.DeselectBuilding();
  }

  private void PreloadScene()
  {
    if (!Game.IsActive() || Game.instance.player.IsUnemployed() || this.screenMode != UIScreen.ScreenMode.Mode3D)
      return;
    this.mHeadquarters = Game.instance.player.team.headquarters;
    GameObject sceneGameObject = SceneManager.instance.GetSceneGameObject(this.mHeadquarters.sceneName);
    if (!((UnityEngine.Object) sceneGameObject != (UnityEngine.Object) null) || this.screenMode != UIScreen.ScreenMode.Mode3D)
      return;
    this.mHeadquartersScene = sceneGameObject.GetComponent<HeadquartersScene>();
    this.buildings = this.mHeadquartersScene.GetBuildings();
    this.LoadBuildings(true);
  }

  private void LoadScene()
  {
    SceneManager.instance.SwitchScene(this.mHeadquarters.sceneName);
    GameObject sceneGameObject = SceneManager.instance.GetSceneGameObject(this.mHeadquarters.sceneName);
    if (!((UnityEngine.Object) sceneGameObject != (UnityEngine.Object) null) || this.screenMode != UIScreen.ScreenMode.Mode3D)
      return;
    this.mHeadquartersScene = sceneGameObject.GetComponent<HeadquartersScene>();
    this.mHeadquartersScene.screen = this;
    this.mHeadquartersScene.OnStart();
    this.buildings = this.mHeadquartersScene.GetBuildings();
    this.LoadBuildings(false);
    this.mHeadquartersScene.SetDefaultCamera(this.mBuilding);
    this.mHeadquartersScene.cameraController.SetVignette(false);
  }

  private void LeaveScene()
  {
    SceneManager.instance.LeaveCurrentScene();
  }

  private void LoadUI()
  {
    if (this.screenMode == UIScreen.ScreenMode.Mode3D)
    {
      this.HideBuildingList(true);
    }
    else
    {
      this.ShowBuildingList(!this.overviewWidget.isBuildToggleInteractable ? UIHQBuildingsBarWidget.Mode.Upgrade : UIHQBuildingsBarWidget.Mode.Build);
      this.overviewWidget.SelectToggle();
    }
    this.barWidget.SetNotifications();
    UIHQRollover.Close();
    HQUpgradePopup.Close();
    if (this.screenMode == UIScreen.ScreenMode.Mode3D)
    {
      this.infoWidget.Setup();
      this.levellingWidget.Setup();
    }
    else
    {
      this.infoWidget.HideAll();
      this.levellingWidget.HideAll();
    }
    this.selectionWidget.Hide();
    this.overviewWidget.Setup();
    this.SelectBuilding(this.mBuilding);
  }

  private void RemoveNotifications()
  {
    Notification notification = Game.instance.notificationManager.GetNotification("HQBuilding");
    if (notification.count <= 0)
      return;
    notification.RemoveAllCompletedChilds();
    if (notification.count > 0)
      return;
    notification.ResetCount();
  }

  public void ShowBuildingList(UIHQBuildingsBarWidget.Mode inMode)
  {
    this.DeselectBuilding();
    GameUtility.SetActive(this.barWidget.gameObject, true);
    this.barWidget.Setup(inMode);
    if (this.screenMode != UIScreen.ScreenMode.Mode3D)
      return;
    GameUtility.SetActive(this.levellingWidget.gameObject, false);
    GameUtility.SetActive(this.infoWidget.gameObject, false);
  }

  public void HideBuildingList(bool inSetToggles = true)
  {
    if (inSetToggles)
      this.overviewWidget.SetTogglesOff();
    GameUtility.SetActive(this.barWidget.gameObject, false);
    if (this.screenMode != UIScreen.ScreenMode.Mode3D)
      return;
    GameUtility.SetActive(this.levellingWidget.gameObject, this.levellingWidget.display);
    GameUtility.SetActive(this.infoWidget.gameObject, true);
  }

  private void LoadBuildings(bool inPreLoad = false)
  {
    for (int index = 0; index < this.buildings.Count; ++index)
    {
      HQsBuilding_v1 building = this.mHeadquarters.GetBuilding(this.buildings[index].buildingType);
      if (building != null)
      {
        if (inPreLoad)
          this.buildings[index].PreLoad(building);
        else
          this.buildings[index].Load(building);
      }
    }
  }

  public void SelectBuilding(HQsBuilding_v1 inBuilding)
  {
    if (inBuilding == null || this.screenMode != UIScreen.ScreenMode.Mode3D)
      return;
    HQsStaticBuilding staticBuilding = this.GetStaticBuilding(inBuilding);
    if (!((UnityEngine.Object) staticBuilding != (UnityEngine.Object) null))
      return;
    this.SelectBuilding(staticBuilding);
  }

  public void SelectBuilding(HQsStaticBuilding inBuilding)
  {
    if (!((UnityEngine.Object) inBuilding != (UnityEngine.Object) null) || this.screenMode != UIScreen.ScreenMode.Mode3D)
      return;
    if ((UnityEngine.Object) this.mStaticBuilding != (UnityEngine.Object) null)
    {
      this.mStaticBuilding.SetStatus(HQsStaticBuilding.Status.Clickable);
      this.mStaticBuilding.PreviewBuilding(false, false);
    }
    inBuilding.SetStatus(HQsStaticBuilding.Status.Locked);
    inBuilding.PreviewBuilding(true, inBuilding.canHighlight);
    this.mHeadquartersScene.SetCameraTarget(inBuilding);
    this.SelectBuildingUI(inBuilding.sBuilding);
    this.mStaticBuilding = inBuilding;
    this.infoWidget.SelectBuilding();
  }

  private void SelectBuildingUI(HQsBuilding_v1 inBuilding)
  {
    if (inBuilding != null)
    {
      scSoundManager.Instance.PlaySound(SoundID.Sfx_CameraMoveHQ, 0.0f);
      this.HideBuildingList(true);
      GameUtility.SetActive(this.levellingWidget.gameObject, false);
      this.selectionWidget.Setup(inBuilding);
    }
    else
    {
      this.selectionWidget.Hide();
      GameUtility.SetActive(this.levellingWidget.gameObject, this.levellingWidget.display);
    }
  }

  public void DeselectBuilding()
  {
    if (this.screenMode != UIScreen.ScreenMode.Mode3D)
      return;
    if ((UnityEngine.Object) this.mStaticBuilding != (UnityEngine.Object) null)
    {
      this.mStaticBuilding.SetStatus(HQsStaticBuilding.Status.Clickable);
      this.mStaticBuilding.PreviewBuilding(false, this.mStaticBuilding.canHighlight);
    }
    this.SelectBuildingUI((HQsBuilding_v1) null);
    this.mStaticBuilding = (HQsStaticBuilding) null;
    this.infoWidget.SelectBuilding();
  }

  public HQsStaticBuilding GetStaticBuilding(HQsBuilding_v1 inBuilding)
  {
    int count = this.buildings.Count;
    for (int index = 0; index < count; ++index)
    {
      HQsStaticBuilding building = this.buildings[index];
      if (building.sBuilding == inBuilding)
        return building;
    }
    return (HQsStaticBuilding) null;
  }

  public void ProcessTransaction(HQsBuilding_v1 inBuilding, Action inActionSuccess)
  {
    bool flag = false;
    Transaction transaction = (Transaction) null;
    if (inBuilding.isBuilt)
    {
      if (inBuilding.CanUpgrade())
      {
        StringVariableParser.building = inBuilding;
        transaction = new Transaction(Transaction.Group.HQ, Transaction.Type.Debit, (long) inBuilding.costs.GetUpgradeCost(), Localisation.LocaliseID("PSG_10010252", (GameObject) null));
        flag = true;
      }
    }
    else if (inBuilding.CanBuild())
    {
      StringVariableParser.building = inBuilding;
      transaction = new Transaction(Transaction.Group.HQ, Transaction.Type.Debit, inBuilding.info.initialCost, Localisation.LocaliseID("PSG_10010032", (GameObject) null));
      flag = true;
    }
    if (!flag)
      return;
    Game.instance.player.team.financeController.finance.ProcessTransactions(inActionSuccess, (Action) null, true, transaction);
  }
}
