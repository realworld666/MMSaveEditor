// Decompiled with JetBrains decompiler
// Type: UIGridRowWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIGridRowWidget : MonoBehaviour
{
  public UIGridDriverWidget driverPositionOne;
  public UIGridDriverWidget driverPositionTwo;
  public GameObject finishLine;
  public GameObject divider;
  public TextMeshProUGUI rowLabel;

  public void SetupRow(int rowIndex, int driverOnePosition, RaceEventResults.ResultData inReferenceDriverForOne, RaceEventResults.ResultData inReferenceDriverForTwo, RaceEventResults.ResultData inDriverOne, RaceEventResults.ResultData inDriverTwo)
  {
    GameUtility.SetActive(this.finishLine, rowIndex == 0);
    GameUtility.SetActive(this.divider, rowIndex > 0);
    this.driverPositionOne.Setup(inDriverOne, inReferenceDriverForOne, driverOnePosition);
    this.driverPositionTwo.Setup(inDriverTwo, inReferenceDriverForTwo, driverOnePosition + 1);
    StringVariableParser.ordinalNumberString = GameUtility.FormatForPosition(rowIndex + 1, (string) null);
    this.rowLabel.text = Localisation.LocaliseID("PSG_10010584", (GameObject) null);
  }

  public bool HasDriver(Driver inDriver)
  {
    bool flag1 = this.driverPositionOne.driver == inDriver;
    bool flag2 = this.driverPositionTwo.driver == inDriver;
    if (!flag1)
      return flag2;
    return true;
  }
}
