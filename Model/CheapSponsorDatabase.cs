// Decompiled with JetBrains decompiler
// Type: CheapSponsorDatabase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

internal class CheapSponsorDatabase
{
  public List<CheapSponsorDatabase.CheapSponsorData> sponsors { get; private set; }

  public CheapSponsorDatabase(Database database)
  {
    List<DatabaseEntry> sponsorData = database.sponsorData;
    this.sponsors = new List<CheapSponsorDatabase.CheapSponsorData>(sponsorData.Count);
    for (int index = 0; index < sponsorData.Count; ++index)
    {
      DatabaseEntry databaseEntry = sponsorData[index];
      this.sponsors.Add(new CheapSponsorDatabase.CheapSponsorData()
      {
        name = databaseEntry.GetStringValue("Name"),
        logoIndex = databaseEntry.GetIntValue("Logo Index")
      });
    }
  }

  public class CheapSponsorData
  {
    public string name;
    public int logoIndex;
  }
}
