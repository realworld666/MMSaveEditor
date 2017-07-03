
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class Tweet
{
	public DateTime storyDate = new DateTime();
	public List<TextDynamicData> textSpecialData = new List<TextDynamicData>();
	public Person sender;
	public DialogRule storyRule;
	public SessionDetails.SessionType sessionType;


}
