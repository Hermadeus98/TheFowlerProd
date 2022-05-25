using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TheFowler
{
    public class TextMenu : UISelectorElement
    {
        [SerializeField] private Color normalColor;
        [SerializeField] private Color selectedColor;

        [SerializeField] private UnityEvent OnSelect, OnHighLigh;

        private Sequence pulse;

        public SliderCustom Slider;
        public Slider SliderSimple;
        public TextMeshProUGUI textSlider;
        public TextChoice TextChoice;
        
        private bool isSelected = false;

        [TextArea(2,4)] public string descriptionText;

        protected override void OnStart()
        {
            base.OnStart();

            if (Slider != null)
            {
                Slider.gameObject.SetActive(true);
                Slider.Generate();
            }
            
            if(SliderSimple != null)
            {
                SliderSimple.gameObject.SetActive(true);
                SliderSimple.maxValue = 100;
                textSlider.gameObject.SetActive(true);
                SliderSimple.onValueChanged.AddListener(delegate(float value)
                {
                    textSlider.SetText(SliderSimple.value + "/" + SliderSimple.maxValue);
                });
                if (TryGetComponent<VolumeUpdater>(out var s))
                {
                    SliderSimple.value = s.GetValue();
                }
                else
                {
                    SliderSimple.value = 100;
                }
                SliderSimple.wholeNumbers = true;
                SliderSimple.interactable = false;
            }

            if (TextChoice != null)
            {
                TextChoice.gameObject.SetActive(true);
                TextChoice.Initialize();
                OnHighLigh.AddListener(delegate
                {
                    FindObjectsOfType<DescriptionText>().ForEach(w => w.UpdateText(TextChoice.CurrentOption.description));
                });
            }
            else
            {
                OnHighLigh.AddListener(delegate
                {
                    FindObjectsOfType<DescriptionText>().ForEach(w=> w.UpdateText(descriptionText));
                });
            }
        }

        public override void Select()
        {
            isSelected = true;
            
            text.color = selectedColor;

            pulse?.Kill();
            pulse = DOTween.Sequence();
            pulse.Append(
                text.rectTransform.DOScale(1.1f, .6f)
                );
            pulse.Append(
                text.rectTransform.DOScale(1f, .6f)
                );
            pulse.SetLoops(-1);
            pulse.Play();
            
            OnHighLigh?.Invoke();
        }

        public override void DeSelect()
        {
            isSelected = false;
            
            text.color = normalColor;
            pulse?.Kill();
            text.rectTransform.DOScale(1f, .2f);
        }

        public override void OnClick()
        {
            base.OnClick();
            OnSelect?.Invoke();
        }

        private void Update()
        {
            if (!isActive && !Player.isInPauseMenu)
                return;
            
            if (isSelected)
            {
                if (Gamepad.current != null)
                {

                    if (Gamepad.current.dpad.right.wasPressedThisFrame)
                    {
                        Slider?.Add();
                        TextChoice?.Next();
                    }

                    if (Gamepad.current.dpad.left.wasPressedThisFrame)
                    {
                        Slider?.Remove();
                        TextChoice?.Previous();
                    }

                    if (Gamepad.current.dpad.right.isPressed)
                    {
                        if (SliderSimple != null)
                        {
                            SliderSimple.value++;
                            SliderSimple.value = Mathf.Clamp(SliderSimple.value, 0, SliderSimple.maxValue);
                        }
                    }

                    if (Gamepad.current.dpad.left.isPressed)
                    {
                        if (SliderSimple != null)
                        {
                            SliderSimple.value--;
                            SliderSimple.value = Mathf.Clamp(SliderSimple.value, 0, SliderSimple.maxValue);
                        }
                    }
                }
            }
        }
    }
}
