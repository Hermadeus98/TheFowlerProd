using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using AK.Wwise;

namespace TheFowler
{
    [CreateAssetMenu(menuName = "TheFowler/Datas/Punchlines Data")]
    public class PunchlinesData : ScriptableObject
    {
        [Title("Action Picking")]
        public PunchlineStruct ActionPickingPunchlines;

        [Title("Fury")]
        public PunchlineStruct FuryPunchlines;

        [Title("SkillExecution")]
        public PunchlineStruct SkillExecutionPunchlines;

        [Title("StartBattle")]
        public PunchlineStruct StartBattlePunchlines;

        [Title("TargetPicking")]
        public PunchlineStruct TargetPickingPunchlines;

        [Title("SkillPicking")]
        public PunchlineStruct SkillPickingPunchlines;

        [Title("Death")]
        public PunchlineStruct DeathPunchlines;

        [Title("Ally Death")]
        public PunchlineStruct AllyDeathPunchlines;

        [Title("Kill")]
        public PunchlineStruct KillPunchlines;

    }
    [System.Serializable]
    public struct PunchlineStruct
    {
        public List<AK.Wwise.Event> Events;
        [Range(0,100)]
        public int porcentage;
    }
}

