
using System.Xml.Serialization;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PerformanceDesignData
{
    [XmlAttribute("isActive")]
    public bool isActive = true;
}
