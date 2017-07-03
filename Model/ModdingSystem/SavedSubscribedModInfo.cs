
using FullSerializer;

namespace ModdingSystem
{
    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class SavedSubscribedModInfo
    {
        private string mName = string.Empty;
        private ulong mID;
        private bool mIsNewGameRequired;

        public string name
        {
            get
            {
                return this.mName;
            }
            set
            {
                this.mName = value;
            }
        }

        public ulong id
        {
            get
            {
                return this.mID;
            }
            set
            {
                this.mID = value;
            }
        }

        public bool isNewGameRequired
        {
            get
            {
                return this.mIsNewGameRequired;
            }
            set
            {
                this.mIsNewGameRequired = value;
            }
        }
    }
}
