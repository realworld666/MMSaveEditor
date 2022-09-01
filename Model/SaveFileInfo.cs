using FullSerializer;
using MM2;
using ModdingSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using MMSaveEditor;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SaveFileInfo
{
    public static Regex autosavePathAllBracketsRegex = new Regex("(( \\(\\d+\\))+)$");
    public static Regex autosavePathLastBracketsRegex = new Regex(" \\(\\d+\\)$");
    public static Regex filenameToDisplayNameRegex = new Regex("^Save(.+?)\\s*\\((\\d+)\\).sav$|Save(.+?).sav$");
    private static Regex autosavePathLastBracketsAndExtensionRegex = new Regex("( \\(\\d+\\))\\.sav$");
    public SaveInfo saveInfo;
    public GameInfo_v2 gameInfo = new GameInfo_v2();
    public List<SavedSubscribedModInfo> subscribedModsInfo = new List<SavedSubscribedModInfo>();
    public List<Dlc> ownedDLCInfo = new List<Dlc>();
    [NonSerialized]
    private FileInfo _fileInfo;
    [NonSerialized]
    private string _displayName;

    public string FilenameNoAutoSave
    {
        get
        {
            return SaveFileInfo.DisplayNameToSaveName(this._displayName);
        }
    }

    public int AutoSaveVersion
    {
        get
        {
            return SaveFileInfo.DisplayNameToAutoSave(this._displayName);
        }
    }

    public bool isBroken { get; set; }

    public FileInfo fileInfo
    {
        get
        {
            return this._fileInfo;
        }
        set
        {
            this._fileInfo = value;
            this._displayName = this.CalculateDisplayName();
        }
    }

    public static SaveFileInfo Create(string name, bool isAutoSave)
    {
        SaveFileInfo saveFileInfo = new SaveFileInfo();
        saveFileInfo.saveInfo = new SaveFileInfo.SaveInfo()
        {
            name = name,
            date = DateTime.Now,
            version = GameVersionNumber.version,
            isAutoSave = isAutoSave
        };
        Game instance = Game.instance;
        saveFileInfo.gameInfo.isChallenge = Game.instance.challengeManager.IsAttemptingChallenge();
        saveFileInfo.gameInfo.gameTime = DateTime.Now;
        saveFileInfo.gameInfo.teamName = instance.player.team.name;
        saveFileInfo.gameInfo.teamLogoID = instance.player.team.teamID;
        saveFileInfo.gameInfo.teamColor = null;//TeamColorManager.Instance.GetColor(instance.player.team.colorID);
        saveFileInfo.gameInfo.championship = instance.player.team.championship.GetChampionshipName(false);
        saveFileInfo.gameInfo.championshipID = instance.player.team.championship.championshipID;
        saveFileInfo.gameInfo.championshipSeries = instance.player.team.championship.series;
        saveFileInfo.gameInfo.playerName = instance.player.name;
        saveFileInfo.gameInfo.playerAge = instance.player.GetAge();
        saveFileInfo.gameInfo.playerGender = instance.player.gender;
        saveFileInfo.gameInfo.playerPortrait = instance.player.portrait;
        saveFileInfo.gameInfo.playerNationality = instance.player.nationality;
        saveFileInfo.gameInfo.teamLogo = !instance.player.team.isCreatedByPlayer ? (TeamLogo)null : instance.player.team.customLogo;
        Team team = instance.player.careerHistory.currentTeam != null ? instance.player.careerHistory.currentTeam : instance.player.careerHistory.previousTeam;
        saveFileInfo.gameInfo.seasonNumber = team.championship.standingsHistory.historyCount + 1;
        saveFileInfo.gameInfo.raceNumber = team.championship.eventNumberForUI;
        saveFileInfo.gameInfo.racesInSeason = team.championship.eventCount;
        for (int index = 0; index < CarManager.carCount; ++index)
            saveFileInfo.gameInfo.playerTeamCarData[index] = instance.player.team != Game.instance.teamManager.nullTeam ? instance.player.team.carManager.GetCar(index).GetDataForCar(index) : new FrontendCarData();
        //if (SteamManager.Initialized)
        //  saveFileInfo.subscribedModsInfo = instance.savedSubscribedModsInfo;
        //for (int index = 0; index < App.instance.dlcManager.allDlc.Count; ++index)
        //  saveFileInfo.ownedDLCInfo.Add(App.instance.dlcManager.allDlc[index]);
        return saveFileInfo;
    }

    public bool IsWorkshopSave()
    {
        return this.subscribedModsInfo.Count > 0;
    }

    public string GetDisplayName()
    {
        return this._displayName;
    }

    public static string DisplayNameToSaveName(string inDisplayName)
    {
        string str = SaveFileInfo.TrimAutoSaves(inDisplayName);
        foreach (char invalidFileNameChar in System.IO.Path.GetInvalidFileNameChars())
            str = str.Replace(invalidFileNameChar, '_');
        foreach (char invalidPathChar in System.IO.Path.GetInvalidPathChars())
            str = str.Replace(invalidPathChar, '_');
        return str.ToLowerInvariant();
    }

    public static string TrimAutoSaves(string inDisplayName)
    {
        Match match = SaveFileInfo.autosavePathAllBracketsRegex.Match(inDisplayName);
        if (match.Success)
            return inDisplayName.Substring(0, match.Index);
        return inDisplayName;
    }

    public static int DisplayNameToAutoSave(string inDisplayName)
    {
        Match match = SaveFileInfo.autosavePathLastBracketsRegex.Match(inDisplayName);
        if (match.Success)
            return Convert.ToInt32(inDisplayName.Substring(match.Index + 2, match.Length - 3));
        return 0;
    }

    private string CalculateDisplayName()
    {
        using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
        {
            StringBuilder stringBuilder = builderSafe.stringBuilder;
            Match match = SaveFileInfo.filenameToDisplayNameRegex.Match(this.fileInfo.Name);
            if (match.Success)
            {
                if (match.Groups[1].Success)
                {
                    stringBuilder.Append(match.Groups[1].Value);
                    stringBuilder.Append(" (");
                    stringBuilder.Append(match.Groups[2].Value);
                    stringBuilder.Append(")");
                }
                else
                    stringBuilder.Append(match.Groups[3].Value);
            }
            return stringBuilder.ToString();
        }
    }

    public string GetPathOfMainSaveIfExists()
    {
        Match match = SaveFileInfo.autosavePathLastBracketsAndExtensionRegex.Match(this.fileInfo.FullName);
        if (match.Success)
            return this.fileInfo.FullName.Substring(0, match.Index) + ".sav";
        return this.fileInfo.FullName;
    }

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class SaveInfo
    {
        public DateTime date;
        public string name;
        public VersionNumber version;
        public bool isAutoSave;

        public string sanitisedName
        {
            get
            {
                return this.sanitisedName;
            }
        }

        public string GetFilename(int index = -1)
        {
            StringBuilder stringBuilder = new StringBuilder("Save", 260);
            stringBuilder.Append(this.sanitisedName);
            if (index != -1)
                stringBuilder.AppendFormat(" ({0})", (object)index);
            stringBuilder.Append(".sav");
            return stringBuilder.ToString();
        }
    }

    [fsObject("v2", new Type[] { typeof(GameInfo_v1) }, MemberSerialization = fsMemberSerialization.OptOut)]
    public class GameInfo_v2
    {
        public bool isChallenge;
        public DateTime gameTime;
        public string teamName;
        public int teamLogoID;
        public TeamColor teamColor;
        public string championship;
        public int championshipID;
        public Championship.Series championshipSeries;
        public string playerName;
        public int playerAge;
        public Person.Gender playerGender;
        public Portrait playerPortrait;
        public Nationality playerNationality;
        public FrontendCarData[] playerTeamCarData;
        public int seasonNumber;
        public int raceNumber;
        public int racesInSeason;
        public TeamLogo teamLogo;

        public GameInfo_v2()
        {
            this.teamName = string.Empty;
            this.teamLogoID = 0;
            this.teamColor = new TeamColor();
            this.championship = string.Empty;
            this.playerName = string.Empty;
            this.playerAge = 24;
            this.playerGender = Person.Gender.Male;
            this.playerPortrait = new Portrait();
            this.playerNationality = new Nationality();
            this.playerTeamCarData = new FrontendCarData[CarManager.carCount];
        }

        public GameInfo_v2(GameInfo_v1 v1)
        {
            this.isChallenge = v1.isChallenge;
            this.gameTime = v1.gameTime;
            this.teamName = v1.teamName;
            this.teamLogoID = v1.teamLogoID;
            this.teamColor = v1.teamColor;
            this.championship = v1.championship;
            this.championshipID = -1;
            this.championshipSeries = Championship.Series.SingleSeaterSeries;
            this.playerName = v1.playerName;
            this.playerAge = v1.playerAge;
            this.playerGender = v1.playerGender;
            this.playerPortrait = v1.playerPortrait;
            this.playerNationality = v1.playerNationality;
            this.playerTeamCarData = v1.playerTeamCarData;
            this.seasonNumber = v1.seasonNumber;
            this.raceNumber = v1.raceNumber;
            this.racesInSeason = v1.racesInSeason;
        }
    }

    [fsObject("v0", new Type[] { }, MemberSerialization = fsMemberSerialization.OptOut)]
    public class GameInfo
    {
        public string teamName = string.Empty;
        public TeamColor teamColor = new TeamColor();
        public string championship = string.Empty;
        public string playerName = string.Empty;
        public Portrait playerPortrait = new Portrait();
        public Nationality playerNationality = new Nationality();
        public FrontendCarData[] playerTeamCarData = new FrontendCarData[CarManager.carCount];
        public bool isChallenge;
        public DateTime gameTime;
        public int teamLogoID;
        public Person.Gender playerGender;
        public int seasonNumber;
        public int raceNumber;
        public int racesInSeason;
    }

    [fsObject("v1", new Type[] { typeof(GameInfo) }, MemberSerialization = fsMemberSerialization.OptOut)]
    public class GameInfo_v1
    {
        public bool isChallenge;
        public DateTime gameTime;
        public string teamName;
        public int teamLogoID;
        public TeamColor teamColor;
        public string championship;
        public string playerName;
        public int playerAge;
        public Person.Gender playerGender;
        public Portrait playerPortrait;
        public Nationality playerNationality;
        public FrontendCarData[] playerTeamCarData;
        public int seasonNumber;
        public int raceNumber;
        public int racesInSeason;
        public TeamLogo teamLogo;

        public GameInfo_v1(GameInfo v0)
        {
            this.isChallenge = v0.isChallenge;
            this.gameTime = v0.gameTime;
            this.teamName = v0.teamName;
            this.teamLogoID = v0.teamLogoID;
            this.teamColor = v0.teamColor;
            this.championship = v0.championship;
            this.playerName = v0.playerName;
            this.playerAge = 24;
            this.playerGender = v0.playerGender;
            this.playerPortrait = v0.playerPortrait;
            this.playerNationality = v0.playerNationality;
            this.playerTeamCarData = v0.playerTeamCarData;
            this.seasonNumber = v0.seasonNumber;
            this.raceNumber = v0.raceNumber;
            this.racesInSeason = v0.racesInSeason;
        }
    }
}
