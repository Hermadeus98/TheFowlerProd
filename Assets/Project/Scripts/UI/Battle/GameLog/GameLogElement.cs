using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class GameLogElement : MonoBehaviour
    {
        [SerializeField] private Image spellImage;
        [SerializeField, HideInInspector] private BattleActor enemy;
        [SerializeField] private Image[] receivers;
        [SerializeField] private Image emitter;
        public CanvasGroup canvasGroup;

        public void Initialize(BattleGameLogComponent.EnemyActionDatas datas)
        {
            enemy = datas.emitter;

            if (enemy.BattleActorData.sprite != null)
                emitter.sprite = enemy.BattleActorData.sprite;

            if (spellImage.sprite != null)
                spellImage.overrideSprite = datas.spell.sprite;
            else
            {
                spellImage.overrideSprite = null;
            }

            for (int i = 0; i < receivers.Length; i++)
            {
                receivers[i].GetComponent<CanvasGroup>().alpha = 0;
            }

            for (int i = 0; i < datas.receivers.Length; i++)
            {
                receivers[i].GetComponent<CanvasGroup>().alpha = 1;

                if(datas.receivers[i].BattleActorData.sprite != null)   
                    receivers[i].sprite = datas.receivers[i].BattleActorData.sprite;
            }
        } 
    }
}

