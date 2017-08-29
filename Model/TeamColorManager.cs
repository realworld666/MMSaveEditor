using FullSerializer;
using System.Collections.Generic;
using System.Diagnostics;

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
        this.mStoredTeamColour = (TeamColor)null;
        this.mPlayersID = -1;
    }

    public TeamColor GetColor(int id)
    {
        if (this.mTeamColor.Length == 0)
            Debug.Assert(false, string.Format("TeamColorManager.GetColor: attempting to get team colour {0} when there are no team colours in the database", (object)id));
        for (int index = 0; index < this.mTeamColor.Length; ++index)
        {
            if (this.mTeamColor[index].colorID == id)
                return this.mTeamColor[index];
        }
        return this.mTeamColor[0];
    }
}
