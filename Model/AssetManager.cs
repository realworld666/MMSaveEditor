
using System;
using System.Collections.Generic;
using System.IO;
using ModdingSystem;

public class AssetManager
{
    private ModManager _modManager;

    public AssetManager(ModManager modManager)
    {
        this._modManager = modManager;
    }

    public List<DatabaseEntry> ReadDatabase(Database.DatabaseType inDatabaseType)
    {
        switch (inDatabaseType)
        {
            case Database.DatabaseType.Challenges:
                return DatabaseReader.LoadFromFile("Data/Database/Challenges");
            case Database.DatabaseType.Teams:
                return DatabaseReader.LoadFromFile("Data/Database/Teams");
            case Database.DatabaseType.TeamColours:
                return DatabaseReader.LoadFromFile("Data/Database/Team Colours");
            case Database.DatabaseType.Liveries:
                return DatabaseReader.LoadFromFile("Data/Database/Liveries");
            case Database.DatabaseType.CarParts:
                return DatabaseReader.LoadFromFile("Data/Database/Default Parts");
            case Database.DatabaseType.CarPartModels:
                return DatabaseReader.LoadFromFile("Data/Database/Car Part Models");
            case Database.DatabaseType.CarChassis:
                return DatabaseReader.LoadFromFile("Data/Database/Chassis");
            case Database.DatabaseType.Championships:
                return DatabaseReader.LoadFromFile("Data/Database/Championships");
            case Database.DatabaseType.Locations:
                return DatabaseReader.LoadFromFile("Data/Database/Locations");
            case Database.DatabaseType.Climate:
                return DatabaseReader.LoadFromFile("Data/Database/Climate");
            case Database.DatabaseType.Buildings:
                return DatabaseReader.LoadFromFile("Data/Database/Buildings");
            case Database.DatabaseType.Votes:
                return DatabaseReader.LoadFromFile("Data/Database/Rule Changes");
            case Database.DatabaseType.Headquarters:
                return DatabaseReader.LoadFromFile("Data/Database/HQ");
            case Database.DatabaseType.Sponsors:
                return DatabaseReader.LoadFromFile("Data/Database/Sponsors");
            case Database.DatabaseType.Clauses:
                return DatabaseReader.LoadFromFile("Data/Database/Sponsor Clauses");
            case Database.DatabaseType.Suppliers:
                return DatabaseReader.LoadFromFile("Data/Database/Part Suppliers");
            case Database.DatabaseType.PartsSettings:
                return DatabaseReader.LoadFromFile("Data/Database/Parts");
            case Database.DatabaseType.PartComponents:
                return DatabaseReader.LoadFromFile("Data/Database/Part Components");
            case Database.DatabaseType.MediaOutlets:
                return DatabaseReader.LoadFromFile("Data/Database/Media Outlets");
            case Database.DatabaseType.PersonalityTraits:
                return DatabaseReader.LoadFromFile("Data/Database/Personality Traits");
            case Database.DatabaseType.SimulationSettings:
                return DatabaseReader.LoadFromFile("Data/Database/Simulation Settings");
            case Database.DatabaseType.Investors:
                return DatabaseReader.LoadFromFile("Data/Database/Investors");
            case Database.DatabaseType.FrontEnd:
                return DatabaseReader.LoadFromFile("Data/Localisation/Frontend");
            case Database.DatabaseType.MediaStories:
                return DatabaseReader.LoadFromFile("Data/Localisation/MediaReports");
            case Database.DatabaseType.MediaTweets:
                return DatabaseReader.LoadFromFile("Data/Localisation/Tweets");
            case Database.DatabaseType.MediaInterviews:
                return DatabaseReader.LoadFromFile("Data/Localisation/Interviews");
            case Database.DatabaseType.MessageDialogs:
                return DatabaseReader.LoadFromFile("Data/Localisation/Messages");
            case Database.DatabaseType.RaceEventDialogs:
                return DatabaseReader.LoadFromFile("Data/Localisation/RaceEvent");
            case Database.DatabaseType.PreRaceTalkDialogs:
                return DatabaseReader.LoadFromFile("Data/Localisation/PreRaceTalk");
            case Database.DatabaseType.SimulationDialogs:
                return DatabaseReader.LoadFromFile("Data/Localisation/Simulation");
            case Database.DatabaseType.TeamRadioDialogs:
                return DatabaseReader.LoadFromFile("Data/Localisation/TeamRadio");
            case Database.DatabaseType.DilemmaDialogs:
                return DatabaseReader.LoadFromFile("Data/Localisation/Dilemmas");
            case Database.DatabaseType.TutorialDialogs:
                return DatabaseReader.LoadFromFile("Data/Localisation/Tutorials");
            case Database.DatabaseType.SpecialStringTable:
                return DatabaseReader.LoadFromFile("Data/Localisation/Special Gender Cases");
            case Database.DatabaseType.CreateTeamDefaults:
                return DatabaseReader.LoadFromFile("Data/Database/Create Team Defaults");
            case Database.DatabaseType.CreateTeamDrivers:
                return DatabaseReader.LoadFromFile("Data/Database/Create Team Drivers");
            case Database.DatabaseType.PlayerDefaults:
                return DatabaseReader.LoadFromFile("Data/Database/Player Defaults");
            case Database.DatabaseType.Drivers:
                string path = System.IO.Path.Combine(Application.dataPath, "Database/Drivers.csv");
                if (File.Exists(path))
                    return DatabaseReader.LoadFromText(File.ReadAllText(path), path);
                return DatabaseReader.LoadFromFile("Data/Database/Drivers");
            case Database.DatabaseType.TeamAssistants:
                return DatabaseReader.LoadFromFile("Data/Database/Assistants");
            case Database.DatabaseType.Journalists:
                return DatabaseReader.LoadFromFile("Data/Database/Journalists");
            case Database.DatabaseType.Mechanics:
                return DatabaseReader.LoadFromFile("Data/Database/Mechanics");
            case Database.DatabaseType.Scouts:
                return DatabaseReader.LoadFromFile("Data/Database/Scouts");
            case Database.DatabaseType.Engineers:
                return DatabaseReader.LoadFromFile("Data/Database/Engineers");
            case Database.DatabaseType.Chairman:
                return DatabaseReader.LoadFromFile("Data/Database/Chairman");
            case Database.DatabaseType.TeamPrincipals:
                return DatabaseReader.LoadFromFile("Data/Database/Team Principals");
            case Database.DatabaseType.Celebrities:
                return DatabaseReader.LoadFromFile("Data/Database/Celebrities");
            case Database.DatabaseType.PoliticsPresidents:
                return DatabaseReader.LoadFromFile("Data/Database/Politicians");
            case Database.DatabaseType.DriverStatsProgression:
                return DatabaseReader.LoadFromFile("Data/Database/Driver Stat Progression");
            case Database.DatabaseType.EngineerStatsProgression:
                return DatabaseReader.LoadFromFile("Data/Database/Engineer Stat Progression");
            case Database.DatabaseType.MechanicStatsProgression:
                return DatabaseReader.LoadFromFile("Data/Database/Mechanic Stat Progression");
            case Database.DatabaseType.EngineerSessionSetups:
                return DatabaseReader.LoadFromFile("Data/Database/Setups");
            case Database.DatabaseType.TeamExpectationWeightings:
                return DatabaseReader.LoadFromFile("Data/Database/Team Expectation Weightings");
            case Database.DatabaseType.PersonExpectationWeightings:
                return DatabaseReader.LoadFromFile("Data/Database/Person Expectation Weightings");
            case Database.DatabaseType.MechanicBonuses:
                return DatabaseReader.LoadFromFile("Data/Database/Mechanic Bonuses");
            case Database.DatabaseType.TeamAIWeightings:
                return DatabaseReader.LoadFromFile("Data/Database/Team AI Weightings");
            case Database.DatabaseType.Nationality:
                return DatabaseReader.LoadFromFile("Data/Localisation/Nationalities");
            case Database.DatabaseType.SessionAIOrders:
                return DatabaseReader.LoadFromFile("Data/Database/AISessionOrders");
            default:
                return (List<DatabaseEntry>)null;
        }
    }

    public GameObject GetHeadquartersBuilding(HQsBuildingInfo.Type inType, int inLevel, bool inGetBuildingScaffoling = false)
    {
        GameObject inMesh = (GameObject)null;
        string inModelName = !inGetBuildingScaffoling ? inType.ToString() : inType.ToString() + ModModelFileInfo.buildingUpgradesSuffix;
        if (this._modManager.modLoader.GetMesh(ModModelFileInfo.ModelType.HQBuilding, -1, -1, inLevel, inModelName, out inMesh))
            return UnityEngine.Object.Instantiate<GameObject>(inMesh);
        return inMesh;
    }

    public bool IsModdingFrontendCarModel(string inCarPartName, int championshipID, int inTeamID)
    {
        return this._modManager.modLoader.IsModdingFrontendCarModel(inCarPartName, championshipID, inTeamID);
    }

    public void AllowModdingFrontendCarModel(bool inAllow)
    {
        this._modManager.modLoader.allowFrontendCarModding = inAllow;
    }

    public void ReloadAssetsAndDatabases()
    {
        this._modManager.modLoader.LoadAssetsAndDatabasesWithMods();
    }

    public void ReloadAtlases()
    {
        this._modManager.modLoader.ReloadAtlases();
    }

    public void ResetRaceCarModel()
    {
        this._modManager.modLoader.ResetRaceModCar();
        this._modManager.modLoader.ResetRaceModCarVariables();
    }

    public GameObject GetCarMesh(string inCarPartName, int inTeamID, int inChampionshipID)
    {
        GameObject inMesh = (GameObject)null;
        if (this._modManager.modLoader.GetMesh(ModModelFileInfo.ModelType.Vehicle, inChampionshipID, inTeamID, -1, inCarPartName, out inMesh))
            return UnityEngine.Object.Instantiate<GameObject>(inMesh);
        return (GameObject)null;
    }

    public GameObject GetRaceCarModMesh(int championshipID, int inTeamIndex)
    {
        GameObject inRaceModCar = (GameObject)null;
        if (!this._modManager.modLoader.GetRaceModCar(championshipID, inTeamIndex, out inRaceModCar))
            return (GameObject)null;
        GameObject inGameObject = UnityEngine.Object.Instantiate<GameObject>(inRaceModCar);
        GameUtility.SetActive(inGameObject, true);
        return inGameObject;
    }

    public Texture2D GetAtlasTexture(AtlasManager.Atlas inAtlasType)
    {
        Texture2D inTexture = (Texture2D)null;
        if (this._modManager.modLoader.GetAtlasTexture(inAtlasType, out inTexture))
            return inTexture;
        return (Texture2D)null;
    }

    public List<LiveryData> GetCustomLiveryPack()
    {
        List<LiveryData> inLiveryPack = (List<LiveryData>)null;
        if (this._modManager.modLoader.GetModLiveryPack(out inLiveryPack))
            return inLiveryPack;
        return (List<LiveryData>)null;
    }

    public void GetLiveryTexture(string inBaseTextureName, string inDetailsTextureName, string inAssetBundleName, LiveryData.LiveryShaderBaseProjection inBaseProjection, LiveryData.LiveryShaderDetailProjection inDetailProjection, int inDlcId, int inLiveryID, out Texture baseLiveryTexture, out Texture detailLiveryTexture)
    {
        baseLiveryTexture = (Texture)null;
        detailLiveryTexture = (Texture)null;
        if (this._modManager.modLoader.GetTextureImage(ModImageFileInfo.ImageType.LiveryPack, inBaseTextureName, inAssetBundleName, inLiveryID, out baseLiveryTexture) && this._modManager.modLoader.GetTextureImage(ModImageFileInfo.ImageType.LiveryPack, inDetailsTextureName, inAssetBundleName, inLiveryID, out detailLiveryTexture))
            return;
        string inImageName1 = inBaseProjection != LiveryData.LiveryShaderBaseProjection.Side ? ModImageFileInfo.liveryDetailName : ModImageFileInfo.liveryBaseName;
        string inImageName2 = inDetailProjection != LiveryData.LiveryShaderDetailProjection.Top ? ModImageFileInfo.liveryBaseName : ModImageFileInfo.liveryDetailName;
        if (this._modManager.modLoader.GetTextureImage(ModImageFileInfo.ImageType.Liveries, inImageName1, (string)null, inLiveryID, out baseLiveryTexture) && this._modManager.modLoader.GetTextureImage(ModImageFileInfo.ImageType.Liveries, inImageName2, (string)null, inLiveryID, out detailLiveryTexture))
            return;
        if (inBaseTextureName.IndexOf("DLC", StringComparison.OrdinalIgnoreCase) == 0)
        {
            string assetBundleName = DLCManager.GetDlcById(inDlcId).assetBundleName;
            baseLiveryTexture = (Texture)App.instance.assetBundleManager.GetAssetFromBundle(inBaseTextureName + ".psd", assetBundleName);
            detailLiveryTexture = (Texture)App.instance.assetBundleManager.GetAssetFromBundle(inDetailsTextureName + ".psd", assetBundleName);
        }
        else
        {
            baseLiveryTexture = Resources.Load<Texture>("CarCustomisation/LiveryTextures/" + inBaseTextureName);
            detailLiveryTexture = Resources.Load<Texture>("CarCustomisation/LiveryTextures/" + inDetailsTextureName);
        }
    }

    private Texture2D GetTexture(ModImageFileInfo.ImageType inImageType, string inImageName, int inID)
    {
        ModLoader modLoader = this._modManager.modLoader;
        Texture inTexture = (Texture)null;
        if (modLoader.GetTextureImage(inImageType, inImageName, (string)null, inID, out inTexture))
            return inTexture as Texture2D;
        return (Texture2D)null;
    }

    public Texture2D GetPortraitForPerson(Person inPerson)
    {
        return this.GetTexture(ModImageFileInfo.ImageType.Portraits, inPerson.contract.job == Contract.Job.Journalist || inPerson.contract.job == Contract.Job.Fan ? inPerson.contract.job.ToString() : (inPerson.contract.job != Contract.Job.IMAPresident ? inPerson.GetType().ToString() : ModImageFileInfo.presidentPortraitName), inPerson.GetPersonIndexInManager());
    }

    public Texture2D GetSponsorLogo(int inSponsorID, bool inSponsorCar)
    {
        return this.GetTexture(ModImageFileInfo.ImageType.SponsorLogos, !inSponsorCar ? ModImageFileInfo.sponsorImagePrefix : ModImageFileInfo.sponsorImagePrefixCar, inSponsorID - 1);
    }

    public Texture2D GetChampionshipLogo(Championship inChampionship, bool inBlackAndWhite)
    {
        return this.GetTexture(ModImageFileInfo.ImageType.ChampionshipLogos, !inBlackAndWhite ? ModImageFileInfo.championshipImagePrefix : ModImageFileInfo.championshipImagePrefixBW, inChampionship.championshipID);
    }

    public Texture2D GetTeamLogo(int inTeamID, UITeamLogo.TeamType inTeamType, UITeamLogo.Type inImageType)
    {
        string inImageName = string.Empty;
        switch (inImageType)
        {
            case UITeamLogo.Type.Team:
                switch (inTeamType)
                {
                    case UITeamLogo.TeamType.HighRez:
                        inImageName = ModImageFileInfo.teamImagePrefix;
                        break;
                    case UITeamLogo.TeamType.LowRez:
                        inImageName = ModImageFileInfo.teamImagePrefixSmall;
                        break;
                    case UITeamLogo.TeamType.BlackAndWhite:
                        inImageName = ModImageFileInfo.teamImagePrefixBW;
                        break;
                }
            case UITeamLogo.Type.Hat:
                inImageName = ModImageFileInfo.teamImagePrefixHat;
                break;
            case UITeamLogo.Type.Body:
                inImageName = ModImageFileInfo.teamImagePrefixBody;
                break;
        }
        return this.GetTexture(ModImageFileInfo.ImageType.TeamLogos, inImageName, inTeamID);
    }

    public Texture2D GetMediaLogo(int inMediaLogoID)
    {
        return this.GetTexture(ModImageFileInfo.ImageType.MediaLogos, ModImageFileInfo.mediaImagePrefix, inMediaLogoID - 1);
    }

    public string GetCustomMoviePath(string inMovieName)
    {
        string inMovieFullFilePath = (string)null;
        if (this._modManager.modLoader.GetMovieFullFilePath(inMovieName, out inMovieFullFilePath))
            return inMovieFullFilePath;
        return (string)null;
    }
}
