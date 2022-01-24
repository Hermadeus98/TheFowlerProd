using UnityEngine;

namespace QRCode.Extensions
{
    public static class Vector3Extension
    {
        public static (float x, float y) XY(this Vector3 vector)
        {
            return (vector.x, vector.y);
        }
        
        public static (float x, float z) XZ(this Vector3 vector)
        {
            return (vector.x, vector.z);
        }
        
        public static (float y, float z) YZ(this Vector3 vector)
        {
            return (vector.y, vector.z);
        }

        public static Vector3 Multiply (this Vector3 vecA, Vector3 vecb)
            => new Vector3(vecA.x * vecb.x, vecA.y * vecb.y, vecA.z * vecb.z);


        /// <summary>
        /// Transform a Vec3 in tuple.
        /// </summary>
        public static void Deconstruct(this Vector3 vec, out float x, out float y, out float z)
        {
            x = vec.x;
            y = vec.y;
            z = vec.z;
        }
    }
}

