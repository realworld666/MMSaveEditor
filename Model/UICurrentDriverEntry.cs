// Decompiled with JetBrains decompiler
// Type: UICurrentDriverEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class UICurrentDriverEntry : UIDriverEntry
{
  private Driver mDriver;

  public override void Setup(Driver inDriver)
  {
    base.Setup(inDriver);
    this.mDriver = inDriver;
  }

  private void OnRenewButtonPress()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    UIManager.instance.ChangeScreen("ContractNegotiationScreen", (Entity) this.driver, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }

  private void OnFireButtonPress()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    FirePopup dialog = UIManager.instance.dialogBoxManager.GetDialog<FirePopup>();
    dialog.Setup(this.mDriver);
    dialog.gameObject.SetActive(true);
  }

  private void OnPromoteButtonPress()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    PromotePopup dialog = UIManager.instance.dialogBoxManager.GetDialog<PromotePopup>();
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
    dialog.Setup((Person) this.mDriver);
  }
}
