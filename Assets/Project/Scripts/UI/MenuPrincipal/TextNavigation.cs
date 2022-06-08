using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TheFowler
{
    public class TextNavigation : UISelector
    {
        public void StartNavigate()
        {
            ResetElements();
            SelectElement();
        }

        public void ShowAnim()
        {
            canvasGroup.DOFade(1f, .2f).SetUpdate(true).OnComplete(delegate { isActive = true; });
            currentSelectedElement?.Select();

            all_elements.ForEach(w => w.isActive = true);
        }
        
        public void HideAnim()
        {
            canvasGroup.DOFade(0f, .2f).SetUpdate(true);
            isActive = false;
            
            all_elements.ForEach(w => w.isActive = false);
        }
    }
}
