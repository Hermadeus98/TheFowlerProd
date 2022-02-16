using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class BattleActorAnimator : SerializedMonoBehaviour
    {
        public Animator Animator;

        public void Death()
        {
            Animator.SetTrigger("Death");
        }

        public void ResetAnimator()
        {
            Animator.SetTrigger("Reset");
        }
    }
}
