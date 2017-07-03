// Decompiled with JetBrains decompiler
// Type: UIHUBStrategyToggle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHUBStrategyToggle : MonoBehaviour
{
  public Fuel.EngineMode engineMode = Fuel.EngineMode.High;
  public UIHUBStrategyToggle.Type type;
  public DrivingStyle.Mode drivingStyle;
  public Toggle toggle;
  public Image graphic;
  public UIHUBStrategyEntry widget;

  public void OnStart()
  {
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle()));
  }

  public void OnToggle()
  {
    if (!this.toggle.isOn)
      return;
    switch (this.type)
    {
      case UIHUBStrategyToggle.Type.DrivingStyle:
        this.widget.SelectDrivingStyle(this.drivingStyle, this.graphic);
        break;
      case UIHUBStrategyToggle.Type.EngineModes:
        this.widget.SelectEngineModes(this.engineMode, this.graphic);
        break;
    }
  }

  public enum Type
  {
    DrivingStyle,
    EngineModes,
  }
}
