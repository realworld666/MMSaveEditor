using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
class Ray2
{
	public Vector2 Center;
	public Vector2 Direction;
}
