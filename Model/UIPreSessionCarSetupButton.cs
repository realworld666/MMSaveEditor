// Decompiled with JetBrains decompiler
// Type: UIPreSessionCarSetupButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPreSessionCarSetupButton : MonoBehaviour, IEventSystemHandler, IPointerClickHandler
{
  public UIPreSessionCarSetupButton.VehicleNum vehicleTarget;

  public void OnPointerClick(PointerEventData inEventData)
  {
    UIManager.instance.GetScreen<PitScreen>().Setup(Game.instance.vehicleManager.GetPlayerVehicles()[(int) this.vehicleTarget], PitScreen.Mode.PreSession);
    UIManager.instance.ChangeScreen("PitScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  public enum VehicleNum
  {
    One,
    Two,
  }
}
