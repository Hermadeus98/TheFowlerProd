using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    public class Punchline : MonoBehaviour
    {
        [SerializeField] private PunchlinesData referedPunchlinesData;
        [SerializeField] private string speaker;
        
        public PunchlinesData ReferedPunchlinesData => referedPunchlinesData;

        public static bool punchlineIsPlaying;

        private static List<IEnumerator> registerPunchlines = new List<IEnumerator>();
        
        public void PlayPunchline(PunchlineCallback callback)
        {
            /*if(punchlineIsPlaying)
                return;*/

            StartCoroutine(PlayPunchlineIE(referedPunchlinesData.GetRandom(callback)));
        }

        public void RegisterPunchline(PunchlineCallback callback)
        {
            registerPunchlines.Add(PlayPunchlineIE(referedPunchlinesData.GetRandom(callback)));
        }

        public void PlayWithoutCoroutine(PunchlineCallback callback, out PunchlineData data)
        {
            data = referedPunchlinesData.GetRandom(callback);
            
            if (punchlineIsPlaying || data == null)
                return;
            
            if(data.audio != null)
                SoundManager.PlaySound(data.audio, gameObject);
        }

        IEnumerator PlayPunchlineIE(PunchlineData data)
        {
            if(data == null)
                yield break;
            
            punchlineIsPlaying = true;

            if(data.audio != null)
                SoundManager.PlaySound(data.audio, gameObject);
            var dialogueView = UI.GetView<DialogueStaticView>(UI.Views.StaticDialogs);
            dialogueView.Refresh(data.text, speaker);
            
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
        DAMAGE_TAKEN_LOW,
        DAMAGE_TAKEN_HIGH,
        GIVING_BREAKDOWN,
        RECEIVING_BREAKDOWN,
        HEALING,
        TAUNT,
        PROTECT,
        KILL,
        LOW_HP,
        OVERTIME,
        SKILL_EXECUTION,
        SKILL_PICKING,
        START_TURN,
        DEATH,
        RECEIVING_REVIVE,
        REACT_TO_PHOEBE_PHOEBE,
        REACT_TO_PHOEBE_ABI,
        REACT_TO_PHOEBE_ROBYN,
        REACT_TO_ABI_PHOEBE,
        REACT_TO_ABI_ABI,
        REACT_TO_ABI_ROBYN,
        REACT_TO_ROBYN_PHOEBE,
        REACT_TO_ROBYN_ABI,
        REACT_TO_ROBYN_ROBYN,
    }
}

