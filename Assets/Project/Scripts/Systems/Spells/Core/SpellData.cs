using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using QRCode;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

namespace TheFowler
{
    [CreateAssetMenu]
    public class SpellData : ScriptableObjectSingleton<SpellData>
    {
        [FoldoutGroup("Robyn Basic Attack")]
        public VisualEffect Robyn_VisualEffect_BasicAttack_BirdFalling;
        [FoldoutGroup("Robyn Basic Attack")]
        public VisualEffect Robyn_VisualEffect_BasicAttack_Shock;
        [FoldoutGroup("Robyn Basic Attack")]
        public float Robyn_Timer_BasicAttack_BirdFallingDuration = .85f;
        [FoldoutGroup("Robyn Basic Attack")]
        public GameObject Robyn_Flash_BasicAttack_Shock;
        
        [FoldoutGroup("Abi Basic Attack")]
        public ParticleSystem Abi_PS_BasicAttack_Trail;
        [FoldoutGroup("Abi Basic Attack")]
        public float Abi_Timer_BasicAttack_TrailDuration = 1f;
        [FoldoutGroup("Abi Basic Attack")]
        public ParticleSystem Abi_PS_BasicAttack_Impact;
        [FoldoutGroup("Abi Basic Attack")]
        public GameObject Abi_Flash_BasicAttack_Shock;

        [FoldoutGroup("Guard Basic Attack")] public GuardBasicAttackBinding Guard_BasicAttackBinding;
        [FoldoutGroup("Guard Basic Attack")]
        public float Guard_Timer_BasicAttack_ImpactDuration = 1f;

        [FoldoutGroup("Balancing - Buff Defense")] public int
            buffDefense = 35,
            buffDefenseAOE = 20,
            debuffDefense = 25,
            debuffDefenseAOE = 20,
            maxBuffDefense = 100,
            minBuffDefense = -100;

        [FoldoutGroup("Balancing - Buff Attack")] public int 
            buffAttack = 35,
            buffAttackAOE = 20,
            debuffAttack = 25,
            debuffAttackAOE = 20,
            maxBuffAttack = 100,
            minBuffAttack = -100;

        [FoldoutGroup("Balancing - Coodldown")]
        public int
            maxBuffCD = 3,
        minBuffCD = 1;
    }
}
