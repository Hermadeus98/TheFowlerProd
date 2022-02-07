using System.Collections;
using System.Collections.Generic;
using QRCode;
using QRCode.Utils;
using UnityEngine;

namespace TheFowler
{
    public static class PlayerTurn
    {
        public static void Initialize()
        {
        }

        public static void Play()
        {
            Coroutiner.Play(wait());
        }
        
        static IEnumerator wait()
        {
            yield return new WaitForSeconds(1f);
            BattleManager.CurrentBattle.ChangeBattleState<BattleState_ActionPicking>(BattleStateEnum.ACTION_PICKING);
            yield break;
        }
    }
}
