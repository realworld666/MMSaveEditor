namespace ModdingSystem
{
    public class ModDatabaseFileInfo : ModFileInfo
    {
        public static readonly string[] databaseTags = new string[9] { "General", "Drivers", "Staff", "Sponsors", "Teams", "Politics", "Chairmen", "Media", "Weather" };
        public static readonly string[] databaseTagsIDs = new string[9] { "PSG_10005800", "PSG_10006093", "PSG_10003781", "PSG_10003782", "PSG_10002271", "PSG_10003783", "PSG_10003555", "PSG_10001563", "PSG_10004341" };
        private ModDatabaseFileInfo.DatabaseType mDatabaseType = ModDatabaseFileInfo.DatabaseType.None;

        public enum DatabaseType
        {
            Teams,
            Championships,
            Drivers,
            Engineers,
            Mechanics,
            Sponsors,
            TeamPrincipals,
            Chairman,
            Assistants,
            Scouts,
            Journalists,
            MediaOutlets,
            Politicians,
            Climate,
            SimulationSettings,
            TeamColours,
            DefaultParts,
            Parts,
            PartSuppliers,
            Chassis,
            HQ,
            PersonalityTraits,
            PartComponents,
            DriverStatsProgression,
            EngineerStatsProgression,
            MechanicStatsProgression,
            Buildings,
            None,
        }
    }
}
