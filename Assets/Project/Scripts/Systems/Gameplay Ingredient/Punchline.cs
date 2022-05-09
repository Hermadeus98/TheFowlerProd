using System.Collections;
using System.Collections.Generic;
using AK.Wwise;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

namespace TheFowler
{
    public class Punchline : MonoBehaviour
    {
        private bool canPlay = true;
        
        [SerializeField] private PunchlinesData punchlinesData;

        public void PlayPunchline(PunchlineEnum punchlineEnum)
        {
            switch (punchlineEnum)
            {
                case PunchlineEnum.ACTIONPICKING:
                    CallPunchlineData(punchlinesData.ActionPickingPunchlines);
                    break;
                case PunchlineEnum.FURY:
                    CallPunchlineData(punchlinesData.FuryPunchlines);
                    break;
                case PunchlineEnum.SKILLEXECUTION:
                    CallPunchlineData(punchlinesData.SkillExecutionPunchlines);
                    break;
                case PunchlineEnum.STARTBATTLE:
                    CallPunchlineData(punchlinesData.StartBattlePunchlines);
                    break;
                case PunchlineEnum.TARGETPICKING:
                    CallPunchlineData(punchlinesData.TargetPickingPunchlines);
                    break;
                case PunchlineEnum.SKILLPICKING:
                    CallPunchlineData(punchlinesData.SkillPickingPunchlines);
                    break;
                case PunchlineEnum.DEATH:
                    CallPunchlineData(punchlinesData.DeathPunchlines);
                    break;
                case PunchlineEnum.ALLYDEATH:
                    CallPunchlineData(punchlinesData.AllyDeathPunchlines);
                    break;
                case PunchlineEnum.KILL:
                    CallPunchlineData(punchlinesData.KillPunchlines);
                    break;
                case PunchlineEnum.DAMAGETAKEN:
                    CallPunchlineData(punchlinesData.DamageTakenPunchline);
                    break;
            }
        }
        public void CallPunchlineData(PunchlineStruct punchlineStruct)
        {
            if (punchlineStruct.Events.IsNullOrEmpty())
                return;
            
            int rand = Random.Range(0, 100);

            if (rand > punchlineStruct.porcentage) return;

            if (punchlineStruct.Events.Count == 0) return;

            int rand2 = Random.Range(0, punchlineStruct.Events.Count);

            if (canPlay)
            {
                StartCoroutine(Mute(punchlineStruct.Events[rand2]));
            }
        }

        IEnumerator Mute(AK.Wwise.Event evnt)
        {
            //evnt.Post(gameObject, new CallbackFlags(){value = 0}, CallBackFunction);       
            
            SoundManager.PlaySound(evnt, gameObject);
            
            canPlay = false;
            while (evnt.ObjectReference.IsComplete())
            {
                yield return null;
            }

            yield return new WaitForSeconds(.5f);

            canPlay = true;
        }
        
        void CallBackFunction(object in_cookie, AkCallbackType callType, object in_info)
        {
            if (callType == AkCallbackType.AK_EndOfEvent)
            {
                canPlay = true;
            }
        }
    }


    public enum PunchlineEnum
    {
        ACTIONPICKING,
        FURY,
        SKILLEXECUTION,
        STARTBATTLE,
        TARGETPICKING,
        SKILLPICKING,
        DEATH,
        ALLYDEATH,
        KILL,
        DAMAGETAKEN
    }
}

