using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class GeneralInputsHandler : MonoBehaviour
    {
        [SerializeField] private UnityEngine.InputSystem.PlayerInput Inputs;

        void Update()
        {
            if (Inputs.actions["SkillTree"].WasPressedThisFrame())
            {
                UI.GetView<SkillTreeView>(UI.Views.SkillTree).Show();
            }
        }
    }
}

