namespace ModdingSystem
{
    public class ModImageFileInfo : ModFileInfo
    {
        public static readonly string[] imageTags = new string[4] { "Custom Portraits", "Liveries", "Livery Pack", "Logos" };
        public static readonly string[] imageTagsIDs = new string[4] { "PSG_10012158", "PSG_10012160", "PSG_10011288", "PSG_10012018" };

        private ModImageFileInfo.ImageType mImageType;

        public enum ImageType
        {
            None,
            Liveries,
            Portraits,
            LiveryPack,
            TeamLogos,
            SponsorLogos,
            ChampionshipLogos,
            MediaLogos,
        }
    }
}
