using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class BuffIcon : StateIcon
    {
        public Sprite bonus_low, bonus_medium, bonus_high, malus_low, malus_medium, malus_high;
        public BuffType buffType;
        
        public enum BuffType
        {
            ATT,
            DEF,
            CD
        }
        
        public void Refresh(float value)
        {
            switch (buffType)
            {
                case BuffType.ATT:
                    Apply(value, SpellData.Instance.maxBuffAttack, SpellData.Instance.minBuffAttack);
                    break;
                case BuffType.DEF:
                    Apply(value, SpellData.Instance.maxBuffDefense, SpellData.Instance.minBuffDefense);
                    break;
                case BuffType.CD:
                    switch (value)
                    {
                        case 1:
                            icon.sprite = bonus_low;
                            Feedback();
                            break;
                        case 2:
                            icon.sprite = bonus_medium;
                            Feedback();
                            break;
                        case 3:
                            icon.sprite = bonus_high;
                            Feedback();
                            break;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Apply(float value, float max, float min)
        {
            if (value > 0)
            {
                if (value <= max * .25f)
                    icon.sprite = bonus_low;
                else if (value <= max * .5f)
                    icon.sprite = bonus_medium;
                else if (value <= max * .75f)
                    icon.sprite = bonus_high;
            }
            else if (value < 0)
            {
                if (value >= min * .25f)
                    icon.sprite = malus_low;
                else if (value >= min * .5f)
                    icon.sprite = malus_medium;
                else if (value >= min * .75f)
                    icon.sprite = malus_high;
            }
            
            Feedback();
        }
    }
}
