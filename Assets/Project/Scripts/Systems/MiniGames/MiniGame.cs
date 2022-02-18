using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class MiniGame : GameplayPhase
    {
        [SerializeField] protected EventBricks EventBricks;
        [SerializeField] protected PlayerInput Inputs;

        [SerializeField] protected bool resetCamera = true;
        private CinemachineVirtualCameraBase lastCamera;
        
        public override void PlayPhase()
        {
            base.PlayPhase();
            
            lastCamera = CameraManager.Current;

            EventBricks.OnEnd += EndPhase;
            EventBricks.Play();
            if (BattleManager.CurrentBattle != null)
                BattleManager.CurrentBattle.BattleState.Pause = true;
        }

        public override void EndPhase()
        {
            base.EndPhase();
            EventBricks.OnEnd -= EndPhase;
            if (BattleManager.CurrentBattle != null)
                BattleManager.CurrentBattle.BattleState.Pause = false;
            
            if(resetCamera)
                CameraManager.Instance.SetCamera(lastCamera);
        }
    }
}
