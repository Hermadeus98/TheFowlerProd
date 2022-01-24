using System;
using UnityEngine;

namespace QRCode.Extensions
{
    public static class TupleExtension
    {
        public static Vector2 ToVector2(this ValueTuple<float, float> valueTuple)
            =>  new Vector2(valueTuple.Item1, valueTuple.Item2);

        public static Vector3 ToVector3(this ValueTuple<float, float, float> valueTuple)
            => new Vector3(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
    
        public static Vector2Int ToVector2(this ValueTuple<int, int> valueTuple)
            =>  new Vector2Int(valueTuple.Item1, valueTuple.Item2);

        public static Vector3Int ToVector3(this ValueTuple<int, int, int> valueTuple)
            => new Vector3Int(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);

        public static (float x, float y) Multiply(this ValueTuple<float, float> valueTuple, ValueTuple<float, float> multiplier)
            => (valueTuple.Item1 * multiplier.Item1, valueTuple.Item2 * valueTuple.Item2);
    }
}