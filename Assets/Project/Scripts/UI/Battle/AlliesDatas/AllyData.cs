using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

        [SerializeField] private Image head;
        [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite deathSprite, outlineSprite;

        [SerializeField] private Image blood, hearth_icon, mana_icon;

        [SerializeField] private Color hp_color, mana_color, death_color;

        public StateIcons StateIcons;

        [SerializeField] private Image furyFlamme;

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
            healthBar.DOFillAmount(referedActor.Health.NormalizedHealth, .2f);
            manaBar.DOFillAmount(referedActor.Mana.NormalizedMana, .2f);
            
            healthText.SetText(referedActor.Health.CurrentHealth.ToString() + "/" + referedActor.Health.MaxHealth);
            manaText.SetText(referedActor.Mana.CurrentMana.ToString() + "/" + referedActor.Mana.MaxMana);

            if (referedActor.BattleActorInfo.isDeath)
            {
                SetGraphicToDeath();
            }
            else
            {
                SetGraphicToNormal();
            }
            
            HearthIconBeating();
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
        private void SetGraphicToNormal()
        {
            if (head.sprite == outlineSprite)
            {
                head.sprite = normalSprite;
            }
            else
            {
                head.sprite = normalSprite;
            }
            
            blood.fillAmount = 0;

            healthBar.color = hp_color;
            manaBar.color = mana_color;

            hearth_icon.color = hp_color;
            mana_icon.color = mana_color;
        }
        
        [Button]
        private void SetGraphicToDeath()
        {
            head.sprite = deathSprite;
            blood.DOFillAmount(1f, .5f).SetEase(Ease.InOutSine);

            healthBar.color = death_color;
            manaBar.color = death_color;
            
            hearth_icon.color = death_color;
            mana_icon.color = death_color;
            
            StateIcons.HideAll();
            
            StopBeat();
        }

        public void Select()
        {
            //head.sprite = outlineSprite;
            transform.DOScale(1f, .2f);
        }

        public void UnSelect()
        {
            //head.sprite = normalSprite;
            transform.DOScale(.85f, .2f);
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
            if (state)
            {
                furyFlamme.DOFade(1f, 0.01f);
            }
            else
            {
                furyFlamme.DOFade(0f, 0.01f);
            }
        }
    }
}
