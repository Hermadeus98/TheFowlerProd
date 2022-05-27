using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using MoreMountains.Feedbacks;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace TheFowler
{
    public class CustomElement : Button
    {


        [SerializeField] private MMFeedbacks _OnSelect, _OnDeselect;


        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);

            if(_OnSelect != null)
            _OnSelect.PlayFeedbacks();
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);

            if(_OnDeselect != null)
            _OnDeselect.PlayFeedbacks();
        }


    }
}

