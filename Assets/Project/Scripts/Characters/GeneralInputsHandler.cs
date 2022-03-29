using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TheFowler
{
    public class GeneralInputsHandler : MonoBehaviour
    {
        [SerializeField] private UnityEngine.InputSystem.PlayerInput Inputs;
        [TabGroup("SkillTree")] [SerializeField] private UnityEngine.Events.UnityEvent skillIn, skillOut;
        [TabGroup("SkillTree")] private bool isSkillTree = false;
        void Update()
        {
            if (Inputs.actions["SkillTree"].WasPressedThisFrame())
            {
                UI.GetView<SkillTreeView>(UI.Views.SkillTree).Show();
                skillIn.Invoke();
                isSkillTree = true;
            }

            else if (Inputs.actions["Return"].WasPressedThisFrame())
            {
                if (isSkillTree)
                {
                    UI.GetView<SkillTreeView>(UI.Views.SkillTree).Hide();
                    skillOut.Invoke();
                    isSkillTree = false;
                }
            }
        }
    }

}

