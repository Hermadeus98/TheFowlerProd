using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class TypeIcon : UIElement
    {
        [SerializeField] private Image Image;
        [SerializeField] private StringSpriteDatabase typeDatabase;
        [SerializeField] private CanvasGroup CanvasGroup;

        public void Refresh(Spell.SpellTypeEnum spellType)
        {
            switch (spellType)
            {
                case Spell.SpellTypeEnum.NULL:
                    CanvasGroup.alpha = 0;
                    break;
                case Spell.SpellTypeEnum.CLAW:
                    CanvasGroup.alpha = 1;
                    Image.sprite = typeDatabase.GetElement("Claw");
                    break;
                case Spell.SpellTypeEnum.BEAK:
                    CanvasGroup.alpha = 1;
                    Image.sprite = typeDatabase.GetElement("Beak");
                    break;
                case Spell.SpellTypeEnum.FEATHER:
                    CanvasGroup.alpha = 1;
                    Image.sprite = typeDatabase.GetElement("Feather");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
