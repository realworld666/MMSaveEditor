// Decompiled with JetBrains decompiler
// Type: MM2.CrossGameAchievementDataStorage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using ModdingSystem;
using System;
using System.Collections.Generic;
using Wenzil.Console;

namespace MM2
{
  public class CrossGameAchievementDataStorage
  {
    private List<KeyValuePair<string, Circuit.TrackLayout>> _trackLayoutsAvailable = new List<KeyValuePair<string, Circuit.TrackLayout>>();
    private CrossGameAchievementDataStorage.Storage _storage;

    public CrossGameAchievementDataStorage.Storage storage
    {
      get
      {
        return this._storage;
      }
    }

    public List<KeyValuePair<string, Circuit.TrackLayout>> trackLayoutsAvalible
    {
      get
      {
        return this._trackLayoutsAvailable;
      }
    }

    public CrossGameAchievementDataStorage()
    {
      if (!CrossGameAchievementDataStorageSaveFile.saveFileExists)
      {
        Debug.Log((object) "Cross game achievement save file doesn't exist so creating it for the first time", (UnityEngine.Object) null);
        this.Create();
        CrossGameAchievementDataStorageSaveFile.Save(this._storage);
      }
      else
      {
        this._storage = CrossGameAchievementDataStorageSaveFile.Load();
        if (this._storage == null)
        {
          Debug.Log((object) "Cross game achievement save file failed to load, so creating a new one", (UnityEngine.Object) null);
          this.Create();
          CrossGameAchievementDataStorageSaveFile.Save(this._storage);
        }
      }
      this.PopulateTrackLayoutsAvailable();
    }

    public void AddTrackVariationRacedOn(string location, Circuit.TrackLayout layout)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CrossGameAchievementDataStorage.\u003CAddTrackVariationRacedOn\u003Ec__AnonStorey24 racedOnCAnonStorey24 = new CrossGameAchievementDataStorage.\u003CAddTrackVariationRacedOn\u003Ec__AnonStorey24();
      // ISSUE: reference to a compiler-generated field
      racedOnCAnonStorey24.location = location;
      // ISSUE: reference to a compiler-generated field
      racedOnCAnonStorey24.layout = layout;
      // ISSUE: reference to a compiler-generated method
      if (!this._storage.trackLayoutsRacedOn.Exists(new Predicate<KeyValuePair<string, Circuit.TrackLayout>>(racedOnCAnonStorey24.\u003C\u003Em__0)))
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        this._storage.trackLayoutsRacedOn.Add(new KeyValuePair<string, Circuit.TrackLayout>(racedOnCAnonStorey24.location, racedOnCAnonStorey24.layout));
        CrossGameAchievementDataStorageSaveFile.Save(this._storage);
      }
      if (!App.instance.steamAchievementsManager.ShouldCheckToUnlockAchievement(Achievements.AchievementEnum.Race_On_All_Track_Variations) || !this.HasRacedOnAllTrackLayouts())
        return;
      App.instance.steamAchievementsManager.UnlockAchievement(Achievements.AchievementEnum.Race_On_All_Track_Variations);
    }

    private bool HasRacedOnAllTrackLayouts()
    {
      for (int index = 0; index < this._trackLayoutsAvailable.Count; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: reference to a compiler-generated method
        if (!this._storage.trackLayoutsRacedOn.Exists(new Predicate<KeyValuePair<string, Circuit.TrackLayout>>(new CrossGameAchievementDataStorage.\u003CHasRacedOnAllTrackLayouts\u003Ec__AnonStorey25() { layout = this._trackLayoutsAvailable[index] }.\u003C\u003Em__1)))
          return false;
      }
      return true;
    }

    private void Create()
    {
      this._storage = new CrossGameAchievementDataStorage.Storage();
    }

    private void PopulateTrackLayoutsAvailable()
    {
      List<DatabaseEntry> databaseEntryList = new AssetManager((ModManager) null).ReadDatabase(Database.DatabaseType.Locations);
      for (int index = 0; index < databaseEntryList.Count; ++index)
      {
        DatabaseEntry databaseEntry = databaseEntryList[index];
        Circuit.TrackLayout layoutFromString = CircuitManager.GetTrackLayoutFromString(databaseEntry.GetStringValue("Track Layout"));
        this._trackLayoutsAvailable.Add(new KeyValuePair<string, Circuit.TrackLayout>(databaseEntry.GetStringValue("Location"), layoutFromString));
      }
    }

    public static ConsoleCommandResult DisplayTracksRacedOn(params string[] inStrings)
    {
      string inText = string.Empty;
      List<KeyValuePair<string, Circuit.TrackLayout>> trackLayoutsRacedOn = App.instance.crossGameAchievementData.storage.trackLayoutsRacedOn;
      trackLayoutsRacedOn.Sort((Comparison<KeyValuePair<string, Circuit.TrackLayout>>) ((x, y) => x.Key.CompareTo(y.Key)));
      for (int index = 0; index < trackLayoutsRacedOn.Count; ++index)
      {
        if (index % 5 == 0)
          inText += "\n";
        inText = inText + trackLayoutsRacedOn[index].Key + " " + trackLayoutsRacedOn[index].Value.ToString() + "\t";
      }
      UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>().Show((Action) null, (string) null, (Action) null, "OK", inText, "Tracks Raced On");
      return ConsoleCommandResult.Succeeded((string) null);
    }

    public static ConsoleCommandResult DisplayTracksNotRacedOn(params string[] inStrings)
    {
      string inText = string.Empty;
      List<KeyValuePair<string, Circuit.TrackLayout>> trackLayoutsAvalible = App.instance.crossGameAchievementData.trackLayoutsAvalible;
      List<KeyValuePair<string, Circuit.TrackLayout>> trackLayoutsRacedOn = App.instance.crossGameAchievementData.storage.trackLayoutsRacedOn;
      trackLayoutsAvalible.Sort((Comparison<KeyValuePair<string, Circuit.TrackLayout>>) ((x, y) => x.Key.CompareTo(y.Key)));
      int num = 0;
      for (int index = 0; index < trackLayoutsAvalible.Count; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        CrossGameAchievementDataStorage.\u003CDisplayTracksNotRacedOn\u003Ec__AnonStorey26 racedOnCAnonStorey26 = new CrossGameAchievementDataStorage.\u003CDisplayTracksNotRacedOn\u003Ec__AnonStorey26();
        // ISSUE: reference to a compiler-generated field
        racedOnCAnonStorey26.layout = trackLayoutsAvalible[index];
        // ISSUE: reference to a compiler-generated method
        if (!trackLayoutsRacedOn.Exists(new Predicate<KeyValuePair<string, Circuit.TrackLayout>>(racedOnCAnonStorey26.\u003C\u003Em__4)))
        {
          ++num;
          if (num % 5 == 0)
            inText += "\n";
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          inText = inText + racedOnCAnonStorey26.layout.Key + " " + racedOnCAnonStorey26.layout.Value.ToString() + "\t";
        }
      }
      UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>().Show((Action) null, (string) null, (Action) null, "OK", inText, "Tracks Left To Race On");
      return ConsoleCommandResult.Succeeded((string) null);
    }

    public class Storage
    {
      public List<KeyValuePair<string, Circuit.TrackLayout>> trackLayoutsRacedOn = new List<KeyValuePair<string, Circuit.TrackLayout>>();
    }
  }
}
