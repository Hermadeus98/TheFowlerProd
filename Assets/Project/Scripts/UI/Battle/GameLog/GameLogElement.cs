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
        [SerializeField, HideInInspector] private BattleActor enemy;
        [SerializeField, HideInInspector] private BattleActor[] receivers;
        [SerializeField] private ContentGameLog[] contentGameLogs;
        public CanvasGroup canvasGroup;
        private Spell spell;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Image vertical, descriptionBox;

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            _Select();
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            _Deselect();

            descriptionBox.enabled = false;
            vertical.color = Color.black;
        }

        public void _Select()
        {
            enemy.OnTargetEmitterLog();

            for (int i = 0; i < receivers.Length; i++)
            {
                receivers[i].OnTarget();
            }

            descriptionBox.enabled = true;
            vertical.color = selectedColor;

        }

        public void _Deselect()
        {
            if(enemy != null)
            {
                enemy.OnEndTargetEmitterLog();

            }

            for (int i = 0; i < receivers.Length; i++)
            {
                if (receivers[i] != null)
                    receivers[i].OnEndTarget();
            }
        }

        public void Initialize(BattleGameLogComponent.EnemyActionDatas datas)
        {
            enemy = datas.emitter;
            spell = datas.spell;
            receivers = new BattleActor[datas.receivers.Length];

            for (int i = 0; i < datas.receivers.Length; i++)
            {
                receivers[i] = datas.receivers[i];
            }

            for (int i = 0; i < contentGameLogs.Length; i++)
            {
                contentGameLogs[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < spell.Effects.Length; i++)
            {
                contentGameLogs[i].gameObject.SetActive(true);
                contentGameLogs[i].Initialize(spell.Effects[i]);
            }


        } 
    }
}

