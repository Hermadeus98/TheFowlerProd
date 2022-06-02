using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace TheFowler
{
    public class LineBehavior : MonoBehaviour
    {

        [SerializeField] private Color colorDisable, colorSelected, colorUnSelected;
        [SerializeField] private Image line;
        [SerializeField] private Outline outline;

        
        public void ToDisable()
        {
            line.color = colorDisable;
            outline.enabled = false;
        }

        public void ToSelected()
        {
            line.color = colorSelected;
            outline.enabled = true;
        }

        public void ToUnSelected()
        {
            line.color = colorUnSelected;
            outline.enabled = false;
        }

        public void EnableOutline(bool value)
        {
            //outline.enabled = value;
        }
    }
}

