using System;

namespace TheFowler
{
    public class BattleState_ActionPicking : BattleState
    {
        private ActionPickingView ActionPickingView;
        
        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);

            if (BattleManager.IsAllyTurn)
            {
                ActionPickingView = UI.OpenView<ActionPickingView>("ActionPickingView");
                ActionPickingView.Refresh(EventArgs.Empty);
            }
        }

        public override void OnStateExecute()
        {
            base.OnStateExecute();

            if (BattleManager.IsAllyTurn)
            {
                if (ActionPickingView.CheckActions(out var actionType))
                {
                    switch (actionType)
                    {
                        case ActionPickerElement.PlayerActionType.NONE:
                            break;
                        case ActionPickerElement.PlayerActionType.SPELL:
                            BattleManager.CurrentBattle.ChangeBattleState<BattleState_SkillPicking>(BattleStateEnum.SKILL_PICKING);
                            break;
                        case ActionPickerElement.PlayerActionType.PARRY:
                            BattleManager.CurrentBattle.ChangeBattleState<BattleState_SkillPicking>(BattleStateEnum.TARGET_PICKING);
                            break;
                        case ActionPickerElement.PlayerActionType.ATTACK:
                            BattleManager.CurrentBattle.ChangeBattleState<BattleState_SkillPicking>(BattleStateEnum.TARGET_PICKING);
                            break;
                        case ActionPickerElement.PlayerActionType.FURY:
                            //
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            else if (BattleManager.IsEnemyTurn)
            {
                BattleManager.CurrentBattle.ChangeBattleState<BattleState_SkillPicking>(BattleStateEnum.SKILL_PICKING);
            }
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);
            UI.CloseView("ActionPickingView");
        }
    }
}
