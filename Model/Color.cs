namespace UnityEngine
{
    public struct Color
    {
        internal static Color white = new Color(1, 1, 1, 1);
        public float r;
        public float g;
        public float b;
        public float a;

        public Color(int v1, int v2, int v3, int v4)
        {
            r = v1;
            g = v2;
            b = v3;
            a = v4;
        }
    }
}
