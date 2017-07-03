// Decompiled with JetBrains decompiler
// Type: FlyCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FlyCamera : MonoBehaviour
{
  private float mainSpeed = 200f;
  private float shiftAdd = 25f;
  private float maxShift = 100f;
  private float camSens = 0.35f;
  private Vector3 lastMouse = new Vector3((float) (Screen.width / 2), (float) (Screen.height / 2), 0.0f);
  private float totalRun = 1f;

  private void Update()
  {
    this.lastMouse = Input.mousePosition - this.lastMouse;
    this.lastMouse = new Vector3(-this.lastMouse.y * this.camSens, this.lastMouse.x * this.camSens, 0.0f);
    this.lastMouse = new Vector3(this.transform.eulerAngles.x + this.lastMouse.x, this.transform.eulerAngles.y + this.lastMouse.y, 0.0f);
    if (Input.GetMouseButton(1))
      this.transform.eulerAngles = this.lastMouse;
    this.lastMouse = Input.mousePosition;
    Vector3 direction = this.getDirection();
    Vector3 vector3;
    if (Input.GetKey(KeyCode.LeftShift))
    {
      this.totalRun += GameTimer.deltaTime;
      vector3 = direction * this.totalRun * this.shiftAdd;
      vector3.x = Mathf.Clamp(vector3.x, -this.maxShift, this.maxShift);
      vector3.y = Mathf.Clamp(vector3.y, -this.maxShift, this.maxShift);
      vector3.z = Mathf.Clamp(vector3.z, -this.maxShift, this.maxShift);
    }
    else
    {
      this.totalRun = Mathf.Clamp(this.totalRun * 0.5f, 1f, 1000f);
      vector3 = direction * this.mainSpeed;
    }
    Vector3 translation = vector3 * GameTimer.deltaTime;
    Vector3 position = this.transform.position;
    if (Input.GetKey(KeyCode.V))
    {
      this.transform.Translate(translation);
      position.x = this.transform.position.x;
      position.z = this.transform.position.z;
      this.transform.position = position;
    }
    else
      this.transform.Translate(translation);
  }

  private Vector3 getDirection()
  {
    Vector3 vector3 = new Vector3();
    if (Input.GetKey(KeyCode.W))
      vector3 += new Vector3(0.0f, 0.0f, 1f);
    if (Input.GetKey(KeyCode.S))
      vector3 += new Vector3(0.0f, 0.0f, -1f);
    if (Input.GetKey(KeyCode.A))
      vector3 += new Vector3(-1f, 0.0f, 0.0f);
    if (Input.GetKey(KeyCode.D))
      vector3 += new Vector3(1f, 0.0f, 0.0f);
    if (Input.GetKey(KeyCode.R))
      vector3 += new Vector3(0.0f, 1f, 0.0f);
    if (Input.GetKey(KeyCode.F))
      vector3 += new Vector3(0.0f, -1f, 0.0f);
    return vector3;
  }

  public void resetRotation(Vector3 lookAt)
  {
    this.transform.LookAt(lookAt);
  }
}
