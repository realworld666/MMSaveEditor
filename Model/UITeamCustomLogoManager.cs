// Decompiled with JetBrains decompiler
// Type: UITeamCustomLogoManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class UITeamCustomLogoManager : MonoBehaviour
{
  private Dictionary<UITeamCreateLogoStyleEntry, PrefabPool> mPrefabPools = new Dictionary<UITeamCreateLogoStyleEntry, PrefabPool>();
  public UITeamCreateLogo teamCreateLogoPrefab;

  public void OnStart()
  {
    Game.OnNewGame += new Action(this.ManagePrefabPools);
    Game.OnGameDataChanged += new Action(this.ManagePrefabPools);
  }

  public UITeamCreateLogoStyleEntry GetTeamCustomLogoCopy(int inStyleID, GameObject inParent)
  {
    UITeamCreateLogoStyleEntry style = this.teamCreateLogoPrefab.GetStyle(inStyleID);
    UITeamCreateLogoStyleEntry createLogoStyleEntry = (UITeamCreateLogoStyleEntry) null;
    if ((UnityEngine.Object) style != (UnityEngine.Object) null)
    {
      if (!this.mPrefabPools.ContainsKey(style))
        this.mPrefabPools.Add(style, new PrefabPool(style.gameObject, 1, this.transform));
      createLogoStyleEntry = this.mPrefabPools[style].GetInstance<UITeamCreateLogoStyleEntry>();
      GameUtility.SetParent(createLogoStyleEntry.gameObject, inParent, false);
    }
    return createLogoStyleEntry;
  }

  public void ReturnTeamCustomLogo(UITeamCreateLogoStyleEntry inEntry)
  {
    UITeamCreateLogoStyleEntry style = this.teamCreateLogoPrefab.GetStyle(inEntry.styleID);
    if ((UnityEngine.Object) style != (UnityEngine.Object) null && this.mPrefabPools.ContainsKey(style))
    {
      GameUtility.SetParent(inEntry.gameObject, this.gameObject, false);
      this.mPrefabPools[style].ReturnInstance(inEntry.gameObject);
    }
    else
      UnityEngine.Object.Destroy((UnityEngine.Object) inEntry.gameObject);
  }

  private void ManagePrefabPools()
  {
    if (!Game.IsActive())
      return;
    Team team = (Team) null;
    if (Game.instance.player.hasCreatedTeam)
      team = Game.instance.teamManager.GetEntity(Game.instance.player.createdTeamID);
    if (this.mPrefabPools.Count <= 0)
      return;
    List<UITeamCreateLogoStyleEntry> createLogoStyleEntryList = new List<UITeamCreateLogoStyleEntry>((IEnumerable<UITeamCreateLogoStyleEntry>) this.mPrefabPools.Keys);
    int count = createLogoStyleEntryList.Count;
    for (int index = 0; index < count; ++index)
    {
      UITeamCreateLogoStyleEntry key = createLogoStyleEntryList[index];
      if (team == null || key.styleID != team.customLogo.styleID)
      {
        PrefabPool mPrefabPool = this.mPrefabPools[key];
        mPrefabPool.DestroyUnusedInstances();
        if (mPrefabPool.totalCount <= 0)
        {
          mPrefabPool.ResetPool();
          this.mPrefabPools.Remove(key);
        }
      }
    }
  }
}
