
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class DialogRule
{
	public List<DialogCriteria> criteriaList = new List<DialogCriteria>();
	public List<DialogCriteria> userData = new List<DialogCriteria>();
	public List<DialogCriteria> remember = new List<DialogCriteria>();
	public string databaseIndex = string.Empty;
	public string localisationID = string.Empty;
	public string gameArea = string.Empty;
	public DialogCriteria who;
	public DialogQuery triggerQuery;

}
