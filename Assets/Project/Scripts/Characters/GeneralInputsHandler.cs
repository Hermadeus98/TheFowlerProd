using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using QRCode;

namespace TheFowler
{
    public class GeneralInputsHandler : MonoBehaviourSingleton<GeneralInputsHandler>
    {
        public UnityEngine.InputSystem.PlayerInput Inputs;
        public UnityEngine.InputSystem.PlayerInput RobynInputs;
        [TabGroup("SkillTree")] [SerializeField] private UnityEngine.Events.UnityEvent skillIn, skillOut;
        [TabGroup("SkillTree")] private bool isSkillTree = false;
        //void Update()
        //{
        //    if (Inputs.actions["SkillTree"].WasPressedThisFrame())
        //    {
        //        UI.GetView<SkillTreeView>(UI.Views.SkillTree).Show();
        //        skillIn.Invoke();
        //        isSkillTree = true;
        //    }

        //    else if (Inputs.actions["Return"].WasPressedThisFrame())
        //    {
        //        if (isSkillTree)
        //        {
        //            UI.GetView<SkillTreeView>(UI.Views.SkillTree).Hide();
        //            skillOut.Invoke();
        //            isSkillTree = false;
        //        }
        //    }
        //}
    }

}

