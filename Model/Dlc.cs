public class Dlc
{
    private int mDlcId;
    private uint mAppId;
    private uint mPackageId;
    private string mAssetBundleName;
    private string mFriendlyName;
    private string mLocalisationID;
    private bool mDlcOwned;

    public string localisationID
    {
        get
        {
            return this.mLocalisationID;
        }
    }

    public string assetBundleName
    {
        get
        {
            return this.mAssetBundleName;
        }
    }

    public string friendlyName
    {
        get
        {
            return this.mFriendlyName;
        }
    }

    public int dlcId
    {
        get
        {
            return this.mDlcId;
        }
    }

    public uint packageId
    {
        get
        {
            return this.mPackageId;
        }
    }

    public uint appId
    {
        get
        {
            return this.mAppId;
        }
    }

    public bool isOwned
    {
        get
        {
            return this.mDlcOwned;
        }
    }

    public Dlc(int inDlcId, uint inAppId, uint inPackageId, string inFriendlyName, string inLocalisationID, string inAssetBundleName, bool inIsDlcOwned = false)
    {
        this.mDlcId = inDlcId;
        this.mAppId = inAppId;
        this.mPackageId = inPackageId;
        this.mAssetBundleName = inAssetBundleName;
        this.mFriendlyName = inFriendlyName;
        this.mLocalisationID = inLocalisationID;
        this.mDlcOwned = inIsDlcOwned;
    }
}
