// Decompiled with JetBrains decompiler
// Type: SkippableSessionDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;

public class SkippableSessionDialog : UIDialogBox
{
  public TextMeshProUGUI titleLabel;
  public TextMeshProUGUI descriptionLabel;
  public TextMeshProUGUI yesButtonLabel;
  public TextMeshProUGUI noButtonLabel;

  protected override void Awake()
  {
    base.Awake();
  }

  public static void Show(string inTitle, string inDescription, string inYesButtonText, string inNoButtonText, Action inYesAction, Action inNoAction)
  {
    SkippableSessionDialog dialog = UIManager.instance.dialogBoxManager.GetDialog<SkippableSessionDialog>();
    dialog.titleLabel.text = inTitle;
    dialog.descriptionLabel.text = inDescription;
    dialog.OnYesButton += inYesAction;
    dialog.OnNoButton += inNoAction;
    dialog.yesButtonLabel.text = inYesButtonText;
    dialog.noButtonLabel.text = inNoButtonText;
    UIManager.instance.dialogBoxManager.Show("SkippableSessionDialog");
  }
}
