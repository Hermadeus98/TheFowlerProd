using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public static class Tutoriel
    {
        public static bool isTutoriel = false;
        public static bool BasicAttack = false;
        public static bool Fury = false;
        public static bool Skill = false;
        public static bool Parry = false;
        public static bool LockTarget = false;
        public static bool LockSkill = false;

        public static void Kill()
        {
            isTutoriel = false;
            BasicAttack = false;
            Fury = false;
            Skill = false;
            Parry = false;
            LockTarget = false;
            LockSkill = false;
        }
    }
}

