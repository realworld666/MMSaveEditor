// Decompiled with JetBrains decompiler
// Type: ErrorReporterDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorReporterDialog : UIDialogBox
{
  [SerializeField]
  private TextMeshProUGUI logTypeLabel;
  [SerializeField]
  private TextMeshProUGUI messageLabel;
  [SerializeField]
  private TextMeshProUGUI stackTraceLabel;
  [SerializeField]
  private TextMeshProUGUI actionButtonLabel;
  [SerializeField]
  private Image backingColourImage;
  private LogType logType;
  public static bool disableCompletely;
  public static bool alwaysAllowContinue;

  private bool canBeContinuedFrom
  {
    get
    {
      if (!ErrorReporterDialog.alwaysAllowContinue && !Application.isEditor)
        return this.logType != LogType.Exception;
      return true;
    }
  }

  protected override void Awake()
  {
    base.Awake();
    GameUtility.Assert((UnityEngine.Object) this.logTypeLabel != (UnityEngine.Object) null, "logTypeLabel != null", (UnityEngine.Object) this);
    GameUtility.Assert((UnityEngine.Object) this.messageLabel != (UnityEngine.Object) null, "messageLabel != null", (UnityEngine.Object) this);
    GameUtility.Assert((UnityEngine.Object) this.stackTraceLabel != (UnityEngine.Object) null, "stackTraceLabel != null", (UnityEngine.Object) this);
    GameUtility.Assert((UnityEngine.Object) this.actionButtonLabel != (UnityEngine.Object) null, "actionButtonLabel != null", (UnityEngine.Object) this);
    GameUtility.Assert((UnityEngine.Object) this.backingColourImage != (UnityEngine.Object) null, "backingColourImage != null", (UnityEngine.Object) this);
    this.OnOKButton += new Action(this.OnActionButton);
  }

  public static void Show(string inMessage, string inStackTrace, LogType inLogType)
  {
    if (ErrorReporterDialog.disableCompletely || !ErrorReporterDialog.IsReady())
      return;
    ErrorReporterDialog dialog = UIManager.instance.dialogBoxManager.GetDialog<ErrorReporterDialog>();
    if (dialog.gameObject.activeSelf)
      return;
    dialog.logType = inLogType;
    dialog.logTypeLabel.text = inLogType != LogType.Exception ? "Non-fatal error; this is a bug but can be continued from" : "Exception";
    dialog.messageLabel.text = inMessage;
    dialog.stackTraceLabel.text = inStackTrace;
    dialog.backingColourImage.color = inLogType != LogType.Exception ? new Color(1f, 0.4f, 0.0f, 0.7f) : new Color(1f, 0.0f, 0.0f, 0.7f);
    dialog.actionButtonLabel.text = !dialog.canBeContinuedFrom ? "Exit" : "Continue";
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
  }

  private static bool IsReady()
  {
    if (UIManager.instance.dialogBoxManager != null)
      return UIManager.instance.dialogBoxManager.isReady;
    return false;
  }

  private void OnActionButton()
  {
    if (this.canBeContinuedFrom)
      this.Hide();
    else
      Application.Quit();
  }
}
