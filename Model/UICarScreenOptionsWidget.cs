// Decompiled with JetBrains decompiler
// Type: UICarScreenOptionsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UICarScreenOptionsWidget : MonoBehaviour
{
  public UIDesignPartsWidget designPartsWidget;
  public UIFitPartsWidget fitPartsWidget;
  public UIImprovePartsWidget improvePartsWidget;

  public void OnStart()
  {
    this.designPartsWidget.OnStart();
    this.fitPartsWidget.OnStart();
    this.improvePartsWidget.OnStart();
  }

  public void OnEnter()
  {
    this.designPartsWidget.OnEnter();
    this.fitPartsWidget.OnEnter();
    this.improvePartsWidget.OnEnter();
  }

  public void OnExit()
  {
    this.designPartsWidget.OnExit();
    this.fitPartsWidget.OnExit();
    this.improvePartsWidget.OnExit();
  }
}
