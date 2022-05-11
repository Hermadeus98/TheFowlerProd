using System.Collections;
using UnityEngine;

namespace TheFowler
{
    public class Punchline : MonoBehaviour
    {
        [SerializeField] private PunchlinesData referedPunchlinesData;
        public PunchlinesData ReferedPunchlinesData => referedPunchlinesData;

        public static bool punchlineIsPlaying;

        public void PlayPunchline(PunchlineCallback callback)
        {
            if(punchlineIsPlaying)
                return;

            StartCoroutine(PlayPunchlineIE(referedPunchlinesData.GetRandom(callback)));
        }

        IEnumerator PlayPunchlineIE(PunchlineData data)
        {
            punchlineIsPlaying = true;

            SoundManager.PlaySound(data.audio, gameObject);
            // Ajouter le txt
            
            yield return new WaitForSeconds(data.soundDuration);
            punchlineIsPlaying = false;
        }
    }


    public enum PunchlineCallback
    {
        ACTIONPICKING,
        FURY,
        SKILLEXECUTION,
        TARGETPICKING,
        SKILLPICKING,
        DEATH,
        KILL,
        DAMAGETAKEN
    }
}

