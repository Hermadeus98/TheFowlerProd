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
        public void ToDisable()
        {
            line.color = colorDisable;
        }

        public void ToSelected()
        {
            line.color = colorSelected;
        }

        public void ToUnSelected()
        {
            line.color = colorUnSelected;
        }
    }
}

