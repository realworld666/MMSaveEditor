using FullSerializer;
using System.Collections.Generic;


[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TextDynamicData
{
  public Dictionary<string, string> translatedText = new Dictionary<string, string>();
  public string textID = string.Empty;

}
