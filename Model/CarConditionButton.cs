// Decompiled with JetBrains decompiler
// Type: CarConditionButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class CarConditionButton : MonoBehaviour
{
  public GameObject[] conditionNotifications = new GameObject[0];
  public Toggle toggle;
  private RacingVehicle mVehicle;
  private Car mCar;

  public void SetVehicle(RacingVehicle inVehicle)
  {
    this.mVehicle = inVehicle;
    this.mCar = this.mVehicle.car;
    this.Update();
    for (int index = 0; index < this.conditionNotifications.Length; ++index)
      this.UpdateNotifications(this.conditionNotifications[index]);
  }

  private void Update()
  {
    if (this.mCar == null)
      return;
    for (int index = 0; index < this.conditionNotifications.Length; ++index)
      this.UpdateNotifications(this.conditionNotifications[index]);
  }

  private void UpdateNotifications(GameObject inObject)
  {
    if (this.mCar.isConditionFixed)
      GameUtility.SetActive(inObject, false);
    else if (this.mCar.AreAnyPartsOnRedCondition())
      GameUtility.SetActive(inObject, true);
    else
      GameUtility.SetActive(inObject, false);
  }
}
