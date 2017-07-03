// Decompiled with JetBrains decompiler
// Type: TeamColorManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using System.Collections.Generic;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TeamColorManager
{
  private int mPlayersID = -1;
  private TeamColor[] mTeamColor;
  private TeamColor mStoredTeamColour;

  public TeamColor[] colours
  {
    get
    {
      return this.mTeamColor;
    }
  }

  public void LoadColorsFromDatabase(Database inDatabase)
  {
    this.mTeamColor = (TeamColor[]) null;
    this.LoadColorsData(inDatabase.teamColourData);
  }

  private void LoadColorsData(List<DatabaseEntry> inColorsData)
  {
    List<DatabaseEntry> databaseEntryList = inColorsData;
    this.mTeamColor = new TeamColor[databaseEntryList.Count];
    for (int index = 0; index < databaseEntryList.Count; ++index)
    {
      DatabaseEntry teamColourDatabaseEntry = databaseEntryList[index];
      this.mTeamColor[index] = TeamColorManager.TeamColourFromDatabaseEntry(teamColourDatabaseEntry);
    }
  }

  private static TeamColor TeamColourFromDatabaseEntry(DatabaseEntry teamColourDatabaseEntry)
  {
    TeamColor teamColor = new TeamColor() { colorID = teamColourDatabaseEntry.GetIntValue("ID"), primaryUIColour = new TeamColor.UIColour() { normal = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue("Normal UI")), highlighted = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue("Highlighted UI")), pressed = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue("Pressed UI")), disabled = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue("Disabled UI")) }, secondaryUIColour = new TeamColor.UIColour() { normal = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue("Secondary Normal UI")), highlighted = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue("Secondary Highlighted UI")), pressed = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue("Secondary Pressed UI")), disabled = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue("Secondary Disabled UI")) }, staffColor = new TeamColor.StaffColour() { primary = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue("Staff Primary")), secondary = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue("Staff Secondary")) }, helmetColor = new TeamColor.HelmetColour() { primary = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue("Helmet Primary")), secondary = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue("Helmet Secondary")), tertiary = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue("Helmet Tertiary")) }, carColor = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue("Car")), livery = new TeamColor.LiveryColour() { primary = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue("Primary")), secondary = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue("Secondary")), tertiary = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue("Tertiary")), trim = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue("Trim")), lightSponsor = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue("Light Sponsor 1")), darkSponsor = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue("Dark Sponsor 1")) } };
    teamColor.primaryLiveryOption = teamColor.livery.primary;
    teamColor.secondaryLiveryOption = teamColor.livery.secondary;
    teamColor.tertiaryLiveryOption = teamColor.livery.tertiary;
    teamColor.trimLiveryOption = teamColor.livery.trim;
    int length1 = 10;
    teamColor.liveryEditorOptions = new Color[length1];
    for (int index = 0; index < length1; ++index)
      teamColor.liveryEditorOptions[index] = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue(string.Format("Livery Color {0}", (object) (index + 1))));
    int length2 = 4;
    teamColor.lighSponsorOptions = new Color[length2];
    for (int index = 0; index < length2; ++index)
      teamColor.lighSponsorOptions[index] = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue(string.Format("Light Sponsor {0}", (object) (index + 1))));
    teamColor.darkSponsorOptions = new Color[length2];
    for (int index = 0; index < length2; ++index)
      teamColor.darkSponsorOptions[index] = GameUtility.HexStringToColour(teamColourDatabaseEntry.GetStringValue(string.Format("Dark Sponsor {0}", (object) (index + 1))));
    return teamColor;
  }

  public void SetPlayersColourFromSave(TeamColor team_colour)
  {
    if (team_colour == null)
      return;
    this.mStoredTeamColour = team_colour;
  }

  public void OnLoad(int id)
  {
    if (this.mStoredTeamColour == null || id < 0 || id >= this.mTeamColor.Length)
      return;
    this.mPlayersID = id;
    TeamColor teamColor = this.mTeamColor[this.mPlayersID];
    this.mTeamColor[this.mPlayersID] = this.mStoredTeamColour;
    this.mStoredTeamColour = teamColor;
  }

  public void ResetTeamColour()
  {
    this.mStoredTeamColour = (TeamColor) null;
    this.mPlayersID = -1;
  }

  public TeamColor GetColor(int id)
  {
    if (this.mTeamColor.Length == 0)
      UnityEngine.Debug.LogErrorFormat("TeamColorManager.GetColor: attempting to get team colour {0} when there are no team colours in the database", (object) id);
    for (int index = 0; index < this.mTeamColor.Length; ++index)
    {
      if (this.mTeamColor[index].colorID == id)
        return this.mTeamColor[index];
    }
    return this.mTeamColor[0];
  }
}
