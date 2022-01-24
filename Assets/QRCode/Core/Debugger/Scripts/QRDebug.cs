using QRCode.Extensions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace QRCode
{
    public static class QRDebug
    {
        //---<DEBUG EXTENSIONS>----------------------------------------------------------------------------------------<
        public static void Log
            (object title, FrenchPallet color, object message)
        {
            Debug.Log($"<b><color={ColorPallets.GetFrenchPalletColor(color).ToHtlmColor()}>{title} " +
                      $"-> </color></b> {message}");
        }
        
        public static void LogError(object title, FrenchPallet color, object message)
        {
            Debug.LogError($"<b><color={ColorPallets.GetFrenchPalletColor(color).ToHtlmColor()}>{title} " +
                           $"-> </color></b> {message}");
        }
        
        public static void LogWarning(object title, FrenchPallet color, object message)
        {
            Debug.LogWarning($"<b><color={ColorPallets.GetFrenchPalletColor(color).ToHtlmColor()}>{title} " +
                             $"-> </color></b> {message}");
        }
        
        public static void Log(object title, FrenchPallet color, object message, Object context)
        {
            Debug.Log($"<b><color={ColorPallets.GetFrenchPalletColor(color).ToHtlmColor()}>{title} " +
                      $"-> </color></b> {message}", context);
        }
        
        public static void LogError(object title, FrenchPallet color, object message, Object context)
        {
            Debug.LogError($"<b><color={ColorPallets.GetFrenchPalletColor(color).ToHtlmColor()}>{title} " +
                           $"-> </color></b> {message}", context);
        }
        
        public static void LogWarning(object title, FrenchPallet color, object message, Object context)
        {
            Debug.LogWarning($"<b><color={ColorPallets.GetFrenchPalletColor(color).ToHtlmColor()}>{title} " +
                             $"-> </color></b> {message}", context);
        }
    }
}
