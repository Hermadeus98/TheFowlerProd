using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class BattleGameLogComponent : MonoBehaviour
    {
        public List<EnemyActionDatas> enemyActionDatas;
        public EnemyActionDatas datas;


        public void AddDatas(Spell spell, EnemyActor enemy, List<BattleActor> receivers)
        {
            datas.emitter = null;
            datas.spell = null;

            datas.spell = spell;
            datas.emitter = enemy;

            datas.receivers = new BattleActor[receivers.Count];
            for (int i = 0; i < receivers.Count; i++)
            {
                datas.receivers[i] = receivers[i];
            }

            enemyActionDatas.Add(datas);

        }

        public void ShowGameLogView()
        {
            UI.GetView<GameLogView>(UI.Views.GameLog).Show();
        }

        public void HideGameLogView()
        {

            UI.GetView<GameLogView>(UI.Views.GameLog).Hide();
        }

        public void UpdateGameLogView()
        {
            UI.GetView<GameLogView>(UI.Views.GameLog).Blink();
        }

        public void ResetComponent()
        {
            enemyActionDatas.Clear();
        }

        [System.Serializable]
        public struct EnemyActionDatas
        {
            public Spell spell;
            public EnemyActor emitter;
            public BattleActor[] receivers;
        }
    }
}

