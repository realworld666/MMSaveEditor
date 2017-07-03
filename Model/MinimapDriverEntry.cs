// Decompiled with JetBrains decompiler
// Type: MinimapDriverEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MinimapDriverEntry : MinimapVehicleEntry
{
  protected Color mDefaultColor = Color.gray;
  private int mDriverPosition = -1;
  public Image outerCircle;
  public Image innerCircle;
  public Image hoverTeamStrip;
  public TextMeshProUGUI hoverDriverName;
  public TextMeshProUGUI position;
  public TextMeshProUGUI hoverPosition;
  public CanvasGroup hoverCanvasGroup;
  protected MinimapOptions.ChangeColor mChangeColor;
  protected bool mDisplayDriverNameAlways;
  protected RacingVehicle mVehicle;

  public override Vehicle vehicle
  {
    get
    {
      return (Vehicle) this.mVehicle;
    }
  }

  public override void OnStart()
  {
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle()));
    this.hoverCanvasGroup.alpha = 0.0f;
    this.mDefaultColor = this.innerCircle.color;
  }

  public override void Setup(Vehicle inVehicle, MinimapOptions.ChangeColor inColorOption)
  {
    if (inVehicle == null)
      return;
    this.mVehicle = inVehicle as RacingVehicle;
    Color normal = this.mVehicle.driver.contract.GetTeam().GetTeamColor().primaryUIColour.normal;
    if (((double) normal.r + (double) normal.r + (double) normal.g + (double) normal.g + (double) normal.g + (double) normal.b) / 6.0 > 0.75)
      this.ColorOuterImages(Color.black);
    else
      this.ColorOuterImages(Color.white);
    this.SetDriverColor(normal);
    this.hoverDriverName.text = this.mVehicle.driver.shortName;
    this.hoverTeamStrip.color = normal;
  }

  private void ColorOuterImages(Color inColor)
  {
    Color color = inColor;
    color.a = this.position.color.a;
    this.position.color = color;
    if (!((Object) this.outerCircle != (Object) null))
      return;
    this.outerCircle.color = inColor;
  }

  public override void SetPosition(Vector2 inMapPosition, int inPosition, bool inDisplay, bool inDisplayDriverNameAlways, bool inRotateVehicleLabelsWithCamera, MinimapOptions.ChangeColor inColorOption)
  {
    if (this.gameObject.activeSelf != inDisplay)
      this.gameObject.SetActive(inDisplay);
    if ((Object) Simulation2D.instance != (Object) null && inRotateVehicleLabelsWithCamera)
    {
      this.transform.localRotation = Simulation2D.instance.camera2D.transform.rotation;
      Vector3 one = Vector3.one;
      one.x = Mathf.Lerp(0.5f, 1f, 1f - Simulation2D.instance.camera2D.zoomNormalized);
      one.y = one.x;
      this.transform.localScale = one * Simulation2D.instance.scale;
    }
    this.rectTransform.anchoredPosition = inMapPosition;
    if (this.mDriverPosition != inPosition)
    {
      this.mDriverPosition = inPosition;
      this.position.text = this.mDriverPosition.ToString();
      this.hoverPosition.text = this.mDriverPosition.ToString();
    }
    this.mDisplayDriverNameAlways = inDisplayDriverNameAlways;
    if (this.mDisplayDriverNameAlways && (double) this.hoverCanvasGroup.alpha != 1.0)
      this.hoverCanvasGroup.alpha = 1f;
    else if (!this.mDisplayDriverNameAlways && !this.mSelected && (double) this.hoverCanvasGroup.alpha != 0.0)
      this.hoverCanvasGroup.alpha = 0.0f;
    if (this.mChangeColor == inColorOption)
      return;
    this.mChangeColor = inColorOption;
    this.SetDriverColor(this.mVehicle.driver.contract.GetTeam().GetTeamColor().primaryUIColour.normal);
  }

  private void SetDriverColor(Color inColor)
  {
    bool flag1 = false;
    bool flag2 = false;
    switch (this.mChangeColor)
    {
      case MinimapOptions.ChangeColor.MyDriversOnly:
        flag1 = true;
        break;
      case MinimapOptions.ChangeColor.RivalsOnly:
        flag2 = true;
        break;
      case MinimapOptions.ChangeColor.All:
        flag1 = true;
        flag2 = true;
        break;
    }
    if (this.mVehicle.driver.IsPlayersDriver())
    {
      Image innerCircle = this.innerCircle;
      Color color;
      if (flag1)
      {
        color = inColor;
      }
      else
      {
        Color mDefaultColor = this.mDefaultColor;
        this.innerCircle.color = mDefaultColor;
        color = mDefaultColor;
      }
      innerCircle.color = color;
    }
    else
      this.innerCircle.color = !flag2 ? this.mDefaultColor : inColor;
  }

  private void OnToggle()
  {
    if (!this.toggle.isOn || this.mVehicle == null)
      return;
    App.instance.cameraManager.SetTarget((Vehicle) this.mVehicle, CameraManager.Transition.Smooth);
  }

  public override void OnPointerEnter(PointerEventData eventData)
  {
    this.mSelected = true;
    if (this.mDisplayDriverNameAlways)
      return;
    this.hoverCanvasGroup.alpha = 1f;
  }

  public override void OnPointerExit(PointerEventData eventData)
  {
    this.mSelected = false;
    if (this.mDisplayDriverNameAlways)
      return;
    this.hoverCanvasGroup.alpha = 0.0f;
  }
}
