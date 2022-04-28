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
        [SerializeField] private GameObject[] allies;
        public CanvasGroup canvasGroup;

        public void Initialize(BattleGameLogComponent.EnemyActionDatas datas)
        {
            if(spellImage.sprite != null)
                spellImage.overrideSprite = datas.spell.sprite;
            else
            {
                spellImage.overrideSprite = null;
            }

            for (int i = 0; i < allies.Length; i++)
            {
                allies[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < datas.reveivers.Count; i++)
            {
                switch (datas.reveivers[i].BattleActorData.actorName)
                {
                    case "Robyn":
                        allies[0].gameObject.SetActive(true);
                        break;
                    case "Abigail":
                        allies[1].gameObject.SetActive(true);
                        break;
                    case "Phoebe":
                        allies[2].gameObject.SetActive(true);
                        break;
                }
            }
        } 
    }
}

