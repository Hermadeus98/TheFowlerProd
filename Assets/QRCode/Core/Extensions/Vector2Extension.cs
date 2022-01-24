using UnityEngine;

namespace QRCode.Extensions
{
    public static class Vector2Extension
    {
        /// <summary>
        /// Transform a Vec2 in tuple.
        /// </summary>
        public static void Deconstruct(this Vector2 vec, out float x, out float y)
        {
            x = vec.x;
            y = vec.y;
        }
    }
}