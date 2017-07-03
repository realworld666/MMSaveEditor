// Decompiled with JetBrains decompiler
// Type: VirtualSafetyCarWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class VirtualSafetyCarWidget : FlagWidget
{
  public TextMeshProUGUI safetyCarTimer;

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
    if ((int) Game.instance.sessionManager.raceDirector.crashDirector.virtualSafetyCarTimer != 1)
    {
      StringVariableParser.intValue1 = (int) Game.instance.sessionManager.raceDirector.crashDirector.virtualSafetyCarTimer;
      this.safetyCarTimer.text = Localisation.LocaliseID("PSG_10010685", (GameObject) null);
    }
    else
      this.safetyCarTimer.text = Localisation.LocaliseID("PSG_10011126", (GameObject) null);
  }
}
