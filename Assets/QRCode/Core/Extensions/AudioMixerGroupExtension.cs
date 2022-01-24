using DG.Tweening;
using UnityEngine.Audio;

namespace QRCode.Extensions
{
    public static class AudioMixerGroupExtension
    {
        public static void SetMasterVolume(this AudioMixerGroup @group, float to, float duration = 1f, Ease easing = Ease.InOutSine)
        {
            group.audioMixer.GetFloat("MasterVolume", out var volume);
            DOTween.To(
                () => volume,
                (x) => volume = x,
                to,
                duration
                ).OnUpdate(()=> group.audioMixer.SetFloat("MasterVolume", volume));
        }
        
        public static void SetFloat(this AudioMixerGroup @group, string value, float to, float duration = 1f, Ease easing = Ease.InOutSine)
        {
            group.audioMixer.GetFloat(value, out var _value);
            DOTween.To(
                () => _value,
                (x) => _value = x,
                to,
                duration
            ).OnUpdate(()=> group.audioMixer.SetFloat(value, _value));
        }
    }
}
