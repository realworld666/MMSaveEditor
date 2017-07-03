
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class CommentaryManager
{
	public List<Comment> commentsHistory = new List<Comment>();
	public Action<Comment> onNewCommentAdded;


}
