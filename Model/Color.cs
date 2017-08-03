using FullSerializer;

namespace UnityEngine
{
    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public struct Color
    {
        public float r;
        public float g;
        public float b;
        public float a;
    }
}