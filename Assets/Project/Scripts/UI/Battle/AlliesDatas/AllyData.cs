using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class AllyData : UIElement
    {
        public CanvasGroup CanvasGroup;
        
        [SerializeField] private Image healthBar;
        [SerializeField] private Image manaBar;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI manaText;

        [SerializeField, ReadOnly] private BattleActor referedActor;

        [SerializeField] [CanBeNull] private Image head, glowSprite;
        [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite deathSprite, selectedSprite;

        [SerializeField] private Image blood, hearth_icon, mana_icon;

        public StateIcons StateIcons;

        //[SerializeField] private Image furyFlamme;

        [SerializeField] private Image picks;
        [SerializeField] private Sprite pick_tall, pick_medium, pick_none;

        protected override void OnStart()
        {
            base.OnStart();
            transform.localScale = Vector3.one * .85f;
        }

        public void Register(BattleActor actor)
        {
            referedActor = actor;
            referedActor.AllyData = this;
            Refresh();
            actor.StateIcons = StateIcons;
            StateIcons.HideAll();
        }

        public void Refresh()
        {
            if (referedActor == null) return;
            healthBar.DOFillAmount(referedActor.Health.NormalizedHealth, .2f);
            
            healthText.SetText(referedActor.Health.CurrentHealth.ToString() + "/" + referedActor.Health.MaxHealth);

            if (referedActor.BattleActorInfo.isDeath)
            {
                SetGraphicToDeath();
            }
            else
            {
                SetGraphicToNormal();
            }
            
            HearthIconBeating();
            SetNext();
        }

        private void SetNext()
        {
            var nextTurnActor = BattleManager.CurrentRound.GetNextAlly();

            if (nextTurnActor != null)
            {
                if (nextTurnActor is AllyActor)
                {
                    picks.sprite = pick_medium;
                }
            }
        }

        private Sequence beat;
        private bool isBeating;
        
        private void HearthIconBeating()
        {
            var lowHealth = referedActor.Health.NormalizedHealth < .25f;

            if (referedActor.Health.NormalizedHealth == 0)
            {
                StopBeat();
                return;
            }

            if (lowHealth)
            {
                if (!isBeating)
                {
                    beat = DOTween.Sequence();
                    beat.Append(hearth_icon.transform.DOScale(1.1f, .2f).SetEase(Ease.InSine));
                    beat.Append(hearth_icon.transform.DOScale(1f, .2f).SetEase(Ease.InSine));
                    beat.SetLoops(-1);
                    beat.Play();

                    HeartBeating.Instance.isBeating = true;
                }

                isBeating = true;
            }
            else
            {
                StopBeat();
            }
        }

        private void StopBeat()
        {
            beat?.Kill();
            hearth_icon.transform.localScale = Vector3.one;
            isBeating = false;
            HeartBeating.Instance.isBeating = false;
        }

        [Button]
        public void SetGraphicToNormal()
        {
            if (head.sprite == selectedSprite)
            {
                head.sprite = selectedSprite;
                glowSprite.DOFade(0f, .1f).SetEase(Ease.OutSine);
            }
            else
            {
                head.sprite = normalSprite;
                glowSprite.DOFade(0f, .1f).SetEase(Ease.OutSine);
            }
            
            blood.fillAmount = 0;
        }
        
        [Button]
        private void SetGraphicToDeath()
        {
            head.sprite = deathSprite;
            blood.DOFillAmount(1f, .5f).SetEase(Ease.InOutSine);

            glowSprite.DOFade(0f, .1f).SetEase(Ease.OutSine);
            
            StateIcons.HideAll();
            
            StopBeat();
        }

        public void Select()
        {
            head.sprite = selectedSprite;
            transform.DOScale(1f, .2f);
            glowSprite.DOFade(1f, .1f).SetEase(Ease.OutSine);
            picks.sprite = pick_tall;
        }

        public void UnSelect()
        {
            head.sprite = normalSprite;
            transform.DOScale(.85f, .2f);
            glowSprite.DOFade(0f, .1f).SetEase(Ease.OutSine);
            picks.sprite = pick_none;
        }

        public void ShakeHearth()
        {
            var pos = hearth_icon.transform.position;
            hearth_icon.transform.DOShakePosition(.2f, 10, 2).OnComplete(delegate
            {
                hearth_icon.transform.position = pos;
            });
        }

        public void Fury(bool state)
        {
            /*if (state)
            {
                furyFlamme.DOFade(1f, 0.01f);
            }
            else
            {
                furyFlamme.DOFade(0f, 0.01f);
            }*/
        }
    }
}
