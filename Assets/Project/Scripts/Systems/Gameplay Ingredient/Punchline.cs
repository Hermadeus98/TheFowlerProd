using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    public class Punchline : MonoBehaviour
    {
        [SerializeField] private PunchlinesData referedPunchlinesData;
        public PunchlinesData ReferedPunchlinesData => referedPunchlinesData;

        public static bool punchlineIsPlaying;

        private static List<IEnumerator> registerPunchlines = new List<IEnumerator>();
        
        public void PlayPunchline(PunchlineCallback callback)
        {
            if(punchlineIsPlaying)
                return;

            StartCoroutine(PlayPunchlineIE(referedPunchlinesData.GetRandom(callback)));
        }

        public void RegisterPunchline(PunchlineCallback callback)
        {
            registerPunchlines.Add(PlayPunchlineIE(referedPunchlinesData.GetRandom(callback)));
        }

        IEnumerator PlayPunchlineIE(PunchlineData data)
        {
            punchlineIsPlaying = true;

            SoundManager.PlaySound(data.audio, gameObject);
            // Ajouter le txt
            
            yield return new WaitForSeconds(data.soundDuration);
            punchlineIsPlaying = false;

            if (!registerPunchlines.IsNullOrEmpty())
            {
                for (int i = 0; i < registerPunchlines.Count; i++)
                {
                    yield return new WaitForSeconds(.5f);
                    yield return registerPunchlines[i];
                }
                
                registerPunchlines.Clear();
            }
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

