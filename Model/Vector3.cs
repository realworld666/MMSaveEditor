using FullSerializer;

namespace UnityEngine
{
    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public struct Vector3
    {
        public float x;
        public float y;
        public float z;
    }
}