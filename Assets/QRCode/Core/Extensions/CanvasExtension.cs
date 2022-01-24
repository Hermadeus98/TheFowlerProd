using UnityEngine;

namespace QRCode.Extensions
{
    public static class CanvasExtension
    {
        public static Vector2 MousePositionInCanvas(this Canvas canvas)
        {
            var NormalizePosInScreen = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
            var rectTransform = canvas.GetComponent<RectTransform>();
            
            return new Vector2(NormalizePosInScreen.x * rectTransform.sizeDelta.x, NormalizePosInScreen.y * rectTransform.sizeDelta.y);
        }

        public static Vector2 MousePositionInCanvasPercentage(this Canvas canvas)
        {
            return new Vector2(
                (canvas.MousePositionInCanvas().x / canvas.RectTransform().sizeDelta.x) * 100,
                (canvas.MousePositionInCanvas().y / canvas.RectTransform().sizeDelta.y) * 100
                );
        }

        public static RectTransform RectTransform(this Canvas canvas) =>
            canvas.GetComponent<RectTransform>();
    }
}
