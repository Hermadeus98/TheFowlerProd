using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TheFowler
{
    public class Punchline : MonoBehaviour
    {
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
            }
        }
        public void CallPunchlineData(PunchlineStruct punchlineStruct)
        {
            int rand = Random.Range(0, 100);

            if (rand > punchlineStruct.porcentage) return;

            if (punchlineStruct.Events.Count == 0) return;

            int rand2 = Random.Range(0, punchlineStruct.Events.Count);
            SoundManager.PlaySound(punchlineStruct.Events[rand2], gameObject);
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
        KILL
    }
}

