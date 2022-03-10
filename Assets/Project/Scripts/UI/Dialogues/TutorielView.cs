using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
namespace TheFowler
{
    public class TutorielView : UIView
    {

        [TabGroup("References"), SerializeField] private MMFeedbacks feedbackIn, feedbackOut;
        [TabGroup("Panels"), SerializeField] private CanvasGroup basicAttack, basicAttack2, spell, types, fury, target, buff, parry, heal, done;


        private bool isShowed = false;
        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);
        }

        public override void Show()
        {
            base.Show();

            if (!isShowed)
            {
                feedbackIn.PlayFeedbacks();
                isShowed = true;
            }

        }

        public  void Show(PanelTutoriel panel)
        {
            FadeAllCanvasGroup();
            switch (panel)
            {
                case PanelTutoriel.BASICATTACK:
                    basicAttack.DOFade(1, .2f);
                    break;
                case PanelTutoriel.BASICATTACK2:
                    basicAttack2.DOFade(1, .2f);
                    break;
                case PanelTutoriel.SPELL:
                    spell.DOFade(1, .2f);
                    break;
                case PanelTutoriel.TYPES:
                    types.DOFade(1, .2f);
                    break;
                case PanelTutoriel.FURY:
                    fury.DOFade(1, .2f);
                    break;
                case PanelTutoriel.TARGET:
                    target.DOFade(1, .2f);
                    break;
                case PanelTutoriel.BUFF:
                    buff.DOFade(1, .2f);
                    break;
                case PanelTutoriel.PARRY:
                    parry.DOFade(1, .2f);
                    break;
                case PanelTutoriel.HEAL:
                    heal.DOFade(1, .2f);
                    break;
                case PanelTutoriel.DONE:
                    done.DOFade(1, .2f);
                    break;
            }

            Show();


        }

        private void FadeAllCanvasGroup()
        {
            basicAttack.alpha = 0;
            basicAttack2.alpha = 0;
        }

        public override void Hide()
        {
            base.Hide();
            if (isShowed)
            {
                feedbackOut.PlayFeedbacks();
                isShowed = false;
            }
        }
    }
}

