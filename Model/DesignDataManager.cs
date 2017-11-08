
using System.IO;
using System.Xml.Serialization;

[XmlRoot("DesignData")]
public class DesignDataManager
{
    [XmlElement("RaceLength")]
    public RaceLengthDesignData raceLengthData = new RaceLengthDesignData();
    [XmlElement("CarStats")]
    public CarStatsDesignData carStatsData = new CarStatsDesignData();
    [XmlElement("Tyre")]
    public TyreDesignData tyreData = new TyreDesignData();
    [XmlElement("ShortGameLength")]
    public DesignData shortGameLength = new DesignData();
    [XmlElement("MediumGameLength")]
    public DesignData mediumGameLength = new DesignData();
    [XmlElement("LongGameLength")]
    public DesignData longGameLength = new DesignData();
    public static DesignDataManager instance;

}
