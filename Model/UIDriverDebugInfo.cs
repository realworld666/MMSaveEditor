// Decompiled with JetBrains decompiler
// Type: UIDriverDebugInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIDriverDebugInfo : MonoBehaviour
{
  public TextMeshProUGUI debugTextMain;
  public TextMeshProUGUI debugTextSecondary;
  private RacingVehicle mVehicle;

  public void ActivateDebug(bool inValue)
  {
    this.gameObject.SetActive(inValue);
  }

  public void Setup(RacingVehicle inVehicle)
  {
    this.mVehicle = inVehicle;
  }

  private void Update()
  {
    if (this.mVehicle == null)
      return;
    this.debugTextMain.text = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}", (object) string.Format("AIBehaviour: {0}", (object) this.mVehicle.behaviourManager.currentBehaviour.behaviourType.ToString()), (object) string.Format("Performance Total: {0}", (object) this.mVehicle.performance.currentPerformance.statsTotal.ToString()), (object) string.Format("SessTime: {0}", (object) this.mVehicle.timer.sessionTime), (object) string.Format("SessDistance: {0}", (object) this.mVehicle.timer.sessionDistanceTraveled), (object) string.Format("TyreWear: {0}", (object) this.mVehicle.setup.tyreSet.GetCondition().ToString("P0")), (object) string.Format("CrashPoints: {0}", (object) this.mVehicle.sessionEvents.crashPoints.ToString("N0")), (object) string.Format("SpinPoints: {0}", (object) this.mVehicle.sessionEvents.spinOutPoints.ToString("N0")), (object) string.Format("LockUpPoints: {0}", (object) this.mVehicle.sessionEvents.lockUpPoints.ToString("N0")));
    string str1 = string.Format("Speed: {0}", (object) this.mVehicle.speed.ToString());
    string str2 = string.Format("Lap Count: {0}", (object) this.mVehicle.timer.lap);
    string str3 = string.Format("Fuel: {0} ({1})", (object) this.mVehicle.performance.fuel.GetFuelLapsRemaining(), (object) this.mVehicle.performance.fuel.GetTargetFuelLapDelta());
    string str4 = "PittingOrders:";
    if (this.mVehicle.strategy.IsGoingToPit())
    {
      string[] strArray = new string[this.mVehicle.strategy.pitOrders.Count];
      for (int index = 0; index < strArray.Length; ++index)
        strArray[index] = this.mVehicle.strategy.pitOrders[index].ToString();
      str4 += string.Join("\n", strArray);
    }
    string[] strArray1 = new string[this.mVehicle.sessionAIOrderController.orders.Count];
    for (int index1 = 0; index1 < strArray1.Length; ++index1)
    {
      SessionAIOrder order = this.mVehicle.sessionAIOrderController.orders[index1];
      strArray1[index1] = string.Format("AIOrder ID:({0} )", (object) order.ID);
      for (int index2 = 0; index2 < order.outputTypes.Count; ++index2)
      {
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ^@strArray1[index1] += string.Format("\n{0}", (object) order.outputTypes[index2]);
      }
    }
    this.debugTextSecondary.text = string.Format("{0}\n{1}\n{2}\n{3}\n{4}", (object) str1, (object) str3, (object) str2, (object) string.Join("\n", strArray1), (object) str4);
  }
}
