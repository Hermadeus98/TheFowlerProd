using Cinemachine;
using DG.Tweening;

namespace QRCode.Extensions
{
    public static class CinemachineExtension
    {
        public static Tween DoMaxFOV(this CinemachineFollowZoom zoom, float to, float duration, Ease ease = Ease.InOutSine)
        {
            return DOTween.To(
                () => zoom.m_MaxFOV,
                (x) => zoom.m_MaxFOV = x,
                to,
                duration
                ).SetEase(ease);
        }
        
        public static Tween DoFOV(this CinemachineVirtualCamera camera, float to, float duration, Ease ease = Ease.InOutSine)
        {
            return DOTween.To(
                () => camera.m_Lens.FieldOfView,
                (x) => camera.m_Lens.FieldOfView = x,
                to,
                duration
            ).SetEase(ease);
        }

        public static Tween DoOrthographicSize(this CinemachineVirtualCamera camera, float to, float duration,
            Ease ease = Ease.InOutSine)
        {
            return DOTween.To(
                () => camera.m_Lens.OrthographicSize,
                (x) => camera.m_Lens.OrthographicSize = x,
                to,
                duration).SetEase(ease);
        }
        
        public static Tween DoDutch(this CinemachineVirtualCamera camera, float to, float duration,
            Ease ease = Ease.InOutSine)
        {
            return DOTween.To(
                () => camera.m_Lens.Dutch,
                (x) => camera.m_Lens.Dutch = x,
                to,
                duration).SetEase(ease);
        }

        public static Tween DoTrackDollyPath(this CinemachineTrackedDolly trackedDolly, float to, float duration,
            Ease ease = Ease.InOutSine)
        {
            return DOTween.To(
                () => trackedDolly.m_PathPosition,
                (x) => trackedDolly.m_PathPosition = x,
                to,
                duration).SetEase(ease);
        }
    }
}
