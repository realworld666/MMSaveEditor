using FullSerializer;

namespace UnityEngine
{
    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public struct Vector3
    {
        internal static Vector3 zero = new Vector3(0, 0, 0);
        internal static Vector3 forward = new Vector3(0, 0, 1);
        internal static Vector3 right = new Vector3(1, 0, 0);
        public float x;
        public float y;
        public float z;

        public Vector3(float v1, float v2, float v3)
        {
            x = v1;
            y = v2;
            z = v3;
        }

        #region Operators

        /// <summary>
        /// Addition of two vectors.
        /// </summary>
        /// <param name="v1">Vector3 to be added to </param>
        /// <param name="v2">Vector3 to be added</param>
        /// <returns>Vector3 representing the sum of two Vectors</returns>
        /// <Acknowledgement>This code is adapted from CSOpenGL - Lucas Viñas Livschitz </Acknowledgement>
        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(
                v1.x + v2.x,
                v1.y + v2.y,
                v1.z + v2.z);
        }

        /// <summary>
        /// Subtraction of two vectors.
        /// </summary>
        /// <param name="v1">Vector3 to be subtracted from </param>
        /// <param name="v2">Vector3 to be subtracted</param>
        /// <returns>Vector3 representing the difference of two Vectors</returns>
        /// <Acknowledgement>This code is adapted from CSOpenGL - Lucas Viñas Livschitz </Acknowledgement>
        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(
                v1.x - v2.x,
                v1.y - v2.y,
                v1.z - v2.z);
        }

        /// <summary>
        /// Product of a vector and a scalar value.
        /// </summary>
        /// <param name="v1">Vector3 to be multiplied </param>
        /// <param name="s2">Scalar value to be multiplied by </param>
        /// <returns>Vector3 representing the product of the vector and scalar</returns>
        /// <Acknowledgement>This code is adapted from CSOpenGL - Lucas Viñas Livschitz </Acknowledgement>
        public static Vector3 operator *(Vector3 v1, float s2)
        {
            return
                new Vector3(
                        v1.x * s2,
                        v1.y * s2,
                        v1.z * s2);
        }

        /// <summary>
        /// Product of a scalar value and a vector.
        /// </summary>
        /// <param name="s1">Scalar value to be multiplied </param>
        /// <param name="v2">Vector3 to be multiplied by </param>
        /// <returns>Vector3 representing the product of the scalar and Vector3</returns>
        /// <Acknowledgement>This code is adapted from CSOpenGL - Lucas Viñas Livschitz </Acknowledgement>
        /// <Implementation>
        /// Using the commutative law 'scalar x vector'='vector x scalar'.
        /// Thus, this function calls 'operator*(Vector3 v1, float s2)'.
        /// This avoids repetition of code.
        /// </Implementation>
        public static Vector3 operator *(float s1, Vector3 v2)
        {
            return v2 * s1;
        }

        /// <summary>
        /// Division of a vector and a scalar value.
        /// </summary>
        /// <param name="v1">Vector3 to be divided </param>
        /// <param name="s2">Scalar value to be divided by </param>
        /// <returns>Vector3 representing the division of the vector and scalar</returns>
        /// <Acknowledgement>This code is adapted from CSOpenGL - Lucas Viñas Livschitz </Acknowledgement>
        public static Vector3 operator /(Vector3 v1, float s2)
        {
            return new Vector3(
                        v1.x / s2,
                        v1.y / s2,
                        v1.z / s2);
        }

        /// <summary>
        /// Negation of a vector.
        /// Invert the direction of the Vector3
        /// Make Vector3 negative (-vector)
        /// </summary>
        /// <param name="v1">Vector3 to be negated  </param>
        /// <returns>Negated vector</returns>
        /// <Acknowledgement>This code is adapted from Exocortex - Ben Houston </Acknowledgement>
        public static Vector3 operator -(Vector3 v1)
        {
            return new Vector3(
                -v1.x,
                -v1.y,
                -v1.z);
        }

        /// <summary>
        /// Reinforcement of a vector.
        /// Make Vector3 positive (+vector).
        /// </summary>
        /// <param name="v1">Vector3 to be reinforced </param>
        /// <returns>Reinforced vector</returns>
        /// <Acknowledgement>This code is adapted from Exocortex - Ben Houston </Acknowledgement>
        /// <Implementation>
        /// Using the rules of Addition (i.e. '+-x' = '-x' and '++x' = '+x')
        /// This function actually  does nothing but return the argument as given
        /// </Implementation>
        public static Vector3 operator +(Vector3 v1)
        {
            return new Vector3(
                +v1.x,
                +v1.y,
                +v1.z);
        }


        /// <summary>
        /// Compare two Vectors for equality.
        /// Are two Vectors equal.
        /// </summary>
        /// <param name="v1">Vector3 to be compared for equality </param>
        /// <param name="v2">Vector3 to be compared to </param>
        /// <returns>Boolean decision (truth for equality)</returns>
        /// <implementation>
        /// Checks the equality of each pair of components, all pairs must be equal
        /// </implementation>
        public static bool operator ==(Vector3 v1, Vector3 v2)
        {
            return
                v1.x == v2.x &&
                v1.y == v2.y &&
                v1.z == v2.z;
        }

        /// <summary>
        /// Negative comparator of two Vectors.
        /// Are two Vectors different.
        /// </summary>
        /// <param name="v1">Vector3 to be compared for in-equality </param>
        /// <param name="v2">Vector3 to be compared to </param>
        /// <returns>Boolean decision (truth for in-equality)</returns>
        /// <Acknowledgement>This code is adapted from CSOpenGL - Lucas Viñas Livschitz </Acknowledgement>
        /// <implementation>
        /// Uses the equality operand function for two vectors to prevent code duplication
        /// </implementation>
        public static bool operator !=(Vector3 v1, Vector3 v2)
        {
            return !(v1 == v2);
        }

        #endregion
    }
}