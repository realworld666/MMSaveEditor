
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class DialogQuery
{
	private static readonly string SOURCE_STRING = "source";
	public static readonly string ERROR_STRING = "Error";
	private static readonly string EQUAL_STRING = " = ";
	private static readonly string SEMICOLON_STRING = ";";
	public List<DialogCriteria> criteriaList = new List<DialogCriteria>( 128 );
	public DialogCriteria who;

}
