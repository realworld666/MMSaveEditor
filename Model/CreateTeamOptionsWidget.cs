// Decompiled with JetBrains decompiler
// Type: CreateTeamOptionsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class CreateTeamOptionsWidget : MonoBehaviour
{
  public CreateTeamStep[] steps;
  public ToggleGroup toggleGroup;

  public void OnStart()
  {
    for (int index = 0; index < this.steps.Length; ++index)
      this.steps[index].OnStart();
  }

  public void Setup()
  {
    this.EnableStepsSound(false);
    this.toggleGroup.SetAllTogglesOff();
    for (int index = 0; index < this.steps.Length; ++index)
      this.steps[index].Setup();
    this.GoToNextStep();
    this.EnableStepsSound(true);
  }

  public void OnExit()
  {
    for (int index = 0; index < this.steps.Length; ++index)
      this.steps[index].OnExit();
  }

  public void GoToNextStep()
  {
    bool flag = false;
    for (int index = 0; index < this.steps.Length; ++index)
    {
      if (this.steps[index].isIncomplete)
      {
        this.steps[index].toggle.isOn = true;
        this.steps[index].Highlight();
        flag = true;
        break;
      }
      if (!this.steps[index].isComplete)
      {
        this.steps[index].toggle.isOn = true;
        flag = true;
        break;
      }
    }
    if (flag || this.steps.Length <= 0)
      return;
    this.steps[0].toggle.isOn = true;
  }

  public bool StepsComplete()
  {
    for (int index = 0; index < this.steps.Length; ++index)
    {
      if (!this.steps[index].isComplete)
        return false;
    }
    return true;
  }

  private void EnableStepsSound(bool inValue)
  {
    for (int index = 0; index < this.steps.Length; ++index)
      this.steps[index].allowMenuSounds = inValue;
  }
}
