
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class LiveryData
{
    public static LiveryData defaultLivery = new LiveryData() { id = 0, friendlyNameInt = 0, chassis = new LiveryData.LiveryPartData() { baseLiveryTexture = "GP1/Livery5/LiveryBase", detailLiveryTexture = "GP1/Livery5/LiveryDetail", baseProjection = LiveryData.LiveryShaderBaseProjection.Side, detailProjection = LiveryData.LiveryShaderDetailProjection.Top, baseIgnoreForSponsor = false, detailIgnoreForSponsor = false, baseXTile = 1f, baseXOffset = 1f, baseYTile = 1f, baseYOffset = 1f, detailXTile = 1f, detailXOffset = 1f, detailYTile = 1f, detailYOffset = 1f }, frontWing = new LiveryData.LiveryPartData() { baseLiveryTexture = "GP1/Shared/FrontWing/FrontWing1", detailLiveryTexture = "GP1/Shared/FrontWing/FrontWing1", baseProjection = LiveryData.LiveryShaderBaseProjection.Side, detailProjection = LiveryData.LiveryShaderDetailProjection.Top, baseIgnoreForSponsor = false, detailIgnoreForSponsor = false, baseXTile = 1f, baseXOffset = 1f, baseYTile = 1f, baseYOffset = 1f, detailXTile = 1f, detailXOffset = 1f, detailYTile = 1f, detailYOffset = 1f }, rearWing = new LiveryData.LiveryPartData() { baseLiveryTexture = "GP1/Shared/RearWing/RearWing1", detailLiveryTexture = "GP1/Shared/RearWing/RearWing1", baseProjection = LiveryData.LiveryShaderBaseProjection.Side, detailProjection = LiveryData.LiveryShaderDetailProjection.Top, baseIgnoreForSponsor = false, detailIgnoreForSponsor = false, baseXTile = 1f, baseXOffset = 1f, baseYTile = 1f, baseYOffset = 1f, detailXTile = 1f, detailXOffset = 1f, detailYTile = 1f, detailYOffset = 1f } };
    public LiveryData.LiveryPartData chassis = new LiveryData.LiveryPartData();
    public LiveryData.LiveryPartData frontWing = new LiveryData.LiveryPartData();
    public LiveryData.LiveryPartData rearWing = new LiveryData.LiveryPartData();
    public int[] championshipID = new int[0];
    public int id;
    public int friendlyNameInt;
    private int mDlcId;



    public enum LiveryShaderBaseProjection
    {
        Top,
        Side,
    }

    public enum LiveryShaderDetailProjection
    {
        Top,
        Side,
        Both,
    }

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class LiveryPartData
    {
        public string baseLiveryTexture = string.Empty;
        public string detailLiveryTexture = string.Empty;
        public LiveryData.LiveryShaderBaseProjection baseProjection;
        public LiveryData.LiveryShaderDetailProjection detailProjection;
        public bool baseIgnoreForSponsor;
        public bool detailIgnoreForSponsor;
        public float baseXTile;
        public float baseXOffset;
        public float baseYTile;
        public float baseYOffset;
        public float detailXTile;
        public float detailXOffset;
        public float detailYTile;
        public float detailYOffset;
        public int dlcId;
        public int liveryId = -1;
        public string customAssetBundleName;

    }
}
