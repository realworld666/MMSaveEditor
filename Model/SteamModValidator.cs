// Decompiled with JetBrains decompiler
// Type: ModdingSystem.SteamModValidator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using Steamworks;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ModdingSystem
{
  public class SteamModValidator
  {
    private ModManager mModManager;

    public SteamModValidator(ModManager inModManager)
    {
      this.mModManager = inModManager;
    }

    public bool ValidateSubscribedModsOnContinueGame(SaveFileInfo inSaveFileInfo, Action onConfirmValidation)
    {
      if (SteamManager.Initialized)
      {
        List<SavedSubscribedModInfo> subscribedModsInfo = inSaveFileInfo.subscribedModsInfo;
        List<SavedSubscribedModInfo> inNotReadyMods = new List<SavedSubscribedModInfo>();
        if (subscribedModsInfo.Count > 0)
        {
          for (int index = 0; index < subscribedModsInfo.Count; ++index)
          {
            if (!this.IsModValid(subscribedModsInfo[index].id))
              inNotReadyMods.Add(subscribedModsInfo[index]);
          }
          if (inNotReadyMods.Count > 0)
          {
            this.SetupConfirmValidationDialogbox(inNotReadyMods, onConfirmValidation);
            return false;
          }
        }
      }
      return true;
    }

    private bool IsModValid(ulong inPublishedID)
    {
      if (!SteamManager.Initialized)
        return true;
      uint inItemState = (uint) (0 | 1) | 4U;
      bool flag1 = SteamMod.HasFlagState((PublishedFileId_t) inPublishedID, inItemState);
      bool flag2 = SteamMod.HasFlagState((PublishedFileId_t) inPublishedID, 8U);
      bool flag3 = SteamMod.HasFlagState((PublishedFileId_t) inPublishedID, 16U) || SteamMod.HasFlagState((PublishedFileId_t) inPublishedID, 32U);
      SteamMod subscribedMod = this.mModManager.GetSubscribedMod(inPublishedID);
      bool flag4 = subscribedMod != null && subscribedMod.isActive;
      if (flag1 && flag4 && !flag2)
        return !flag3;
      return false;
    }

    private void SetupConfirmValidationDialogbox(List<SavedSubscribedModInfo> inNotReadyMods, Action onConfirmValidation)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      SteamModValidator.\u003CSetupConfirmValidationDialogbox\u003Ec__AnonStorey62 dialogboxCAnonStorey62 = new SteamModValidator.\u003CSetupConfirmValidationDialogbox\u003Ec__AnonStorey62();
      // ISSUE: reference to a compiler-generated field
      dialogboxCAnonStorey62.inNotReadyMods = inNotReadyMods;
      // ISSUE: reference to a compiler-generated field
      dialogboxCAnonStorey62.onConfirmValidation = onConfirmValidation;
      // ISSUE: reference to a compiler-generated field
      dialogboxCAnonStorey62.\u003C\u003Ef__this = this;
      GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
      // ISSUE: reference to a compiler-generated field
      Action confirmValidation = dialogboxCAnonStorey62.onConfirmValidation;
      // ISSUE: reference to a compiler-generated method
      Action inConfirmAction = new Action(dialogboxCAnonStorey62.\u003C\u003Em__8C);
      string inTitle = Localisation.LocaliseID("PSG_10012083", (GameObject) null);
      string inCancelString = Localisation.LocaliseID("PSG_10012121", (GameObject) null);
      string inConfirmString = Localisation.LocaliseID("PSG_10012120", (GameObject) null);
      using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
      {
        StringBuilder stringBuilder = builderSafe.stringBuilder;
        stringBuilder.AppendLine(Localisation.LocaliseID("PSG_10012118", (GameObject) null));
        stringBuilder.AppendLine(Localisation.LocaliseID("PSG_10012119", (GameObject) null));
        stringBuilder.AppendLine(Localisation.LocaliseID("PSG_10012086", (GameObject) null));
        // ISSUE: reference to a compiler-generated field
        for (int index = 0; index < dialogboxCAnonStorey62.inNotReadyMods.Count; ++index)
        {
          // ISSUE: reference to a compiler-generated field
          StringVariableParser.modName = dialogboxCAnonStorey62.inNotReadyMods[index].name;
          // ISSUE: reference to a compiler-generated field
          StringVariableParser.modID = dialogboxCAnonStorey62.inNotReadyMods[index].id;
          // ISSUE: reference to a compiler-generated field
          StringVariableParser.modStatus = this.mModManager.GetModStatus(dialogboxCAnonStorey62.inNotReadyMods[index].id);
          stringBuilder.AppendLine(Localisation.LocaliseID("PSG_10012087", (GameObject) null));
        }
        string inText = stringBuilder.ToString();
        dialog.Show(confirmValidation, inCancelString, inConfirmAction, inConfirmString, inText, inTitle);
      }
    }

    private void ForceActivateNotReadyMods(List<SavedSubscribedModInfo> inNotReadyMods)
    {
      for (int index = 0; index < inNotReadyMods.Count; ++index)
      {
        SteamMod subscribedMod = this.mModManager.GetSubscribedMod(inNotReadyMods[index].id);
        if (subscribedMod != null && subscribedMod.isInstalled && subscribedMod.isSubscribed)
          subscribedMod.SetActive(true);
      }
    }

    public bool ValidateModOnNewCareer(Action onConfirmValidation)
    {
      List<SteamMod> userSubscribedMods = this.mModManager.allUserSubscribedMods;
      StagingMod stagingMod = this.mModManager.modPublisher.stagingMod;
      int[] duplicateDatabaseCount = new int[27];
      for (int index1 = 0; index1 < duplicateDatabaseCount.Length; ++index1)
      {
        for (int index2 = 0; index2 < userSubscribedMods.Count; ++index2)
        {
          SteamMod steamMod = userSubscribedMods[index2];
          if (!stagingMod.isStaging && steamMod.isActive && steamMod.modDatabases.Count > 0)
            this.CheckDuplicateDatabases((ModDatabaseFileInfo.DatabaseType) index1, (BasicMod) steamMod, ref duplicateDatabaseCount);
        }
        if (stagingMod.isStaging && stagingMod.modDatabases.Count > 0)
          this.CheckDuplicateDatabases((ModDatabaseFileInfo.DatabaseType) index1, (BasicMod) stagingMod, ref duplicateDatabaseCount);
      }
      bool flag = false;
      for (int index = 0; index < duplicateDatabaseCount.Length; ++index)
      {
        if (duplicateDatabaseCount[index] > 1)
          flag = true;
      }
      if (!flag)
        return true;
      this.SetupDuplicateDatabasesPopup(duplicateDatabaseCount, onConfirmValidation);
      return false;
    }

    private void CheckDuplicateDatabases(ModDatabaseFileInfo.DatabaseType inDatabaseType, BasicMod inBasicMod, ref int[] duplicateDatabaseCount)
    {
      for (int index = 0; index < inBasicMod.modDatabases.Count; ++index)
      {
        if (inBasicMod.modDatabases[index].databaseType == inDatabaseType)
          ++duplicateDatabaseCount[(int) inDatabaseType];
      }
    }

    private void SetupDuplicateDatabasesPopup(int[] inDuplicateDatabasesCount, Action onConfirmationAction)
    {
      GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
      string inTitle = Localisation.LocaliseID("PSG_10012144", (GameObject) null);
      string inConfirmString = Localisation.LocaliseID("PSG_10003119", (GameObject) null);
      string inCancelString = Localisation.LocaliseID("PSG_10001281", (GameObject) null);
      string empty = string.Empty;
      using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
      {
        StringBuilder stringBuilder = builderSafe.stringBuilder;
        stringBuilder.AppendLine(Localisation.LocaliseID("PSG_10012145", (GameObject) null));
        for (int index = 0; index < inDuplicateDatabasesCount.Length; ++index)
        {
          if (inDuplicateDatabasesCount[index] > 1)
          {
            StringVariableParser.ordinalNumberString = inDuplicateDatabasesCount[index].ToString((IFormatProvider) Localisation.numberFormatter);
            StringVariableParser.duplicateDatabaseType = ((ModDatabaseFileInfo.DatabaseType) index).ToString();
            stringBuilder.AppendLine(Localisation.LocaliseID("PSG_10012146", (GameObject) null));
          }
        }
        stringBuilder.AppendLine(Localisation.LocaliseID("PSG_10012147", (GameObject) null));
        empty = stringBuilder.ToString();
      }
      dialog.Show((Action) null, inCancelString, onConfirmationAction, inConfirmString, empty, inTitle);
    }
  }
}
