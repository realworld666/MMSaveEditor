using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class Color
{
	public float r;
	public float g;
	public float b;
	public float a;
}