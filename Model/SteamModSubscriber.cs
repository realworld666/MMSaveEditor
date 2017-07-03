// Decompiled with JetBrains decompiler
// Type: ModdingSystem.SteamModSubscriber
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using Steamworks;
using System;

namespace ModdingSystem
{
  public class SteamModSubscriber
  {
    public Action OnSubscribeItemAction;
    public Action OnUnsubscribeItemAction;
    public Action OnInstalledItemAction;
    private Callback<RemoteStoragePublishedFileSubscribed_t> mItemSubscribed;
    private Callback<RemoteStoragePublishedFileUnsubscribed_t> mItemUnsubscribed;
    private Callback<ItemInstalled_t> mItemInstalled;
    [NonSerialized]
    private SteamMod mSubscribingModCache;

    public SteamModSubscriber(SteamModFetcher inModFetcher)
    {
      if (!SteamManager.Initialized)
        return;
      this.mItemSubscribed = Callback<RemoteStoragePublishedFileSubscribed_t>.Create(new Callback<RemoteStoragePublishedFileSubscribed_t>.DispatchDelegate(this.OnSubscribeItem));
      this.mItemUnsubscribed = Callback<RemoteStoragePublishedFileUnsubscribed_t>.Create(new Callback<RemoteStoragePublishedFileUnsubscribed_t>.DispatchDelegate(this.OnUnsubscribeItem));
      this.mItemInstalled = Callback<ItemInstalled_t>.Create(new Callback<ItemInstalled_t>.DispatchDelegate(this.OnItemInstalled));
    }

    public void SubscribeItem(SteamMod inModToSubscribe)
    {
      scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
      if (!SteamManager.Initialized)
      {
        Debug.Log((object) "ModManager::SubscribeItem - trying to subscribe item when Steam is not Initialized!", (UnityEngine.Object) null);
      }
      else
      {
        this.mSubscribingModCache = inModToSubscribe;
        SteamUGC.SubscribeItem(inModToSubscribe.modDetails.m_nPublishedFileId);
      }
    }

    public void UnsubscribeItem(SteamMod inModToUnsubscribe)
    {
      scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
      if (!SteamManager.Initialized)
        Debug.Log((object) "ModManager::UnsubscribeItem - trying to unsubscribe item when Steam is not Initialized!", (UnityEngine.Object) null);
      else
        SteamUGC.UnsubscribeItem(inModToUnsubscribe.modDetails.m_nPublishedFileId);
    }

    private void OnSubscribeItem(RemoteStoragePublishedFileSubscribed_t publishedFileSubscription)
    {
      if (!(publishedFileSubscription.m_nAppID == SteamManager.MM2AppID))
        return;
      ModManager modManager = App.instance.modManager;
      if (!modManager.IsModAlreadyInList(modManager.allUserSubscribedMods, publishedFileSubscription.m_nPublishedFileId))
      {
        if (this.mSubscribingModCache != null && this.mSubscribingModCache.modDetails.m_nPublishedFileId == publishedFileSubscription.m_nPublishedFileId)
        {
          modManager.allUserSubscribedMods.Add(this.mSubscribingModCache);
          this.mSubscribingModCache.LoadModFolderInfo();
          this.mSubscribingModCache.LoadMetadataForFiles();
          this.mSubscribingModCache = (SteamMod) null;
        }
        else
        {
          PublishedFileId_t[] inPublishedFileIDs = new PublishedFileId_t[1]{ publishedFileSubscription.m_nPublishedFileId };
          modManager.modFetcher.GetWorkshopItemsDetails(inPublishedFileIDs, 1U);
        }
      }
      if (this.OnSubscribeItemAction == null)
        return;
      this.OnSubscribeItemAction();
    }

    private void OnUnsubscribeItem(RemoteStoragePublishedFileUnsubscribed_t publishedFileUnsubscribed)
    {
      if (!(publishedFileUnsubscribed.m_nAppID == SteamManager.MM2AppID))
        return;
      this.RemoveUnsubscribedItem(publishedFileUnsubscribed.m_nPublishedFileId);
      if (this.OnUnsubscribeItemAction == null)
        return;
      this.OnUnsubscribeItemAction();
    }

    private void OnItemInstalled(ItemInstalled_t itemInstalled)
    {
      if (!(itemInstalled.m_unAppID == SteamManager.MM2AppID))
        return;
      ModManager modManager = App.instance.modManager;
      if (modManager.IsModAlreadyInList(modManager.allUserSubscribedMods, itemInstalled.m_nPublishedFileId))
      {
        SteamMod subscribedMod = modManager.GetSubscribedMod(itemInstalled.m_nPublishedFileId.m_PublishedFileId);
        subscribedMod.LoadModFolderInfo();
        subscribedMod.LoadMetadataForFiles();
      }
      if (this.OnInstalledItemAction == null)
        return;
      this.OnInstalledItemAction();
    }

    public void RemoveUnsubscribedItem(PublishedFileId_t inSteamModID)
    {
      ModManager modManager = App.instance.modManager;
      for (int index = 0; index < modManager.allUserSubscribedMods.Count; ++index)
      {
        if (modManager.allUserSubscribedMods[index].modDetails.m_nPublishedFileId == inSteamModID)
        {
          modManager.allUserSubscribedMods[index].UnloadCachedAssets();
          modManager.allUserSubscribedMods.RemoveAt(index);
          break;
        }
      }
    }
  }
}
