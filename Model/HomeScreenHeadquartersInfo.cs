// Decompiled with JetBrains decompiler
// Type: HomeScreenHeadquartersInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HomeScreenHeadquartersInfo : HomeScreenInfoPanel
{
  private HomeScreenHeadquartersInfo.Stage mStage = HomeScreenHeadquartersInfo.Stage.General;
  private Headquarters mHeadquarters;
  private HQsBuilding_v1 mBuilding;

  public override void OnStart()
  {
    this.button.onClick.AddListener(new UnityAction(this.OnButton));
  }

  public override void Setup()
  {
    this.mHeadquarters = Game.instance.player.team.headquarters;
    this.SelectStage();
    this.SetStage();
  }

  private void OnButton()
  {
    switch (this.mStage)
    {
      case HomeScreenHeadquartersInfo.Stage.UnlockedBuilding:
        UIManager.instance.ChangeScreen("HeadquartersScreen", (Entity) this.mBuilding, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
        break;
      case HomeScreenHeadquartersInfo.Stage.General:
        UIManager.instance.ChangeScreen("HeadquartersScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
        break;
    }
  }

  private void SelectStage()
  {
    List<HQsBuilding_v1> unlockedBuildings = this.mHeadquarters.unlockedBuildings;
    bool flag = false;
    int count = unlockedBuildings.Count;
    for (int index = 0; index < count; ++index)
    {
      HQsBuilding_v1 hqsBuildingV1 = unlockedBuildings[index];
      if (!hqsBuildingV1.isBuilt && !hqsBuildingV1.isLeveling)
      {
        this.mBuilding = hqsBuildingV1;
        flag = true;
        break;
      }
    }
    if (flag)
      this.mStage = HomeScreenHeadquartersInfo.Stage.UnlockedBuilding;
    else
      this.mStage = HomeScreenHeadquartersInfo.Stage.General;
  }

  private void SetStage()
  {
    switch (this.mStage)
    {
      case HomeScreenHeadquartersInfo.Stage.UnlockedBuilding:
        this.titleLabel.text = Localisation.LocaliseID("PSG_10010279", (GameObject) null);
        StringVariableParser.building = this.mBuilding;
        this.descriptionLabel.text = Localisation.LocaliseID("PSG_10010280", (GameObject) null);
        this.buttonLabel.text = this.mBuilding.buildingName;
        this.statusLabel.text = Localisation.LocaliseID("PSG_10010281", (GameObject) null);
        break;
      case HomeScreenHeadquartersInfo.Stage.General:
        this.titleLabel.text = Localisation.LocaliseID("PSG_10002250", (GameObject) null);
        this.descriptionLabel.text = Localisation.LocaliseID("PSG_10010282", (GameObject) null);
        this.buttonLabel.text = Localisation.LocaliseID("PSG_10002250", (GameObject) null);
        this.statusLabel.text = Localisation.LocaliseID("PSG_10005796", (GameObject) null);
        break;
    }
  }

  public override bool isDefaultState()
  {
    return this.mStage == HomeScreenHeadquartersInfo.Stage.General;
  }

  public enum Stage
  {
    UnlockedBuilding,
    General,
  }
}
