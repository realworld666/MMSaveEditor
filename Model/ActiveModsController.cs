// Decompiled with JetBrains decompiler
// Type: ModdingSystem.ActiveModsController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace ModdingSystem
{
  public class ActiveModsController
  {
    private List<SteamMod> mActiveMods = new List<SteamMod>();
    private ModManager mModManager;
    private bool isStaging;

    public ActiveModsController(ModManager inModManager)
    {
      this.mModManager = inModManager;
    }

    public void RecordActiveMods()
    {
      this.mActiveMods.Clear();
      List<SteamMod> userSubscribedMods = this.mModManager.allUserSubscribedMods;
      for (int index = 0; index < userSubscribedMods.Count; ++index)
      {
        if (userSubscribedMods[index].isActive)
          this.mActiveMods.Add(userSubscribedMods[index]);
      }
      this.isStaging = this.mModManager.modPublisher.isStaging;
    }

    public bool ActiveModsHaveChangedSinceLastRecording()
    {
      if (this.isStaging != this.mModManager.modPublisher.isStaging && this.mModManager.modPublisher.stagingMod.IsAssetLoadRequired())
        return true;
      bool flag1 = false;
      List<SteamMod> userSubscribedMods = this.mModManager.allUserSubscribedMods;
      for (int index1 = 0; index1 < userSubscribedMods.Count; ++index1)
      {
        SteamMod steamMod = userSubscribedMods[index1];
        bool flag2 = false;
        for (int index2 = 0; index2 < this.mActiveMods.Count; ++index2)
        {
          if ((long) this.mActiveMods[index2].modID == (long) steamMod.modID)
          {
            flag2 = true;
            break;
          }
        }
        if ((steamMod.isActive && !flag2 || !steamMod.isActive && flag2) && steamMod.IsAssetLoadRequired())
        {
          flag1 = true;
          break;
        }
      }
      if (!flag1)
      {
        for (int index1 = 0; index1 < this.mActiveMods.Count; ++index1)
        {
          bool flag2 = false;
          for (int index2 = 0; index2 < userSubscribedMods.Count; ++index2)
          {
            if ((long) userSubscribedMods[index2].modID == (long) this.mActiveMods[index1].modID)
            {
              flag2 = true;
              break;
            }
          }
          if (!flag2 && this.mActiveMods[index1].IsAssetLoadRequired())
          {
            flag1 = true;
            break;
          }
        }
      }
      this.mActiveMods.Clear();
      return flag1;
    }
  }
}
