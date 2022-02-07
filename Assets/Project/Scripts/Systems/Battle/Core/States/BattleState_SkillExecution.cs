using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class BattleState_SkillExecution : BattleState
    {
        private float t;
        
        public bool fury;
        
        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);

            StartCoroutine(Cast());
        }

        IEnumerator Cast()
        {
            yield return new WaitForSeconds(.5f);

            if (fury)
            {
                BattleManager.CurrentBattle.ChangeBattleState<BattleState_Fury>(BattleStateEnum.FURY);
            }
            else
            {
                BattleManager.CurrentBattle.TurnSystem.NextTurn();
            }
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);
            fury = false;
        }
    }
}
