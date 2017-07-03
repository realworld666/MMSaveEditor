// Decompiled with JetBrains decompiler
// Type: UIVehicleInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIVehicleInfo : MonoBehaviour
{
  public RectTransform canvasRectTransform;
  public Button selectButton;
  private RectTransform mRectTransform;
  private float mNextUpdateTime;
  private Vehicle mVehicle;

  public Vehicle vehicle
  {
    get
    {
      return this.mVehicle;
    }
  }

  public bool isShowing
  {
    get
    {
      return this.gameObject.activeSelf;
    }
  }

  private void Awake()
  {
    this.Hide();
    this.selectButton.onClick.AddListener(new UnityAction(this.SetCameraTarget));
  }

  private void SetCameraTarget()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mVehicle == null)
      return;
    App.instance.cameraManager.SetTarget(this.mVehicle, CameraManager.Transition.Smooth);
  }

  public void Show(Vehicle inVehicle)
  {
    if (this.gameObject.activeSelf && inVehicle == this.mVehicle)
      return;
    this.mVehicle = inVehicle;
    this.Show();
  }

  public void Show()
  {
    if (this.mVehicle == null)
      return;
    if ((Object) this.mRectTransform == (Object) null)
      this.mRectTransform = this.GetComponent<RectTransform>();
    this.mNextUpdateTime = 0.0f;
    GameUtility.SetActive(this.gameObject, true);
    this.Update();
  }

  public void HideAndClearVehicle()
  {
    this.mVehicle = (Vehicle) null;
    this.Hide();
  }

  public void Hide()
  {
    GameUtility.SetActive(this.gameObject, false);
  }

  private void Update()
  {
    if (this.mVehicle == null)
    {
      this.Hide();
    }
    else
    {
      if ((double) this.mNextUpdateTime >= (double) Time.time)
        return;
      this.mNextUpdateTime = Time.time + 1f;
    }
  }

  public void UpdatePosition()
  {
    if (this.mVehicle == null)
      return;
    bool flag = !App.instance.preferencesManager.videoPreferences.isSimulationRunning2DMode;
    Vector2 zero = Vector2.zero;
    Vector2 vector2 = !flag ? (Vector2) Simulation2D.instance.camera.WorldToViewportPoint(Simulation2D.instance.miniMapWidget.GetWorldPositionOfEntry(this.mVehicle)) : (Vector2) App.instance.cameraManager.GetCamera().WorldToViewportPoint(this.mVehicle.unityTransform.position);
    vector2.x = (float) ((double) vector2.x * (double) this.canvasRectTransform.sizeDelta.x - (double) this.canvasRectTransform.sizeDelta.x * 0.5);
    vector2.y = (float) ((double) vector2.y * (double) this.canvasRectTransform.sizeDelta.y - (double) this.canvasRectTransform.sizeDelta.y * 0.5);
    this.mRectTransform.anchoredPosition = vector2;
  }
}
