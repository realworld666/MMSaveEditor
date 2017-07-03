// Decompiled with JetBrains decompiler
// Type: ModdingSystem.SteamModVoter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using Steamworks;
using System;

namespace ModdingSystem
{
  public class SteamModVoter
  {
    public Action<ulong, bool> OnVoteSuccessful;
    public Action<ulong, bool, bool, bool> OnGetVoteSuccessful;
    private CallResult<SetUserItemVoteResult_t> mSetUserItemVote;
    private CallResult<GetUserItemVoteResult_t> mGetUserItemVote;

    public void VoteMod(SteamMod inSteamMod, bool inThumbsUp)
    {
      if (SteamManager.Initialized)
      {
        this.mSetUserItemVote = CallResult<SetUserItemVoteResult_t>.Create((CallResult<SetUserItemVoteResult_t>.APIDispatchDelegate) null);
        this.mSetUserItemVote.Set(SteamUGC.SetUserItemVote(inSteamMod.modDetails.m_nPublishedFileId, inThumbsUp), new CallResult<SetUserItemVoteResult_t>.APIDispatchDelegate(this.OnSetUserItemVote));
      }
      else
        Debug.LogWarning((object) "SteamModVoter::SetUserVote - Failed to vote for mod", (UnityEngine.Object) null);
    }

    public void GetModVote(SteamMod inSteamMod)
    {
      if (SteamManager.Initialized)
      {
        this.mGetUserItemVote = CallResult<GetUserItemVoteResult_t>.Create(new CallResult<GetUserItemVoteResult_t>.APIDispatchDelegate(this.OnGetUserItemVote));
        this.mGetUserItemVote.Set(SteamUGC.GetUserItemVote(inSteamMod.modDetails.m_nPublishedFileId), new CallResult<GetUserItemVoteResult_t>.APIDispatchDelegate(this.OnGetUserItemVote));
      }
      else
        Debug.LogWarning((object) "SteamModVoter::GetUserVote - Failed to get vote for mod", (UnityEngine.Object) null);
    }

    private void OnSetUserItemVote(SetUserItemVoteResult_t callback, bool IOFailure)
    {
      if (callback.m_eResult != EResult.k_EResultOK || IOFailure)
      {
        Debug.LogWarning((object) "SteamModVoter::SetUserVote - Failed to vote for mod", (UnityEngine.Object) null);
      }
      else
      {
        if (this.OnVoteSuccessful == null)
          return;
        this.OnVoteSuccessful(callback.m_nPublishedFileId.m_PublishedFileId, callback.m_bVoteUp);
      }
    }

    private void OnGetUserItemVote(GetUserItemVoteResult_t callback, bool IOFailure)
    {
      if (callback.m_eResult != EResult.k_EResultOK || IOFailure)
      {
        Debug.LogWarning((object) "SteamModVoter::GetUserVote - Failed to get vote for mod", (UnityEngine.Object) null);
      }
      else
      {
        if (this.OnGetVoteSuccessful == null)
          return;
        this.OnGetVoteSuccessful(callback.m_nPublishedFileId.m_PublishedFileId, callback.m_bVotedDown, callback.m_bVotedUp, callback.m_bVoteSkipped);
      }
    }
  }
}
