
using System.Text;
using FullSerializer;

namespace MM2
{
    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class VersionNumber
    {
        private int _major;
        private int _minor;
        private int _patch;
        private string _preReleaseVersion = string.Empty;
        private string _buildMetadata = string.Empty;
        private bool _isDirty = true;
        private string _versionStringCache;

        public int major
        {
            get
            {
                return this._major;
            }
            set
            {
                this._major = value;
                this._isDirty = true;
            }
        }

        public int minor
        {
            get
            {
                return this._minor;
            }
            set
            {
                this._minor = value;
                this._isDirty = true;
            }
        }

        public int patch
        {
            get
            {
                return this._patch;
            }
            set
            {
                this._patch = value;
                this._isDirty = true;
            }
        }

        public string preReleaseVersion
        {
            get
            {
                return this._preReleaseVersion;
            }
            set
            {
                this._preReleaseVersion = value;
                this._isDirty = true;
            }
        }

        public string buildMetadata
        {
            get
            {
                return this._buildMetadata;
            }
            set
            {
                this._buildMetadata = value;
                this._isDirty = true;
            }
        }

        public string fullVersionString
        {
            get
            {
                if (this._isDirty)
                {
                    this._versionStringCache = VersionNumber.CreateVersionString(this.major, this.minor, this.patch, this.preReleaseVersion, this.buildMetadata);
                    this._isDirty = false;
                }
                return this._versionStringCache;
            }
        }

        public override string ToString()
        {
            return this.fullVersionString;
        }

        public static string CreateVersionString(int major, int minor, int patch, string identifier, string buildMetadata)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(major);
            stringBuilder.Append('.');
            stringBuilder.Append(minor);
            stringBuilder.Append('.');
            stringBuilder.Append(patch);
            if (identifier != string.Empty)
            {
                stringBuilder.Append('-');
                stringBuilder.Append(identifier);
            }
            if (buildMetadata != string.Empty)
            {
                stringBuilder.Append('+');
                stringBuilder.Append(buildMetadata);
            }
            return stringBuilder.ToString();
        }
    }
}
