// Decompiled with JetBrains decompiler
// Type: WorkshopAssetLoading
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using ModdingSystem;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WorkshopAssetLoading : UIDialogBox
{
  private float mTimeOnEnter;
  private Thread mThread;
  private WorkshopAssetLoading.State mState;
  private bool mErrorOccured;
  private WorkshopAssetLoading.LoadingCompleteAction mLoadingCompleteAction;
  private SaveFileInfo mSaveFileToLoad;

  protected override void Awake()
  {
    base.Awake();
  }

  public void SetLoadingCompleteAction(WorkshopAssetLoading.LoadingCompleteAction inLoadingCompleteAction, SaveFileInfo inSaveFileToLoad = null)
  {
    this.mLoadingCompleteAction = inLoadingCompleteAction;
    this.mSaveFileToLoad = inSaveFileToLoad;
  }

  private void Update()
  {
    if (this.mState == WorkshopAssetLoading.State.WaitingForInstall)
    {
      List<SteamMod> userSubscribedMods = App.instance.modManager.allUserSubscribedMods;
      bool flag = true;
      for (int index = 0; index < userSubscribedMods.Count; ++index)
      {
        SteamMod steamMod = userSubscribedMods[index];
        if (steamMod.isActive && !steamMod.isInstalled)
          flag = false;
      }
      if (!flag)
        return;
      this.mState = WorkshopAssetLoading.State.LoadingAssets;
      this.mThread = new Thread(new ThreadStart(this.LoadAssets));
      this.mThread.Start();
    }
    else
    {
      if (this.mState != WorkshopAssetLoading.State.LoadingAssets || this.mThread == null || ((double) Time.unscaledTime - (double) this.mTimeOnEnter <= 2.0 || this.mThread.IsAlive))
        return;
      this.Hide();
      try
      {
        App.instance.assetManager.ReloadAtlases();
        switch (this.mLoadingCompleteAction)
        {
          case WorkshopAssetLoading.LoadingCompleteAction.CreateNewGame:
            Game.instance.StartNewGame();
            break;
          case WorkshopAssetLoading.LoadingCompleteAction.LoadMostRecentSave:
            App.instance.saveSystem.LoadMostRecentFile();
            break;
          case WorkshopAssetLoading.LoadingCompleteAction.LoadSpecificSave:
            App.instance.saveSystem.Load(this.mSaveFileToLoad, false);
            break;
        }
      }
      catch (Exception ex)
      {
        this.mErrorOccured = true;
        Debug.Log((object) ex.ToString(), (UnityEngine.Object) null);
      }
      App.instance.errorReporter.SetEnabled(true);
      if (this.mErrorOccured)
      {
        UIManager.instance.dialogBoxManager.Show("WorkshopAssetLoadError");
      }
      else
      {
        UIManager.instance.UIBackground.ForceBackgroundChange();
        if (this.mLoadingCompleteAction == WorkshopAssetLoading.LoadingCompleteAction.CreateNewGame)
          UIManager.instance.ChangeScreen("CreatePlayerScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
        else if (this.mLoadingCompleteAction == WorkshopAssetLoading.LoadingCompleteAction.ExitingWorkshopScreen)
          UIManager.instance.ChangeScreen("TitleScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
      }
      this.mLoadingCompleteAction = WorkshopAssetLoading.LoadingCompleteAction.CreateNewGame;
      this.mSaveFileToLoad = (SaveFileInfo) null;
      this.mThread = (Thread) null;
      this.mState = WorkshopAssetLoading.State.Idle;
    }
  }

  protected override void OnEnable()
  {
    base.OnEnable();
    App.instance.errorReporter.SetEnabled(false);
    this.mTimeOnEnter = Time.unscaledTime;
    this.mErrorOccured = false;
    this.mState = WorkshopAssetLoading.State.WaitingForInstall;
  }

  private void LoadAssets()
  {
    try
    {
      App.instance.assetManager.ReloadAssetsAndDatabases();
    }
    catch (Exception ex)
    {
      this.mErrorOccured = true;
      Debug.Log((object) ex.ToString(), (UnityEngine.Object) null);
    }
  }

  public enum LoadingCompleteAction
  {
    CreateNewGame,
    LoadMostRecentSave,
    LoadSpecificSave,
    ExitingWorkshopScreen,
  }

  public enum State
  {
    Idle,
    WaitingForInstall,
    LoadingAssets,
  }
}
