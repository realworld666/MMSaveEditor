// Decompiled with JetBrains decompiler
// Type: FactoryPartDevelopmentScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;

public class FactoryPartDevelopmentScreen : UIScreen
{
  public UIPartDevStatImprovementWidget[] partStatImprovementWidgets = new UIPartDevStatImprovementWidget[0];
  public UIpartDevImprovementWidget[] partImprovementWidgets = new UIpartDevImprovementWidget[0];
  public UIPartDevItemsWidget itemsWidget;
  public UIPartDevMechanicAllocationWidget mechanicsWidget;
  private bool mRefreshWidgets;

  public override void OnStart()
  {
    base.OnStart();
    this.PreloadScene();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    Game.instance.player.team.carManager.partImprovement.OnItemListsChangedForUI += new Action(this.RefreshAllWidgets);
    this.itemsWidget.Setup();
    this.mechanicsWidget.Setup();
    for (int index = 0; index < this.partImprovementWidgets.Length; ++index)
      this.partImprovementWidgets[index].Setup();
    for (int index = 0; index < this.partStatImprovementWidgets.Length; ++index)
      this.partStatImprovementWidgets[index].Setup();
    this.showNavigationBars = true;
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionFactory, 0.0f);
  }

  public override void OnExit()
  {
    base.OnExit();
    Game.instance.player.team.carManager.partImprovement.OnItemListsChangedForUI -= new Action(this.RefreshAllWidgets);
  }

  private void PreloadScene()
  {
    if (!Game.IsActive() || Game.instance.player.IsUnemployed())
      return;
    this.itemsWidget.Setup();
    this.mechanicsWidget.Setup();
    for (int index = 0; index < this.partImprovementWidgets.Length; ++index)
      this.partImprovementWidgets[index].Setup();
    for (int index = 0; index < this.partStatImprovementWidgets.Length; ++index)
      this.partStatImprovementWidgets[index].Setup();
  }

  public void Update()
  {
    for (int index = 0; index < this.partStatImprovementWidgets.Length; ++index)
      this.partStatImprovementWidgets[index].UpdateWidget();
  }

  private void LateUpdate()
  {
    if (!this.mRefreshWidgets)
      return;
    this.mechanicsWidget.UpdateMechanicLabels();
    for (int index = 0; index < this.partStatImprovementWidgets.Length; ++index)
    {
      this.partStatImprovementWidgets[index].UpdateWidget();
      this.partStatImprovementWidgets[index].RefreshItemsData();
    }
    this.itemsWidget.RefreshList();
    this.mRefreshWidgets = false;
  }

  public void RefreshAllWidgets()
  {
    if (!this.hasFocus)
      return;
    this.mRefreshWidgets = true;
  }
}
