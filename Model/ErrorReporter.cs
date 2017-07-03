// Decompiled with JetBrains decompiler
// Type: ErrorReporter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class ErrorReporter : MonoBehaviour
{
  private bool mIsEnabled = true;

  public bool isEnabled
  {
    get
    {
      return this.mIsEnabled;
    }
  }

  private void OnEnable()
  {
    Application.logMessageReceived += new Application.LogCallback(this.LogMessage);
  }

  private void OnDisable()
  {
    Application.logMessageReceived -= new Application.LogCallback(this.LogMessage);
  }

  private void LogMessage(string inMessage, string inStackTrace, LogType inLogType)
  {
    if (!this.mIsEnabled || inLogType != LogType.Exception && inLogType != LogType.Error && inLogType != LogType.Assert || inStackTrace == string.Empty)
      return;
    if (Application.isEditor)
    {
      ErrorReporterDialog.Show(inMessage, inStackTrace, inLogType);
    }
    else
    {
      if (inLogType != LogType.Exception)
        return;
      if (App.instance.modManager.GetNewGameModsCount(true) > 0)
        this.ShowGenericConfirmationModsError();
      else
        Application.Quit();
    }
  }

  private void ShowGenericConfirmationModsError()
  {
    string inTitle = Localisation.LocaliseID("PSG_10012152", (GameObject) null);
    string inText = Localisation.LocaliseID("PSG_10012153", (GameObject) null);
    string inConfirmString = Localisation.LocaliseID("PSG_10012154", (GameObject) null);
    GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
    Action inConfirmAction = (Action) (() => Application.Quit());
    dialog.Show((Action) null, string.Empty, inConfirmAction, inConfirmString, inText, inTitle);
  }

  public void SetEnabled(bool inIsEnabled)
  {
    this.mIsEnabled = inIsEnabled;
  }
}
