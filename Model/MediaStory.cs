
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class MediaStory
{
    public Person journalist;
    public DialogRule storyRule;
    public DateTime storyDate = new DateTime();
    public List<TextDynamicData> textSpecialData = new List<TextDynamicData>();


}
