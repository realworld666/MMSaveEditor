
using FullSerializer;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Portrait
{
    public static readonly Color[] hairColors;
    public static readonly Color[] skinColors;
    public static readonly string[] hairStylesMale = new string[14] { "PSG_10002348", "PSG_10002349", "PSG_10002350", "PSG_10002351", "PSG_10002352", "PSG_10002353", "PSG_10002354", "PSG_10002355", "PSG_10002356", "PSG_10002357", "PSG_10002358", "PSG_10002359", "PSG_10002360", "PSG_10002361" };
    public static readonly string[] hairStylesFemale = new string[13] { "PSG_10002348", "PSG_10002349", "PSG_10002350", "PSG_10002351", "PSG_10002352", "PSG_10002353", "PSG_10002354", "PSG_10002355", "PSG_10002356", "PSG_10002357", "PSG_10002358", "PSG_10002359", "PSG_10002360" };
    public static readonly string[] hairStylesMaleDriver = new string[7] { "Hair Style 1", "Hair Style 2", "Hair Style 3", "Hair Style 4", "Hair Style 5", "Hair Style 6", "Hair Style 7" };
    public static readonly string[] hairStylesFemaleDriver = new string[7] { "Hair Style 1", "Hair Style 2", "Hair Style 3", "Hair Style 4", "Hair Style 5", "Hair Style 6", "Hair Style 7" };
    public static readonly string[] facialHairMale = new string[12] { "PSG_10010463", "PSG_10009286", "PSG_10009287", "PSG_10009288", "PSG_10009289", "PSG_10002373", "PSG_10002374", "PSG_10002375", "PSG_10002376", "PSG_10010464", "PSG_10010465", "PSG_10010466" };
    public static readonly string[] glassesMale = new string[4] { "PSG_10010467", "PSG_10009290", "PSG_10009291", "PSG_10009292" };
    public static readonly string[] glassesFemale = new string[3] { "PSG_10010467", "PSG_10009290", "PSG_10009291" };
    public static readonly string[] glassesMaleDriver = new string[5] { "No Glasses", "Glasses 1", "Glasses 2", "Glasses 3", "Glasses 4" };
    public static readonly string[] glassesFemaleDriver = new string[5] { "No Glasses", "Glasses 1", "Glasses 2", "Glasses 3", "Glasses 4" };
    public static readonly string[] accessoriesMale = new string[2] { "No Accessory", "Accessory 1" };
    public static readonly string[] accessoriesFemale = new string[2] { "No Accessory", "Accessory 1" };
    public static readonly string[] hatStyles = new string[20] { "Hat Style 1", "Hat Style 2", "Hat Style 3", "Hat Style 4", "Hat Style 5", "Hat Style 6", "Hat Style 7", "Hat Style 8", "Hat Style 9", "Hat Style 10", "Hat Style 11", "Hat Style 12", "Hat Style 13", "Hat Style 14", "Hat Style 15", "Hat Style 16", "Hat Style 17", "Hat Style 18", "Hat Style 19", "Hat Style 20" };
    public static readonly string[] shirtStyles = new string[11] { "Shirt Style 1", "Shirt Style 2", "Shirt Style 3", "Shirt Style 4", "Shirt Style 5", "Shirt Style 6", "Shirt Style 7", "Shirt Style 8", "Shirt Style 9", "Shirt Style 10", "Shirt Style 11" };
    private int mHead;
    private int mFacialHair;
    private int mHair;
    private int mHairColor;
    private int mAccessory;
    private int mGlasses;
    private int mBrows;


    public enum SkinTone
    {
        White,
        Medium,
        Black,
        All,
    }

    public enum HairTone
    {
        Blonde,
        Red,
        Brown,
        Black,
        Grey,
        Dark,
        All,
    }

    public enum AgeGroup
    {
        Young,
        Medium,
        Old,
    }

    public enum FeatureType
    {
        Head,
        FacialHair,
        Hair,
        HairColor,
        Accessory,
        Glasses,
        Brows,
    }
}
