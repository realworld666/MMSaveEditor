// Decompiled with JetBrains decompiler
// Type: UIPartDesignComponentEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPartDesignComponentEntry : MonoBehaviour
{
  public UIPartComponentIcon icon;
  public TextMeshProUGUI componentName;
  public TextMeshProUGUI componentEffect;
  public Toggle toggle;
  public Animator animator;
  private CarPartDesign mPartDesign;
  private CarPartComponent mComponent;
  private PartDesignScreen mScreen;

  private void Start()
  {
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnToggleValueChanged(value)));
  }

  private void OnEnable()
  {
    if (this.mPartDesign != null)
      this.toggle.isOn = this.mPartDesign.part.components.Contains(this.mComponent);
    this.animator.SetBool("Selected", this.toggle.isOn);
  }

  private void OnToggleValueChanged(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if ((Object) this.mScreen == (Object) null)
      this.mScreen = UIManager.instance.GetScreen<PartDesignScreen>();
    if (inValue && this.mPartDesign.part.hasComponentSlotsAvailable)
      this.animator.SetBool("Selected", true);
    else if (inValue)
      this.toggle.isOn = false;
    else
      this.animator.SetBool("Selected", false);
  }

  public void Setup(CarPartComponent inComponent)
  {
    this.mPartDesign = Game.instance.player.team.carManager.carPartDesign;
    this.mComponent = inComponent;
    this.icon.Setup(this.mComponent);
    this.componentName.text = this.mComponent.GetName(this.mPartDesign.part);
  }
}
