using System.Runtime.Serialization;
using UnityEngine;

namespace QRCode
{
    public class Vector3SerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var vector3 = (Vector3) obj;
            info.AddValue("x", vector3.x);
            info.AddValue("y", vector3.y);
            info.AddValue("z", vector3.z);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var vector3 = (Vector3) obj;
            vector3.x = (float) info.GetValue("x", typeof(float));
            vector3.y = (float) info.GetValue("y", typeof(float));
            vector3.z = (float) info.GetValue("z", typeof(float));
            obj = vector3;
            return obj;
        }
    }

    public class QuaternionSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var quaternion = (Quaternion) obj;
            info.AddValue("x", quaternion.x);
            info.AddValue("y", quaternion.y);
            info.AddValue("z", quaternion.z);
            info.AddValue("w", quaternion.w);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var quaternion = (Quaternion) obj;
            quaternion.x = (float) info.GetValue("x", typeof(float));
            quaternion.y = (float) info.GetValue("y", typeof(float));
            quaternion.z = (float) info.GetValue("z", typeof(float));
            quaternion.w = (float) info.GetValue("w", typeof(float));
            obj = quaternion;
            return obj;
        }
    }
}
