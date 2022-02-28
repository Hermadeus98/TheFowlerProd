using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class MashingElement : UIElement
    {
        [SerializeField] private Image input;
        [SerializeField] private RectTransform timer;

        [SerializeField] private Sprite X, Y, A, B;


        public void Refresh(MashingDataProfile profile)
        {
            switch (profile.touch)
            {
                case Touch.A:
                    input.sprite = A;
                    break;
                case Touch.B:
                    input.sprite = B;
                    break;
                case Touch.X:
                    input.sprite = X;
                    break;
                case Touch.Y:
                    input.sprite = Y;
                    break;
            }
            
            timer.localScale = Vector3.one;
            
            //timer.DOScale(ve)
        }
    }
}
