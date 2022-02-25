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
        [SerializeField] private GameObject back;
        
        [SerializeField] private Image healthBar;
        [SerializeField] private Image manaBar;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI manaText;

        [SerializeField, ReadOnly] private BattleActor referedActor;
        
        public void Register(BattleActor actor)
        {
            referedActor = actor;
            referedActor.AllyData = this;
            Refresh();
        }

        public void Refresh()
        {
            Debug.Log(referedActor.Health.NormalizedHealth);
            healthBar.DOFillAmount(referedActor.Health.NormalizedHealth, .2f);
            manaBar.DOFillAmount(referedActor.Mana.NormalizedMana, .2f);
            
            healthText.SetText(referedActor.Health.CurrentHealth.ToString());
            manaText.SetText(referedActor.Mana.CurrentMana.ToString());
        }

        public void Select() => back.SetActive(true);
        public void UnSelect() => back.SetActive(false);
    }
}
