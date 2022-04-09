using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class SpellTreeSelector : MonoBehaviour
    {
        [SerializeField] private GameObject hover, unhover;
        [SerializeField] private Image spellSprite;
        [SerializeField] private TMPro.TextMeshProUGUI spellNameTxt, cooldownTxt;

        public Spell associatedSpell;

        public void SetSpells(Spell spell)
        {

            unhover.SetActive(true);
            hover.SetActive(false);

            if(spell == null)
            {
                spellSprite.enabled = false;
                spellNameTxt.text = "????";
                cooldownTxt.text = "X";
                associatedSpell = null;
                return;
            }
            associatedSpell = spell;
            spellSprite.enabled = true;
            spellSprite.sprite = spell.sprite;
            spellNameTxt.text = spell.SpellName;
            cooldownTxt.text = spell.Cooldown.ToString();
        }

        public void SetHover()
        {
            unhover.SetActive(false);
            hover.SetActive(true);
        }

        public void SetUnHover()
        {
            unhover.SetActive(true);
            hover.SetActive(false);
        }


    }
}


