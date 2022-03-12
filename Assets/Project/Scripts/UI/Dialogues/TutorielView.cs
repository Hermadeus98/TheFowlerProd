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

        private CanvasGroup currentPanel;


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
                    currentPanel = basicAttack;
                    break;
                case PanelTutoriel.BASICATTACK2:
                    basicAttack2.DOFade(1, 0f);
                    currentPanel = basicAttack2;
                    break;
                case PanelTutoriel.SPELL:
                    spell.DOFade(1, .2f);
                    currentPanel = spell;
                    break;
                case PanelTutoriel.TYPES:
                    types.DOFade(1, 0);
                    currentPanel = types;
                    break;
                case PanelTutoriel.FURY:
                    fury.DOFade(1, .2f);
                    currentPanel = fury;
                    break;
                case PanelTutoriel.TARGET:
                    target.DOFade(1, .2f);
                    currentPanel = target;
                    break;
                case PanelTutoriel.BUFF:
                    buff.DOFade(1, .2f);
                    currentPanel = buff;
                    break;
                case PanelTutoriel.PARRY:
                    parry.DOFade(1, .2f);
                    currentPanel = parry;
                    break;
                case PanelTutoriel.HEAL:
                    heal.DOFade(1, .2f);
                    currentPanel = heal;
                    break;
                case PanelTutoriel.DONE:
                    done.DOFade(1, .2f);
                    currentPanel = done;
                    break;
            }

            Show();


        }

        private void FadeAllCanvasGroup()
        {
            basicAttack.alpha = 0;
            basicAttack2.alpha = 0;
            spell.alpha = 0;
            types.alpha = 0;
            fury.alpha = 0;
            target.alpha = 0;
            buff.alpha = 0;
            parry.alpha = 0;
            heal.alpha = 0;
            done.alpha = 0;
        }

        public override void Hide()
        {
            base.Hide();
            if (isShowed)
            {
                feedbackOut.PlayFeedbacks();
                isShowed = false;
                currentPanel.alpha = 0;
            }
        }
    }
}

