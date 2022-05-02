using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.Collections;

namespace TheFowler
{
    public class GameLogElement : CustomElement
    {
        [SerializeField] private Image spellImage;
        [SerializeField, HideInInspector] private BattleActor enemy;
        [SerializeField, HideInInspector] private BattleActor[] receivers;
        [SerializeField] private Image[] receiversImage;
        [SerializeField] private Image emitter;
        [SerializeField] private GameObject descriptionBox;
        [SerializeField] private TMPro.TextMeshProUGUI descriptionText;
        public CanvasGroup canvasGroup;
        private Spell spell;

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            _Select();
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            _Deselect();


        }

        public void _Select()
        {
            enemy.SelectionPointer.Show();
            enemy.SelectionPointer.SetEmitterColor();

            for (int i = 0; i < receivers.Length; i++)
            {
                receivers[i].SelectionPointer.Show();
                receivers[i].SelectionPointer.SetTargetColor();
            }

            descriptionBox.SetActive(true);
            descriptionText.text = spell.SpellDescription;
        }

        public void _Deselect()
        {
            if(enemy != null)
            {

                enemy.SelectionPointer.Hide();
                enemy.SelectionPointer.SetTargetColor();

            }

            for (int i = 0; i < receivers.Length; i++)
            {
                if(receivers[i] != null)
                    receivers[i].SelectionPointer.Hide();
            }
            descriptionBox.SetActive(false);
        }

        public void Initialize(BattleGameLogComponent.EnemyActionDatas datas)
        {
            enemy = datas.emitter;
            spell = datas.spell;

            receivers = new BattleActor[datas.receivers.Length];

            if (enemy.BattleActorData.sprite != null)
                emitter.sprite = enemy.BattleActorData.sprite;

            if (spellImage.sprite != null)
                spellImage.overrideSprite = datas.spell.sprite;
            else
            {
                spellImage.overrideSprite = null;
            }

            for (int i = 0; i < receiversImage.Length; i++)
            {
                receiversImage[i].GetComponent<CanvasGroup>().alpha = 0;
            }

            for (int i = 0; i < datas.receivers.Length; i++)
            {
                receivers[i] = datas.receivers[i];

                receiversImage[i].GetComponent<CanvasGroup>().alpha = 1;

                if(datas.receivers[i].BattleActorData.sprite != null)
                    receiversImage[i].sprite = datas.receivers[i].BattleActorData.sprite;
            }
        } 
    }
}

