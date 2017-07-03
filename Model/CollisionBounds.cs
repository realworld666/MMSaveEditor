using FullSerializer;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class CollisionBounds
{
	private List<Vector2> mPoints = new List<Vector2>();
	private List<Vector2> mEdges = new List<Vector2>();
	private Vector2 mForward;
	private Vector2 mPosition;

}
