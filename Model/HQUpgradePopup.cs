// Decompiled with JetBrains decompiler
// Type: HQUpgradePopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class HQUpgradePopup : UIDialogBox
{
  public UIHQUpgradePopup upgradeWidget;

  public static void Open(HQsBuilding_v1 inBuilding)
  {
    HQUpgradePopup dialog = UIManager.instance.dialogBoxManager.GetDialog<HQUpgradePopup>();
    dialog.upgradeWidget.Setup(inBuilding);
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
  }

  public static void Close()
  {
    UIManager.instance.dialogBoxManager.GetDialog<HQUpgradePopup>().Hide();
  }
}
