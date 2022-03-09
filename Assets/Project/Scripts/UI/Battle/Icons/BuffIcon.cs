using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class BuffIcon : StateIcon
    {
        [SerializeField] private Sprite debuff, buff;
        
        public void Refresh(float value)
        {
            Debug.Log(value);
            
            if (value > 0)
            {
                icon.sprite = buff;
            }
            else if (value < 0)
            {
                icon.sprite = debuff;
            }
        }
    }
}
