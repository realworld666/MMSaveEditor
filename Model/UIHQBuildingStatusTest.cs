// Decompiled with JetBrains decompiler
// Type: UIHQBuildingStatusTest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHQBuildingStatusTest : MonoBehaviour
{
  public Button nextStage;
  public Button prevStage;
  public Button maxStage;
  public Button minStage;
  public Button destroy;
  private HQsBuilding_v1 building;

  public void OnStart()
  {
    this.nextStage.onClick.RemoveAllListeners();
    this.nextStage.onClick.AddListener((UnityAction) (() => this.OnButton(UIHQBuildingStatusTest.StageAction.NextStage)));
    this.prevStage.onClick.RemoveAllListeners();
    this.prevStage.onClick.AddListener((UnityAction) (() => this.OnButton(UIHQBuildingStatusTest.StageAction.PrevStage)));
    this.maxStage.onClick.RemoveAllListeners();
    this.maxStage.onClick.AddListener((UnityAction) (() => this.OnButton(UIHQBuildingStatusTest.StageAction.MaxStage)));
    this.minStage.onClick.RemoveAllListeners();
    this.minStage.onClick.AddListener((UnityAction) (() => this.OnButton(UIHQBuildingStatusTest.StageAction.MinStage)));
    this.destroy.onClick.RemoveAllListeners();
    this.destroy.onClick.AddListener((UnityAction) (() => this.OnButton(UIHQBuildingStatusTest.StageAction.Destroy)));
  }

  public void Setup(HQsBuilding_v1 inBuilding)
  {
    this.building = inBuilding;
    if (this.building == null)
      return;
    this.Refresh();
  }

  public void Refresh()
  {
    if (this.building == null)
      return;
    this.UpdateButtonsState();
  }

  private void OnButton(UIHQBuildingStatusTest.StageAction inAction)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.building.ActionTest(inAction);
  }

  private void UpdateButtonsState()
  {
    this.nextStage.interactable = this.building.CanNextStage();
    this.prevStage.interactable = this.building.state != HQsBuilding_v1.BuildingState.NotBuilt;
    this.maxStage.interactable = this.building.CanNextStage();
    this.minStage.interactable = this.building.isBuilt && (this.building.currentLevel > 0 || this.building.currentLevel == 0 && this.building.state != HQsBuilding_v1.BuildingState.Constructed);
    this.destroy.interactable = this.building.state != HQsBuilding_v1.BuildingState.NotBuilt;
  }

  public enum StageAction
  {
    NextStage,
    PrevStage,
    MaxStage,
    MinStage,
    Reopen,
    Destroy,
  }
}
