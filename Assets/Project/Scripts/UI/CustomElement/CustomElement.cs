using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using MoreMountains.Feedbacks;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TheFowler
{
    public class CustomElement : Button
    {
        [SerializeField] private MMFeedbacks _OnActivation, _OnDisable;
        public SkillTreeSelector skillTreeSelector;

        protected override void OnEnable()
        {
            if(skillTreeSelector == null)
            {
                skillTreeSelector = transform.parent.GetComponent<SkillTreeSelector>();
            }
        }


        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            UI.GetView<SkillTreeView>(UI.Views.SkillTree).currentCustomElement = this;

            _OnActivation.PlayFeedbacks();
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            _OnDisable.PlayFeedbacks();
        }


    }
}

