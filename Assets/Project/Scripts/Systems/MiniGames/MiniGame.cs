using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class MiniGame : GameplayPhase
    {
        [SerializeField] protected EventBricks EventBricks;
        [SerializeField] protected PlayerInput Inputs;
        
        public override void PlayPhase()
        {
            base.PlayPhase();
            EventBricks.OnEnd += EndPhase;
            EventBricks.Play();
        }

        public override void EndPhase()
        {
            base.EndPhase();
            EventBricks.OnEnd -= EndPhase;
        }
    }
}
