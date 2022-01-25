using UnityEngine;

namespace QRCode.Extensions
{
    public static class ObjectExtension
    {
        public static bool IsNotNull(this Object o)
        {
            return o ?? true;
        }
        
        public static bool IsNull(this Object o)
        {
            return o ? o : true;
        }
    }
}
