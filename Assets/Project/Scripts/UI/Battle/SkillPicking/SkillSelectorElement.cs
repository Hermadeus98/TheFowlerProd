using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using QRCode;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class SkillSelectorElement : UISelectorElement
    {
        [SerializeField, ReadOnly] public Spell referedSpell;
        public TextMeshProUGUI manaCostText;
        [SerializeField] private Image spellTypeIcon;
        [SerializeField] private SpellTypeDatabase SpellTypeDatabase;

        [SerializeField] private Image selectableFeedback;
        [SerializeField] private Image cross;

        [SerializeField] private CanvasGroup CanvasGroup;

        [SerializeField] private Image soulignage;

        [SerializeField] private Color manaColorNormal, manaColorNotEnoughMana, textColorDisabled, textColorCDPreview;

        [SerializeField] private Image manaLogo;

        [SerializeField] private Image fillCooldown, cooldown;

        [SerializeField] private Image strenght;
        [SerializeField] private RectTransform[] targets;
        [SerializeField] private CanvasGroup descCanvasGroup;
        [SerializeField] private TextMeshProUGUI desctext;
        [SerializeField] private Image logoBuff;
        
        public bool isSelectable { get; set; } = false;

        private Tween crossAnim;
        
        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);

            if (args is WrapperArgs<SpellHandler.SpellHandled> cast)
            {
                referedSpell = cast.Arg.Spell;
                int newCooldown = referedSpell.CurrentCooldown;

                if (BattleManager.IsReducingCD)
                {
                    newCooldown--;

                    if (newCooldown <= 0) newCooldown = 0;
                }

                if (LocalisationManager.language == Language.ENGLISH)
                {
                    text.SetText(referedSpell.SpellName);
                }
                else
                {
                    text.SetText(referedSpell.SpellNameFrench);
                }

                
                //manaCostText.SetText(referedSpell.ManaCost.ToString());

                var cooldownPercent = 0f;

                if (referedSpell.Cooldown != 0)
                {
                    cooldownPercent = (float)newCooldown / (float)referedSpell.Cooldown;

                }
                else
                {
                    cooldownPercent = 0;
                }



                fillCooldown.DOFillAmount(cooldownPercent, .2f);
                manaCostText.SetText(newCooldown.ToString());
                spellTypeIcon.sprite = SpellTypeDatabase.GetElement(referedSpell.SpellType);

                //if (BattleManager.CurrentBattleActor.Mana.HaveEnoughMana(referedSpell.ManaCost))
                if (newCooldown <= 0)
                {
                    isSelectable = true;
                    canvasGroup.alpha = 1f;
                    manaCostText.color = Color.white;
                    cooldown.color = manaColorNormal;

                    crossAnim?.Kill();
                    cross.fillAmount = 0;
                    manaLogo.DOFade(1f, 0.05f);

                    manaCostText.SetText((referedSpell.Cooldown).ToString());

                    text.color = Color.white;

                    if (BattleManager.IsReducingCD)
                    {
                        if (referedSpell.isRechargingCooldown)
                        {
                            manaCostText.color = textColorCDPreview;
                        }


                    }

                    if(referedSpell.Cooldown < referedSpell.InitialCooldown)
                    {
                        if (!referedSpell.isRechargingCooldown)
                        {
                            manaCostText.color = textColorCDPreview;
                        }
                    }
                    else
                    {
                        if (!referedSpell.isRechargingCooldown)
                        {
                            manaCostText.color = Color.white;
                        }
                    }



                    //selectableFeedback.enabled = false;
                }
                else
                {
                    isSelectable = false;
                    canvasGroup.alpha = .5f;
                    manaCostText.color = manaColorNotEnoughMana;
                    cooldown.color = Color.grey;
                    
                    crossAnim?.Kill();
                    crossAnim = cross.DOFillAmount(1f, .25f);
                    manaLogo.DOFade(.5f, 0.05f);

                    text.color = textColorDisabled;

                    if (BattleManager.IsReducingCD)
                    {
                        if (referedSpell.isRechargingCooldown)
                        {
                            manaCostText.color = textColorCDPreview;
                        }


                    }

                    if (referedSpell.Cooldown < referedSpell.InitialCooldown)
                    {
                        if (!referedSpell.isRechargingCooldown)
                        {
                            manaCostText.color = textColorCDPreview;
                        }
                    }


                    //selectableFeedback.enabled = true;
                }
            }
        }

        public void Refresh(Spell spell)
        {
            referedSpell = spell;

            if (LocalisationManager.language == Language.ENGLISH)
            {
                text.SetText(referedSpell.SpellName);
            }
            else
            {
                text.SetText(referedSpell.SpellNameFrench);
            }

            //manaCostText.SetText(referedSpell.ManaCost.ToString());
            spellTypeIcon.sprite = SpellTypeDatabase.GetElement(referedSpell.SpellType);
        }

        private Tween s;
        private Tween textSize;

        private int sizeUnselect = 30, sizeSelect = 35;

        public override void Select()
        {
            //canvasGroup.alpha = 1f;
            s?.Kill();
            s = soulignage.DOFillAmount(1f, .1f).SetEase(Ease.InOutSine);
            textSize?.Kill();
            textSize = DoFontSize(text, sizeSelect, .1f, Ease.InOutSine);
            ShowDescription();

            if (referedSpell.ContainEffect<DamageEffect>(out DamageEffect damageEffect))
            {
                var e = TargetSelector.GetAllEnemies().Cast<EnemyActor>();
                SkillPickingView.enemies = new List<EnemyActor>(e);
                
                var weak = SkillPickingView.enemies.Cast<EnemyActor>().Where(w => w.IsWeakOf(referedSpell.SpellType));
                weak.ForEach(w => w.weak.Show());

                var resist = SkillPickingView.enemies.Cast<EnemyActor>()
                    .Where(w => w.IsResistantOf(referedSpell.SpellType));
                resist.ForEach(w => w.resist.Show());
            }
        }

        private Tween DoFontSize(TextMeshProUGUI text, float to, float duration, Ease ease)
        {
            return DOTween.To(
                () => text.fontSize,
                (x) => text.fontSize = x,
                to, duration
            ).SetEase(ease);
        }

        public override void DeSelect()
        {
            //canvasGroup.alpha = .5f;
            s.Kill();
            s = soulignage.DOFillAmount(0f, .1f).SetEase(Ease.InOutSine);
            textSize?.Kill();
            textSize = DoFontSize(text, sizeUnselect, .1f, Ease.InOutSine);
            HideDescription();
            
            if (SkillPickingView.enemies.Count > 0)
            {
                SkillPickingView.enemies.ForEach(delegate(EnemyActor w)
                {
                    w.resist.Hide();
                    w.weak.Hide();
                });
            }
        }

        private Coroutine desc;
        private Tween fade;

        public void ShowDescription()
        {
            if (referedSpell.logoBuff != null)
            {
                logoBuff.enabled = true;
                logoBuff.sprite = referedSpell.logoBuff;
            }
            else
            {
                logoBuff.enabled = false;
            }


            if (LocalisationManager.language == Language.ENGLISH)
            {
                desctext.SetText(referedSpell.SpellDescription);
            }
            else
            {
                desctext.SetText(referedSpell.SpellDescriptionFrench);
            }

            
            fade?.Kill();
            fade = descCanvasGroup.DOFade(1f, .2f);
        
            targets.ForEach(w => w.transform.localScale = Vector3.zero);

            spellTypeIcon.sprite = SpellTypeDatabase.GetElement(referedSpell.SpellType);
            
            if(desc != null)
                StopCoroutine(desc);
            desc = StartCoroutine(ShowCor());
        }

        public Sprite enemyIcon, allyIcon;
        
        IEnumerator ShowCor()
        {
            strenght.fillAmount = 0f;
        
            var strenghtAmount = referedSpell.SpellPower switch
            {
                Spell.SpellPowerEnum.NULL => 0f,
                Spell.SpellPowerEnum.EASY => .2f,
                Spell.SpellPowerEnum.MEDIUM => .5f,
                Spell.SpellPowerEnum.HARD => 1f,
                _ => throw new ArgumentOutOfRangeException()
            };

            yield return new WaitForSeconds(.4f);

            strenght.DOFillAmount(strenghtAmount, .2f).SetEase(Ease.OutCirc);
            yield return new WaitForSeconds(.2f);

            switch (referedSpell.TargetType)
            {
                case TargetTypeEnum.SELF:
                    targets.ForEach(w => w.GetComponent<Image>().sprite = allyIcon);
                    targets[0].transform.DOScale(Vector3.one, .1f).SetEase(Ease.OutCirc);
                    yield return new WaitForSeconds(.1f);
                    break;
                case TargetTypeEnum.SOLO_ENEMY:
                    targets.ForEach(w => w.GetComponent<Image>().sprite = enemyIcon);
                    targets[0].transform.DOScale(Vector3.one, .1f).SetEase(Ease.OutCirc);
                    yield return new WaitForSeconds(.1f);
                    break;
                case TargetTypeEnum.ALL_ENEMIES:
                    targets.ForEach(w => w.GetComponent<Image>().sprite = enemyIcon);
                    for (int i = 0; i < targets.Length; i++)
                    {
                        targets[i].transform.DOScale(Vector3.one, .1f).SetEase(Ease.OutCirc);
                        yield return new WaitForSeconds(.1f);
                    }
                    break;
                case TargetTypeEnum.SOLO_ALLY:
                    targets.ForEach(w => w.GetComponent<Image>().sprite = allyIcon);
                    targets[0].transform.DOScale(Vector3.one, .1f).SetEase(Ease.OutCirc);
                    yield return new WaitForSeconds(.1f);
                    break;
                case TargetTypeEnum.ALL_ALLIES:
                    targets.ForEach(w => w.GetComponent<Image>().sprite = allyIcon);
                    for (int i = 0; i < targets.Length; i++)
                    {
                        targets[i].transform.DOScale(Vector3.one, .1f).SetEase(Ease.OutCirc);
                        yield return new WaitForSeconds(.1f);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void HideDescription()
        {
            fade?.Kill();
            fade = descCanvasGroup.DOFade(0f, .2f);
        }
    }
}
