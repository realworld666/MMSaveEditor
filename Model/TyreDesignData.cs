﻿
using System.Xml.Serialization;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TyreDesignData
{
    [XmlAttribute("isActive")]
    public bool isActive = true;
    [XmlElement("TyreTemperaturePerformanceModiffier")]
    public float tyreTemperaturePerformanceModiffier = 0.05f;
    [XmlElement("TyreTemperatureWearModiffier")]
    public float tyreTemperatureWearModiffier = 0.05f;
    [XmlElement("TyreSupplierHighTyreWearModiffier")]
    public float tyreSupplierHighTyreWearModiffier = 0.05f;
    [XmlElement("TyreSupplierSpeedBonusMaxTimeCost")]
    public float tyreSupplierSpeedBonusMaxTimeCost = 0.05f;
    [XmlElement("BestTyreSupplierWaterRangeOffset")]
    public float bestTyreSupplierWaterRangeOffset = 0.05f;
    [XmlElement("MaxAdaptabilityWaterRangeOffset")]
    public float maxAdaptabilityWaterRangeOffset = 0.05f;
    [XmlElement("MaxSlickTreadSurfaceWaterRange")]
    public float maxSlickTreadSurfaceWaterRange = 0.2f;
    [XmlElement("MinLightTreadSurfaceWaterRange")]
    public float minLightTreadSurfaceWaterRange = 0.15f;
    [XmlElement("MaxLightTreadSurfaceWaterRange")]
    public float maxLightTreadSurfaceWaterRange = 0.9f;
    [XmlElement("MinHeavyTreadSurfaceWaterRange")]
    public float minHeavyTreadSurfaceWaterRange = 0.8f;
    [XmlElement("MaxHeavyTreadSurfaceWaterRange")]
    public float maxHeavyTreadSurfaceWaterRange = 1f;
    [XmlElement("MinMaxWeatherTemperatureRateChangeClamp")]
    public float minMaxWeatherTemperatureRateChangeClamp = 0.1f;
    [XmlElement("WetsAndIntermediatesTyreCount")]
    public int wetWeatherTyreCount = 5;
    [XmlElement("UltraSofts")]
    public TyreCompoundDesignData ultraSofts = new TyreCompoundDesignData();
    [XmlElement("SuperSofts")]
    public TyreCompoundDesignData superSofts = new TyreCompoundDesignData();
    [XmlElement("Softs")]
    public TyreCompoundDesignData softs = new TyreCompoundDesignData();
    [XmlElement("Mediums")]
    public TyreCompoundDesignData mediums = new TyreCompoundDesignData();
    [XmlElement("Hards")]
    public TyreCompoundDesignData hards = new TyreCompoundDesignData();
    [XmlElement("Inters")]
    public TyreCompoundDesignData inters = new TyreCompoundDesignData();
    [XmlElement("Wets")]
    public TyreCompoundDesignData wets = new TyreCompoundDesignData();
    [XmlElement("MinSlickTreadSurfaceWaterRange")]
    public float minSlickTreadSurfaceWaterRange;
    [XmlElement("WrongTyreCompoundTimeCost")]
    public float wrongTyreCompoundTimeCost;
    [XmlElement("WrongTyreCompoundTyreWearCost")]
    public float wrongTyreCompoundTyreWearCost;
    [XmlElement("LostTyreTimeCost")]
    public float lostTyreTimeCost;
    [XmlElement("MinimumDetachTyreDelay")]
    public float minDetachTyreTimer;
    [XmlElement("MaximumDetachTyreDelay")]
    public float maxDetachTyreTimer;

    public TyreCompoundDesignData GetCompoundData(TyreSet.Compound inCompound)
    {
        switch (inCompound)
        {
            case TyreSet.Compound.SuperSoft:
                return this.superSofts;
            case TyreSet.Compound.Soft:
                return this.softs;
            case TyreSet.Compound.Medium:
                return this.mediums;
            case TyreSet.Compound.Hard:
                return this.hards;
            case TyreSet.Compound.Intermediate:
                return this.inters;
            case TyreSet.Compound.Wet:
                return this.wets;
            case TyreSet.Compound.UltraSoft:
                return this.ultraSofts;
            default:
                return this.ultraSofts;
        }
    }
}
