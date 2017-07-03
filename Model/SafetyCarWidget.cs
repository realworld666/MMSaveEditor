// Decompiled with JetBrains decompiler
// Type: SafetyCarWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SafetyCarWidget : FlagWidget
{
  public TextMeshProUGUI safetyCarTimer;
  public Button targetSafetyCar;

  private void Awake()
  {
    this.targetSafetyCar.onClick.AddListener(new UnityAction(this.SelectSafetyCar));
  }

  private void SelectSafetyCar()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    App.instance.cameraManager.SetTarget((Vehicle) Game.instance.vehicleManager.safetyVehicle, CameraManager.Transition.Smooth);
  }

  public override void FlagChange()
  {
    base.FlagChange();
  }

  public override void Show()
  {
    base.Show();
  }

  public override void Hide()
  {
    base.Hide();
  }

  public override void Update()
  {
    base.Update();
    AISafetyCarBehaviour behaviour = Game.instance.vehicleManager.safetyVehicle.behaviourManager.GetBehaviour<AISafetyCarBehaviour>();
    switch (behaviour.state)
    {
      case AISafetyCarBehaviour.SafetyCarState.ExitingGarage:
        this.safetyCarTimer.text = Localisation.LocaliseID("PSG_10010688", (GameObject) null);
        break;
      case AISafetyCarBehaviour.SafetyCarState.OnGoing:
        int num = Game.instance.sessionManager.raceDirector.crashDirector.lapsLenghtSafetyCar - behaviour.laps;
        switch (num)
        {
          case 0:
            this.safetyCarTimer.text = Localisation.LocaliseID("PSG_10010689", (GameObject) null);
            return;
          case 1:
            this.safetyCarTimer.text = Localisation.LocaliseID("PSG_10011127", (GameObject) null);
            return;
          default:
            StringVariableParser.intValue1 = num;
            this.safetyCarTimer.text = Localisation.LocaliseID("PSG_10010690", (GameObject) null);
            return;
        }
    }
  }
}
