// Decompiled with JetBrains decompiler
// Type: MinimapVehicleEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MinimapVehicleEntry : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
  public Toggle toggle;
  public RectTransform rectTransform;
  protected bool mSelected;
  protected Vehicle mVehicle;

  public bool selected
  {
    get
    {
      return this.mSelected;
    }
  }

  public virtual Vehicle vehicle
  {
    get
    {
      return this.mVehicle;
    }
  }

  public virtual void OnStart()
  {
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle()));
  }

  public virtual void Setup(Vehicle inVehicle, MinimapOptions.ChangeColor inColorOption)
  {
    if (inVehicle == null)
      return;
    this.mVehicle = inVehicle;
  }

  public virtual void SetPosition(Vector2 inMapPosition, int inPosition, bool inDisplay, bool inDisplayDriverNameAlways, bool inRotateVehicleLabelsWithCamera, MinimapOptions.ChangeColor inColorOption)
  {
    if (this.gameObject.activeSelf != inDisplay)
      this.gameObject.SetActive(inDisplay);
    if (inDisplay)
      this.rectTransform.anchoredPosition = inMapPosition;
    if (!((Object) Simulation2D.instance != (Object) null) || !inRotateVehicleLabelsWithCamera)
      return;
    this.transform.localRotation = Simulation2D.instance.camera2D.transform.rotation;
    Vector3 one = Vector3.one;
    one.x = Mathf.Lerp(0.5f, 1f, 1f - Simulation2D.instance.camera2D.zoomNormalized);
    one.y = one.x;
    this.transform.localScale = one * Simulation2D.instance.scale;
  }

  private void OnToggle()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!this.toggle.isOn || this.mVehicle == null)
      return;
    App.instance.cameraManager.SetTarget(this.mVehicle, CameraManager.Transition.Smooth);
  }

  public virtual void OnPointerEnter(PointerEventData eventData)
  {
    this.mSelected = true;
  }

  public virtual void OnPointerExit(PointerEventData eventData)
  {
    this.mSelected = false;
  }
}
