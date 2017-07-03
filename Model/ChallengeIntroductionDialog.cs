// Decompiled with JetBrains decompiler
// Type: ChallengeIntroductionDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ChallengeIntroductionDialog : UIDialogBox
{
  public UICharacterPortrait driverPortrait;
  public UICharacterPortrait secondDriverPortrait;

  protected override void OnEnable()
  {
    base.OnEnable();
    if ((Object) this.driverPortrait != (Object) null)
      this.driverPortrait.SetPortrait((Person) Game.instance.player.team.GetDriver(0));
    if (!((Object) this.secondDriverPortrait != (Object) null))
      return;
    this.secondDriverPortrait.SetPortrait((Person) Game.instance.player.team.GetDriver(1));
  }
}
