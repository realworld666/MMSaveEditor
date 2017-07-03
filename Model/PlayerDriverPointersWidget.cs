// Decompiled with JetBrains decompiler
// Type: PlayerDriverPointersWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerDriverPointersWidget : MonoBehaviour
{
  public RectTransform canvasRectTransform;
  public RectTransform[] driverHUD;
  public RectTransform[] arrow;
  public UICharacterPortrait[] driverPortrait;
  public Button[] portraitButton;
  private RacingVehicle[] mVehicles;

  private void Awake()
  {
    for (int index = 0; index < this.portraitButton.Length; ++index)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: reference to a compiler-generated method
      this.portraitButton[index].onClick.AddListener(new UnityAction(new PlayerDriverPointersWidget.\u003CAwake\u003Ec__AnonStoreyA9()
      {
        \u003C\u003Ef__this = this,
        driverID = index
      }.\u003C\u003Em__21C));
    }
  }

  private void OnEnable()
  {
    this.mVehicles = Game.instance.vehicleManager.GetPlayerVehicles();
    if (this.mVehicles == null)
      return;
    for (int index = 0; index < this.mVehicles.Length; ++index)
      this.driverPortrait[index].SetPortrait((Person) this.mVehicles[index].driver);
  }

  private void Update()
  {
    if (this.mVehicles == null)
      return;
    Transform transform = App.instance.cameraManager.gameCamera.transform;
    Vector3 position1 = transform.position;
    Vector3 forward = transform.forward;
    position1.y = 0.0f;
    forward.y = 0.0f;
    bool flag1 = !App.instance.preferencesManager.videoPreferences.isSimulationRunning2DMode;
    for (int index = 0; index < this.mVehicles.Length; ++index)
    {
      if (flag1)
      {
        Vector3 position2 = this.mVehicles[index].unityTransform.position;
        Vector3 viewportPoint = App.instance.cameraManager.gameCamera.GetCamera().WorldToViewportPoint(position2);
        bool flag2 = (double) viewportPoint.x < 0.0 || (double) viewportPoint.x > 1.0 || (double) viewportPoint.y < 0.0 || (double) viewportPoint.y > 1.0;
        if (this.mVehicles[index].timer.hasSeenChequeredFlag || this.mVehicles[index].behaviourManager.currentBehaviour.behaviourType == AIBehaviourStateManager.Behaviour.Crashed)
          flag2 = false;
        if (flag2)
        {
          GameUtility.SetActive(this.driverHUD[index].gameObject, true);
          position2.y = 0.0f;
          Vector3 normalized = (position2 - position1).normalized;
          float z = Vector3.Angle(forward, normalized);
          if ((double) Vector3.Cross(forward, normalized).y > 0.0)
            z = -z;
          Vector2 vector2_1 = (Vector2) (Quaternion.Euler(0.0f, 0.0f, z) * (Vector3) Vector2.up * 0.5f);
          vector2_1.x += 0.5f;
          vector2_1.y += 0.5f;
          vector2_1.x = (float) ((double) vector2_1.x * (double) this.canvasRectTransform.sizeDelta.x - (double) this.canvasRectTransform.sizeDelta.x * 0.5);
          vector2_1.y = (float) ((double) vector2_1.y * (double) this.canvasRectTransform.sizeDelta.y - (double) this.canvasRectTransform.sizeDelta.y * 0.5);
          Vector2 vector2_2 = vector2_1 * 0.7f;
          this.driverHUD[index].anchoredPosition = vector2_2;
          this.arrow[index].rotation = Quaternion.Euler(0.0f, 0.0f, z);
        }
        else
          GameUtility.SetActive(this.driverHUD[index].gameObject, false);
      }
      else
        GameUtility.SetActive(this.driverHUD[index].gameObject, false);
    }
  }

  public void OnPortraitButton(int inDriverID)
  {
    if (this.mVehicles == null)
      return;
    App.instance.cameraManager.SetTarget((Vehicle) this.mVehicles[inDriverID], CameraManager.Transition.Smooth);
  }
}
