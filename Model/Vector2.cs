
using FullSerializer;
namespace UnityEngine
{
    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public struct Vector2
    {
        /// <summary>
        ///   <para>Shorthand for writing Vector2(1, 0).</para>
        /// </summary>
        public static Vector2 right
        {
            get
            {
                return new Vector2(1f, 0.0f);
            }
        }
        /// <summary>
        ///   <para>Shorthand for writing Vector2(0, 0).</para>
        /// </summary>
        public static Vector2 zero
        {
            get
            {
                return new Vector2(0.0f, 0.0f);
            }
        }
        public float x;
        public float y;

        /// <summary>
        ///   <para>Constructs a new vector with given x, y components.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}