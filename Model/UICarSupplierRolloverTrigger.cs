// Decompiled with JetBrains decompiler
// Type: UICarSupplierRolloverTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class UICarSupplierRolloverTrigger : MonoBehaviour
{
  private bool mActivated;
  private bool mDisplay;
  private Team mTeam;
  private bool mForceCurrentSuppliers;
  private SupplierRolloverWidget mSupplierRolloverWidget;

  public void Setup(Team inTeam, bool inForceCurrentSuppliers = false)
  {
    this.mTeam = inTeam;
    this.mForceCurrentSuppliers = inForceCurrentSuppliers;
    this.mSupplierRolloverWidget = UIManager.instance.dialogBoxManager.GetDialog<SupplierRolloverWidget>();
  }

  private void Update()
  {
    GameUtility.HandlePopup(ref this.mActivated, this.gameObject, new Action(this.ShowRollover), new Action(this.HideRollover));
  }

  private void OnDisable()
  {
    if (!this.mDisplay)
      return;
    this.HideRollover();
  }

  private void ShowRollover()
  {
    this.mSupplierRolloverWidget.Open(this.mTeam, this.mForceCurrentSuppliers);
    this.mDisplay = true;
  }

  private void HideRollover()
  {
    this.mDisplay = false;
    this.mSupplierRolloverWidget.Close();
  }
}
