// Decompiled with JetBrains decompiler
// Type: UIPastChemistryEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPastChemistryEntry : MonoBehaviour
{
  public Flag flag;
  public TextMeshProUGUI driverName;
  public TextMeshProUGUI unlocksText;
  public TextMeshProUGUI weeksTogetherText;
  public Button goToDriverButton;

  public void Setup(Driver inDriver, int weeksTogether, int unlocks)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UIPastChemistryEntry.\u003CSetup\u003Ec__AnonStorey9F setupCAnonStorey9F = new UIPastChemistryEntry.\u003CSetup\u003Ec__AnonStorey9F();
    // ISSUE: reference to a compiler-generated field
    setupCAnonStorey9F.inDriver = inDriver;
    // ISSUE: reference to a compiler-generated field
    this.driverName.SetText(setupCAnonStorey9F.inDriver.name);
    // ISSUE: reference to a compiler-generated field
    this.flag.SetNationality(setupCAnonStorey9F.inDriver.nationality);
    this.unlocksText.SetText(unlocks.ToString());
    StringVariableParser.intValue1 = weeksTogether;
    this.weeksTogetherText.text = Localisation.LocaliseID(weeksTogether != 1 ? "PSG_10010612" : "PSG_10010613", (GameObject) null);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated method
    setupCAnonStorey9F.goToPlayerScreen = new Action(setupCAnonStorey9F.\u003C\u003Em__1D8);
    // ISSUE: reference to a compiler-generated method
    this.goToDriverButton.onClick.AddListener(new UnityAction(setupCAnonStorey9F.\u003C\u003Em__1D9));
  }
}
