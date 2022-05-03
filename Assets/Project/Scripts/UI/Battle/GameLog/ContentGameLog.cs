using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class ContentGameLog : MonoBehaviour
    {
        [SerializeField] private Image[] gameLogTargets;
        [SerializeField] private Image Effect;
        [SerializeField] private TMPro.TextMeshProUGUI effectText;

        

        private BattleActor enemy;
        private BattleActor[] receivers;

        public void Initialize(Effect effect)
        {
            enemy = effect.Emitter;

            receivers = new BattleActor[effect.Receivers.Length];
            for (int i = 0; i < effect.Receivers.Length; i++)
            {
                receivers[i] = effect.Receivers[i];
            }

            if(effect.sprite != null)
            {
                Effect.enabled = true;
                effectText.enabled = false;
                Effect.sprite = effect.sprite;
            }
            else
            {
                Effect.enabled = false;
                effectText.enabled = true;
                if (effect.damage != 0)
                {
                    effectText.color = Color.red;
                    effectText.text = "-" + effect.damage.ToString();
                }
                else if (effect.heal != 0)
                {
                    effectText.color = Color.green;
                    effectText.text = "+" + effect.heal.ToString();
                }
            }

            for (int i = 0; i < gameLogTargets.Length; i++)
            {
                gameLogTargets[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < effect.Receivers.Length; i++)
            {
                gameLogTargets[i].gameObject.SetActive(true);
                gameLogTargets[i].sprite = receivers[i].BattleActorData.sprite;
            }
        }

    }

}
