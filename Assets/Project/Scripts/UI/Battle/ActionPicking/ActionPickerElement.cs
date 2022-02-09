using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class ActionPickerElement : UIElement
    {
        [SerializeField] private string actionKey;

        [SerializeField] private PlayerActionType playerActionType;
        public enum PlayerActionType
        {
            NONE,
            SPELL,
            PARRY,
            ATTACK,
            FURY,
        }

        public bool CheckInput(PlayerInput input, out PlayerActionType playerActionType)
        {
            if (input.actions[actionKey].WasPressedThisFrame())
            {
                playerActionType = this.playerActionType;
                return true;
            }

            playerActionType = PlayerActionType.NONE;
            return false;
        }
    }
    
}
