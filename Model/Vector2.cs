
using FullSerializer;
namespace UnityEngine
{
    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public struct Vector2
    {
        public float x;
        public float y;
    }
}