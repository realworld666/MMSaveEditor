using System.Collections.Generic;

namespace ModdingSystem
{
    public class BasicMod
    {
        public List<ModModelFileInfo> modModels = new List<ModModelFileInfo>();
        public List<ModDatabaseFileInfo> modDatabases = new List<ModDatabaseFileInfo>();
        public List<ModImageFileInfo> modImages = new List<ModImageFileInfo>();
        public List<ModLogoFileInfo> modLogos = new List<ModLogoFileInfo>();
        public List<ModVideoFileInfo> modVideos = new List<ModVideoFileInfo>();
        private List<ModFileInfo> mModFiles = new List<ModFileInfo>();
        protected const string mModelsFolderName = "Models";
        protected const string mDatabasesFolderName = "Databases";
        protected const string mLogosFolderName = "Logos";
        protected const string mImagesFolderName = "Images";
        protected const string mVideosFolderName = "Videos";
        protected const string mVehicleSubfolderName = "Vehicle";
        protected const string mHQsSubfolderName = "HQs";
        protected const string mTeamMoviesSubfolder = "TeamIntroMovies";
        protected const string mTrackMoviesSubfolder = "TrackMovies";
        protected const string mChampionshipMoviesSubfolder = "ChampionshipMovies";
        protected string mModFolder;
    }
}
