// Decompiled with JetBrains decompiler
// Type: YellowFlagWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class YellowFlagWidget : FlagWidget
{
  public TextMeshProUGUI label;

  public override void FlagChange()
  {
    base.FlagChange();
  }

  public override void Show()
  {
    base.Show();
    StringVariableParser.intValue1 = Game.instance.sessionManager.yellowFlagSector + 1;
    this.label.text = Localisation.LocaliseID("PSG_10010691", (GameObject) null);
  }

  public override void Hide()
  {
    base.Hide();
  }

  public override void Update()
  {
    base.Update();
  }
}
