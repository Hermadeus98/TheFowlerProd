using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class EnemySpellBox : UIView
    {

        [SerializeField]
        private RectTransform Container;

        [SerializeField] private Image logo;
        [SerializeField] private TextMeshProUGUI text;

        protected override void OnStart()
        {
            base.OnStart();
        }

        IEnumerator HideIE()
        {
            yield return new WaitForSeconds(3f);
            Hide();
        }

        public override void Show()
        {
            base.Show();
            Container.DOAnchorPosY(0f, .2f);
            StartCoroutine(HideIE());
        }

        public override void Hide()
        {
            base.Hide();
            Container.DOAnchorPosY(Container.sizeDelta.y, .2f);
        }

        public void Refresh(Spell spell)
        {
            if (spell.logoBuff != null)
            {
                logo.gameObject.SetActive(true);
                logo.sprite = spell.logoBuff;
            }
            else
            {
                logo.gameObject.SetActive(false);
            }

            if (LocalisationManager.language == Language.ENGLISH)
            {
                text.SetText(spell.SpellDescription);
            }
            else
            {
                text.SetText(spell.SpellDescriptionFrench);
            }

           
        }
    }
}
